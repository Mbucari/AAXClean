using System;
using System.IO;

namespace Mpeg4Lib.ID3;

public abstract class Header
{
	public abstract string Identifier { get; }
	public int Size { get; protected init; }
	public abstract int HeaderSize { get; }
	public abstract void Render(Stream stream, int renderSize, ushort version);

	public static ushort ReadUInt16BE(Stream stream)
	{
		Span<byte> word = stackalloc byte[2];
		stream.ReadExactly(word);

		return (ushort)(word[0] << 8 | word[1]);
	}

	public static int UnSyncSafify(uint value)
		=> (int)(((value & 0x7f000000) >> 3) | ((value & 0x7f0000) >> 2) | ((value & 0x7f00) >> 1) | (value & 0x7f));

	public static uint SyncSafify(int value)
		=> (uint)(((value << 3) & 0x7f000000) | ((value << 2) & 0x7f0000) | ((value << 1) & 0x7f00) | (value & 0x7F));

	public static uint ReadUInt32BE(Stream stream)
	{
		Span<byte> dword = stackalloc byte[4];
		stream.ReadExactly(dword);

		return (uint)(dword[0] << 24 | dword[1] << 16 | dword[2] << 8 | dword[3]);
	}

	public static byte[] ReadBlock(Stream stream, int numBytes)
	{
		var block = new byte[numBytes];
		stream.ReadExactly(block, 0, numBytes);
		return block;
	}

	public static void WriteUInt32BE(Stream stream, uint value)
		=> stream.Write([(byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value]);

	public static void WriteUInt16BE(Stream stream, uint value)
		=> stream.Write([(byte)(value >> 8), (byte)value]);

	public override string ToString() => Identifier is "\0\0\0\0" ? @"\0\0\0\0" : Identifier is "\0\0\0" ? @"\0\0\0" : Identifier;

	public void SeekForwardToPosition(Stream file, long endPos)
	{
		if (file.Position < endPos)
		{
			//ID3 tag is padded. Seek to end of tag.
			if (file.CanSeek)
				file.Position = endPos;
			else
			{
				var buffer = new byte[4096];
				while (file.Position < endPos)
				{
					var bytesToRead = int.Min(buffer.Length, (int)(endPos - file.Position));
					file.ReadExactly(buffer, 0, bytesToRead);
				}
			}
		}
	}
}
