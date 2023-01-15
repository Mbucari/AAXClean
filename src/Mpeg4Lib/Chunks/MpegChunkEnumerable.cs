using Mpeg4Lib.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mpeg4Lib.Chunks
{
	/// <summary>
	/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
	/// </summary>
	public sealed class MpegChunkEnumerable : IEnumerable<TrackChunk>
	{
		public int NumberOfTracks => Tracks.Length;
		private readonly TrackEnum[] Tracks;
		/// <summary>
		/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
		/// </summary>
		/// <param name="tracks">Track chunk handlers</param>
		public MpegChunkEnumerable(params TrakBox[] tracks)
		{
			Tracks = new TrackEnum[tracks.Length];

			for (int i = 0; i < tracks.Length; i++)
			{
				Tracks[i] = new TrackEnum
				{
					ChunkEntryList = new ChunkEntryList(tracks[i]),
				};
			}
		}
		public IEnumerator<TrackChunk> GetEnumerator()
			=> new MpegChunkEnumerator(Tracks);
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		/// <summary>
		/// Enumerates Chunks in one or more <see cref="TrakBox"/> in order of the chunk offset.
		/// </summary>
		private class MpegChunkEnumerator : IEnumerator<TrackChunk>
		{
			private readonly TrackEnum[] Tracks;
			public MpegChunkEnumerator(TrackEnum[] tracks)
			{
				Tracks = tracks;

				for (int i = 0; i < tracks.Length; i++)
				{
					Tracks[i].ChunkEnumerator = Tracks[i].ChunkEntryList.GetEnumerator();
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
					//Do not dispose of Tracks[i].Handler or Tracks[i].ChunkEntryList.
					//They are still used after the MpegChunkEnumerator disposes
				}

				GC.SuppressFinalize(this);
			}

			public bool MoveNext()
			{
				//Exit the enumerator after all tracks have reached the end
				if (Tracks.All(t => t.TrackEnded)) return false;

				int trackIndex = 0;
				TrackEnum nextTrack = Tracks[trackIndex];

				//Find the next chunk offset across all Tracks
				for (int i = 1; i < Tracks.Length; i++)
				{
					if (nextTrack.TrackEnded)
					{
						trackIndex = i;
						nextTrack = Tracks[trackIndex];
						continue;
					}

					if (Tracks[i].ChunkEnumerator.Current.ChunkOffset < nextTrack.ChunkEnumerator.Current.ChunkOffset && !Tracks[i].TrackEnded)
					{
						trackIndex = i;
						nextTrack = Tracks[trackIndex];
					}
				}

				Current = new TrackChunk
				{
					Entry = nextTrack.ChunkEnumerator.Current,
					TrackNum = trackIndex
				};

				//Once we have the next chuk offset, move to the next chunk in the track where we found it
				nextTrack.TrackEnded = !nextTrack.ChunkEnumerator.MoveNext();

				return true;
			}

			public void Reset()
			{
				for (int i = 0; i < Tracks.Length; i++)
					Tracks[i].ChunkEnumerator.Reset();
			}
		}
		private class TrackEnum
		{
			public ChunkEntryList ChunkEntryList { get; init; }
			/// <summary>
			/// The <see cref="ChunkEntryList"/> enumerator
			/// </summary>
			public IEnumerator<ChunkEntry> ChunkEnumerator { get; set; }
			/// <summary>
			/// If true, the last chunk in the track has already been enumerated
			/// </summary>
			public bool TrackEnded { get; set; }
		}
	}
}
