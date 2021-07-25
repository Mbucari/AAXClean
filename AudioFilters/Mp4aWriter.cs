using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    class Mp4aWriter
    {
        Stream OutputFile;
        MoovBox Moov;
        long mdatStart = 0;
        SttsBox Stts;
        StscBox Stsc;
        StszBox Stsz;
        StcoBox Stco;
        Co64Box Co64;

        private const int AAC_TIME_DOMAIN_SAMPLES = 1024;

        private long lastSamplesPerChunk = -1;
        private uint samplesPerChunk = 0;
        private uint currentChunk = 0;

        private bool isCo64;
        public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov, bool co64)
        {
            OutputFile = outputFile;
            isCo64 = co64;
            Moov = MakeBlankMoov(moov, isCo64);

            Stts = Moov.AudioTrack.Mdia.Minf.Stbl.Stts;
            Stsc = Moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
            Stco = Moov.AudioTrack.Mdia.Minf.Stbl.Stco;
            Stsz = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
            Co64 = Moov.AudioTrack.Mdia.Minf.Stbl.Co64;

            ftyp.Save(OutputFile);
            mdatStart = OutputFile.Position;

            //Placeholder mdat header
            OutputFile.WriteUInt32BE(0);
            OutputFile.WriteType("mdat");
            if (isCo64)
                outputFile.WriteInt64BE(0);
        }
        public void Close()
        {
            if (!OutputFile.CanWrite) return;

            long mdatEnd = OutputFile.Position;

            long mdatSize = mdatEnd - mdatStart;

            OutputFile.Position = mdatStart;

            if (mdatSize <= uint.MaxValue)
                OutputFile.WriteUInt32BE((uint)mdatSize);
            else
            {
                OutputFile.WriteUInt32BE(1);
            }
            OutputFile.WriteType("mdat");

            if (mdatSize > uint.MaxValue)
                OutputFile.WriteInt64BE(mdatSize);


            OutputFile.Position = mdatEnd;

            Stts.EntryCount = 1;
            Stts.Samples.Add(new SttsBox.SampleEntry((uint)Stsz.SampleSizes.Count, AAC_TIME_DOMAIN_SAMPLES));

            Stsz.SampleCount = (uint)Stsz.SampleSizes.Count;
            Stsc.EntryCount = (uint)Stsc.Samples.Count;

            if (isCo64)
                Co64.EntryCount = (uint)Co64.ChunkOffsets.Count;
            else
                Stco.EntryCount = (uint)Stco.ChunkOffsets.Count;

            Moov.AudioTrack.Mdia.Mdhd.Duration = (ulong)Stsz.SampleCount * AAC_TIME_DOMAIN_SAMPLES;
            Moov.Mvhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration * Moov.Mvhd.Timescale / Moov.AudioTrack.Mdia.Mdhd.Timescale;
            Moov.AudioTrack.Tkhd.Duration = Moov.Mvhd.Duration;         

            Moov.Save(OutputFile);
            OutputFile.Close();
        }

        public void WriteChapters(ChapterInfo chapters)
        {
            Moov.TextTrack.Mdia.Minf.Stbl.Stsc.EntryCount = 1;
            Moov.TextTrack.Mdia.Minf.Stbl.Stsc.Samples.Add(new StscBox.ChunkEntry(1, 1, 1));

            foreach (var c in chapters)
            {
                uint sampleDelta = (uint)(c.Duration.TotalSeconds * Moov.AudioTrack.Mdia.Mdhd.Timescale);

                Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Add(new SttsBox.SampleEntry(sampleCount: 1, sampleDelta));
                Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Add(c.RenderSize);

                if (isCo64)
                    Moov.TextTrack.Mdia.Minf.Stbl.Co64.ChunkOffsets.Add(OutputFile.Position);
                else
                Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Add((uint)OutputFile.Position);

                c.WriteChapter(OutputFile);
            }
            Moov.TextTrack.Mdia.Minf.Stbl.Stts.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Count;
            Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Count;
            if (isCo64)
                Moov.TextTrack.Mdia.Minf.Stbl.Co64.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Co64.ChunkOffsets.Count;
            else
                Moov.TextTrack.Mdia.Minf.Stbl.Stco.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Count;

        }

        public void RemoveTextTrack()
        {
            Moov.Children.Remove(Moov.TextTrack);

            Moov.Mvhd.NextTrackID--;
        }
        public void AddFrame(byte[] frame, bool newChunk)
        {
            if (newChunk)
            {
                currentChunk++;

                if (isCo64)
                    Co64.ChunkOffsets.Add(OutputFile.Position);
                else
                    Stco.ChunkOffsets.Add((uint)OutputFile.Position);

                if (samplesPerChunk > 0 && samplesPerChunk != lastSamplesPerChunk)
                {
                    Stsc.Samples.Add(new StscBox.ChunkEntry(currentChunk - 1, samplesPerChunk, 1));

                    lastSamplesPerChunk = samplesPerChunk;
                }
                samplesPerChunk = 0;
            }

            Stsz.SampleSizes.Add(frame.Length);
            samplesPerChunk++;

            OutputFile.Write(frame);
        }

        public void Dispose()
        {
            Close();
            Stsc?.Samples.Clear();
            Stsc = null;
            Stsz?.SampleSizes.Clear();
            Stsc = null;
            Stco?.ChunkOffsets.Clear();
            Stco = null;
            Moov = null;
        }

        private static MoovBox MakeBlankMoov(MoovBox moov, bool co64)
        {
            var a1 = moov.AudioTrack.Mdia.Minf.Stbl.Stts;
            var a2 = moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
            var a3 = moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
            var a4 = moov.AudioTrack.Mdia.Minf.Stbl.Stco;
            var a5 = moov.AudioTrack.Mdia.Minf.Stbl.Co64;


            var t1 = moov.TextTrack.Mdia.Minf.Stbl.Stts;
            var t2 = moov.TextTrack.Mdia.Minf.Stbl.Stsc;
            var t3 = moov.TextTrack.Mdia.Minf.Stbl.Stsz;
            var t4 = moov.TextTrack.Mdia.Minf.Stbl.Stco;
            var t5 = moov.TextTrack.Mdia.Minf.Stbl.Co64;

            moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a1);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a2);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a3);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a4);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a5);

            moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t1);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t2);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t3);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t4);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t5);

            MemoryStream ms = new MemoryStream();

            moov.Save(ms);

            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a1);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a2);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a3);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a4);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a5);

            moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t1);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t2);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t3);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t4);
            moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t5);

            ms.Position = 0;

            MoovBox newMoov = new MoovBox(ms, new BoxHeader(ms), null);

            SttsBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
            StscBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
            StszBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);

            SttsBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
            StscBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
            StszBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);

            if (co64)
            {
                Co64Box.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
                Co64Box.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
            }
            else
            {
                StcoBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
                StcoBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
            }

            return newMoov;
        }
    }
}
