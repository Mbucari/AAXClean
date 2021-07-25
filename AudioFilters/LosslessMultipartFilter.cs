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
        private MoovBox Moov { get; }

        private Mp4aWriter writer;

        public LosslessMultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, FtypBox ftyp, MoovBox moov)
        {
            Ftyp = ftyp;
            Moov = moov;

            if (splitChapters.Count == 0)
                throw new Exception($"{nameof(splitChapters)} must contain at least one chapter.");

            SplitChapters = splitChapters.GetEnumerator();
            EndFrame = -1;
            NewFileCallback = newFileCallback;

            SampleRate = (int)moov.AudioTrack.Mdia.Mdhd.Timescale;
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

                writer = new Mp4aWriter(callback.OutputFile, Ftyp, Moov, false);
                writer.RemoveTextTrack();

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
