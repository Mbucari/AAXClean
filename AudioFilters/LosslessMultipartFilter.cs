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
    class LosslessMultipartFilter : IFrameeFilter
    {
        private int SampleRate { get; }
        private IEnumerator<Chapter> SplitChapters { get; }
        private Action<NewSplitCallback> NewFileCallback { get; }
        private uint StartFrame;
        private long EndFrame;
        private const int AAC_TIME_DOMAIN_SAMPLES = 1024;
        private FtypBox Ftyp { get; }
        private MemoryStream rawmoov { get; }

        Mp4aWriter writer;

        public LosslessMultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, FtypBox ftyp, MoovBox moov)
        {
            Ftyp = ftyp;

            rawmoov = MakeMultipartMoov(moov);

            if (splitChapters.Count == 0)
                throw new Exception($"{nameof(splitChapters)} must contain at least one chapter.");

            SplitChapters = splitChapters.GetEnumerator();
            EndFrame = -1;
            NewFileCallback = newFileCallback;

            SampleRate = (int)moov.AudioTrack.Mdia.Mdhd.Timescale;
        }

        private MemoryStream MakeMultipartMoov(MoovBox moov)
        {
            var s1 = moov.AudioTrack.Mdia.Minf.Stbl.Stts;
            var s2 = moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
            var s3 = moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
            var s4 = moov.AudioTrack.Mdia.Minf.Stbl.Stco;
            var tt = moov.TextTrack;
            var tref = moov.AudioTrack.GetChildren<UnknownBox>().FirstOrDefault(b => b.Header.Type == "tref");

            var removeOK = moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(s1);
            removeOK = moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(s2);
            removeOK = moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(s3);
            removeOK = moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(s4);
            removeOK = moov.Children.Remove(tt);
            removeOK = moov.AudioTrack.Children.Remove(tref);

            MemoryStream ms = new MemoryStream();

            moov.Save(ms);

            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(s1);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(s2);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(s3);
            moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(s4);
            moov.Children.Add(tt);
            moov.AudioTrack.Children.Add(tref);

            ms.Position = 0;

            MoovBox newMoov = new MoovBox(ms, new BoxHeader(ms), null);

            SttsBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
            StscBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
            StszBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
            StcoBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);

            newMoov.Mvhd.NextTrackID = 2;

            ms = new MemoryStream();
            newMoov.Save(ms);

            return ms;
        }

        private MoovBox NewMoov()
        {
            rawmoov.Position = 0;
            MoovBox newMoov = new MoovBox(rawmoov, new BoxHeader(rawmoov), null);

            return newMoov;
        }

        public void Close()
        {
            writer?.Close();
        }

        public void Dispose()
        {
            SplitChapters.Dispose();
            writer?.Dispose();
        }

        long lastChunk = -1;
        public bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] audioFrame)
        {
            if (frameIndex > EndFrame)
            {
                writer?.Close();

                if (!GetNextChapter())
                    return false;

                var callback = new NewSplitCallback(SplitChapters.Current, null);
                NewFileCallback(callback);

                writer = new Mp4aWriter(callback.OutputFile, Ftyp, NewMoov());

                writer.AddFrame(audioFrame, true);
                lastChunk = chunkIndex;
            }
            else if (frameIndex >= StartFrame)
            {
                bool newChunk = false;
                if (chunkIndex > lastChunk)
                {
                    newChunk = true;
                    lastChunk = chunkIndex;
                }
                writer.AddFrame(audioFrame, newChunk);
            }

            return true;
        }

        private bool GetNextChapter()
        {
            if (!SplitChapters.MoveNext())
                return false;

            StartFrame = (uint)(SplitChapters.Current.StartOffset.TotalSeconds * SampleRate / AAC_TIME_DOMAIN_SAMPLES);
            EndFrame = (uint)(SplitChapters.Current.EndOffset.TotalSeconds * SampleRate / AAC_TIME_DOMAIN_SAMPLES);
            return true;
        }
    }
}
