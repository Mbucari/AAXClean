using AAXClean.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AAXClean.Chunks
{
	/// <summary>
	/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
	/// </summary>
	internal sealed class MpegChunkCollection : IEnumerable<TrackChunk>
	{
		private readonly TrackChunkCollection[] trackChunks;
		/// <summary>
		/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
		/// </summary>
		/// <param name="handler">A track chunk handler</param>
		/// <param name="handlers">Additional track chunk handlers</param>
		public MpegChunkCollection(IChunkHandler handler, params IChunkHandler[] handlers)
		{
			trackChunks = new TrackChunkCollection[handlers.Length + 1];
			trackChunks[0] = new TrackChunkCollection(handler);
			for (int i = 0; i < handlers.Length; i++)
				trackChunks[i + 1] = new TrackChunkCollection(handlers[i]);
		}
		public IEnumerator<TrackChunk> GetEnumerator()
			=> new MpegChunkEnumerator(trackChunks);

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		/// <summary>
		/// Enumerates Chunks in one or more <see cref="TrakBox"/> in order of the chunk offset.
		/// </summary>
		private class MpegChunkEnumerator : IEnumerator<TrackChunk>
		{
			private bool ReachedEnd = false;
			private TrackEnums[] Tracks;
			public MpegChunkEnumerator(TrackChunkCollection[] trackChunks)
			{
				Tracks = new TrackEnums[trackChunks.Length];

				for (int i = 0; i < trackChunks.Length; i++)
				{
					Tracks[i] = new TrackEnums
					{
						Handler = trackChunks[i].Handler,
						ChunkEnumerator = trackChunks[i].GetEnumerator(),
					};
					Tracks[i].TrackEnded = !Tracks[i].ChunkEnumerator.MoveNext();
				}
			}
			public TrackChunk Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose()
			{
				for (int i = 0; i < Tracks.Length; i++)
				{
					Tracks[i].ChunkEnumerator.Dispose();
					//Do not dispose of Tracks[i].Handler. It is still used after the MpegChunkCollection
				}
				Tracks = null;
			}

			public bool MoveNext()
			{
				if (ReachedEnd) return false;

				TrackEnums nextTrack = Tracks[0];

				//Find the next chunk offset across all Tracks
				for (int i = 1; i < Tracks.Length; i++)
				{
					if (Tracks[i].ChunkEnumerator.Current.ChunkOffset < nextTrack.ChunkEnumerator.Current.ChunkOffset && !Tracks[i].TrackEnded)
						nextTrack = Tracks[i];
				}

				Current = new TrackChunk
				{
					Entry = nextTrack.ChunkEnumerator.Current,
					Handler = nextTrack.Handler
				};

				//Once we have the next chuk offset, move to the next chunk in the track where we found it
				nextTrack.TrackEnded = !nextTrack.ChunkEnumerator.MoveNext();

				//Exit the enumerator after all tracks have reached the end
				ReachedEnd = true;
				foreach (TrackEnums t in Tracks)
					ReachedEnd &= t.TrackEnded;

				return true;
			}

			public void Reset()
			{
				for (int i = 0; i < Tracks.Length; i++)
					Tracks[i].ChunkEnumerator.Reset();
			}
			private class TrackEnums
			{
				/// <summary>
				/// The Track's Chunk enumerator
				/// </summary>
				public IEnumerator<ChunkEntry> ChunkEnumerator { get; init; }
				/// <summary>
				/// The handler of the track tha is being enumerated
				/// </summary>
				public IChunkHandler Handler { get; init; }
				/// <summary>
				/// If true, the last chunk in the track has already been enumerated
				/// </summary>
				public bool TrackEnded { get; set; }
			}
		}

		/// <summary>
		/// Enumerates Chunks in a <see cref="TrakBox"/>
		/// </summary>
		private class TrackChunkCollection : IEnumerable<ChunkEntry>
		{
			private TrakBox Track => Handler.Track;
			public IChunkHandler Handler { get; }
			public TrackChunkCollection(IChunkHandler handler)
			{
				Handler = handler;
			}
			public IEnumerator<ChunkEntry> GetEnumerator()
				=> new TrachChunkEnumerator(Track);

			IEnumerator IEnumerable.GetEnumerator()
				=> GetEnumerator();

			/// <summary>
			/// Enumerate over all track chunks in a track, and retrieve all information about that chunk.
			/// </summary>
			private class TrachChunkEnumerator : IEnumerator<ChunkEntry>
			{
				private IReadOnlyList<ChunkOffsetEntry> ChunkTable;
				private IReadOnlyList<int> SampleSizes;
				private readonly uint EntryCount;
				private readonly (uint firstFrameIndex, uint numFrames)[] ChunkFrameTable;
				private int CurrentChunkIndex = 0;
				public TrachChunkEnumerator(TrakBox track)
				{
					SampleSizes = track.Mdia.Minf.Stbl.Stsz.SampleSizes;

					if (track.Mdia.Minf.Stbl.Stco is not null)
					{
						ChunkTable = track.Mdia.Minf.Stbl.Stco.ChunkOffsets;
						EntryCount = track.Mdia.Minf.Stbl.Stco.EntryCount;
					}
					else
					{
						ChunkTable = track.Mdia.Minf.Stbl.Co64.ChunkOffsets;
						EntryCount = track.Mdia.Minf.Stbl.Co64.EntryCount;
					}

					ChunkFrameTable = CalculateChunkFrameTable(EntryCount, track.Mdia.Minf.Stbl.Stsc.Samples);
				}

				public ChunkEntry Current { get; private set; }

				object IEnumerator.Current => Current;

				public void Dispose()
				{
					ChunkTable = null;
					SampleSizes = null;
				}

				public bool MoveNext()
				{
					if (CurrentChunkIndex >= EntryCount) return false;

					ChunkOffsetEntry cEntry = ChunkTable[CurrentChunkIndex];

					(uint firstFrameIndex, uint numFrames) = ChunkFrameTable[cEntry.EntryIndex];

					(int[] frameSizes, int totalChunkSize) = GetChunkFrameSizes(firstFrameIndex, numFrames);

					Current = new ChunkEntry
					{
						FirstFrameIndex = firstFrameIndex,
						FrameSizes = frameSizes,
						ChunkIndex = cEntry.EntryIndex,
						ChunkSize = totalChunkSize,
						ChunkOffset = cEntry.ChunkOffset
					};

					CurrentChunkIndex++;
					return true;
				}

				public void Reset()
				{
					CurrentChunkIndex = 0;
				}

				private (int[] frameSizes, int totalChunkSize) GetChunkFrameSizes(uint firstFrameIndex, uint numFrames)
				{
					int[] frameSizes = new int[numFrames];
					int totalChunkSize = 0;

					for (uint i = 0; i < numFrames; i++)
					{
						if (i + firstFrameIndex >= SampleSizes.Count)
						{
							//This handels a case where the last Stsc entry was not written correctly.
							//This is only necessary to correct for an early error in AAXClean when
							//decrypting to m4b. This bug was introduced to Libation in 5.1.9 and was
							//fixed in 5.4.4. All m4b files created in affected versions will fail to
							//convert to mp3.
							int[] correctFrameSizes = new int[i];
							Array.Copy(frameSizes, 0, correctFrameSizes, 0, i);
							return (frameSizes, totalChunkSize);
						}

						frameSizes[i] = SampleSizes[(int)(i + firstFrameIndex)];
						totalChunkSize += frameSizes[i];
					}

					return (frameSizes, totalChunkSize);
				}

				/// <summary>
				/// Effectively expand the Stsc table to one entry for every chunk. Table size is 8 * <paramref name="numChunks"/> bytes.
				/// </summary>
				/// <returns>A zero-base table of frame indices and sizes for each chunk</returns>
				private static (uint firstFrameIndex, uint numFrames)[] CalculateChunkFrameTable(uint numChunks, IReadOnlyList<StscBox.ChunkEntry> stscSamples)
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
			}
		}
	}
}
