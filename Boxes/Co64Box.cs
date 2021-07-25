using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Boxes
{
    internal class Co64Box : FullBox
    {
        public override long RenderSize => base.RenderSize + 4 + ChunkOffsets.Count * 8;
        internal uint EntryCount { get; set; }
        internal List<long> ChunkOffsets { get; } = new List<long>();

        internal static Co64Box CreateBlank(Box parent)
        {
            int size = 4 + 12 /* empty Box size*/;
            var header = new BoxHeader((uint)size, "co64");

            var co64Box = new Co64Box(new byte[] { 0, 0, 0, 0 }, header, parent);

            parent.Children.Add(co64Box);
            return co64Box;
        }
        private Co64Box(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent)
        {

        }

        internal Co64Box(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            EntryCount = file.ReadUInt32BE();

            for (int i = 0; i < EntryCount; i++)
            {
                long sampleSize = file.ReadInt64BE();
                ChunkOffsets.Add(sampleSize);
            }
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE((uint)ChunkOffsets.Count);
            foreach (var chunkOffset in ChunkOffsets)
            {
                file.WriteInt64BE(chunkOffset);
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
