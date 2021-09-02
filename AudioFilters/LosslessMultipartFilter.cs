using AAXClean.Boxes;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
	internal sealed class LosslessMultipartFilter : MultipartFilter
	{
		protected override Action<NewSplitCallback> NewFileCallback { get; }
		private FtypBox Ftyp { get; }
		private MoovBox Moov { get; }

		private Mp4aWriter writer;

		private BlockingCollection<(bool newFrame, byte[] audioFrame)> waveFrameQueue;
		private Task encoderLoopTask;

		public LosslessMultipartFilter(ChapterInfo splitChapters, Action<NewSplitCallback> newFileCallback, FtypBox ftyp, MoovBox moov)
			: base(moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob, splitChapters)
		{
			NewFileCallback = newFileCallback;
			Ftyp = ftyp;
			Moov = moov;
		}

		private void EncoderLoop()
		{
			while (waveFrameQueue.TryTake(out (bool newFrame, byte[] audioFrame) aacFrame, -1))
			{
				writer.AddFrame(aacFrame.audioFrame, aacFrame.newFrame);
			}
			writer.Close();
		}

		protected override void CloseCurrentWriter()
		{
			waveFrameQueue?.CompleteAdding();
			encoderLoopTask?.Wait();
		}
		protected override void WriteFrameToFile(Span<byte> audioFrame, bool newChunk) => waveFrameQueue.Add((newChunk, audioFrame.ToArray()));
		protected override void CreateNewWriter(NewSplitCallback callback)
		{
			NewFileCallback(callback);

			writer = new Mp4aWriter(callback.OutputFile, Ftyp, Moov, false);
			writer.RemoveTextTrack();

			waveFrameQueue = new BlockingCollection<(bool newFrame, byte[] audioFrame)>();
			encoderLoopTask = new Task(EncoderLoop);
			encoderLoopTask.Start();
		}

		protected override void Dispose(bool disposing)
		{
			CloseCurrentWriter();
			Ftyp.Dispose();
			Moov.Dispose();
			base.Dispose(disposing);
		}
	}
}
