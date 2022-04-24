using System;

namespace AAXClean.AudioFilters
{
    public abstract class AudioFilter : IDisposable
    {
        protected bool _disposed = false;
        protected ChapterInfo Chapters { get; private set; }
        public abstract bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioFrame);
        internal void SetChapters(ChapterInfo chapters)
        {
            Chapters = chapters;
        }
        public abstract void Close();
        internal virtual void EndOfAudio() { }
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Close();
            }

            _disposed = true;
        }
    }
}
