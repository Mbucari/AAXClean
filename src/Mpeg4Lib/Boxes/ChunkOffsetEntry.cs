namespace Mpeg4Lib.Boxes;

public sealed class ChunkOffsetEntry
{
	public uint EntryIndex { get; init; }
	public long ChunkOffset { get; set; }
}
