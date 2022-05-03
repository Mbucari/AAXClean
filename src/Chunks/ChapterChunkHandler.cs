using AAXClean.Boxes;
using AAXClean.FrameFilters;
using System;
using System.Text;

namespace AAXClean.Chunks
{
	internal sealed class ChapterChunkHandler : ChunkHandlerBase
	{
		public ChapterInfo Chapters => Builder.ToChapterInfo();
		private readonly ChapterBuilder Builder;

		public ChapterChunkHandler(TrakBox trak, IFrameFilter frameFilter = null) : base(trak)
		{
			FrameFilter = frameFilter;
			Builder = new ChapterBuilder(TimeScale);
		}
		
		public override bool HandleFrame(ChunkEntry cEntry, uint frameIndex, Span<byte> frameData)
		{
			int size = frameData[1] | frameData[0];

			string title = Encoding.UTF8.GetString(frameData.Slice(2, size));

			Builder.AddChapter(cEntry.ChunkIndex, title, (int)Stts.Samples[(int)frameIndex].FrameDelta);

			return FrameFilter?.FilterFrame(cEntry, frameIndex, frameData) ?? true;
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
