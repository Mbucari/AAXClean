using AAXClean.Boxes;
using System;

namespace AAXClean.Chunks
{
    internal interface IChunkHandler : IDisposable
    {
        bool InputStreamSeekable { get; }
        TrakBox Track { get; }
        bool ChunkAvailable(Span<byte> chunk, uint chunkIndex, uint frameIndex, int totalChunkSize, int[] frameSizes);
    }
}
