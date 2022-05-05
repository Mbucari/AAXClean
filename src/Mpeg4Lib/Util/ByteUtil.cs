using System;

namespace Mpeg4Lib.Util
{
	public class ByteUtil
	{
		public static byte[] BytesFromHexString(string hexString)
		{
			int byteCount = hexString.Length / 2;
			byte[] bytes = new byte[byteCount];

			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = byte.Parse(hexString.Substring(2 * i, 2), System.Globalization.NumberStyles.HexNumber);
			}
			return bytes;
		}
		public static byte[] CloneBytes(byte[] src)
		{
			return CloneBytes(src, 0, src.Length);
		}
		public static byte[] CloneBytes(byte[] src, int srcOffset, int count)
		{
			byte[] dst = new byte[count];
			Buffer.BlockCopy(src, srcOffset, dst, 0, count);
			return dst;
		}
		public static bool BytesEqual(byte[] array1, byte[] array2, bool reverseDirection = false)
		{
			return BytesEqual(array1, 0, array2, 0, array1.Length, reverseDirection);
		}
		public static bool BytesEqual(byte[] array1, int startIndex1, byte[] array2, int startIndex2, int count, bool reverseDirection = false)
		{
			if (array1.Length < startIndex1 + count || array2.Length < startIndex2 + count) return false;

			int indexDiff = startIndex2 - startIndex1;

			for (int i = startIndex1; i < startIndex1 + count; i++)
			{
				int array2Index = reverseDirection ?
					startIndex2 + count - 1 - (i - startIndex1)
					: i + indexDiff;

				if (array1[i] != array2[array2Index])
					return false;
			}
			return true;
		}
	}
}
