using AAXClean.Boxes;
using System;

namespace AAXClean.AudioFilters
{
    sealed class LosslessMultipartFilter : MultipartFilter
    {
        protected override Action<NewSplitCallback> NewFileCallback { get; }
        private FtypBox Ftyp { get; }
        private MoovBox Moov { get; }

        private Mp4aWriter writer;

        public LosslessMultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, FtypBox ftyp, MoovBox moov)
            : base(moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob, splitChapters)
        {
            NewFileCallback = newFileCallback;
            Ftyp = ftyp;
            Moov = moov;
        }

        protected override void CloseCurrentWriter() => writer?.Close();
        protected override void WriteFrameToFile(byte[] audioFrame, bool newChunk) => writer.AddFrame(audioFrame, newChunk);
        protected override void CreateNewWriter(NewSplitCallback callback)
        {
            NewFileCallback(callback);
            writer = new Mp4aWriter(callback.OutputFile, Ftyp, Moov, false);
            writer.RemoveTextTrack();
        }

        protected override void Dispose(bool disposing)
        {
            writer?.Dispose();
            Ftyp.Dispose();
            Moov.Dispose();
            base.Dispose(disposing);
        }
    }
}
