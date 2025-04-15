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
	internal static class MiscExt
	{
		public static bool Between<T>(this T value, T lower, T upper) where T : IComparable<T>
		{
			return value.CompareTo(lower) >= 0 && value.CompareTo(upper) <= 0;
		}

		public static bool ChunkHasFrameInRange(this ChunkEntry value, uint lower, uint upper)
		{
			return value.FirstFrameIndex + value.FrameSizes.Length >= lower && value.FirstFrameIndex <= upper;
		}
	}

	public interface IChunkReader
	{
        Task RunAsync(CancellationTokenSource cancellationSource);
        Action<ConversionProgressEventArgs> OnProggressUpdateDelegate { get; set; }
		void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter);
    }

	internal class ChunkReader : IChunkReader
    {
		private const uint ACC_SAMPLES_PER_FRAME = 1024;
		private readonly List<TrakBox> Tracks = new();
		private readonly List<FrameFilterBase<FrameEntry>> FirstFilters = new();
		private readonly Stream InputStream;
		private uint[] LastFrameProcessed;
		private uint[] TimeScales;
		private SttsBox[] Stts;
		private uint[] StartFrames;
		private uint[] EndFrames;
        public Action<ConversionProgressEventArgs> OnProggressUpdateDelegate { get; set; }
		private TimeSpan StartTime { get; }
        private TimeSpan EndTime { get; }
		internal ChunkReader(Stream inputStream, TimeSpan startTime, TimeSpan endTime)
		{
			InputStream = inputStream;
			StartTime = startTime;
			EndTime = endTime;
		}

		public void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter)
		{
			Tracks.Add(trak);
			FirstFilters.Add(filter);
		}

		private void Initialize()
		{
			LastFrameProcessed = new uint[Tracks.Count];
			Stts = Tracks.Select(t => t.Mdia.Minf.Stbl.Stts).ToArray();
			TimeScales = Tracks.Select(t => t.Mdia.Mdhd.Timescale).ToArray();

			StartFrames = TimeScales.Select(ts => (uint)Math.Round(StartTime.TotalSeconds / ACC_SAMPLES_PER_FRAME * ts)).ToArray();
			EndFrames = TimeScales.Select(ts => (uint)Math.Round(Math.Min(EndTime.TotalSeconds / ACC_SAMPLES_PER_FRAME * ts, uint.MaxValue))).ToArray();
		}

        public async Task RunAsync(CancellationTokenSource cancellationSource)
		{
			Initialize();

			//All filters share the came cancellation source.
			foreach (var filter in FirstFilters)
				filter.SetCancellationToken(cancellationSource.Token);

			beginProcess = DateTime.Now;
			nextUpdate = beginProcess;

			try
			{
				var chunkEnumerable = new MpegChunkEnumerable(Tracks.ToArray());

				foreach (TrackChunk c in chunkEnumerable.Where(c => c.Entry.ChunkHasFrameInRange(StartFrames[c.TrackNum], EndFrames[c.TrackNum])))
				{
					if (cancellationSource.IsCancellationRequested)
						break;

					Memory<byte> chunkdata = new byte[c.Entry.ChunkSize];
					InputStream.ReadNextChunk(c.Entry.ChunkOffset, chunkdata.Span);

					await DispatchChunk(c.TrackNum, c.Entry, chunkdata);
				}
			}
			catch (OperationCanceledException) { }
			catch
			{
				cancellationSource.Cancel();
				throw;
			}
			finally
			{
				OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, EndTime, (EndTime - StartTime) / (DateTime.Now - beginProcess)));

				//Always call CompleteAsync() on all filters so that every
				//FilterLoop gets awaited and any exceptions are thrown.
				await Task.WhenAll(FirstFilters.Select(f => f.CompleteAsync()));
			}
		}

		private DateTime beginProcess;
		private DateTime nextUpdate;

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

				//Throttle update so it doesn't bog down UI
				if (DateTime.Now > nextUpdate)
				{
					TimeSpan position = ProcessPosition(trackIndex);
					double speed = (position - StartTime) / (DateTime.Now - beginProcess);
					OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, position, speed));

					nextUpdate = DateTime.Now.AddMilliseconds(100);
				}

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
