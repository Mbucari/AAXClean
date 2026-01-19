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
	private readonly ChunkOffsetList ChunkOffsets;
	private readonly IStszBox Stsz;
	private readonly SttsBox Stts;
	private readonly ChunkFrames[] ChunkFrameTable;
	private readonly uint TrackId;
	public int Count { get; }

	public ChunkEntryList(TrakBox track)
	{
		TrackId = track.Tkhd.TrackID;
		Stsz = track.Mdia.Minf.Stbl.Stsz ?? throw new ArgumentNullException(nameof(track));
		var coBox = track.Mdia.Minf.Stbl.COBox;
		ArgumentOutOfRangeException.ThrowIfGreaterThan(coBox.EntryCount, (uint)int.MaxValue, "COBox.EntryCount");
		ChunkOffsets = coBox.ChunkOffsets;
		Count = (int)coBox.EntryCount;
		Stts = track.Mdia.Minf.Stbl.Stts;
		ChunkFrameTable = track.Mdia.Minf.Stbl.Stsc.CalculateChunkFrameTable(coBox.EntryCount);
	}

	public IEnumerator<ChunkEntry> GetEnumerator()
		=> EnumerateChunks().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private IEnumerable<ChunkEntry> EnumerateChunks()
	{
		long startSample = 0;
		for (int chunkIndex = 0; chunkIndex < Count; chunkIndex++)
		{
			long chunkOffset = ChunkOffsets.GetOffsetAtIndex(chunkIndex);
			var chunkFrames = ChunkFrameTable[chunkIndex];

			(int[] frameSizes, int totalChunkSize) = Stsz.GetFrameSizes(chunkFrames.FirstFrameIndex, chunkFrames.NumberOfFrames);

			var frameDurations = Stts.EnumerateFrameDeltas(chunkFrames.FirstFrameIndex).Take(frameSizes.Length).ToArray();

			var entry = new ChunkEntry
			{
				TrackId = TrackId,
				FrameSizes = frameSizes,
				ChunkIndex = (uint)chunkIndex,
				ChunkSize = totalChunkSize,
				ChunkOffset = chunkOffset,
				FirstSample = startSample,
				FrameDurations = frameDurations
			};

			startSample += entry.FrameDurations.Sum(d => d);
			yield return entry;
		}
	}
}
