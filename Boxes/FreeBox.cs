using System.IO;

namespace AAXClean.Boxes
{
    internal class FreeBox : Box
    {
        public override uint RenderSize => base.RenderSize + Header.TotalBoxSize - Header.HeaderSize;
        internal FreeBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            for (uint i = Header.HeaderSize; i < Header.TotalBoxSize; i++)
                file.ReadByte();
        }
        protected override void Render(Stream file)
        {
            for (uint i = Header.HeaderSize; i < Header.TotalBoxSize; i++)
                file.WriteByte(0);
        }
    }
}
