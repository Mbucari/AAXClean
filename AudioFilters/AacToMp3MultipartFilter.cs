using NAudio.Lame;
using System;

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
            LameConfig = lameConfig;
            ASC = audioSpecificConfig;
            SampleSize = sampleSize;
            LameConfig = lameConfig;
            NewFileCallback = newFileCallback;
        }

        protected override void CloseCurrentWriter() => AacToMp3Filter?.Dispose();

        protected override void WriteFrameToFile(Span<byte> audioFrame, bool newChunk) => AacToMp3Filter.FilterFrame(0, 0, audioFrame);

        protected override void CreateNewWriter(NewSplitCallback callback)
        {
            callback.LameConfig = LameConfig;
            NewFileCallback(callback);
            LameConfig = callback.LameConfig;
            AacToMp3Filter = new AacToMp3Filter(callback.OutputFile, ASC, SampleSize, callback.LameConfig);
        }
        protected override void Dispose(bool disposing)
        {
            AacToMp3Filter?.Dispose();
            base.Dispose(disposing);
        }
    }
}
