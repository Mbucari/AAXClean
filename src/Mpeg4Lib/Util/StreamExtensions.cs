using Mpeg4Lib.Boxes;
using System;
using System.IO;

namespace Mpeg4Lib.Util
{
	public static class StreamExtensions
	{
		public static void WriteHeader(this Stream stream, BoxHeader header, long renderSize)
		{
			if (header.Version == 1 || renderSize > uint.MaxValue)
			{
				stream.WriteUInt32BE(1);
				stream.WriteType(header.Type);
				stream.WriteInt64BE(renderSize);
			}
			else
			{
				stream.WriteUInt32BE((uint)renderSize);
				stream.WriteType(header.Type);
			}
		}

		/// <summary>
		/// Read the next track chuink from the input stream
		/// </summary>
		/// <param name="chunkOffset">The chuink's file offset</param>
		/// <param name="chunkBuffer">Buffer to copy the chink data into</param>
		public static void ReadNextChunk(this Stream inputStream, long chunkOffset, Span<byte> chunkBuffer)
		{
			if (inputStream.Position < chunkOffset)
			{
				//Unknown Track or data type. Read past it to next known chunk.
				if (inputStream.CanSeek)
					inputStream.Position = chunkOffset;
				else
					inputStream.ReadBlock((int)(chunkOffset - inputStream.Position));
			}
			else if (inputStream.Position > chunkOffset)
			{
				if (inputStream.CanSeek)
					inputStream.Position = chunkOffset;
				else
					throw new NotSupportedException($"Input stream position 0x{inputStream.Position:X8} is past the chunk offset 0x{chunkOffset:X8} and is not seekable.");
			}
			if (inputStream.Read(chunkBuffer) != chunkBuffer.Length)
				throw new EndOfStreamException($"Stream ended at position {inputStream.Position} before all {chunkBuffer.Length} bytes were read.");
		}		

		public static void WriteType(this Stream stream, string type)
		{
			if (type?.Length != 4)
				throw new ArgumentException("Type must be 4 chas long.");

			stream.WriteDWord(new byte[] { (byte)type[0], (byte)type[1], (byte)type[2], (byte)type[3] });
		}
		public static void WriteInt16BE(this Stream stream, short value)
		{
			stream.WriteUInt16BE((ushort)value);
		}
		public static void WriteUInt16BE(this Stream stream, ushort value)
		{
			stream.WriteWord(new byte[] { (byte)(value >> 8), (byte)value });
		}
		public static void WriteInt32BE(this Stream stream, int value)
		{
			stream.WriteUInt32BE((uint)value);
		}
		public static void WriteUInt32BE(this Stream stream, uint value)
		{
			stream.WriteDWord(new byte[] { (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
		}
		public static void WriteInt64BE(this Stream stream, long value)
		{
			stream.WriteUInt64BE((ulong)value);
		}
		public static void WriteUInt64BE(this Stream stream, ulong value)
		{
			stream.WriteQWord(new byte[] { (byte)(value >> 56), (byte)(value >> 48), (byte)(value >> 40), (byte)(value >> 32), (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
		}
		public static void WriteWord(this Stream stream, byte[] word)
		{
			if (word.Length != 2) throw new ArgumentException("Word must be 2 bytes.");
			stream.Write(word);
		}
		public static void WriteDWord(this Stream stream, byte[] dWord)
		{
			if (dWord.Length != 4) throw new ArgumentException("DWord must be 4 bytes.");
			stream.Write(dWord);
		}
		public static void WriteQWord(this Stream stream, byte[] qWord)
		{
			if (qWord.Length != 8) throw new ArgumentException("QWord must be 8 bytes.");
			stream.Write(qWord);
		}
		public static byte[] ReadBlock(this Stream stream, int length)
		{
			if (length < 0)
				throw new ArgumentException("Length must be non-negative", nameof(length));

			if (length == 0)
				return Array.Empty<byte>();


			byte[] buffer = new byte[length];
			int read = 0, needed = length;
			int count;
			do
			{
				count = stream.Read(buffer, read, needed);
				read += count;
				needed -= count;
			} while (needed > 0 && count != 0);

			if (needed > 0)
				throw new EndOfStreamException($"Stream ended before all {length} bytes could be read");
			return buffer;
		}
		public static ushort ReadUInt16BE(this Stream stream)
		{
			return (ushort)stream.ReadInt16BE();
		}
		public static short ReadInt16BE(this Stream stream)
		{
			byte[] qword = stream.ReadWord();

			return (short)(qword[0] << 8 | qword[1]);
		}
		public static uint ReadUInt32BE(this Stream stream)
		{
			return (uint)stream.ReadInt32BE();
		}
		public static int ReadInt32BE(this Stream stream)
		{
			byte[] qword = stream.ReadDWord();

			return qword[0] << 24 | qword[1] << 16 | qword[2] << 8 | qword[3];
		}
		public static ulong ReadUInt64BE(this Stream stream)
		{
			byte[] qword = stream.ReadQWord();

			return (ulong)qword[0] << 56 | (ulong)qword[1] << 48 | (ulong)qword[2] << 40 | (ulong)qword[3] << 32 | (ulong)qword[4] << 24 | (ulong)qword[5] << 16 | (ulong)qword[6] << 8 | qword[7];
		}
		public static long ReadInt64BE(this Stream stream)
		{
			return (long)stream.ReadUInt64BE();
		}
		public static byte[] ReadWord(this Stream stream) => stream.ReadBlock(2);
		public static byte[] ReadDWord(this Stream stream) => stream.ReadBlock(4);
		public static byte[] ReadQWord(this Stream stream) => stream.ReadBlock(8);
		public static string ReadType(this Stream stream)
		{
			byte[] qword = stream.ReadDWord();

			return new string(new char[] { (char)qword[0], (char)qword[1], (char)qword[2], (char)qword[3] });
		}
	}
}
