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
        StcoBox Stco;
        StszBox Stsz;

        private const int AAC_TIME_DOMAIN_SAMPLES = 1024;


        long lastSamplesPerChunk = -1;
        long samplesPerChunk = 0;
        private uint currentChunk = 0;
        public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov)
        {
            OutputFile = outputFile;
            Moov = moov;
            Stts = Moov.AudioTrack.Mdia.Minf.Stbl.Stts;
            Stsc = Moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
            Stco = Moov.AudioTrack.Mdia.Minf.Stbl.Stco;
            Stsz = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz;

            ftyp.Save(OutputFile);
            mdatStart = OutputFile.Position;
            OutputFile.WriteUInt32BE(0);
            OutputFile.WriteType("mdat");
        }
        public void End()
        {
            long mdatEnd = OutputFile.Position;

            OutputFile.Position = mdatStart;

            OutputFile.WriteUInt32BE((uint)(mdatEnd - mdatStart));

            OutputFile.Position = mdatEnd;

            Stts.EntryCount = 1;
            Stts.Samples.Add(new SttsBox.SampleEntry((uint)Stsz.SampleSizes.Count, AAC_TIME_DOMAIN_SAMPLES));

            Stsz.SampleCount = (uint)Stsz.SampleSizes.Count;
            Stco.EntryCount = (uint)Stco.ChunkOffsets.Count;
            Stsc.EntryCount = (uint)Stsc.Samples.Count;
            Moov.AudioTrack.Mdia.Mdhd.Duration = Stsz.SampleCount * AAC_TIME_DOMAIN_SAMPLES;
            Moov.AudioTrack.Tkhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
            Moov.Mvhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
            Moov.Mvhd.Timescale = Moov.AudioTrack.Mdia.Mdhd.Timescale;

            Moov.Save(OutputFile);
            OutputFile.Close();
        }
        public void AddFrame(byte[] frame, bool newChunk)
        {
            if (newChunk)
            {
                currentChunk++;

                Stco.ChunkOffsets.Add((uint)OutputFile.Position);

                if (samplesPerChunk > 0 && samplesPerChunk != lastSamplesPerChunk)
                {
                    Stsc.Samples.Add(new StscBox.ChunkEntry(currentChunk - 1, (uint)samplesPerChunk, 1));

                    lastSamplesPerChunk = samplesPerChunk;
                }
                samplesPerChunk = 0;
            }

            Stsz.SampleSizes.Add(frame.Length);
            samplesPerChunk++;

            OutputFile.Write(frame);
        }
    }
}
