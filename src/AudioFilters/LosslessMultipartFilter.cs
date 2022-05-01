using AAXClean.Boxes;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
	internal sealed class LosslessMultipartFilter : MultipartFilterBase
	{
		protected override Action<NewSplitCallback> NewFileCallback { get; }

		private readonly FtypBox Ftyp;
		private readonly MoovBox Moov;
		private Mp4aWriter Writer;
		private Task EncoderLoopTask;
		private BlockingCollection<(bool newFrame, byte[] audioFrame)> WaveFrameQueue;

		public LosslessMultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, FtypBox ftyp, MoovBox moov)
			: base(moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.AscBlob, splitChapters)
		{
			NewFileCallback = newFileCallback;
			Ftyp = ftyp;
			Moov = moov;
		}

		private void EncoderLoop()
		{
			while (WaveFrameQueue.TryTake(out (bool newFrame, byte[] audioFrame) aacFrame, -1))
			{
				Writer.AddFrame(aacFrame.audioFrame, aacFrame.newFrame);
			}

			Writer.Close();
			Writer.OutputFile.Close();
		}

		protected override void CloseCurrentWriter()
		{
			WaveFrameQueue?.CompleteAdding();
			EncoderLoopTask?.Wait();
		}
		protected override void WriteFrameToFile(Span<byte> audioFrame, bool newChunk) => WaveFrameQueue.Add((newChunk, audioFrame.ToArray()));
		protected override void CreateNewWriter(NewSplitCallback callback)
		{
			NewFileCallback(callback);

			Writer = new Mp4aWriter(callback.OutputFile, Ftyp, Moov, false);
			Writer.RemoveTextTrack();

			WaveFrameQueue = new BlockingCollection<(bool newFrame, byte[] audioFrame)>();
			EncoderLoopTask = new Task(EncoderLoop);
			EncoderLoopTask.Start();
		}

		protected override void Dispose(bool disposing)
		{
			CloseCurrentWriter();
			base.Dispose(disposing);
		}
	}
}
