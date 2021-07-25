using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class StcoBox : FullBox
    {
        public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 4;
        internal uint EntryCount { get; set; }
        internal List<uint> ChunkOffsets { get; } = new List<uint>();

        internal static StcoBox CreateBlank(Box parent)
        {
            int size = 4 + 12 /* empty Box size*/;
            var header = new BoxHeader((uint)size, "stco");

            var stcoBox = new StcoBox(new byte[] { 0, 0, 0, 0 }, header, parent);

            parent.Children.Add(stcoBox);
            return stcoBox;
        }
        private StcoBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent)
        {

        }
        internal StcoBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            EntryCount = file.ReadUInt32BE();

            for (int i = 0; i < EntryCount; i++)
            {
                uint sampleSize = file.ReadUInt32BE();
                ChunkOffsets.Add(sampleSize);
            }
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE((uint)ChunkOffsets.Count);
            foreach (var chunkOffset in ChunkOffsets)
            {
                file.WriteUInt32BE(chunkOffset);
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
