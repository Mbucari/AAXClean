using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Audio
{
	internal class LosslessFilter : FrameFinalBase<FrameEntry>
	{
		public bool Closed { get; private set; }
		protected override int InputBufferSize => 1000;

		private long lastChunkIndex = -1;
		public readonly Mp4aWriter Mp4aWriter;
		private readonly ChapterQueue ChapterQueue;

		public LosslessFilter(Stream outputStream, Mp4File mp4Audio, ChapterQueue chapterQueue)
		{
			Mp4aWriter = new Mp4aWriter(outputStream, mp4Audio.Ftyp, mp4Audio.Moov);
			ChapterQueue = chapterQueue;
		}

		protected override Task FlushAsync()
		{
			//Write any remaining chapters
			while (ChapterQueue.TryGetNextChapter(out var chapterEntry))
				Mp4aWriter.WriteChapter(chapterEntry);

			CloseWriter();
			return Task.CompletedTask;
		}

		protected override Task PerformFilteringAsync(FrameEntry input)
		{
			bool newChunk = input.Chunk.ChunkIndex > lastChunkIndex;

			//Write chapters as soon as they're available.
			while (ChapterQueue.TryGetNextChapter(out var chapterEntry))
			{
				Mp4aWriter.WriteChapter(chapterEntry);
				newChunk = true;
			}

			Mp4aWriter.AddFrame(input.FrameData.Span, newChunk);
			lastChunkIndex = input.Chunk.ChunkIndex;
			return Task.CompletedTask;
		}

		private void CloseWriter()
		{
			if (Closed) return;
			Mp4aWriter.Close();
			Closed = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
			{
				CloseWriter();
				Mp4aWriter?.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
