using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
	internal class Co64Box : FullBox
	{
		public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 8;
		internal uint EntryCount { get; set; }
		internal List<ChunkOffsetEntry> ChunkOffsets { get; } = new List<ChunkOffsetEntry>();

		internal static Co64Box CreateBlank(Box parent)
		{
			int size = 4 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "co64");

			Co64Box co64Box = new Co64Box(new byte[] { 0, 0, 0, 0 }, header, parent);

			parent.Children.Add(co64Box);
			return co64Box;
		}

		private Co64Box(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }

		internal Co64Box(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
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
			//Write ChunkOffsets sorted by the chunk index
			ChunkOffsets.Sort((c1, c2) => c1.EntryIndex.CompareTo(c2.EntryIndex));
			foreach (ChunkOffsetEntry chunkOffset in ChunkOffsets)
			{
				file.WriteInt64BE(chunkOffset.ChunkOffset);
			}
		}
		private bool _disposed = false;
		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				ChunkOffsets.Clear();
			}

			_disposed = true;

			base.Dispose(disposing);
		}
	}
}
