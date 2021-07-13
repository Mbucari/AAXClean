using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean
{
    internal class ChapterChunkHandler : IChunkHandler
    {
        public ChapterInfo Chapters { get; } = new();
        public double TimeScale { get; }
        public TrakBox Track { get; }

        private SttsBox.SampleEntry[] Samples { get; }
        public ChapterChunkHandler(uint timeScale, TrakBox trak)
        {
            TimeScale = timeScale;
            Track = trak;
            Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
        }
        public bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int chunkSize, int[] frameSizes)
        {
            if (chunkIndex < 0 || chunkIndex >= Samples.Length)
                return false;

            var duration = Samples[chunkIndex].SampleDelta / TimeScale;

            short size = file.ReadInt16BE();
            byte[] titleBytes = file.ReadBlock(size);

            var title = Encoding.UTF8.GetString(titleBytes);

            Chapters.AddChapter(title, TimeSpan.FromSeconds(duration));

            return true;
        }
    }
}
