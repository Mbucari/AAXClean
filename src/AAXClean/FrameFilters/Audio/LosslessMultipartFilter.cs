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
		public bool Closed { get; private set; }
		public LosslessMultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, FtypBox ftyp, MoovBox moov)
			: base(moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.AscBlob, splitChapters)
		{
			NewFileCallback = newFileCallback;
			Ftyp = ftyp;
			Moov = moov;
		}
		protected override void CloseCurrentWriter()
		{
			if (Closed) return;
			Mp4writer?.Close();
			Mp4writer?.OutputFile.Close();
			Closed = true;
		}


		protected override void WriteFrameToFile(FrameEntry audioFrame, bool newChunk)
		{
			Mp4writer.AddFrame(audioFrame.FrameData.Span, newChunk);
		}

		protected override void CreateNewWriter(NewSplitCallback callback)
		{
			NewFileCallback(callback);

			Mp4writer = new Mp4aWriter(callback.OutputFile, Ftyp, Moov, false);
			Closed = false;
			Mp4writer.RemoveTextTrack();

			if (Mp4writer.Moov.ILst is not null)
			{
				var tags = new AppleTags(Mp4writer.Moov.ILst);
				tags.Tracks = (callback.TrackNumber, callback.TrackCount);
				tags.Title = callback.TrackTitle ?? tags.Title;
			}
		}
		protected override void Dispose(bool disposing)
		{
			CloseCurrentWriter();
			base.Dispose(disposing);
		}
	}
}
