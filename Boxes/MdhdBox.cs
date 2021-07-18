using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal class MdhdBox : FullBox
    {
        public override uint RenderSize => base.RenderSize + (uint)(Version == 0 ? 20 : 32);
        public ulong CreationTime { get; }
        public ulong ModificationTime { get; }
        public uint Timescale { get; }
        public ulong Duration { get; set; }
        private byte[] Blob { get; }
        internal MdhdBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            if (Version == 0)
            {
                CreationTime = file.ReadUInt32BE();
                ModificationTime = file.ReadUInt32BE();
                Timescale = file.ReadUInt32BE();
                Duration = file.ReadUInt32BE();
            }
            else
            {
                CreationTime = file.ReadUInt64BE();
                ModificationTime = file.ReadUInt64BE();
                Timescale = file.ReadUInt32BE();
                Duration = file.ReadUInt64BE();
            }
            Blob = file.ReadBlock(4);
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            if (Version == 0)
            {
                file.WriteUInt32BE((uint)CreationTime);
                file.WriteUInt32BE((uint)ModificationTime);
                file.WriteUInt32BE(Timescale);
                file.WriteUInt32BE((uint)Duration);
            }
            else
            {
                file.WriteUInt64BE(CreationTime);
                file.WriteUInt64BE(ModificationTime);
                file.WriteUInt32BE(Timescale);
                file.WriteUInt64BE(Duration);
            }
            file.Write(Blob);
        }
    }
}
