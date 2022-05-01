using AAXClean.Boxes;
using System;

namespace AAXClean.Chunks
{
	internal interface IChunkHandler : IDisposable
	{
		TrakBox Track { get; }
		bool ChunkAvailable(ChunkEntry chunkEntry, Span<byte> chunkData);
	}
}
