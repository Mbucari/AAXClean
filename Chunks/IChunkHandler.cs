using AAXClean.Boxes;
using System.IO;

namespace AAXClean.Chunks
{
    internal interface IChunkHandler
    {
        TrakBox Track { get; }
        bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int chunkSize, int[] frameSizes);
    }
}
