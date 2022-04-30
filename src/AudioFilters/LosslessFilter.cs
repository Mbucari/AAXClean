using System;
using System.IO;
using System.Linq;

namespace AAXClean.AudioFilters
{
    internal class LosslessFilter : AudioFilterBase
    {
        private readonly Mp4aWriter Mp4writer;
        private long LastChunkIndex = -1;
        internal override ChapterInfo Chapters
        {
            get => base.Chapters;
            set
            {
                base.Chapters = value;
                if (Chapters != null)
                    Mp4writer.WriteChapters(Chapters);
            }
        }
        public LosslessFilter(Stream outputStream, Mp4File mp4Audio)
        {
            var audioSize = mp4Audio.Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s);
            Mp4writer = new Mp4aWriter(outputStream, mp4Audio.Ftyp, mp4Audio.Moov, audioSize > uint.MaxValue);
        }

        public override bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioSample)
        {
            Mp4writer.AddFrame(audioSample, chunkIndex > LastChunkIndex);
            LastChunkIndex = chunkIndex;
            return true;
        }

        public override void Close()
        {
            if (Closed) return;
            Mp4writer.Close();
            Closed = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Close();
                    Mp4writer?.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}
