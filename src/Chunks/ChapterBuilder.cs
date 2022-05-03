using System;
using System.Collections.Generic;
using System.Linq;

namespace AAXClean.Chunks
{
	internal class ChapterBuilder : IDisposable
	{
		public double TimeScale { get; }
		private bool disposed = false;
		private readonly List<(uint chunkIndex, string title, long frameDelta)> Chapters = new();
		public ChapterBuilder(double timeScale)
		{
			TimeScale = timeScale;
		}

		public void AddChapter(uint chunkIndex, string title, int frameDelta)
		{
			Chapters.Add((chunkIndex, title, frameDelta));
		}

		/// <summary>
		/// This method is necessary because some books have mangled chapters (eg. Broken Angels: B002V8H59I)
		/// with chunk offsets out of order and negative frame deltas.
		/// </summary>
		public ChapterInfo ToChapterInfo()
		{
			List<(string title, long chapterEndFrame)> list = GetChapterEnds();

			long last = 0;
			ChapterInfo cInfo = new();

			//Create ChapterInfo by calculating each chapter's duration.
			foreach ((string title, long frameEnd) in list)
			{
				cInfo.AddChapter(title, TimeSpan.FromSeconds((frameEnd - last) / TimeScale));
				last = frameEnd;
			}

			return cInfo;
		}

		private List<(string title, long chapterEndFrame)> GetChapterEnds()
		{
			checked
			{
				Chapters.Sort((c1, c2) => c1.chunkIndex.CompareTo(c2.chunkIndex));
				List<(string title, long chapterEndFrame)> list = new();

				long last = 0;

				//Calculate the frame position of the chapter's end.
				foreach ((uint chunkIndex, string title, long frameDelta) in Chapters)
				{
					//If the frame delta is negative, assume the duration is 1 frame. This is what ffmpeg does.
					long endFrame = last + (frameDelta < 0 ? 1 : frameDelta);
					list.Add((title, endFrame));
					last += frameDelta;
				}

				list.Sort((c1, c2) => c1.chapterEndFrame.CompareTo(c2.chapterEndFrame));

				return list;
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
