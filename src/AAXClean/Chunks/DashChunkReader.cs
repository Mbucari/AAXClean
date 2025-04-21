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

	protected override FrameEntry CreateFrameEntry(ChunkEntry chunk, int frameInChunk, uint frameDelta, Memory<byte> frameData)
	{
		var entry = base.CreateFrameEntry(chunk, frameInChunk, frameDelta, frameData);

		if (chunk.ExtraData is not byte[][] IVs || IVs.Length <= frameInChunk)
			throw new InvalidDataException($"The dash chunk entry does not contain IVs");

		entry.ExtraData = IVs[frameInChunk];
		return entry;
	}

	public override void AddTrack(TrakBox track, FrameFilterBase<FrameEntry> filter)
	{
		if (TrackEntries.Count > 0)
			throw new InvalidOperationException($"The {nameof(DashChunkReader)} currently only supports a single track.");
		base.AddTrack(track, filter);
	}

	protected override IEnumerable<ChunkEntry> EnumerateChunks()
	{
		//Currently support only a single DASH track
		var singleTrack = TrackEntries.Values.Single();

		long minimumSample = (long)(StartTime.TotalSeconds * singleTrack.Timescale);
		long maximumSample = (long)(EndTime.TotalSeconds * singleTrack.Timescale);

		return new DashChunkEntryies(InputStream, singleTrack.TrackId, Dash.Sidx, Dash.FirstMoof, Dash.FirstMdat, minimumSample, maximumSample);
	}
}
