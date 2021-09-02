using AAXClean.Boxes;
using System;
using System.IO;

namespace AAXClean.Chunks
{
    internal interface IChunkHandler : IDisposable
    {
        bool InputStreamSeekable { get; }
        TrakBox Track { get; }
        bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int totalChunkSize, int[] frameSizes);
    }
}
