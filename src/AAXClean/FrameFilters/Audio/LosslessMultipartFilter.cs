using Mpeg4Lib.Boxes;
using System;

namespace AAXClean.FrameFilters.Audio
{
	internal sealed class LosslessMultipartFilter : MultipartFilterBase<FrameEntry, NewSplitCallback>
	{
		private Action<NewSplitCallback> NewFileCallback { get; }

		private readonly FtypBox Ftyp;
		private readonly MoovBox Moov;
		private Mp4aWriter Mp4writer;
		protected override int InputBufferSize => 1000;
		public bool CurrentWriterOpen { get; private set; }
		public LosslessMultipartFilter(ChapterInfo splitChapters, FtypBox ftyp, MoovBox moov, Action<NewSplitCallback> newFileCallback)
			: base(splitChapters, (SampleRate)moov.AudioTrack.Mdia.Mdhd.Timescale, moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.ChannelCount == 2)
		{
			NewFileCallback = newFileCallback;
			Ftyp = ftyp;
			Moov = moov;
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
			NewFileCallback(callback);
			CurrentWriterOpen = true;

			Mp4writer = new Mp4aWriter(callback.OutputFile, Ftyp, Moov, false);
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
