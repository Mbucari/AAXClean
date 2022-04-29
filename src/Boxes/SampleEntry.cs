using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal abstract class SampleEntry : Box
    {
        public override long RenderSize => base.RenderSize + 8;

        private readonly byte[] reserved;
        public ushort DataReferenceIndex { get; }
        internal SampleEntry(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            reserved = file.ReadBlock(6);
            DataReferenceIndex = file.ReadUInt16BE();
        }

        protected override void Render(Stream file)
        {
            file.Write(reserved);
            file.WriteUInt16BE(DataReferenceIndex);
        }
    }
}
