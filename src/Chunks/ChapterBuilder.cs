using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Chunks
{
	internal class ChapterBuilder : IDisposable
	{
		public double TimeScale { get; }
		private bool disposed = false;
		private readonly List<(uint chunkIndex, string title, int frameDelta)> Chapters = new();
		public ChapterBuilder(double timeScale)
		{
			TimeScale = timeScale;
		}

		public void AddChapter(string title, int frameDelta, uint chunkIndex)
		{
			Chapters.Add((chunkIndex, title, frameDelta));
		}

		/// <summary>
		/// This method is necessary because some books have mangled chapters (eg. Broken Angels: B002V8H59I)
		/// with chunk offsets out of order and negative frame deltas.
		/// </summary>
		public ChapterInfo ToChapterInfo()
		{
			checked
			{
				List<(uint chunkIndex, string title, int frameDelta)> orderedChapters = Chapters.OrderBy(c => c.chunkIndex).ToList();
				List<(string title, long frameEnd)> list = new();

				long last = 0;

				//Calculate the frame position of the chapter's end.
				foreach ((uint chunkIndex, string title, int frameDelta) in orderedChapters)
				{
					//If the frame delta is negative, assume the duration is 1 frame. This is what ffmpeg does.
					long endTime = last + (frameDelta < 0 ? 1 : frameDelta);
					list.Add((title, endTime));
					last += frameDelta;
				}
				List<(string title, long frameEnd)> sortedEnds = list.OrderBy(c => c.frameEnd).ToList();

				last = 0;
				ChapterInfo cInfo = new ChapterInfo();

				//Create ChapterInfo by calculating each chapter's duration.
				foreach ((string title, long frameEnd) in sortedEnds)
				{
					cInfo.AddChapter(title, TimeSpan.FromSeconds((frameEnd - last) / TimeScale));
					last = frameEnd;
				}

				return cInfo;
			}
		}
		public void Dispose() => Dispose(true);
		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					Chapters.Clear();
				}

				disposed = true;
			}
		}
	}
}
