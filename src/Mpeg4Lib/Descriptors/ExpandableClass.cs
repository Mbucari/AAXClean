using System;
using System.IO;

namespace Mpeg4Lib.Descriptors;

public static class ExpandableClass
{
	public static void EncodeSize(Stream file, int sizeOfInstance, int minimumBytes)
	{
		const int IntegerBytes = sizeof(int);
		const int MaxSize = (1 << (7 * IntegerBytes)) - 1;

		ArgumentOutOfRangeException.ThrowIfGreaterThan(sizeOfInstance, MaxSize, nameof(sizeOfInstance));

		int size = GetSizeByteCount(sizeOfInstance, minimumBytes);

		for (int i = size - 1; i > 0; i--)
		{
			var b = 0x80 | ((sizeOfInstance >> (7 * i)) & 0x7f);
			file.WriteByte((byte)b);
		}

		file.WriteByte((byte)(sizeOfInstance & 0x7f));
	}

	public static int GetSizeByteCount(int sizeOfInstance, int minimumBytes)
	{
		int r = 0;
		while ((sizeOfInstance >>= 1) != 0)
			r++;
		return Math.Max(minimumBytes, r / 7 + 1);
	}

	public static int DecodeSize(Stream file)
	{
		int b, sizeOfInstance = 0;
		do
		{
			b = file.ReadByte();
			sizeOfInstance = (sizeOfInstance << 7) | (b & 0x7f);
		} while ((b & 0x80) != 0);

		return sizeOfInstance;
	}
}
