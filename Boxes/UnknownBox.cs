using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal class UnknownBox : Box
    {
        public override long RenderSize => base.RenderSize + Data.Length;
        public byte[] Data { get; }
        internal UnknownBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            Data = file.ReadBlock((int)(Header.TotalBoxSize - header.HeaderSize));
        }

        public override string ToString()
        {
            return nameof(UnknownBox) + "-" + Header.Type;
        }
        protected override void Render(Stream file)
        {
            file.Write(Data);
        }
    }
}
