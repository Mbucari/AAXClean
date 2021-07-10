using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal class AppleDataBox : FullBox
    {
        public enum FlagType : uint
        {
            /// <summary>
            ///    The box contains UTF-8 text.
            /// </summary>
            ContainsText = 0x01,

            /// <summary>
            ///    The box contains binary data.
            /// </summary>
            ContainsData = 0x00,

            /// <summary>
            ///    The box contains data for a tempo box.
            /// </summary>
            ForTempo = 0x15,

            /// <summary>
            ///    The box contains a raw JPEG image.
            /// </summary>
            ContainsJpegData = 0x0D,

            /// <summary>
            ///    The box contains a raw PNG image.
            /// </summary>
            ContainsPngData = 0x0E,

            /// <summary>
            ///    The box contains a raw BMP image.
            /// </summary>
            ContainsBmpData = 0x1B

        }

        public override uint RenderSize => base.RenderSize + 4 + (uint)Data.Length;
        public FlagType DataType { get; }
        public byte[] Data { get; set; }

        public static AppleDataBox Create(Box parent, byte[] data, FlagType type)
        {
            int size = data.Length + 12 /* empty FullBox size*/;
            var header = new BoxHeader((uint)size, "data");

            return new AppleDataBox(header, parent, data, type);
        }
        private AppleDataBox(BoxHeader header, Box parent, byte[] data, FlagType type) : base(new byte[] { 0, 0, 0, 1 }, header, parent)
        {
            DataType = type;
            Data = data;
        }
        internal AppleDataBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            DataType = (FlagType)file.ReadUInt32BE();

            long length = Header.FilePosition + Header.TotalBoxSize - file.Position;
            Data = file.ReadBlock((int)length);
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.WriteUInt32BE((uint)DataType);
            file.Write(Data);
        }
    }
}
