using AAXClean.Util;
using System;
using System.IO;
using System.Text;

namespace AAXClean.Boxes
{
    public class BoxHeader
    {
        public long FilePosition { get; }
        public uint TotalBoxSize { get; }
        public string Type { get; set; }
        public uint HeaderSize { get; }
        internal BoxHeader(Stream file)
        {
            FilePosition = file.Position;
            TotalBoxSize = file.ReadUInt32BE();
            Type = file.ReadType();
            HeaderSize = 8;
        }
        public BoxHeader(uint boxSize, string boxType)
        {
            if (boxSize < 8)
                throw new ArgumentException($"{nameof(boxSize)} must be at least 8 bytes.");

            if (string.IsNullOrEmpty(boxType) || Encoding.UTF8.GetByteCount(boxType) != 4)
                throw new ArgumentException($"{nameof(boxType)} must be a 4-byte long UTF-8 string.");

            FilePosition = -1;
            TotalBoxSize = boxSize;
            Type = boxType;
            HeaderSize = 8;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
