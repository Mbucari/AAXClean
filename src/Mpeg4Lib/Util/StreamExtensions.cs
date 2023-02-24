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

			stream.Write(stackalloc byte[] { (byte)type[0], (byte)type[1], (byte)type[2], (byte)type[3] });
		}

		public static void WriteInt16BE(this Stream stream, short value)
			=> stream.WriteUInt16BE((ushort)value);
		public static void WriteInt32BE(this Stream stream, int value)
			=> stream.WriteUInt32BE((uint)value);
		public static void WriteInt64BE(this Stream stream, long value)
			=> stream.WriteUInt64BE((ulong)value);

		public static void WriteUInt16BE(this Stream stream, ushort value)
			=> stream.Write(stackalloc byte[] { (byte)(value >> 8), (byte)value });
		public static void WriteUInt32BE(this Stream stream, uint value)
			=> stream.Write(stackalloc byte[] { (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
		public static void WriteUInt64BE(this Stream stream, ulong value)
			=> stream.Write(stackalloc byte[] { (byte)(value >> 56), (byte)(value >> 48), (byte)(value >> 40), (byte)(value >> 32), (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });

		public static ushort ReadUInt16BE(this Stream stream) => (ushort)stream.ReadInt16BE();
		public static uint ReadUInt32BE(this Stream stream) => (uint)stream.ReadInt32BE();
		public static ulong ReadUInt64BE(this Stream stream) => (ulong)stream.ReadInt64BE();

		public static short ReadInt16BE(this Stream stream)
		{
			Span<byte> word = stackalloc byte[2];
			stream.Read(word, word.Length);

			return (short)(word[0] << 8 | word[1]);
		}
		public static int ReadInt32BE(this Stream stream)
		{
			Span<byte> dword = stackalloc byte[4];
			stream.Read(dword, dword.Length);

			return dword[0] << 24 | dword[1] << 16 | dword[2] << 8 | dword[3];
		}
		public static long ReadInt64BE(this Stream stream)
		{
			Span<byte> qword = stackalloc byte[8];
			stream.Read(qword, qword.Length);

			return (long)(qword[0] << 24 | qword[1] << 16 | qword[2] << 8 | qword[3]) << 32 | (long)(qword[4] << 24 | qword[5] << 16 | qword[6] << 8 | qword[7]);
		}
		public static string ReadType(this Stream stream)
		{
			Span<byte> dword = stackalloc byte[4];
			stream.Read(dword, dword.Length);

			return new string(stackalloc char[] { (char)dword[0], (char)dword[1], (char)dword[2], (char)dword[3] });
		}

		public static byte[] ReadBlock(this Stream stream, int length)
		{
			if (length < 0)
				throw new ArgumentException("Length must be non-negative", nameof(length));

			if (length == 0)
				return Array.Empty<byte>();

			byte[] buffer = new byte[length];
			stream.Read(buffer, length);

			return buffer;
		}

		private static int Read(this Stream stream, Span<byte> buffer, int size)
		{
			if (buffer.Length < size)
				throw new ArgumentException($"{nameof(buffer)} must be a minimum of {size} bytes");

			if (stream.Read(buffer[..size]) != size)
				throw new EndOfStreamException($"Stream ended before all {size} bytes could be read");

			return size;
		}
	}
}
