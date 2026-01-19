using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

internal class Co64Box : FullBox, IChunkOffsets
{
	public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 8;
	public uint EntryCount => (uint)ChunkOffsets.Count;
	public ChunkOffsetList ChunkOffsets { get; }

	internal static Co64Box CreateBlank(IBox parent, ChunkOffsetList chunkOffsets)
	{
		int size = 4 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "co64");
		chunkOffsets.Sort();
		Co64Box co64Box = new Co64Box(chunkOffsets, [0, 0, 0, 0], header, parent);
		parent.Children.Add(co64Box);
		return co64Box;
	}

	private Co64Box(ChunkOffsetList chunkOffsets, byte[] versionFlags, BoxHeader header, IBox parent)
		: base(versionFlags, header, parent)
	{
		ChunkOffsets = chunkOffsets;
	}

	public Co64Box(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		uint entryCount = file.ReadUInt32BE();
		if (entryCount > int.MaxValue)
			throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} chunk offsets");
		ChunkOffsets = ChunkOffsetList.Read64(file, entryCount);
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteUInt32BE(EntryCount);
		ChunkOffsets.Write64(file);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && !Disposed)
			ChunkOffsets.Clear();
		base.Dispose(disposing);
	}
}
