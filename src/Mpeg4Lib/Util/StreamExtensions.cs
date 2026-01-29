using Mpeg4Lib.Boxes;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
		/// Seeks to a position in the the input stream
		/// </summary>
		/// <param name="chunkOffset">The chunk's file offset</param>
		public static async Task SeekToOffsetAsync(this Stream inputStream, long chunkOffset, CancellationToken token)
		{
			if (inputStream.Position == chunkOffset)
				return;
			else if(inputStream.CanSeek)
				inputStream.Position = chunkOffset;
			else if (inputStream.Position < chunkOffset)
			{
				//Unknown Track or data type. Read past it to next known chunk.
				const int bufferSize = 8 * 1024;
				int toRead = int.Min(bufferSize, (int)(chunkOffset - inputStream.Position));
				using var memoryBuff = MemoryPool<byte>.Shared.Rent(toRead);
				while (toRead > 0)
				{
					await inputStream.ReadExactlyAsync(memoryBuff.Memory[..toRead], token);
					toRead = int.Min(bufferSize, (int)(chunkOffset - inputStream.Position));
				}
			}
			else
				throw new NotSupportedException($"Input stream position 0x{inputStream.Position:X8} is past the chunk offset 0x{chunkOffset:X8} and is not seekable.");
		}

		/// <summary>
		/// Seeks to a position in the the input stream
		/// </summary>
		/// <param name="chunkOffset">The chunk's file offset</param>
		public static void SeekToOffset(this Stream inputStream, long chunkOffset)
		{
			if (inputStream.Position == chunkOffset)
				return;
			else if (inputStream.CanSeek)
				inputStream.Position = chunkOffset;
			else if (inputStream.Position < chunkOffset)
			{
				//Unknown Track or data type. Read past it to next known chunk.
				const int bufferSize = 8 * 1024;
				int toRead = int.Min(bufferSize, (int)(chunkOffset - inputStream.Position));
				using var memoryBuff = MemoryPool<byte>.Shared.Rent(toRead);
				var spanBuff = memoryBuff.Memory.Span;
				while (toRead > 0)
				{
					inputStream.ReadExactly(spanBuff[..toRead]);
					toRead = int.Min(bufferSize, (int)(chunkOffset - inputStream.Position));
				}
			}
			else
				throw new NotSupportedException($"Input stream position 0x{inputStream.Position:X8} is past the chunk offset 0x{chunkOffset:X8} and is not seekable.");
		}

		/// <summary>
		/// Read the next track chunk from the input stream
		/// </summary>
		/// <param name="chunkOffset">The chunk's file offset</param>
		/// <param name="chunkBuffer">Buffer to copy the chink data into</param>
		public static async Task ReadNextChunkAsync(this Stream inputStream, long chunkOffset, Memory<byte> chunkBuffer, CancellationToken token)
		{
			await inputStream.SeekToOffsetAsync(chunkOffset, token);
			if (await inputStream.ReadAsync(chunkBuffer, token) != chunkBuffer.Length)
				throw new EndOfStreamException($"Stream ended at position {inputStream.Position} before all {chunkBuffer.Length} bytes were read.");
		}

		public static void WriteType(this Stream stream, string type)
		{
			if (type?.Length != 4)
				throw new ArgumentException("Type must be 4 chars long.");

			stream.Write([(byte)type[0], (byte)type[1], (byte)type[2], (byte)type[3]]);
		}

		public static void WriteInt16BE(this Stream stream, short value)
		{
			Span<byte> word = stackalloc byte[2];
			BinaryPrimitives.WriteInt16BigEndian(word, value);
			stream.Write(word);
		}
		public static void WriteUInt16BE(this Stream stream, ushort value)
		{
			Span<byte> word = stackalloc byte[2];
			BinaryPrimitives.WriteUInt16BigEndian(word, value);
			stream.Write(word);
		}
		public static void WriteInt32BE(this Stream stream, int value)
		{
			Span<byte> dword = stackalloc byte[4];
			BinaryPrimitives.WriteInt32BigEndian(dword, value);
			stream.Write(dword);
		}
		public static void WriteUInt32BE(this Stream stream, uint value)
		{
			Span<byte> dword = stackalloc byte[4];
			BinaryPrimitives.WriteUInt32BigEndian(dword, value);
			stream.Write(dword);
		}
		public static void WriteInt64BE(this Stream stream, long value)
		{
			Span<byte> qword = stackalloc byte[8];
			BinaryPrimitives.WriteInt64BigEndian(qword, value);
			stream.Write(qword);
		}
		public static void WriteUInt64BE(this Stream stream, ulong value)
		{
			Span<byte> qword = stackalloc byte[8];
			BinaryPrimitives.WriteUInt64BigEndian(qword, value);
			stream.Write(qword);
		}

		public static short ReadInt16BE(this Stream stream)
		{
			Span<byte> word = stackalloc byte[2];
			stream.ReadExactly(word);
			return BinaryPrimitives.ReadInt16BigEndian(word);
		}
		public static ushort ReadUInt16BE(this Stream stream)
		{
			Span<byte> word = stackalloc byte[2];
			stream.ReadExactly(word);
			return BinaryPrimitives.ReadUInt16BigEndian(word);
		}
		public static int ReadInt32BE(this Stream stream)
		{
			Span<byte> dword = stackalloc byte[4];
			stream.ReadExactly(dword);
			return BinaryPrimitives.ReadInt32BigEndian(dword);
		}
		public static uint ReadUInt32BE(this Stream stream)
		{
			Span<byte> dword = stackalloc byte[4];
			stream.ReadExactly(dword);
			return BinaryPrimitives.ReadUInt32BigEndian(dword);
		}
		public static long ReadInt64BE(this Stream stream)
		{
			Span<byte> qword = stackalloc byte[8];
			stream.ReadExactly(qword);
			return BinaryPrimitives.ReadInt64BigEndian(qword);
		}
		public static ulong ReadUInt64BE(this Stream stream)
		{
			Span<byte> qword = stackalloc byte[8];
			stream.ReadExactly(qword);
			return BinaryPrimitives.ReadUInt64BigEndian(qword);
		}

		public static string ReadType(this Stream stream)
		{
			Span<byte> dword = stackalloc byte[4];
			stream.ReadExactly(dword);
			return new string([(char)dword[0], (char)dword[1], (char)dword[2], (char)dword[3]]);
		}

		public static byte[] ReadBlock(this Stream stream, int length)
		{
			if (length < 0)
				throw new ArgumentException("Length must be non-negative", nameof(length));
			else if (length == 0)
				return [];
			else
			{
				byte[] buffer = new byte[length];
				stream.ReadExactly(buffer);
				return buffer;
			}
		}
	}
}
