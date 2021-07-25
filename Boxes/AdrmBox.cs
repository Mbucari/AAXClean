using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAXClean.Util;

namespace AAXClean.Boxes
{
    internal class AdrmBox : Box
    {
        public override long RenderSize => base.RenderSize + beginBlob.Length + DrmBlob.Length + middleBlob.Length + Checksum.Length + endBlob.Length;
        private byte[] beginBlob { get; }
        public byte[] DrmBlob { get; }
        private byte[] middleBlob { get; }
        public byte[] Checksum { get; }
        private byte[] endBlob { get; }
        public AdrmBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            beginBlob = file.ReadBlock(8);
            DrmBlob = file.ReadBlock(56);
            middleBlob = file.ReadBlock(4);
            Checksum = file.ReadBlock(20);
            long len = Header.FilePosition + Header.TotalBoxSize - file.Position;
            endBlob = file.ReadBlock((int)len);
        }

        protected override void Render(Stream file)
        {
            file.Write(beginBlob);
            file.Write(DrmBlob);
            file.Write(middleBlob);
            file.Write(Checksum);
            file.Write(endBlob);
        }
    }
}
