using Mpeg4Lib.Chunks;
using System;

namespace AAXClean.FrameFilters
{
	public class FrameEntry
	{
		public ChunkEntry Chunk { get; init; }
		public uint FrameIndex { get; init; }
		public uint FrameDelta { get; init; }
		public int FrameSize { get; init; }
		public Memory<byte> FrameData { get; init; }
	}
}
