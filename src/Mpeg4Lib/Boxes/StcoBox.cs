using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	public class StcoBox : FullBox, IChunkOffsets
	{
		public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 4;
		public uint EntryCount { get; set; }
		public List<ChunkOffsetEntry> ChunkOffsets { get; private init; } = new List<ChunkOffsetEntry>();

		public static StcoBox CreateBlank(Box parent, List<ChunkOffsetEntry> chunkOffsets)
		{
			int size = 4 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stco");

			StcoBox stcoBox = new StcoBox(new byte[] { 0, 0, 0, 0 }, header, parent)
			{
				ChunkOffsets = chunkOffsets,
				EntryCount = (uint)chunkOffsets.Count
			};

			parent.Children.Add(stcoBox);
			return stcoBox;
		}
		private StcoBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }
		public StcoBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			EntryCount = file.ReadUInt32BE();

			long lastChunkOffset = 0;
			for (uint i = 0; i < EntryCount; i++)
			{
				long chunkOffset = file.ReadUInt32BE();

				//Seems some files incorrectly use stco box with offsets > uint.MAXVALUE (e.g. 50 Self Help Books).
				//This causes the offsets to uint to overflow. Unfottnately, somtimes chapters are out of order and
				//chunkOffset is supposed to be less than lastChunkOffset (e.g. Altered Carbon). To attempt to
				//detect this, only assume it's an overflow if lastChunkOffset - chunkOffset > uint.MaxValue / 2
				if (chunkOffset < lastChunkOffset && lastChunkOffset - chunkOffset > uint.MaxValue / 2)
				{
					chunkOffset += 1L << 32;
				}
				ChunkOffsets.Add(new ChunkOffsetEntry
				{
					EntryIndex = i,
					ChunkOffset = chunkOffset
				});
				lastChunkOffset = chunkOffset;
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
				file.WriteUInt32BE((uint)chunkOffset.ChunkOffset);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing & !Disposed)
				ChunkOffsets.Clear();
			base.Dispose(disposing);
		}
	}
}
