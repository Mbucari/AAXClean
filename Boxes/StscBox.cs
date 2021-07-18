using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
    internal class StscBox : FullBox
    {
        public override uint RenderSize => base.RenderSize + 4 + (uint)Samples.Count * 3 * 4;
        internal uint EntryCount { get; set; }
        internal List<ChunkEntry> Samples { get; } = new List<ChunkEntry>();

        internal static StscBox CreateBlank(Box parent)
        {
            int size = 4 + 12 /* empty Box size*/;
            var header = new BoxHeader((uint)size, "stsc");

            var stscBox = new StscBox(new byte[] { 0, 0, 0, 0 }, header, parent);

            parent.Children.Add(stscBox);
            return stscBox;
        }
        private StscBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent)
        {

        }
        internal StscBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            EntryCount = file.ReadUInt32BE();

            for (int i = 0; i < EntryCount; i++)
            {
                Samples.Add(new ChunkEntry(file));
            }
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE((uint)Samples.Count);
            foreach (var sample in Samples)
            {
                file.WriteUInt32BE(sample.FirstChunk);
                file.WriteUInt32BE(sample.SamplesPerChunk);
                file.WriteUInt32BE(sample.SampleDescriptionIndex);
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

        public class ChunkEntry
        {
            public ChunkEntry(Stream file)
            {
                FirstChunk = file.ReadUInt32BE();
                SamplesPerChunk = file.ReadUInt32BE();
                SampleDescriptionIndex = file.ReadUInt32BE();
            }
            public ChunkEntry(uint firstChunk, uint samplesPerChunk, uint sampleDesIndex)
            {
                FirstChunk = firstChunk;
                SamplesPerChunk = samplesPerChunk;
                SampleDescriptionIndex = sampleDesIndex;
            }
            public uint FirstChunk { get; }
            public uint SamplesPerChunk { get; }
            public uint SampleDescriptionIndex { get; }
        }
    }
}
