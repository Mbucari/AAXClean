using Mpeg4Lib.Boxes;
using System.Collections;
using System.Collections.Generic;

namespace Mpeg4Lib.Chunks
{
	/// <summary>
	/// A readonly list of <see cref="ChunkEntry"/> from a <see cref="TrakBox"/>
	/// </summary>
	public class ChunkEntryList : IReadOnlyList<ChunkEntry>
	{
		private readonly IReadOnlyList<ChunkOffsetEntry> ChunkTable;
		private readonly int EntryCount;
		private readonly IStszBox Stsz;
		private readonly ChunkFrames[] ChunkFrameTable;

		public TrakBox Track { get; }
		public int Count => EntryCount;
		public ChunkEntry this[int chunkIndex] => GetChunkEntry(chunkIndex, ChunkTable, Stsz, ChunkFrameTable);

		public ChunkEntryList(TrakBox track)
		{
			Track = track;
			Stsz = Track.Mdia.Minf.Stbl.Stsz;
			ChunkTable = Track.Mdia.Minf.Stbl.COBox.ChunkOffsets;
			EntryCount = (int)Track.Mdia.Minf.Stbl.COBox.EntryCount;
			ChunkFrameTable = Track.Mdia.Minf.Stbl.Stsc.CalculateChunkFrameTable(EntryCount);
		}

		public IEnumerator<ChunkEntry> GetEnumerator()
			=> new TrachChunkEnumerator(EntryCount, ChunkTable, Stsz, ChunkFrameTable);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private static ChunkEntry GetChunkEntry(
			int chunkIndex,
			IReadOnlyList<ChunkOffsetEntry> chunkTable,
			IStszBox stsz,
			ChunkFrames[] chunkFrameTable)
		{
			ChunkOffsetEntry cEntry = chunkTable[chunkIndex];

			var chunkFrames = chunkFrameTable[cEntry.EntryIndex];

			(int[] frameSizes, int totalChunkSize) = stsz.GetFrameSizes(chunkFrames.FirstFrameIndex, chunkFrames.NumberOfFrames);

			return new ChunkEntry
			{
				FirstFrameIndex = chunkFrames.FirstFrameIndex,
				FrameSizes = frameSizes,
				ChunkIndex = cEntry.EntryIndex,
				ChunkSize = totalChunkSize,
				ChunkOffset = cEntry.ChunkOffset
			};
		}

		/// <summary>
		/// Enumerate over all track chunks in a track, and retrieve all information about that chunk.
		/// </summary>
		private class TrachChunkEnumerator : IEnumerator<ChunkEntry>
		{
			private readonly IReadOnlyList<ChunkOffsetEntry> ChunkTable;
			private readonly int EntryCount;
			private readonly IStszBox Stsz;
			private readonly ChunkFrames[] ChunkFrameTable;
			private int CurrentChunkIndex = 0;

			public TrachChunkEnumerator(int entryCount, IReadOnlyList<ChunkOffsetEntry> chunkTable, IStszBox stsz, ChunkFrames[] chunkFrameTable)
			{
				EntryCount = entryCount;
				ChunkTable = chunkTable;
				Stsz = stsz;
				ChunkFrameTable = chunkFrameTable;
			}

			public ChunkEntry Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() { }

			public bool MoveNext()
			{
				if (CurrentChunkIndex >= EntryCount) return false;

				Current = GetChunkEntry(CurrentChunkIndex++, ChunkTable, Stsz, ChunkFrameTable);

				return true;
			}

			public void Reset()
			{
				CurrentChunkIndex = 0;
			}
		}
	}
}
