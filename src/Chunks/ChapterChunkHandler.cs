using AAXClean.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAXClean.Chunks
{
	internal sealed class ChapterChunkHandler : IChunkHandler
	{
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		public TrakBox Track { get; }
		private int LastFrameProcessed;
		private readonly double TimeScale;
		private readonly IReadOnlyList<SttsBox.SampleEntry> Samples;
		private readonly ChapterBuilder Builder;
		private bool disposed = false;

		public ChapterChunkHandler(TrakBox trak)
		{
			Track = trak;
			TimeScale = Track.Mdia.Mdhd.Timescale;
			Samples = Track.Mdia.Minf.Stbl.Stts.Samples;
			Builder = new ChapterBuilder(TimeScale);
		}

		public bool ChunkAvailable(ChunkEntry chunkEntry, Span<byte> chunkData)
		{
			if (chunkEntry.ChunkIndex < 0 || chunkEntry.ChunkIndex >= Samples.Count)
				return false;

			for (int start = 0, i = 0; i < chunkEntry.FrameSizes.Length; start += chunkEntry.FrameSizes[i], i++, LastFrameProcessed++)
			{
				Span<byte> chunki = chunkData.Slice(start, chunkEntry.FrameSizes[i]);
				int size = chunki[1] | chunki[0];

				string title = Encoding.UTF8.GetString(chunki.Slice(2, size));

				Builder.AddChapter(title, (int)Samples[LastFrameProcessed].FrameDelta, chunkEntry.ChunkIndex);
			}

			return true;
		}

		public void Dispose() => Dispose(true);
		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					Builder.Dispose();
				}

				disposed = true;
			}
		}

		private class ChapterBuilder : IDisposable
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
}
