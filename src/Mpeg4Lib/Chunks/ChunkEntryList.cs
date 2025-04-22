using Mpeg4Lib.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mpeg4Lib.Chunks;

/// <summary>
/// A readonly list of <see cref="ChunkEntry"/> from a <see cref="TrakBox"/>
/// </summary>
public class ChunkEntryList : IReadOnlyCollection<ChunkEntry>
{
	private readonly IReadOnlyList<ChunkOffsetEntry> ChunkTable;
	private readonly IStszBox Stsz;
	private readonly SttsBox Stts;
	private readonly ChunkFrames[] ChunkFrameTable;
	private readonly uint TrackId;
	public int Count { get; }

	public ChunkEntryList(TrakBox track)
	{
		TrackId = track.Tkhd.TrackID;
		Stsz = track.Mdia.Minf.Stbl.Stsz ?? throw new ArgumentNullException(nameof(track));
		var entryCount = track.Mdia.Minf.Stbl.COBox.EntryCount;
		ArgumentOutOfRangeException.ThrowIfGreaterThan(entryCount, (uint)int.MaxValue, "COBox.EntryCount");
		Count = (int)entryCount;
		Stts = track.Mdia.Minf.Stbl.Stts;
		ChunkTable = track.Mdia.Minf.Stbl.COBox.ChunkOffsets;
		ChunkFrameTable = track.Mdia.Minf.Stbl.Stsc.CalculateChunkFrameTable(entryCount);
	}

	public IEnumerator<ChunkEntry> GetEnumerator()
		=> EnumerateChunks().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private IEnumerable<ChunkEntry> EnumerateChunks()
	{
		long startSample = 0;
		for (int chunkIndex = 0; chunkIndex < Count; chunkIndex++)
		{
			ChunkOffsetEntry cEntry = ChunkTable[chunkIndex];

			var chunkFrames = ChunkFrameTable[cEntry.EntryIndex];

			(int[] frameSizes, int totalChunkSize) = Stsz.GetFrameSizes(chunkFrames.FirstFrameIndex, chunkFrames.NumberOfFrames);

			var frameDurations = Stts.EnumerateFrameDeltas(chunkFrames.FirstFrameIndex).Take(frameSizes.Length).ToArray();

			var entry = new ChunkEntry
			{
				TrackId = TrackId,
				FrameSizes = frameSizes,
				ChunkIndex = cEntry.EntryIndex,
				ChunkSize = totalChunkSize,
				ChunkOffset = cEntry.ChunkOffset,
				FirstSample = startSample,
				FrameDurations = frameDurations
			};

			startSample += entry.FrameDurations.Sum(d => d);
			yield return entry;
		}
	}
}
