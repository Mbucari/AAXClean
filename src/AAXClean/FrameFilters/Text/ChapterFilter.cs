using System;
using System.Text;

namespace AAXClean.FrameFilters.Text
{
	public class ChapterFilter : FrameFinalBase<FrameEntry>
	{
		protected override void Flush() { }
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		private readonly ChapterBuilder Builder;
		public ChapterFilter(uint timeScale)
		{
			Builder = new ChapterBuilder(timeScale);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				Builder.Dispose();
			base.Dispose(disposing);
		}

		protected override void PerformFiltering(FrameEntry input)
		{
			Span<byte> frameData = input.FrameData.Span;
			int size = frameData[1] | frameData[0];

			string title = Encoding.UTF8.GetString(frameData.Slice(2, size));

			Builder.AddChapter(input.Chunk.ChunkIndex, title, (int)input.SamplesInFrame);
		}
	}
}
