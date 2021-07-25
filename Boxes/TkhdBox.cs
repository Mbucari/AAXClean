using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    class TkhdBox : FullBox
    {
        public override long RenderSize => base.RenderSize + 3 * (Header.Version == 0 ? 4 : 8) + 2 * 4 + 8 + 4 * 2 + Matrix.Length + 2 * 4;
        internal ulong CreationTime { get; }
        internal ulong ModificationTime { get; }
        internal uint TrackID { get; }
        internal uint Reserved { get; }
        internal ulong Duration { get; set; }
        internal ulong Reserved2 { get; }
        internal short Layer { get; }
        internal short AlternateGroup { get; }
        internal short Volume { get; }
        internal ushort Reserved3 { get; }
        internal byte[] Matrix { get; }
        internal uint Width { get; }
        internal uint Height { get; }

        public TkhdBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            if (Header.Version == 0)
            {
                CreationTime = file.ReadUInt32BE();
                ModificationTime = file.ReadUInt32BE();
                TrackID = file.ReadUInt32BE();
                Reserved = file.ReadUInt32BE();
                Duration = file.ReadUInt32BE();
            }
            else
            {
                CreationTime = file.ReadUInt64BE();
                ModificationTime = file.ReadUInt64BE();
                TrackID = file.ReadUInt32BE();
                Reserved = file.ReadUInt32BE();
                Duration = file.ReadUInt64BE();
            }
            Reserved2 = file.ReadUInt64BE();
            Layer = file.ReadInt16BE();
            AlternateGroup = file.ReadInt16BE();
            Volume = file.ReadInt16BE();
            Reserved3 = file.ReadUInt16BE();
            Matrix = file.ReadBlock(4 * 9);
            Width = file.ReadUInt32BE();
            Height = file.ReadUInt32BE();
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            if (Header.Version == 0)
            {
                file.WriteUInt32BE((uint)CreationTime);
                file.WriteUInt32BE((uint)ModificationTime);
                file.WriteUInt32BE(TrackID);
                file.WriteUInt32BE(Reserved);
                file.WriteUInt32BE((uint)Duration);
            }
            else
            {
                file.WriteUInt64BE(CreationTime);
                file.WriteUInt64BE(ModificationTime);
                file.WriteUInt32BE(TrackID);
                file.WriteUInt32BE(Reserved);
                file.WriteUInt64BE(Duration);
            }
            file.WriteUInt64BE(Reserved2);
            file.WriteInt16BE(Layer);
            file.WriteInt16BE(AlternateGroup);
            file.WriteInt16BE(Volume);
            file.WriteUInt16BE(Reserved3);
            file.Write(Matrix);
            file.WriteUInt32BE(Width);
            file.WriteUInt32BE(Height);
        }
    }
}
