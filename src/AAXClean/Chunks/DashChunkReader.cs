using AAXClean.FrameFilters;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AAXClean.Chunks;

internal class DashChunkReader : ChunkReader
{
	private DashFile Dash { get; }

	public DashChunkReader(DashFile dash, Stream inputStream, TimeSpan startTime, TimeSpan endTime)
		: base(inputStream, startTime, endTime)
	{
		ArgumentNullException.ThrowIfNull(dash, nameof(dash));
		ArgumentNullException.ThrowIfNull(inputStream, nameof(inputStream));
		Dash = dash;
	}

	protected override FrameEntry CreateFrameEntry(ChunkEntry chunk, int frameInChunk, uint frameDelta, long startSample, Memory<byte> frameData)
	{
		var entry = base.CreateFrameEntry(chunk, frameInChunk, frameDelta, startSample, frameData);
		if (chunk.ExtraData is byte[][] IVs)
		{
			entry.ExtraData = IVs.Length > frameInChunk ? IVs[frameInChunk]
			: throw new InvalidDataException($"There are only {IVs.Length} in the chunk, but caller requesting frame at index {frameInChunk}.");
		}
		return entry;
	}

	public override void AddTrack(TrakBox track, FrameFilterBase<FrameEntry> filter, TimeSpan lookback = default)
	{
		if (TrackEntries.Count > 0)
			throw new InvalidOperationException($"The {nameof(DashChunkReader)} currently only supports a single track.");
		base.AddTrack(track, filter, lookback);
	}

	protected override IEnumerable<ChunkEntry> EnumerateChunks()
	{
		//Currently support only a single DASH track
		var singleTrack = TrackEntries.Values.Single();

		long minimumSample = singleTrack.DispatchStartSample;
		long maximumSample = singleTrack.DispatchEndSample;

		return new DashChunkEntries(InputStream, singleTrack.TrackId, Dash.Sidx, Dash.FirstMoof, Dash.FirstMdat, minimumSample, maximumSample);
	}
}
