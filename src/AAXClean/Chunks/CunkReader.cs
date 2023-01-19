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
		private const uint ACC_SAMPLES_PER_FRAME = 1024;
		private readonly List<TrakBox> Tracks = new();
		private readonly List<FrameFilterBase<FrameEntry>> FirstFilters = new();
		private readonly Stream InputStream;
		private uint[] LastFrameProcessed;
		private uint[] StartFrames;
		private uint[] EndFrames;
		private uint[] TimeScales;
		private SttsBox[] Stts;
		internal Action<ConversionProgressEventArgs> OnProggressUpdateDelegate { get; set; }
		internal TimeSpan TotalDuration { get; set; }
		internal CunkReader(Stream inputStream)
		{
			InputStream = inputStream;
		}

		internal void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter)
		{
			Tracks.Add(trak);
			FirstFilters.Add(filter);
		}

		public void Initialize(TimeSpan startTime, TimeSpan endTime)
		{
			LastFrameProcessed = new uint[Tracks.Count];
			Stts = Tracks.Select(t => t.Mdia.Minf.Stbl.Stts).ToArray();
			TimeScales = Tracks.Select(t => t.Mdia.Mdhd.Timescale).ToArray();

			StartFrames = TimeScales.Select(ts => (uint)(startTime.TotalSeconds / ACC_SAMPLES_PER_FRAME * ts)).ToArray();
			EndFrames = TimeScales.Select(ts => (uint)Math.Min(endTime.TotalSeconds / ACC_SAMPLES_PER_FRAME * ts, uint.MaxValue)).ToArray();
		}

		internal async Task RunAsync(CancellationTokenSource cancellationSource)
		{
			DateTime beginProcess = DateTime.Now;
			DateTime nextUpdate = beginProcess;

			//All filters share the came cancellation source.
			foreach (var filter in FirstFilters)
				filter.SetCancellationSource(cancellationSource);

			var chunkEnumerable = new MpegChunkEnumerable(Tracks.ToArray());

			try
			{
				foreach (TrackChunk c in chunkEnumerable.Where(
						c => c.Entry.FirstFrameIndex + c.Entry.FrameSizes.Length >= StartFrames[c.TrackNum]
						&& c.Entry.FirstFrameIndex <= EndFrames[c.TrackNum]))
				{
					if (cancellationSource.IsCancellationRequested)
						break;

					Memory<byte> chunkdata = new byte[c.Entry.ChunkSize];
					InputStream.ReadNextChunk(c.Entry.ChunkOffset, chunkdata.Span);

					await DispatchChunk(c.TrackNum, c.Entry, chunkdata);

					//Throttle update so it doesn't bog down UI
					if (DateTime.Now > nextUpdate)
					{
						TimeSpan position = ProcessPosition(c.TrackNum);
						double speed = position / (DateTime.Now - beginProcess);
						OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs { TotalDuration = TotalDuration, ProcessPosition = position, ProcessSpeed = speed });

						nextUpdate = DateTime.Now.AddMilliseconds(300);
					}
				}
			}
			catch
			{
				cancellationSource.Cancel();
				throw;
			}
			finally
			{
				OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs { TotalDuration = TotalDuration, ProcessPosition = TotalDuration, ProcessSpeed = TotalDuration / (DateTime.Now - beginProcess) });

				//Always call CompleteAsync() on all filters so that every
				//FilterLoop gets awaited and any exceptions are thrown.
				await Task.WhenAll(FirstFilters.Select(f => f.CompleteAsync()));
			}
		}

		private TimeSpan ProcessPosition(int trackNum)
			=> Stts[trackNum].FrameToTime(TimeScales[trackNum], LastFrameProcessed[trackNum]);

		private async Task DispatchChunk(int trackIndex, ChunkEntry chunk, Memory<byte> chunkdata)
		{
			for (int start = 0, f = 0; f < chunk.FrameSizes.Length; start += chunk.FrameSizes[f], f++)
			{
				uint frameIndex = chunk.FirstFrameIndex + (uint)f;
								
				LastFrameProcessed[trackIndex] = frameIndex;

				if (!frameIndex.Between(StartFrames[trackIndex], EndFrames[trackIndex]))
					continue;

				var frameDelta = Stts[trackIndex].FrameToFrameDelta(frameIndex);

				await FirstFilters[trackIndex].AddInputAsync(new FrameEntry
				{
					Chunk = chunk,
					FrameIndex = frameIndex,
					SamplesInFrame = frameDelta,
					FrameData = chunkdata.Slice(start, chunk.FrameSizes[f])
				});
			}
		}
	}
}
