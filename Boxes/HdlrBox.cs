using AAXClean.Util;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AAXClean.Boxes
{
    internal class HdlrBox : FullBox
    {
        public bool HasNullTerminator { get; }
        public override uint RenderSize => base.RenderSize + 20 + (uint)Encoding.ASCII.GetByteCount(HandlerName) + (uint)(HasNullTerminator ? 1 : 0);
        public uint PreDefined { get; }
        public string HandlerType { get; }
        byte[] Reserved { get; }
        public string HandlerName { get; }
        internal HdlrBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            long endPos = Header.FilePosition + Header.TotalBoxSize;

            PreDefined = file.ReadUInt32BE();
            HandlerType = Encoding.ASCII.GetString(file.ReadBlock(4));
            Reserved = file.ReadBlock(12);

            var blist = new List<byte>();
            while (file.Position < endPos)
            {
                var lastByte = (byte)file.ReadByte();
                blist.Add(lastByte);
                HasNullTerminator = lastByte == 0;
            }
            HandlerName = Encoding.ASCII.GetString(blist.ToArray());
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE(PreDefined);
            file.WriteDWord(Encoding.ASCII.GetBytes(HandlerType));
            file.Write(Reserved);
            file.Write(Encoding.ASCII.GetBytes(HandlerName));
            if (HasNullTerminator)
                file.WriteByte(0);
        }
    }
}
