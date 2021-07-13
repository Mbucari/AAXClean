using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class StszBox : FullBox
    {
        public override uint RenderSize => base.RenderSize + 8 + (uint)SampleSizes.Count * 4;
        public uint SampleSize { get; }
        public uint SampleCount { get; }
        public List<int> SampleSizes { get; } = new List<int>();
        internal StszBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            SampleSize = file.ReadUInt32BE();
            SampleCount = file.ReadUInt32BE();

            if (SampleSize > 0) return;

            for (int i = 0; i < SampleCount; i++)
            {
                int sampleSize = file.ReadInt32BE();
                SampleSizes.Add(sampleSize);
            }
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE(SampleSize);
            file.WriteUInt32BE((uint)SampleSizes.Count);
            foreach (var sampleSize in SampleSizes)
            {
                file.WriteInt32BE(sampleSize);
            }
        }

        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                SampleSizes.Clear();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}
