using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Audio
{
	internal class LosslessFilter : FrameFinalBase<FrameEntry>
	{
		public bool Closed { get; private set; }
		public ChapterInfo Chapters => getChapterDelegate?.Invoke();
		protected override int InputBufferSize => 1000;

		private long lastChunkIndex = -1;
		private Func<ChapterInfo> getChapterDelegate;
		private readonly Mp4aWriter mp4writer;

		public LosslessFilter(Stream outputStream, Mp4File mp4Audio)
		{
			long audioSize = mp4Audio.Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s);
			mp4writer = new Mp4aWriter(outputStream, mp4Audio.Ftyp, mp4Audio.Moov);
		}

		public void SetChapterDelegate(Func<ChapterInfo> getChapterDelegate)
		{
			this.getChapterDelegate = getChapterDelegate;
		}

		protected override Task FlushAsync()
		{
			CloseWriter();
			return Task.CompletedTask;
		}

		protected override Task PerformFilteringAsync(FrameEntry input)
		{
			mp4writer.AddFrame(input.FrameData.Span, input.Chunk.ChunkIndex > lastChunkIndex);
			lastChunkIndex = input.Chunk.ChunkIndex;
			return Task.CompletedTask;
		}

		private void CloseWriter()
		{
			if (Closed) return;
			ChapterInfo chinf = Chapters;
			if (chinf is not null)
			{
				mp4writer.WriteChapters(chinf);
			}
			mp4writer.Close();
			mp4writer.OutputFile.Close();
			Closed = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
			{
				CloseWriter();
				mp4writer?.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
