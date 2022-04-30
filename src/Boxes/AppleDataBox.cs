using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal class AppleDataBox : Box
    {
        public override long RenderSize => base.RenderSize + 8 + Data.Length;
        public AppleDataType DataType { get; }
        public uint Flags { get; }
        public byte[] Data { get; set; }

        public static void Create(Box parent, byte[] data, AppleDataType type)
        {
            int size = data.Length + 8 /* empty Box size*/;
            var header = new BoxHeader((uint)size, "data");

            var dataBox = new AppleDataBox(header, parent, data, type);

            parent.Children.Add(dataBox);
        }
        private AppleDataBox(BoxHeader header, Box parent, byte[] data, AppleDataType type) : base(header, parent)
        {
            DataType = type;
            Flags = 0;
            Data = data;
        }
        internal AppleDataBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            DataType = (AppleDataType)file.ReadUInt32BE();
            Flags = file.ReadUInt32BE();
            long length = Header.FilePosition + Header.TotalBoxSize - file.Position;
            Data = file.ReadBlock((int)length);
        }
        protected override void Render(Stream file)
        {
            file.WriteUInt32BE((uint)DataType);
            file.WriteUInt32BE(Flags);
            file.Write(Data);
        }
    }
}
