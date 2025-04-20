using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

public class Co64Box : FullBox, IChunkOffsets
{
	public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 8;
	public uint EntryCount { get; set; }
	public List<ChunkOffsetEntry> ChunkOffsets { get; private init; } = new List<ChunkOffsetEntry>();

	public static Co64Box CreateBlank(IBox parent, List<ChunkOffsetEntry> chunkOffsets)
	{
		int size = 4 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "co64");

		Co64Box co64Box = new Co64Box([0, 0, 0, 0], header, parent)
		{
			ChunkOffsets = chunkOffsets,
			EntryCount = (uint)chunkOffsets.Count
		};

		parent.Children.Add(co64Box);
		return co64Box;
	}

	private Co64Box(byte[] versionFlags, BoxHeader header, IBox parent)
		: base(versionFlags, header, parent) { }

	public Co64Box(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		EntryCount = file.ReadUInt32BE();

		for (uint i = 0; i < EntryCount; i++)
		{
			long chunkOffset = file.ReadInt64BE();
			ChunkOffsets.Add(new ChunkOffsetEntry
			{
				EntryIndex = i,
				ChunkOffset = chunkOffset
			});
		}
		//Load ChunkOffsets sorted by the offset
		ChunkOffsets.Sort((c1, c2) => c1.ChunkOffset.CompareTo(c2.ChunkOffset));
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteUInt32BE((uint)ChunkOffsets.Count);
		//Write ChunkOffsets sorted by the chunk index, leaving ChunkOffsets unsorted
		IOrderedEnumerable<ChunkOffsetEntry> orderedChunkOffsets = ChunkOffsets.OrderBy(co => co.EntryIndex);
		foreach (ChunkOffsetEntry chunkOffset in orderedChunkOffsets)
		{
			file.WriteInt64BE(chunkOffset.ChunkOffset);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && !Disposed)
			ChunkOffsets.Clear();
		base.Dispose(disposing);
	}
}
