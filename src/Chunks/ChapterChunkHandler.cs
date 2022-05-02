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
	}
}
