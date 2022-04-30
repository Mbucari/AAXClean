using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class SttsBox : FullBox
    {
        public override long RenderSize => base.RenderSize + 4 + Samples.Count * 2 * 4;
        internal uint EntryCount { get; set; }
        internal List<SampleEntry> Samples { get; } = new List<SampleEntry>();

        internal static SttsBox CreateBlank(Box parent)
        {
            int size = 4 + 12 /* empty Box size*/;
            var header = new BoxHeader((uint)size, "stts");

            var sttsBox = new SttsBox(new byte[] { 0, 0, 0, 0 }, header, parent);

            parent.Children.Add(sttsBox);
            return sttsBox;
        }
        private SttsBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }

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
