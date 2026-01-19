namespace Mpeg4Lib.Boxes;

public interface IChunkOffsets : IBox
{
	uint EntryCount { get; }
	ChunkOffsetList ChunkOffsets { get; }

	public static IChunkOffsets Create(StblBox stbl, ChunkOffsetList offsets)
	{
		var maxOffset = offsets.GetOffsetAtIndex(offsets.Count - 1);
		return maxOffset > uint.MaxValue ? Co64Box.CreateBlank(stbl, offsets) : StcoBox.CreateBlank(stbl, offsets);
	}
}
