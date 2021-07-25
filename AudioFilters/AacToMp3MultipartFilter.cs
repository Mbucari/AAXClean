using AAXClean.Chunks;
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    sealed class AacToMp3MultipartFilter : MultipartFilter
    {
        protected override Action<NewSplitCallback> NewFileCallback { get; }
        private byte[] ASC { get; }
        private ushort SampleSize { get; }
        private LameConfig LameConfig;

        private AacToMp3Filter AacToMp3Filter;

        public AacToMp3MultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, byte[] audioSpecificConfig, ushort sampleSize, LameConfig lameConfig)
                        : base(audioSpecificConfig, splitChapters)
        {
            if (splitChapters.Count == 0)
                throw new Exception($"{nameof(splitChapters)} must contain at least one chapter.");

            LameConfig = lameConfig;
            ASC = audioSpecificConfig;
            SampleSize = sampleSize;
            LameConfig = lameConfig;
            NewFileCallback = newFileCallback;
        }

        protected override void CloseCurrentWriter() => AacToMp3Filter?.Dispose();
        
        protected override void WriteFrameToFile(byte[] audioFrame, bool newChunk) => AacToMp3Filter.FilterFrame(0, 0, audioFrame);

        protected override void CreateNewWriter(NewSplitCallback callback)
        {
            callback.LameConfig = LameConfig;
            NewFileCallback(callback);
            LameConfig = callback.LameConfig;
            AacToMp3Filter = new AacToMp3Filter(callback.OutputFile, ASC, SampleSize, callback.LameConfig);
        }
    }
}
