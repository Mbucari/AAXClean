using System;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Text
{
	public class ChapterFilter : FrameFinalBase<FrameEntry>
	{
		protected override Task FlushAsync() => Task.CompletedTask;
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		private readonly ChapterBuilder Builder;
		public ChapterFilter(uint timeScale)
		{
			Builder = new ChapterBuilder(timeScale);
		}
		protected override int InputBufferSize => 200;

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				Builder.Dispose();
			base.Dispose(disposing);
		}

		protected override Task PerformFilteringAsync(FrameEntry input)
		{
			Span<byte> frameData = input.FrameData.Span;
			int size = frameData[1] | frameData[0];

			string title = Encoding.UTF8.GetString(frameData.Slice(2, size));

			Builder.AddChapter(input.Chunk.ChunkIndex, title, (int)input.SamplesInFrame);
			return Task.CompletedTask;
		}
	}
}
