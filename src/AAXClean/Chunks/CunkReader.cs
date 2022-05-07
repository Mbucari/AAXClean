using AAXClean.FrameFilters;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Chunks;
using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean.Chunks
{
	internal class CunkReader
	{
		private readonly List<TrakBox> Tracks = new();
		private readonly List<FrameFilterBase<FrameEntry>> FirstFilters = new();
		private readonly Stream InputStream;
		private uint[] LastFrameProcessed;
		private uint[] TimeScales;
		private SttsBox[] Stts;
		public Action<ConversionProgressEventArgs> OnProggressUpdateDelegate { get; set; }
		public TimeSpan TotalDuration { get; set; }
		public CunkReader(Stream inputStream)
		{
			InputStream = inputStream;
		}

		public void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter)
		{
			Tracks.Add(trak);
			FirstFilters.Add(filter);
		}

		public ConversionResult Run(CancellationToken token = default) => RunAsync(token).GetAwaiter().GetResult();

		public async Task<ConversionResult> RunAsync(CancellationToken token = default)
		{
			Initialize();

			DateTime beginProcess = DateTime.Now;
			DateTime nextUpdate = beginProcess;

			foreach (TrackChunk c in new MpegChunkEnumerable(Tracks.ToArray()))
			{
				if (FirstFilters[c.TrackNum].Completion.IsFaulted || 
					FirstFilters[c.TrackNum].Completion.IsCanceled || 
					token.IsCancellationRequested)
					break;

				Memory<byte> chunkdata = new byte[c.Entry.ChunkSize];
				InputStream.ReadNextChunk(c.Entry.ChunkOffset, chunkdata.Span);

				DispatchChunk(c.TrackNum, c.Entry, chunkdata);

				//Throttle update so it doesn't bog down UI
				if (DateTime.Now > nextUpdate)
				{
					TimeSpan position = ProcessPosition(c.TrackNum);
					double speed = position / (DateTime.Now - beginProcess);
					OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs { TotalDuration = TotalDuration, ProcessPosition = position, ProcessSpeed = speed });

					nextUpdate = DateTime.Now.AddMilliseconds(300);
				}
			}

			OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs { TotalDuration = TotalDuration, ProcessPosition = TotalDuration, ProcessSpeed = TotalDuration / (DateTime.Now - beginProcess) });

			return await Finalize(token.IsCancellationRequested);
		}

		private void Initialize()
		{
			LastFrameProcessed = new uint[Tracks.Count];
			Stts = Tracks.Select(t => t.Mdia.Minf.Stbl.Stts).ToArray();
			TimeScales = Tracks.Select(t => t.Mdia.Mdhd.Timescale).ToArray();

			foreach (FrameFilterBase<FrameEntry> f in FirstFilters) f.Start();
		}

		private async Task<ConversionResult> Finalize(bool cancelled)
		{
			await Task.WhenAll(FirstFilters.Where(f => !f.Completion.IsCompleted).Select(f => cancelled ? f.CancelAsync() : f.CompleteAsync()));

			ConversionResult result;
			if (FirstFilters.All(f => f.Completion.IsCompletedSuccessfully))
				result = ConversionResult.NoErrorsDetected;
			else if (FirstFilters.Any(f => f.Completion.IsCanceled))
				result = ConversionResult.Cancelled;
			else result = ConversionResult.Failed;

			return await Task.FromResult(result);
		}

		private TimeSpan ProcessPosition(int trackNum) => Stts[trackNum].FrameToTime(TimeScales[trackNum], LastFrameProcessed[trackNum]);
		private void DispatchChunk(int trackIndex, ChunkEntry chunk, Memory<byte> chunkdata)
		{
			for (int start = 0, f = 0; f < chunk.FrameSizes.Length; start += chunk.FrameSizes[f], f++, LastFrameProcessed[trackIndex]++)
			{
				Memory<byte> frameData = chunkdata.Slice(start, chunk.FrameSizes[f]);

				FirstFilters[trackIndex].AddInput(new FrameEntry
				{
					Chunk = chunk,
					FrameIndex = LastFrameProcessed[trackIndex],
					FrameDelta = Stts[trackIndex].FrameToFrameDelta(LastFrameProcessed[trackIndex]),
					FrameSize = chunk.FrameSizes[f],
					FrameData = frameData
				});
			}
		}
	}
}
