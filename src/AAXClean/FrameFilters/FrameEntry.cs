using Mpeg4Lib.Chunks;
using System;

namespace AAXClean.FrameFilters
{
	public class FrameEntry
	{
		public ChunkEntry Chunk { get; init; }
		public uint FrameIndex { get; init; }
		public uint SamplesInFrame { get; init; }
		public Memory<byte> FrameData { get; init; }
	}
}
