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
		public static async Task<byte[]> HashBoxAsync(this IBox mpeg, Stream inputStream, CancellationToken cancellationToken = default)
		{
			using var sha1 = SHA1.Create();
			return await mpeg.HashBoxAsync(inputStream, sha1, cancellationToken);
		}

		public static async Task<byte[]> HashBoxAsync(this IBox mpeg, Stream inputStream, HashAlgorithm hashAlgorithm, CancellationToken cancellationToken = default)
		{
			await inputStream.SeekToOffsetAsync(mpeg.Header.FilePosition, cancellationToken);

			long readEndPosition = mpeg.Header.FilePosition + mpeg.Header.TotalBoxSize;
			byte[] buffer = new byte[512 * 1024];
			int read;
			do
			{
				int toRead = (int)Math.Min(buffer.Length, readEndPosition - inputStream.Position);
				read = await inputStream.ReadAsync(buffer, 0, toRead, cancellationToken);
				hashAlgorithm.TransformBlock(buffer, 0, read, null, 0);
			} while (read == buffer.Length);
			return hashAlgorithm.TransformFinalBlock(buffer, 0, read);
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

		public static async Task RelocateMoovToBeginningAsync(string mp4FilePath, CancellationToken cancellationToken, ProgressTracker progressTracker)
		{
			const int MoveBufferSize = 8 * 1024 * 1024;
			List<IBox> boxes;

			using (FileStream fileStream = File.OpenRead(mp4FilePath))
				boxes = LoadTopLevelBoxes(fileStream);

			try
			{
				long mdatSize, moovSize = 0, ftypeSize = boxes.OfType<FtypBox>().Single().RenderSize;
				MoovBox moov = boxes.OfType<MoovBox>().Single();

				progressTracker.TotalDuration = TimeSpan.FromSeconds(moov.Mvhd.Duration / (double)moov.Mvhd.Timescale);
				if (moov.Header.FilePosition == ftypeSize)
				{
					progressTracker.MovedBytes = progressTracker.TotalSize = 1;
					return;
				}

				progressTracker.TotalSize = mdatSize = boxes.OfType<MdatBox>().Single().Header.TotalBoxSize;
				do
				{
					//The moov size may change as we shift chunk offsets, so we have to loop until it stabilizes.
					long toShift = moov.RenderSize - moovSize;
					moovSize = moov.RenderSize;
					moov.ShiftChunkOffsets(toShift);
				}
				while (moov.RenderSize != moovSize);

				using FileStream mpegFile = new(mp4FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite) { Position = ftypeSize + mdatSize };

				int read;
				Memory<byte> buffer = new byte[MoveBufferSize];

				do
				{
					int toRead = (int)Math.Min(MoveBufferSize, mpegFile.Position - ftypeSize);

					mpegFile.Position -= toRead;
					read = await mpegFile.ReadAsync(buffer[..toRead], cancellationToken);
					mpegFile.Position -= read - moovSize;
					await mpegFile.WriteAsync(buffer[..read], cancellationToken);
					mpegFile.Position -= read + moovSize;

					progressTracker.MovedBytes += read;
					cancellationToken.ThrowIfCancellationRequested();
				}
				while (read == MoveBufferSize);

				moov.Save(mpegFile);
				progressTracker.ReportProgress(forceReport: true);
			}
			finally
			{
				foreach (IBox box in boxes)
					box.Dispose();
			}
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
