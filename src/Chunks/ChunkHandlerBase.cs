using AAXClean.Boxes;
using System;
using System.Collections.Generic;

namespace AAXClean.Chunks
{
	internal abstract class ChunkHandlerBase :IDisposable
	{
		public TrakBox Track { get; }
		public TimeSpan ProcessPosition => FrameToTime(LastFrameProcessed);

		protected readonly IReadOnlyList<SttsBox.SampleEntry> Samples;
		protected readonly double TimeScale;
		protected uint LastFrameProcessed;
		protected bool Disposed = false;

		public ChunkHandlerBase(TrakBox trak)
		{
			Track = trak;
			TimeScale = Track.Mdia.Mdhd.Timescale;
			Samples = Track.Mdia.Minf.Stbl.Stts.Samples;
		}

		public abstract bool HandleChunk(ChunkEntry chunkEntry, Span<byte> chunkData);

		/// <summary>
		/// Gets the playback timestamp of an audio frame.
		/// </summary>
		private TimeSpan FrameToTime(uint frameIndex)
		{
			double beginDelta = 0;

			foreach (SttsBox.SampleEntry entry in Samples)
			{
				if (frameIndex > entry.FrameCount)
				{
					beginDelta += (ulong)entry.FrameCount * entry.FrameDelta;
					frameIndex -= entry.FrameCount;
				}
				else
				{
					beginDelta += (ulong)frameIndex * entry.FrameDelta;
					break;
				}
			}
			return TimeSpan.FromSeconds(beginDelta / TimeScale);
		}

		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			Disposed = true;
		}
	}
}
