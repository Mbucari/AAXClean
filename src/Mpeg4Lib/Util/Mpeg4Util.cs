using Mpeg4Lib.Boxes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mpeg4Lib.Util
{
	public static class Mpeg4Util
	{
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
		public static List<IBox> LoadAllBoxes(Stream file)
		{
			List<IBox> boxes = new();

			while (file.Position < file.Length)
			{
				var box = BoxFactory.CreateBox(file, null);
				boxes.Add(box);

				if (box is MdatBox)
				{
					file.Seek(box.Header.TotalBoxSize - box.Header.HeaderSize, SeekOrigin.Current);
				}
			}
			return boxes;
		}

		public static async Task RelocateMoovToBeginningAsync(string mp4FilePath, CancellationToken cancellationToken, Action<TimeSpan, TimeSpan, double> progress)
		{
			List<IBox> boxes;

			using (FileStream fileStream = File.OpenRead(mp4FilePath))
				boxes = LoadTopLevelBoxes(fileStream);

			try
			{
				long ftypeSize = boxes.OfType<FtypBox>().Single().RenderSize;
				var Moov = boxes.OfType<MoovBox>().Single();

				if (Moov.Header.FilePosition == ftypeSize) return;

				long moovSize = Moov.RenderSize;
				double duration = (double)Moov.AudioTrack.Mdia.Mdhd.Duration / Moov.AudioTrack.Mdia.Mdhd.Timescale;
				long mdatSize = boxes.OfType<MdatBox>().Single().Header.TotalBoxSize;
				TimeSpan totalDuration = TimeSpan.FromSeconds(duration);

				double secondsPerMb = duration / mdatSize;
				long movedMB = -moovSize;

				Moov.ShiftChunkOffsets(moovSize);

				byte[] buffer1 = new byte[moovSize];

				using (var ms = new MemoryStream(buffer1))
					Moov.Save(ms);

				byte[] buffer2 = new byte[moovSize];

				using var fileStream = new FileStream(mp4FilePath, FileMode.Open, FileAccess.ReadWrite)
				{
					Position = ftypeSize,
				};

				int read;
				DateTime startTime = DateTime.Now;
				DateTime nextUpdate = startTime;

				do
				{
					if (cancellationToken.IsCancellationRequested) return;

					read = await fileStream.ReadAsync(buffer2, cancellationToken);
					fileStream.Position -= read;
					await fileStream.WriteAsync(buffer1, 0, read, cancellationToken);

					movedMB += read;
					if (DateTime.Now > nextUpdate)
					{
						var position = TimeSpan.FromSeconds(secondsPerMb * movedMB);
						double speed = position / (DateTime.Now - startTime);

						progress(totalDuration, position, speed);

						nextUpdate = DateTime.Now.AddMilliseconds(200);
					}

					(buffer1, buffer2) = (buffer2, buffer1);
				}
				while (read == moovSize);

				var finalPosition = TimeSpan.FromSeconds(secondsPerMb * movedMB);
				progress(totalDuration, finalPosition, finalPosition / (DateTime.Now - startTime));
			}
			finally
			{
				foreach (IBox box in boxes)
					box.Dispose();
			}
		}
	}
}
