using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

internal class StcoBox : FullBox, IChunkOffsets
{
	public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 4;
	public uint EntryCount => (uint)ChunkOffsets.Count;
	public ChunkOffsetList ChunkOffsets { get; }

	internal static StcoBox CreateBlank(IBox parent, ChunkOffsetList chunkOffsets)
	{
		int size = 4 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "stco");
		chunkOffsets.Sort();
		StcoBox stcoBox = new StcoBox(chunkOffsets, [0, 0, 0, 0], header, parent);
		parent.Children.Add(stcoBox);
		return stcoBox;
	}
	private StcoBox(ChunkOffsetList chunkOffsets, byte[] versionFlags, BoxHeader header, IBox parent)
		: base(versionFlags, header, parent)
	{
		ChunkOffsets = chunkOffsets;
	}

	public StcoBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		uint entryCount = file.ReadUInt32BE(); if (entryCount > int.MaxValue)
			throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} chunk offsets");

		ChunkOffsets = ChunkOffsetList.Read32(file, entryCount);
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteUInt32BE(EntryCount);
		ChunkOffsets.Write32(file);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing & !Disposed)
			ChunkOffsets.Clear();
		base.Dispose(disposing);
	}
}
