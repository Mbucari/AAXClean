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

#nullable enable
namespace AAXClean.Chunks;

public interface IChunkReader
{
	Task RunAsync(CancellationTokenSource cancellationSource);
	Action<ConversionProgressEventArgs>? OnProggressUpdateDelegate { get; set; }
	void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter);
}

internal class ChunkReader : IChunkReader
{
	public Action<ConversionProgressEventArgs>? OnProggressUpdateDelegate { get; set; }

	protected List<TrakBox> Tracks { get; } = new();
	private List<uint> TimeScales { get; } = new();
	private List<FrameFilterBase<FrameEntry>> FirstFilters { get; } = new();

	protected Stream InputStream { get; }
	protected TimeSpan StartTime { get; }
	protected TimeSpan EndTime { get; }

	public ChunkReader(Stream inputStream, TimeSpan startTime, TimeSpan endTime)
	{
		InputStream = inputStream;
		if (startTime >= endTime)
			throw new ArgumentException("Start time must be less than end time.", nameof(startTime));
		StartTime = startTime;
		EndTime = endTime;
	}

	protected virtual IEnumerable<TrackChunk> EnumerateChunks()
	{
		return new MpegChunkEnumerable(Tracks.ToArray())
		 .Where(ChunkHasFrameInRange);

		bool ChunkHasFrameInRange(TrackChunk value)
		{
			uint timeScale = GetTrackTimescale(value.TrackNum);
			var minimumSample = StartTime.TotalSeconds * timeScale;
			var maximumSample = EndTime.TotalSeconds * timeScale;
			return value.Entry.FirstSample <= maximumSample && (value.Entry.FirstSample + value.Entry.FrameDurations.Sum(d => d)) >= minimumSample;
		}
	}

	protected uint GetTrackTimescale(int trackNum)
	{
		if (trackNum < 0 || trackNum >= TimeScales.Count)
			throw new ArgumentOutOfRangeException(nameof(trackNum), "Track number is out of range or the added tracks");
		return TimeScales[trackNum];
	}

	public virtual void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter)
	{
		Tracks.Add(trak);
		TimeScales.Add(trak.Mdia.Mdhd.Timescale);
		FirstFilters.Add(filter);
	}

	public async Task RunAsync(CancellationTokenSource cancellationSource)
	{
		//All filters share the came cancellation source.
		foreach (var filter in FirstFilters)
			filter.SetCancellationToken(cancellationSource.Token);

		OnInitialProgress();

		try
		{
			foreach (var c in EnumerateChunks())
			{
				Memory<byte> chunkData = new byte[c.Entry.ChunkSize];
				InputStream.ReadNextChunk(c.Entry.ChunkOffset, chunkData, cancellationSource.Token);
				await DispatchChunk(c, chunkData);
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
			OnFinalProgress();

			//Always call CompleteAsync() on all filters so that every
			//FilterLoop gets awaited and any exceptions are thrown.
			await Task.WhenAll(FirstFilters.Select(f => f.CompleteAsync()));
		}
	}

	protected virtual FrameEntry CreateFrameEntry(ChunkEntry chunk, int frameInChunk, uint frameDelta, Memory<byte> frameData)
		=> new()
		{
			Chunk = chunk,
			SamplesInFrame = frameDelta,
			FrameData = frameData
		};

	private async Task DispatchChunk(TrackChunk chunk, Memory<byte> chunkdata)
	{
		long sampleIndex = chunk.Entry.FirstSample;

		var timeScale = GetTrackTimescale(chunk.TrackNum);

		long startSample = (long)(StartTime.TotalSeconds * timeScale);
		long endSample = (long)(EndTime.TotalSeconds * timeScale);
		uint frameDelta;

		for (int start = 0, f = 0; f < chunk.Entry.FrameSizes.Length; start += chunk.Entry.FrameSizes[f], f++, sampleIndex += frameDelta)
		{
			frameDelta = chunk.Entry.FrameDurations[f];

			if (startSample >= sampleIndex + frameDelta)
				continue;

			if (endSample < sampleIndex)
				break;

			OnProgressReport(sampleIndex, timeScale);

			var frameData = chunkdata.Slice(start, chunk.Entry.FrameSizes[f]);
			var frameEntry = CreateFrameEntry(chunk.Entry, f, frameDelta, frameData);
			await FirstFilters[chunk.TrackNum].AddInputAsync(frameEntry);
		}
	}

	private DateTime beginProcess;
	private DateTime nextUpdate;

	private void OnInitialProgress()
	{
		beginProcess = DateTime.UtcNow;
		nextUpdate = beginProcess;
		OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, StartTime, 0));
	}

	private void OnFinalProgress()
	{
		OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, EndTime, (EndTime - StartTime) / (DateTime.UtcNow - beginProcess)));
	}

	private void OnProgressReport(long sampleNumber, uint timeScale)
	{
		//Throttle update so it doesn't bog down UI
		if (DateTime.UtcNow > nextUpdate)
		{
			var trackPosition = TimeSpan.FromSeconds((double)sampleNumber / timeScale);
			double speed = (trackPosition - StartTime) / (DateTime.UtcNow - beginProcess);
			OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, trackPosition, speed));
			nextUpdate = DateTime.UtcNow.AddMilliseconds(100);
		}
	}
}
