using AAXClean.Boxes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AAXClean.Chunks
{
	/// <summary>
	/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
	/// </summary>
	internal sealed class MpegChunkEnumerable : IEnumerable<TrackChunk>
	{
		private readonly TrackEnum[] Tracks;
		/// <summary>
		/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
		/// </summary>
		/// <param name="handlers">Track chunk handlers</param>
		public MpegChunkEnumerable(params ChunkHandlerBase[] handlers)
		{
			Tracks = new TrackEnum[handlers.Length];

			for (int i = 0; i < handlers.Length; i++)
			{
				Tracks[i] = new TrackEnum
				{
					ChunkEntryList = new ChunkEntryList(handlers[i].Track),
					Handler = handlers[i]
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
			}

			public bool MoveNext()
			{
				//Exit the enumerator after all tracks have reached the end
				if (Tracks.All(t => t.TrackEnded)) return false;

				TrackEnum nextTrack = Tracks[0];

				//Find the next chunk offset across all Tracks
				for (int i = 1; i < Tracks.Length; i++)
				{
					if (nextTrack.TrackEnded)
					{
						nextTrack = Tracks[i];
						continue;
					}

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
			/// The handler of the track tha is being enumerated
			/// </summary>
			public ChunkHandlerBase Handler { get; init; }
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
