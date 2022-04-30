using AAXClean.Boxes;
using System;

namespace AAXClean.Chunks
{
    internal interface IChunkHandler : IDisposable
    {
        TrakBox Track { get; }
        bool ChunkAvailable(Span<byte> chunkData, ChunkEntry chunkEntry);
    }
}
