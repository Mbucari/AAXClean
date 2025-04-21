using Mpeg4Lib.Chunks;
using System;

namespace AAXClean.FrameFilters;

public class FrameEntry
{
	public ChunkEntry? Chunk { get; init; }
	public required uint SamplesInFrame { get; init; }
	public required Memory<byte> FrameData { get; init; }
	public object? ExtraData { get; set; }
}
