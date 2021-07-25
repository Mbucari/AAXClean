using System.IO;

namespace AAXClean.Boxes
{
    internal class FreeBox : Box
    {
        public override long RenderSize => base.RenderSize + Header.TotalBoxSize - Header.HeaderSize;

        internal static FreeBox Create(int size, Box parent)
        {
            var header = new BoxHeader((uint)size, "free");
            var free = new FreeBox(header, parent);
            return free;
        }

        private FreeBox(BoxHeader header, Box parent) : base(header, parent) { }
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
