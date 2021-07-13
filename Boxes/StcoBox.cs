using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class StcoBox : FullBox
    {
        public override uint RenderSize => base.RenderSize + 4 + (uint)ChunkOffsets.Count * 4;
        public uint EntryCount { get; }
        public List<uint> ChunkOffsets { get; } = new List<uint>();
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
