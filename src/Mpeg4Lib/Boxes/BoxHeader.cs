using Mpeg4Lib.Util;
using System;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes
{
	public class BoxHeader
	{
		public long FilePosition { get; internal set; }
		public long TotalBoxSize { get; }
		public string Type { get; private set; }
		public uint HeaderSize { get; private set; }
		public int Version { get; private set; }

		public BoxHeader(Stream file)
		{
			FilePosition = file.Position;
			TotalBoxSize = file.ReadUInt32BE();
			Type = file.ReadType();
			HeaderSize = 8;

			if (TotalBoxSize == 1)
			{
				Version = 1;
				TotalBoxSize = file.ReadInt64BE();
				HeaderSize += 8;
			}
		}

		public void ChangeAtomName(string newAtomName)
		{
            if (string.IsNullOrEmpty(newAtomName) || Encoding.UTF8.GetByteCount(newAtomName) != 4)
                throw new ArgumentException($"{nameof(newAtomName)} must be exactly 4 UTF-8 bytes long");
            Type = newAtomName;
        }

		public BoxHeader(long boxSize, string boxType)
		{
			if (boxSize < 8)
				throw new ArgumentException($"{nameof(boxSize)} must be at least 8 bytes.");

			if (string.IsNullOrEmpty(boxType) || Encoding.ASCII.GetByteCount(boxType) != 4)
				throw new ArgumentException($"{nameof(boxType)} must be a 4-byte long ASCII string.");

			FilePosition = 0;

			Version = boxSize > uint.MaxValue ? 1 : 0;
			TotalBoxSize = boxSize;
			Type = boxType;
			HeaderSize = boxSize > uint.MaxValue ? 16u : 8u;
		}

		public override string ToString()
		{
			return Type;
		}
	}
}
