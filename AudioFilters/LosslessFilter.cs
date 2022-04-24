using System;
using System.IO;
using System.Linq;

namespace AAXClean.AudioFilters
{
	public class LosslessFilter : AudioFilter
	{
		private readonly Mp4aWriter Mp4writer;
		public LosslessFilter(Stream outputStream, Mp4File mp4Audio)
		{
			var audioSize = mp4Audio.Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s);
			Mp4writer = new Mp4aWriter(outputStream, mp4Audio.Ftyp, mp4Audio.Moov, audioSize > uint.MaxValue);
		}

		private long lastChunk = -1;
		public override bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioSample)
		{
			Mp4writer.AddFrame(audioSample, chunkIndex > lastChunk);
			lastChunk = chunkIndex;
			return true;
		}

		internal override void EndOfAudio()
		{
			if (Chapters != null)
				Mp4writer.WriteChapters(Chapters);
		}

		public override void Close()
		{
			Mp4writer.Close();
		}

		protected override void Dispose(bool disposing)
		{
			Mp4writer?.Dispose();
			base.Dispose(disposing);
		}
	}
}
