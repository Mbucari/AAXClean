using System;

namespace AAXClean.AudioFilters
{
    public abstract class AudioFilterBase : IDisposable
    {
        public bool Closed { get; protected set; }
        protected bool Disposed { get; private set; }
        internal virtual ChapterInfo Chapters { get; set; }
        public abstract bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioFrame);
        public abstract void Close();
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            Disposed = true;
        }
    }
}
