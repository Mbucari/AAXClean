using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal abstract class FullBox : Box, IFullBox
    {
        public override long RenderSize => base.RenderSize + 4;
        public byte Version => VersionFlags[0];
        public int Flags => VersionFlags[1] << 16 | VersionFlags[2] << 8 | VersionFlags[3];

        protected byte[] VersionFlags { get; }
        internal FullBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            VersionFlags = file.ReadBlock(4);
        }
        internal FullBox(byte[] versionFlags, BoxHeader header, Box parent) : base(header, parent)
        {
            VersionFlags = versionFlags;
        }
        protected override void Render(Stream file)
        {
            file.WriteDWord(VersionFlags);
        }
    }

    internal interface IFullBox
    {
        long RenderSize { get; }
    }
}
