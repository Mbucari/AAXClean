using Mpeg4Lib.Boxes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Util
{
	public static class Mpeg4Util
	{
		public static List<Box> LoadTopLevelBoxes(Stream file)
		{
			List<Box> boxes = new();

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

				if (box is MdatBox && !boxes.OfType<MoovBox>().Any())
					file.Position = box.Header.FilePosition + box.Header.TotalBoxSize;
			}
			return boxes;
		}
		public static void RelocateMoovToBeginning(string mp4FilePath)
		{
			List<Box> boxes;

			using (FileStream fileStream = File.OpenRead(mp4FilePath))
				boxes = LoadTopLevelBoxes(fileStream);

			try
			{
				long ftypeSize = boxes.OfType<FtypBox>().Single().RenderSize;
				var Moov = boxes.OfType<MoovBox>().Single();

				if (Moov.Header.FilePosition == ftypeSize) return;

				long moovSize = Moov.RenderSize;

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

				do
				{
					read = fileStream.Read(buffer2);
					fileStream.Position -= read;
					fileStream.Write(buffer1, 0, read);

					(buffer1, buffer2) = (buffer2, buffer1);
				}
				while (read == moovSize);
			}
			finally
			{
				foreach (Box box in boxes)
					box.Dispose();
			}
		}
	}
}
