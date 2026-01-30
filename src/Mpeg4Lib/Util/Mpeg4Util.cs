using Mpeg4Lib.Boxes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Mpeg4Lib.Util
{
	public static class Mpeg4Util
	{
		public static async Task<byte[]> HashBoxAsync(this IBox mpeg, CancellationToken cancellationToken = default)
		{
			using var sha1 = SHA1.Create();
			return await mpeg.HashBoxAsync(sha1, cancellationToken);
		}

		public static async Task<byte[]> HashBoxAsync(this IBox mpeg, HashAlgorithm hashAlgorithm, CancellationToken cancellationToken = default)
		{
			using MemoryStream outputStream = new MemoryStream();
			using (CryptoStream cryptoStream = new CryptoStream(outputStream, hashAlgorithm, CryptoStreamMode.Write))
			{
				var trackedWrite = new TrackedWriteStream(cryptoStream, mpeg.Header.FilePosition);
				// Copy the input stream to the CryptoStream. The data is hashed as it is written.
				mpeg.Save(trackedWrite);
			}
			return hashAlgorithm.Hash!;
		}

		public static async Task<byte[]> HashBoxAsync(this MdatBox mdat, Stream inputStream, CancellationToken cancellationToken = default)
		{
			using var sha1 = SHA1.Create();
			return await mdat.HashBoxAsync(inputStream, sha1, cancellationToken);
		}

		public static async Task<byte[]> HashBoxAsync(this MdatBox mdat, Stream inputStream, HashAlgorithm hashAlgorithm, CancellationToken cancellationToken = default)
		{
			await inputStream.SeekToOffsetAsync(mdat.Header.FilePosition, cancellationToken);

			long readEndPosition = mdat.Header.FilePosition + mdat.Header.TotalBoxSize;
			byte[] buffer = new byte[512 * 1024];
			int read;
			do
			{
				int toRead = (int)Math.Min(buffer.Length, readEndPosition - inputStream.Position);
				read = await inputStream.ReadAsync(buffer.AsMemory(0, toRead), cancellationToken);
				hashAlgorithm.TransformBlock(buffer, 0, read, null, 0);
			} while (read == buffer.Length);
			_ = hashAlgorithm.TransformFinalBlock(buffer, 0, read);
			return  hashAlgorithm.Hash!;
		}

		public static List<IBox> LoadTopLevelBoxes(Stream file)
		{
			List<IBox> boxes = new();

			while (
				file.Position < file.Length
				&& (
					!boxes.OfType<FtypBox>().Any()
					|| !boxes.OfType<MoovBox>().Any()
					|| !boxes.OfType<MdatBox>().Any()
				)
			)
			{
				var box = BoxFactory.CreateBox(file, null);
				boxes.Add(box);

				//Moov is after Mdat, so we have to seek to get it.
				if (box is MdatBox && !boxes.OfType<MoovBox>().Any())
					file.Position = box.Header.FilePosition + box.Header.TotalBoxSize;
			}
			return boxes;
		}

		public static async Task ShiftDataBlock(Stream file, long start, long length, long shiftVector, ProgressTracker? progressTracker = null, CancellationToken cancellationToken = default)
		{
			const int MoveBufferSize = 8 * 1024 * 1024;
			if (start + shiftVector < 0)
				throw new ArgumentOutOfRangeException(nameof(shiftVector), "Data cannot be shifted to a negative file position.");
			if (!file.CanRead || !file.CanWrite || !file.CanSeek)
				throw new ArgumentException("Stream must support reading, writing, and seeking.", nameof(file));
			if (start > file.Length)
				throw new ArgumentOutOfRangeException(nameof(start), "Start index exceeds the file length.");
			if (start + length > file.Length)
				throw new ArgumentOutOfRangeException(nameof(length), "End of data block is beyond the end of the file.");

			bool backToFront = shiftVector > 0;
			file.Position = backToFront ? start + length : start;
			if (progressTracker is not null)
			{
				progressTracker.TotalSize = length;
				progressTracker.MovedBytes = 0;
			}
			Memory<byte> buffer = new byte[MoveBufferSize];
			int read;
			do
			{
				int toRead = (int)Math.Min(MoveBufferSize, length);
				if (backToFront)
					file.Position -= toRead;

				read = await file.ReadAsync(buffer[..toRead], cancellationToken);
				file.Position += shiftVector - read;
				await file.WriteAsync(buffer[..read], cancellationToken);
				file.Position -= backToFront ? shiftVector + read : shiftVector;


				if (progressTracker is not null)
					progressTracker.MovedBytes += read;
				cancellationToken.ThrowIfCancellationRequested();
				length -= read;
			}
			while (length > 0);
			progressTracker?.ReportProgress(forceReport: true);
		}
	}

	public class ProgressTracker
	{
		public event EventHandler? ProgressUpdated;
		private readonly DateTime StartTime = DateTime.UtcNow;
		private DateTime NextUpdate = default;
		private long movedBytes;
		public long MovedBytes { get => movedBytes; set { movedBytes = value; ReportProgress(); } }
		public TimeSpan TotalDuration { get; set; }
		public long TotalSize { get; set; }
		public double Speed { get; private set; }
		public TimeSpan Position { get; private set; }
		public void ReportProgress(bool forceReport = false)
		{
			if (DateTime.UtcNow > NextUpdate || forceReport)
			{
				Position = TimeSpan.FromSeconds(TotalDuration.TotalSeconds / TotalSize * MovedBytes);
				Speed = Position / (DateTime.UtcNow - StartTime);
				ProgressUpdated?.Invoke(this, EventArgs.Empty);
				NextUpdate = DateTime.UtcNow.AddMilliseconds(200);
			}
		}
	}
}
