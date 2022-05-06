using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Audio
{
	internal class LosslessFilter : FrameFinalBase<FrameEntry>
	{
		private readonly Mp4aWriter Mp4writer;
		private long LastChunkIndex = -1;
		private Func<ChapterInfo> GetChapterDelegate;

		public ChapterInfo Chapters => GetChapterDelegate?.Invoke();

		public bool Closed { get; private set; }

		public LosslessFilter(Stream outputStream, Mp4File mp4Audio)
		{
			long audioSize = mp4Audio.Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s);
			Mp4writer = new Mp4aWriter(outputStream, mp4Audio.Ftyp, mp4Audio.Moov, audioSize > uint.MaxValue);
		}
		public void SetChapterDelegate(Func<ChapterInfo> getChapterDelegate)
		{
			GetChapterDelegate = getChapterDelegate;
		}
		protected override void PerformFiltering(FrameEntry input)
		{
			Mp4writer.AddFrame(input.FrameData.Span, input.Chunk.ChunkIndex > LastChunkIndex);
			LastChunkIndex = input.Chunk.ChunkIndex;
		}
		public override async Task CompleteAsync()
		{
			await base.CompleteAsync();
			CloseWriter();
		}
		private void CloseWriter()
		{
			if (Closed) return;
			ChapterInfo chinf = Chapters;
			if (chinf is not null)
			{
				Mp4writer.WriteChapters(chinf);
			}
			Mp4writer.Close();
			Closed = true;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				CloseWriter();
				Mp4writer?.Dispose();
			}
		}
	}
}
