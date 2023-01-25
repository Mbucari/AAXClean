using Mpeg4Lib.Boxes;
using System;

namespace AAXClean.FrameFilters.Audio
{
	internal sealed class LosslessMultipartFilter : MultipartFilterBase<FrameEntry, NewSplitCallback>
	{
		public bool CurrentWriterOpen { get; private set; }
		protected override int InputBufferSize => 1000;

		private Mp4aWriter Mp4writer;
		private readonly FtypBox ftyp;
		private readonly MoovBox moov;
		private readonly Action<NewSplitCallback> newFileCallback;

		public LosslessMultipartFilter(ChapterInfo splitChapters, FtypBox ftyp, MoovBox moov, Action<NewSplitCallback> newFileCallback)
			: base(splitChapters, (SampleRate)moov.AudioTrack.Mdia.Mdhd.Timescale, moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.ChannelCount == 2)
		{
			this.ftyp = ftyp;
			this.moov = moov;
			this.newFileCallback = newFileCallback;
		}

		protected override void CloseCurrentWriter()
		{
			if (!CurrentWriterOpen) return;

			Mp4writer?.Close();
			Mp4writer?.OutputFile.Close();
			CurrentWriterOpen = false;
		}

		protected override void WriteFrameToFile(FrameEntry audioFrame, bool newChunk)
		{
			Mp4writer.AddFrame(audioFrame.FrameData.Span, newChunk);
		}

		protected override void CreateNewWriter(NewSplitCallback callback)
		{
			newFileCallback(callback);
			CurrentWriterOpen = true;

			Mp4writer = new Mp4aWriter(callback.OutputFile, ftyp, moov);
			Mp4writer.RemoveTextTrack();

			if (Mp4writer.Moov.ILst is not null)
			{
				var tags = new AppleTags(Mp4writer.Moov.ILst);
				if (callback.TrackNumber.HasValue && callback.TrackCount.HasValue)
					tags.Tracks = (callback.TrackNumber.Value, callback.TrackCount.Value);
				tags.Title = callback.TrackTitle ?? tags.Title;
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				CloseCurrentWriter();
			base.Dispose(disposing);
		}
	}
}
