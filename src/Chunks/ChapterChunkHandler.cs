using AAXClean.Boxes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAXClean.Chunks
{
	internal sealed class ChapterChunkHandler : ChunkHandlerBase
	{
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		private readonly ChapterBuilder Builder;

		public ChapterChunkHandler(TrakBox trak) : base(trak)
		{
			Builder = new ChapterBuilder(TimeScale);
		}

		public override bool HandleChunk(ChunkEntry chunkEntry, Span<byte> chunkData)
		{
			if (chunkEntry.ChunkIndex < 0 || chunkEntry.ChunkIndex >= Samples.Count)
				return false;

			for (int start = 0, i = 0; i < chunkEntry.FrameSizes.Length; start += chunkEntry.FrameSizes[i], i++, LastFrameProcessed++)
			{
				Span<byte> chunki = chunkData.Slice(start, chunkEntry.FrameSizes[i]);
				int size = chunki[1] | chunki[0];

				string title = Encoding.UTF8.GetString(chunki.Slice(2, size));

				Builder.AddChapter(title, (int)Samples[(int)LastFrameProcessed].FrameDelta, chunkEntry.ChunkIndex);
			}

			return true;
		}

		protected override void Dispose(bool disposing)
		{
			if (!Disposed && disposing)
			{
				Builder.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}
