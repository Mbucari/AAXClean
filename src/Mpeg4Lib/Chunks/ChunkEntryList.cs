using Mpeg4Lib.Boxes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mpeg4Lib.Chunks
{
	/// <summary>
	/// A readonly list of <see cref="ChunkEntry"/> from a <see cref="TrakBox"/>
	/// </summary>
	public class ChunkEntryList : IReadOnlyCollection<ChunkEntry>
	{
		private readonly IReadOnlyList<ChunkOffsetEntry> ChunkTable;
		private readonly int EntryCount;
		private readonly IStszBox Stsz;
		private readonly SttsBox Stts;
		private readonly ChunkFrames[] ChunkFrameTable;
		public TrakBox Track { get; }
		public int Count => EntryCount;

		public ChunkEntryList(TrakBox track)
		{
			Track = track;
			Stsz = Track.Mdia.Minf.Stbl.Stsz;
			Stts = Track.Mdia.Minf.Stbl.Stts;
			ChunkTable = Track.Mdia.Minf.Stbl.COBox.ChunkOffsets;
			EntryCount = (int)Track.Mdia.Minf.Stbl.COBox.EntryCount;
			ChunkFrameTable = Track.Mdia.Minf.Stbl.Stsc.CalculateChunkFrameTable(EntryCount);
		}

		public IEnumerator<ChunkEntry> GetEnumerator()
			=> new TrachChunkEnumerator(this);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Enumerate over all track chunks in a track, and retrieve all information about that chunk.
		/// </summary>
		private class TrachChunkEnumerator : IEnumerator<ChunkEntry>
		{
			private readonly ChunkEntryList ChunkEntries;
			private int CurrentChunkIndex = 0;
			private long startSample = 0;

			public TrachChunkEnumerator(ChunkEntryList chunkEntries)
			{
				ChunkEntries = chunkEntries;
			}

			public ChunkEntry Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() { }

			public bool MoveNext()
			{
				if (CurrentChunkIndex >= ChunkEntries.EntryCount) return false;

				Current = GetChunkEntry(CurrentChunkIndex++);
				startSample += Current.FrameDurations.Sum(d => d);

				return true;
			}

			public void Reset()
			{
				CurrentChunkIndex = 0;
			}

			private ChunkEntry GetChunkEntry(int chunkIndex)
			{
				ChunkOffsetEntry cEntry = ChunkEntries.ChunkTable[chunkIndex];

				var chunkFrames = ChunkEntries.ChunkFrameTable[cEntry.EntryIndex];

				(int[] frameSizes, int totalChunkSize) = ChunkEntries.Stsz.GetFrameSizes(chunkFrames.FirstFrameIndex, chunkFrames.NumberOfFrames);

				var frameDurations = new uint[frameSizes.Length];
				for (uint i = 0; i < frameSizes.Length; i++)
					frameDurations[i] = ChunkEntries.Stts.FrameToFrameDelta(chunkFrames.FirstFrameIndex + i);

				return new ChunkEntry
				{
					FrameSizes = frameSizes,
					ChunkIndex = cEntry.EntryIndex,
					ChunkSize = totalChunkSize,
					ChunkOffset = cEntry.ChunkOffset,
					FirstSample = startSample,
					FrameDurations = frameDurations
				};
			}
		}
	}
}
