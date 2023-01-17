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
		private readonly List<IFrameFilter<FrameEntry>> FirstFilters = new();
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

		public void AddTrack(TrakBox trak, IFrameFilter<FrameEntry> filter)
		{
			Tracks.Add(trak);
			FirstFilters.Add(filter);
		}

		public async Task RunAsync(CancellationTokenSource cancellationSource, bool doTimeFilter, TimeSpan startTime, TimeSpan endTime)
		{
			Initialize();

			DateTime beginProcess = DateTime.Now;
			DateTime nextUpdate = beginProcess;

			foreach (var filter in FirstFilters)
				filter.SetCancellationSource(cancellationSource);

			try
			{
				foreach (TrackChunk c in new MpegChunkEnumerable(Tracks.ToArray()))
				{
					if (cancellationSource.IsCancellationRequested)
						break;

					Memory<byte> chunkdata = new byte[c.Entry.ChunkSize];
					InputStream.ReadNextChunk(c.Entry.ChunkOffset, chunkdata.Span);

					await DispatchChunk(doTimeFilter, startTime, endTime, c.TrackNum, c.Entry, chunkdata);

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
			finally
			{
				OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs { TotalDuration = TotalDuration, ProcessPosition = TotalDuration, ProcessSpeed = TotalDuration / (DateTime.Now - beginProcess) });

				await Task.WhenAll(FirstFilters.Select(f => cancellationSource.IsCancellationRequested ? f.CancelAsync() : f.CompleteAsync()));
			}
		}

		private void Initialize()
		{
			LastFrameProcessed = new uint[Tracks.Count];
			Stts = Tracks.Select(t => t.Mdia.Minf.Stbl.Stts).ToArray();
			TimeScales = Tracks.Select(t => t.Mdia.Mdhd.Timescale).ToArray();
		}

		private TimeSpan ProcessPosition(int trackNum) => Stts[trackNum].FrameToTime(TimeScales[trackNum], LastFrameProcessed[trackNum]);
		private async Task DispatchChunk(bool doTimeFilter, TimeSpan startTime, TimeSpan endTime, int trackIndex, ChunkEntry chunk, Memory<byte> chunkdata)
		{
			for (int start = 0, f = 0; f < chunk.FrameSizes.Length; start += chunk.FrameSizes[f], f++, LastFrameProcessed[trackIndex]++)
			{
				uint frameDelta = Stts[trackIndex].FrameToFrameDelta(LastFrameProcessed[trackIndex]);

				if (doTimeFilter)
				{
					var time = ProcessPosition(trackIndex);

					if (time < startTime ||
						time > endTime)
						continue;
				}

				await FirstFilters[trackIndex].AddInputAsync(new FrameEntry
				{
					Chunk = chunk,
					FrameIndex = LastFrameProcessed[trackIndex],
					SamplesInFrame = frameDelta,
					FrameData = chunkdata.Slice(start, chunk.FrameSizes[f])
				});
			}
		}
	}
}
