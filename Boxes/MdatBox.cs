using AAXClean.Chunks;
using System;
using System.IO;

namespace AAXClean.Boxes
{
    internal class MdatBox : Box
    {
        public long Position { get; }

        public MdatChunk FirstEntry { get; }
        internal MdatBox(Stream file, BoxHeader header) : base(header, null)
        {
            Position = file.Position - header.HeaderSize;

            FirstEntry = MdatFactory.CreateEntry(file);
        }

        protected override void Render(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
