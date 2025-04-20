using System.Collections.Generic;

namespace Mpeg4Lib.Boxes;

public interface IChunkOffsets : IBox
{
	uint EntryCount { get; set; }
	List<ChunkOffsetEntry> ChunkOffsets { get; }
}
