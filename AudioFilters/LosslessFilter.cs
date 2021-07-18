using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    sealed class LosslessFilter : IFrameeFilter
    {
        public List<uint> ChunkOffsets { get; }
        public Stream OutputStream { get; }
        public LosslessFilter(Stream outputStream)
        {
            OutputStream = outputStream;
            ChunkOffsets = new List<uint>();
        }

        private long lastChunk = -1;
        public bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] audioSample)
        {
            if (chunkIndex > lastChunk)
            {
                ChunkOffsets.Add((uint)OutputStream.Position);
                lastChunk = chunkIndex;
            }
            OutputStream.Write(audioSample);
            return true;
        }

        public void Close()
        {
            OutputStream.Close();
        }

        public void Dispose()
        {
            OutputStream.Dispose();
        }
    }
}
