using AAXClean.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAXClean.Chunks
{
	internal class ChapterChunkHandler : IChunkHandler
	{
		public bool InputStreamSeekable { get; }
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		public double TimeScale { get; }
		public TrakBox Track { get; }

		private SttsBox.SampleEntry[] Samples { get; set; }
		private ChapterBuilder Builder { get; set; }

		public ChapterChunkHandler(uint timeScale, TrakBox trak, bool seekable = false)
		{
			TimeScale = timeScale;
			Track = trak;
			Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
			InputStreamSeekable = seekable;
			Builder = new ChapterBuilder(TimeScale);
		}

		public bool ChunkAvailable(Span<byte> chunk, uint chunkIndex, uint frameIndex, int totalChunkSize, int[] frameSizes)
		{
			if (chunkIndex < 0 || chunkIndex >= Samples.Length || frameSizes.Length != 1)
				return false;

			int size = chunk[1] | chunk[0];

			var title = Encoding.UTF8.GetString(chunk.Slice(2, size));

			Builder.AddChapter(title, (int)Samples[chunkIndex].FrameDelta, chunkIndex);

			return true;
		}

		private bool disposed = false;
		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Dispose managed resources.
					Builder = null;
					Samples = null;
				}

				// Call the appropriate methods to clean up
				// unmanaged resources here.
				disposed = true;
			}
		}

		private class ChapterBuilder
		{
			public double TimeScale { get; }
			private List<(uint chunkIndex, string title, int frameDelta)> Chapters = new();
			public ChapterBuilder(double timeScale)
			{
				TimeScale = timeScale;
			}

			public void AddChapter(string title, int frameDelta, uint index)
			{
				Chapters.Add((index, title, frameDelta));
			}

			/// <summary>
			/// This method is necessary because some books have mangled chapters (eg. Broken Angels: B002V8H59I)
			/// with chunk offsets out of order and negarive frame deltas.
			/// </summary>
			public ChapterInfo ToChapterInfo()
			{
				//Sort chapters by chunk index
				var orderedChapters = Chapters.OrderBy(c => c.chunkIndex).ToList();
				List<(string title, int frameEnd)> list = new();

				var last = 0;

				//Calculate the frame position of the chapter's end.
				foreach (var c in orderedChapters)
				{
					//If the frame delta is negative, assume the duration is 1 frame. This is what ffmpeg does.
					var endTime = last + (c.frameDelta < 0 ? 1 : c.frameDelta);
					list.Add((c.title, endTime));
					last = last + c.frameDelta;
				}
				var sortedEnds = list.OrderBy(c => c.frameEnd).ToList();

				last = 0;
				var cInfo = new ChapterInfo();

				//Create ChapterInfo by calculating each chapter's duration.
				foreach (var c in sortedEnds)
				{
					cInfo.AddChapter(c.title, TimeSpan.FromSeconds((c.frameEnd - last) / TimeScale));
					last = c.frameEnd;
				}

				return cInfo;
			}
		}
	}
}
