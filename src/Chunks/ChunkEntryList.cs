using AAXClean.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AAXClean.Chunks
{
	/// <summary>
	/// A readonly list of <see cref="ChunkEntry"/> from a <see cref="TrakBox"/>
	/// </summary>
	internal class ChunkEntryList : IReadOnlyList<ChunkEntry>
	{
		private readonly IReadOnlyList<ChunkOffsetEntry> ChunkTable;
		private readonly IReadOnlyList<int> SampleSizes;
		private readonly int EntryCount;
		private readonly (uint firstFrameIndex, uint numFrames)[] ChunkFrameTable;
		private delegate ChunkEntry ChunkEntryDelegate(int chunkIndex);

		public TrakBox Track { get; }
		public int Count => ChunkFrameTable.Length;
		public ChunkEntry this[int chunkIndex] => GetChunkEntry(chunkIndex);

		public ChunkEntryList(TrakBox track)
		{
			Track = track;
			SampleSizes = Track.Mdia.Minf.Stbl.Stsz.SampleSizes;

			if (Track.Mdia.Minf.Stbl.Stco is not null)
			{
				ChunkTable = Track.Mdia.Minf.Stbl.Stco.ChunkOffsets;
				EntryCount = (int)Track.Mdia.Minf.Stbl.Stco.EntryCount;
			}
			else
			{
				ChunkTable = Track.Mdia.Minf.Stbl.Co64.ChunkOffsets;
				EntryCount = (int)Track.Mdia.Minf.Stbl.Co64.EntryCount;
			}

			ChunkFrameTable = CalculateChunkFrameTable(EntryCount, Track.Mdia.Minf.Stbl.Stsc.Samples);
		}

		public IEnumerator<ChunkEntry> GetEnumerator()
			=> new TrachChunkEnumerator(EntryCount, i => GetChunkEntry(i));

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private ChunkEntry GetChunkEntry(int chunkIndex)
		{
			ChunkOffsetEntry cEntry = ChunkTable[chunkIndex];

			(uint firstFrameIndex, uint numFrames) = ChunkFrameTable[cEntry.EntryIndex];

			(int[] frameSizes, int totalChunkSize) = GetChunkFrameSizes(SampleSizes, firstFrameIndex, numFrames);

			return new ChunkEntry
			{
				FirstFrameIndex = firstFrameIndex,
				FrameSizes = frameSizes,
				ChunkIndex = cEntry.EntryIndex,
				ChunkSize = totalChunkSize,
				ChunkOffset = cEntry.ChunkOffset
			};
		}

		private static (int[] frameSizes, int totalChunkSize) GetChunkFrameSizes(IReadOnlyList<int> sampleSizes, uint firstFrameIndex, uint numFrames)
		{
			int[] frameSizes = new int[numFrames];
			int totalChunkSize = 0;

			for (uint i = 0; i < numFrames; i++)
			{
				if (i + firstFrameIndex >= sampleSizes.Count)
				{
					//This handels a case where the last Stsc entry was not written correctly.
					//This is only necessary to correct for an early error in AAXClean when
					//decrypting to m4b. This bug was introduced to Libation in 5.1.9 and was
					//fixed in 5.4.4. All m4b files created in affected versions will fail to
					//convert to mp3 without this check.
					int[] correctFrameSizes = new int[i];
					Array.Copy(frameSizes, 0, correctFrameSizes, 0, i);
					return (frameSizes, totalChunkSize);
				}

				frameSizes[i] = sampleSizes[(int)(i + firstFrameIndex)];
				totalChunkSize += frameSizes[i];
			}

			return (frameSizes, totalChunkSize);
		}

		/// <summary>
		/// Effectively expand the Stsc table to one entry per chunk. Table size is 8 * <paramref name="numChunks"/> bytes.
		/// </summary>
		/// <returns>A zero-base table of frame indices and sizes for each chunk index</returns>
		private static (uint firstFrameIndex, uint numFrames)[] CalculateChunkFrameTable(int numChunks, IReadOnlyList<StscBox.ChunkEntry> stscSamples)
		{
			(uint firstFrameIndex, uint numFrames)[] table = new (uint, uint)[numChunks];

			uint firstFrameIndex = 0;
			int lastStscIndex = 0;

			for (uint chunk = 1; chunk <= numChunks; chunk++)
			{
				if (lastStscIndex + 1 < stscSamples.Count && chunk == stscSamples[lastStscIndex + 1].FirstChunk)
					lastStscIndex++;

				table[chunk - 1] = (firstFrameIndex, stscSamples[lastStscIndex].SamplesPerChunk);
				firstFrameIndex += stscSamples[lastStscIndex].SamplesPerChunk;
			}

			return table;
		}

		/// <summary>
		/// Enumerate over all track chunks in a track, and retrieve all information about that chunk.
		/// </summary>
		private class TrachChunkEnumerator : IEnumerator<ChunkEntry>
		{
			private readonly int EntryCount;
			private readonly ChunkEntryDelegate GetChunkEntryDelegate;
			private int CurrentChunkIndex = 0;

			public TrachChunkEnumerator(int entryCount, ChunkEntryDelegate chunkEntryDelegate)
			{
				EntryCount = entryCount;
				GetChunkEntryDelegate = chunkEntryDelegate;
			}

			public ChunkEntry Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() { }

			public bool MoveNext()
			{
				if (CurrentChunkIndex >= EntryCount) return false;

				Current = GetChunkEntryDelegate(CurrentChunkIndex++);

				return true;
			}

			public void Reset()
			{
				CurrentChunkIndex = 0;
			}
		}
	}
}
