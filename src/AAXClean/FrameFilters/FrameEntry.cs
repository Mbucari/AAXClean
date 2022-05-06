using Mpeg4Lib.Chunks;
using System;

namespace AAXClean.FrameFilters
{
	public class FrameEntry : IFrameEntry
	{
		public ChunkEntry Chunk { get; set; }
		public uint FrameIndex { get; set; }
		public uint FrameDelta { get; set; }
		public int FrameSize { get; set; }
		public Memory<byte> FrameData { get; set; }
	}
}
