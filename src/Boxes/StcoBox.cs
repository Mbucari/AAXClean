using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class StcoBox : FullBox
    {
        public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 4;
        internal uint EntryCount { get; set; }
        internal List<ChunkEntry> ChunkOffsets { get; } = new List<ChunkEntry>();

        internal static StcoBox CreateBlank(Box parent)
        {
            int size = 4 + 12 /* empty Box size*/;
            var header = new BoxHeader((uint)size, "stco");

            var stcoBox = new StcoBox(new byte[] { 0, 0, 0, 0 }, header, parent);

            parent.Children.Add(stcoBox);
            return stcoBox;
        }
        private StcoBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }
        internal StcoBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            EntryCount = file.ReadUInt32BE();

            long lastChunkOffset = 0;
            for (uint i = 0; i < EntryCount; i++)
            {
                long chunkOffset = file.ReadUInt32BE();

                //Seems some files incorrectly use stco box with offsets > uint.MAXVALUE (e.g. 50 Self Help Books).
                //This causes the offsets to wrap around. Unfottnately, somtimes chapters are out of order and
                //chunkOffset is supposed to be less than lastChunkOffset (e.g. Altered Carbon). To attempt to
                //detect this, only assume it's a wrap around if lastChunkOffset - chunkOffset > uint.MaxValue / 2
                if (chunkOffset < lastChunkOffset && lastChunkOffset - chunkOffset > uint.MaxValue / 2)
                {
                    chunkOffset += 1L << 32;
                }
                ChunkOffsets.Add(new ChunkEntry
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
            //Write ChunkOffsets sorted by the chunk index
            ChunkOffsets.Sort((c1, c2) => c1.EntryIndex.CompareTo(c2.EntryIndex));
            foreach (var chunkOffset in ChunkOffsets)
            {
                file.WriteUInt32BE((uint)chunkOffset.ChunkOffset);
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
