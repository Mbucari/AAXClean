using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal class MdhdBox : FullBox
    {
        public override uint RenderSize => base.RenderSize + 20;
        public uint CreationTime { get; }
        public uint ModificationTime { get; }
        public uint Timescale { get; }
        public uint Duration { get; }
        private byte[] Blob { get; }
        internal MdhdBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            CreationTime = file.ReadUInt32BE();
            ModificationTime = file.ReadUInt32BE();
            Timescale = file.ReadUInt32BE();
            Duration = file.ReadUInt32BE();
            Blob = file.ReadBlock(4);
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE(CreationTime);
            file.WriteUInt32BE(ModificationTime);
            file.WriteUInt32BE(Timescale);
            file.WriteUInt32BE(Duration);
            file.Write(Blob);
        }
    }
}
