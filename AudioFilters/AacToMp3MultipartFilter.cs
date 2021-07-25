using AAXClean.Chunks;
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    sealed class AacToMp3MultipartFilter : IFrameFilter
    {
        private byte[] ASC { get; }
        private ushort SampleSize { get; }
        private int SampleRate { get; }
        private LameConfig LameConfig { get; }
        private IEnumerator<Chapter> SplitChapters { get; }
        private Action<NewSplitCallback> NewFileCallback { get; }


        private AacToMp3Filter AacToMp3Filter;
        private uint StartFrame;
        private long EndFrame;
        private const int AAC_TIME_DOMAIN_SAMPLES = 1024;

        private static readonly int[] asc_samplerates = { 96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350 };
        public AacToMp3MultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, byte[] audioSpecificConfig, ushort sampleSize, LameConfig lameConfig)
        {
            if (splitChapters.Count == 0)
                throw new Exception($"{nameof(splitChapters)} must contain at least one chapter.");

            ASC = audioSpecificConfig;
            SampleSize = sampleSize;
            LameConfig = lameConfig;
            SplitChapters = splitChapters.GetEnumerator();
            EndFrame = -1;
            NewFileCallback = newFileCallback;

            SampleRate = asc_samplerates[(audioSpecificConfig[0] & 7) << 1 | audioSpecificConfig[1] >> 7];
        }
        public void Close()
        {
            AacToMp3Filter?.Close();
        }

        public void Dispose()
        {
            SplitChapters.Dispose();
            AacToMp3Filter?.Dispose();
        }

        public bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] audioFrame)
        {
            if (frameIndex > EndFrame)
            {
                AacToMp3Filter?.Dispose();
                if (!GetNextChapter())
                    return false;

                var callback = new NewSplitCallback(SplitChapters.Current, LameConfig);
                NewFileCallback(callback);

                AacToMp3Filter = new AacToMp3Filter(callback.OutputFile, ASC, SampleSize, callback.LameConfig);
            }

            if (frameIndex >= StartFrame)
                return AacToMp3Filter.FilterFrame(chunkIndex, frameIndex, audioFrame);

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
