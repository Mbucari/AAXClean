using AAXClean.Boxes;
using System;
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
		private readonly (ChunkEntryList trackChunks, IChunkHandler handler)[] TrackHandlers;
		/// <summary>
		/// Enumerates over all chunks in all Mpeg tracks in order of the cunk offset
		/// </summary>
		/// <param name="handler">A track chunk handler</param>
		/// <param name="handlers">Additional track chunk handlers</param>
		public MpegChunkEnumerable(IChunkHandler handler, params IChunkHandler[] handlers)
		{
			TrackHandlers = new (ChunkEntryList trackChunks, IChunkHandler handler)[handlers.Length + 1];

			TrackHandlers[0] = (new ChunkEntryList(handler.Track), handler);
			for (int i = 0; i < handlers.Length; i++)
				TrackHandlers[i + 1] = (new ChunkEntryList(handlers[i].Track), handlers[i]);
		}
		public IEnumerator<TrackChunk> GetEnumerator()
			=> new MpegChunkEnumerator(TrackHandlers);
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		/// <summary>
		/// Enumerates Chunks in one or more <see cref="TrakBox"/> in order of the chunk offset.
		/// </summary>
		private class MpegChunkEnumerator : IEnumerator<TrackChunk>
		{
			private TrackEnums[] Tracks;
			public MpegChunkEnumerator(params (ChunkEntryList trackChunks, IChunkHandler handler)[] trackHandlers)
			{
				Tracks = new TrackEnums[trackHandlers.Length];

				for (int i = 0; i < trackHandlers.Length; i++)
				{
					Tracks[i] = new TrackEnums
					{
						Handler = trackHandlers[i].handler,
						ChunkEnumerator = trackHandlers[i].trackChunks.GetEnumerator(),
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
				//Exit the enumerator after all tracks have reached the end
				if (Tracks.All(t => t.TrackEnded)) return false;

				TrackEnums nextTrack = Tracks[0];

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
	}
}
