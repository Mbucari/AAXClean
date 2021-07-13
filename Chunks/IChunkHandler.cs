using AAXClean.Boxes;
using System.IO;

namespace AAXClean.Chunks
{
    internal interface IChunkHandler
    {
        TrakBox Track { get; }
        void Init();
        bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int chunkSize, int[] frameSizes);
    }
}
