using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class SttsBox : FullBox
    {
        public override uint RenderSize => base.RenderSize + 4 + (uint)Samples.Count * 2 * 4;
        public uint EntryCount { get; }
        public List<SampleEntry> Samples { get; } = new List<SampleEntry>();
        internal SttsBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            EntryCount = file.ReadUInt32BE();

            for (int i = 0; i < EntryCount; i++)
            {
                Samples.Add(new SampleEntry(file));
            }
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE((uint)Samples.Count);
            foreach (var sample in Samples)
            {
                file.WriteUInt32BE(sample.FrameCount);
                file.WriteUInt32BE(sample.FrameDelta);
            }
        }

        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Samples.Clear();
            }

            _disposed = true;

            base.Dispose(disposing);
        }

        public class SampleEntry
        {
            public SampleEntry(Stream file)
            {
                FrameCount = file.ReadUInt32BE();
                FrameDelta = file.ReadUInt32BE();
            }
            public SampleEntry(uint sampleCount, uint sampleDelta)
            {
                FrameCount = sampleCount;
                FrameDelta = sampleDelta;
            }
            public uint FrameCount { get; }
            public uint FrameDelta { get; }
        }
    }

}
