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

namespace AAXClean.Chunks;

public interface IChunkReader
{
	Task RunAsync(CancellationTokenSource cancellationSource);
	Action<ConversionProgressEventArgs>? OnProgressUpdateDelegate { get; set; }
	void AddTrack(TrakBox track, FrameFilterBase<FrameEntry> filter);
}

internal class ChunkReader : IChunkReader
{
	protected record TrackEntry(uint TrackId, uint Timescale, FrameFilterBase<FrameEntry> FirstFilter, TrakBox TrakBox);

	public Action<ConversionProgressEventArgs>? OnProgressUpdateDelegate { get; set; }
	protected Dictionary<uint, TrackEntry> TrackEntries { get; } = new();
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

	protected virtual IEnumerable<ChunkEntry> EnumerateChunks()
	{
		return TrackEntries
			.Values
			.Select(e => e.TrakBox)
			.InterleaveBy(t => t.ChunkEntries(), t => t.ChunkOffset)
			.Where(ChunkHasFrameInRange);

		bool ChunkHasFrameInRange(ChunkEntry value)
		{
			uint timeScale = GetTrackEntryFromId(value.TrackId).Timescale;
			var minimumSample = StartTime.TotalSeconds * timeScale;
			var maximumSample = EndTime.TotalSeconds * timeScale;
			return value.FirstSample <= maximumSample && (value.FirstSample + value.FrameDurations.Sum(d => d)) >= minimumSample;
		}
	}

	protected TrackEntry GetTrackEntryFromId(uint trackId)
		=> TrackEntries.TryGetValue(trackId, out var trackEntry) ? trackEntry
		: throw new ArgumentOutOfRangeException(nameof(trackId), $"Track ID {trackId} is not present in this {nameof(ChunkReader)} instance.");

	public virtual void AddTrack(TrakBox track, FrameFilterBase<FrameEntry> filter)
	{
		var trackEntry = new TrackEntry(track.Tkhd.TrackID, track.Mdia.Mdhd.Timescale, filter, track);
		TrackEntries.Add(track.Tkhd.TrackID, trackEntry);
	}

	public async Task RunAsync(CancellationTokenSource cancellationSource)
	{
		//All filters share the came cancellation source.
		foreach (var filter in TrackEntries.Values.Select(e => e.FirstFilter))
			filter.SetCancellationToken(cancellationSource.Token);

		OnInitialProgress();
		var token = cancellationSource.Token;

		try
		{
			foreach (var c in EnumerateChunks())
			{
				Memory<byte> chunkData = new byte[c.ChunkSize];
				await InputStream.ReadNextChunkAsync(c.ChunkOffset, chunkData, token);
				await DispatchChunk(c, chunkData, token);
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
			await Task.WhenAll(TrackEntries.Values.Select(e => e.FirstFilter.CompleteAsync()));
		}
	}

	protected virtual FrameEntry CreateFrameEntry(ChunkEntry chunk, int frameInChunk, uint frameDelta, Memory<byte> frameData)
		=> new()
		{
			Chunk = chunk,
			SamplesInFrame = frameDelta,
			FrameData = frameData
		};

	private async Task DispatchChunk(ChunkEntry chunk, Memory<byte> chunkData, CancellationToken token)
	{
		long sampleIndex = chunk.FirstSample;

		var trackEntry = GetTrackEntryFromId(chunk.TrackId);

		long startSample = (long)(StartTime.TotalSeconds * trackEntry.Timescale);
		long endSample = (long)(EndTime.TotalSeconds * trackEntry.Timescale);
		uint frameDelta;

		for (int start = 0, f = 0; f < chunk.FrameSizes.Length; start += chunk.FrameSizes[f], f++, sampleIndex += frameDelta)
		{
			frameDelta = chunk.FrameDurations[f];

			if (startSample >= sampleIndex + frameDelta)
				continue;

			if (endSample < sampleIndex)
				break;

			OnProgressReport(sampleIndex, trackEntry.Timescale);

			var frameData = chunkData.Slice(start, chunk.FrameSizes[f]);
			var frameEntry = CreateFrameEntry(chunk, f, frameDelta, frameData);
			token.ThrowIfCancellationRequested();
			await trackEntry.FirstFilter.AddInputAsync(frameEntry);
		}
	}

	private DateTime beginProcess;
	private DateTime nextUpdate;

	private void OnInitialProgress()
	{
		beginProcess = DateTime.UtcNow;
		nextUpdate = beginProcess;
		OnProgressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, StartTime, 0));
	}

	private void OnFinalProgress()
	{
		OnProgressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, EndTime, (EndTime - StartTime) / (DateTime.UtcNow - beginProcess)));
	}

	private void OnProgressReport(long sampleNumber, uint timeScale)
	{
		//Throttle update so it doesn't bog down UI
		if (DateTime.UtcNow > nextUpdate)
		{
			var trackPosition = TimeSpan.FromSeconds((double)sampleNumber / timeScale);
			double speed = (trackPosition - StartTime) / (DateTime.UtcNow - beginProcess);
			OnProgressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, trackPosition, speed));
			nextUpdate = DateTime.UtcNow.AddMilliseconds(100);
		}
	}
}
