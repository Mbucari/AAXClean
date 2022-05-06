using AAXClean.Chunks;
using Mpeg4Lib.Chunks;
using System;

namespace AAXClean.FrameFilters
{
	public interface IFrameEntry
	{
		ChunkEntry Chunk { get; set; }
		uint FrameIndex { get; set; }
		uint FrameDelta { get; set; }
		int FrameSize { get; set; }
		Memory<byte> FrameData { get; set; }
	}
}
