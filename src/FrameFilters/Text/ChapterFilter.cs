using AAXClean.Chunks;
using System;
using System.Text;

namespace AAXClean.FrameFilters.Text
{
	internal class ChapterFilter : IFrameFilter
	{
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		private readonly ChapterBuilder Builder;
		public ChapterFilter(uint timeScale)
		{
			Builder = new ChapterBuilder(timeScale);
		}
		public void Dispose()
		{
			Builder.Dispose();
		}

		public bool FilterFrame(ChunkEntry cEntry, uint frameIndex, uint frameDelta, Span<byte> frameData)
		{
			int size = frameData[1] | frameData[0];

			string title = Encoding.UTF8.GetString(frameData.Slice(2, size));

			Builder.AddChapter(cEntry.ChunkIndex, title, (int)frameDelta);

			return true;
		}
	}
}
