using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Text;

namespace AAXClean.Chunks
{
    internal class ChapterChunkHandler : IChunkHandler
    {
        public bool InputStreamSeekable { get; }
        public ChapterInfo Chapters { get; private set; } = new();
        public double TimeScale { get; }
        public TrakBox Track { get; }

        private SttsBox.SampleEntry[] Samples { get; set; }

        public ChapterChunkHandler(uint timeScale, TrakBox trak, bool seekable = false)
        {
            TimeScale = timeScale;
            Track = trak;
            Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
            InputStreamSeekable = seekable;
        }

        public bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int totalChunkSize, int[] frameSizes)
        {
            if (chunkIndex < 0 || chunkIndex >= Samples.Length)
                return false;

            var duration = Samples[chunkIndex].FrameDelta / TimeScale;

            short size = file.ReadInt16BE();
            byte[] titleBytes = file.ReadBlock(size);

            var title = Encoding.UTF8.GetString(titleBytes);

            Chapters.AddChapter(title, TimeSpan.FromSeconds(duration));

            return true;
        }

        bool disposed = false;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    Chapters = null;
                    Samples = null;
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                disposed = true;
            }
        }
    }
}
