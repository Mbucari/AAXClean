using AAXClean.Boxes;
using AAXClean.FrameFilters;
using System;

namespace AAXClean.Chunks
{
	internal abstract class ChunkHandlerBase :IDisposable
	{
		public TrakBox Track { get; }
		public TimeSpan ProcessPosition => Stts.FrameToTime(TimeScale, LastFrameProcessed);
		public IFrameFilter FrameFilter { get; set; }

		protected readonly SttsBox Stts;
		protected readonly double TimeScale;
		protected uint LastFrameProcessed;
		protected bool Disposed = false;

		public ChunkHandlerBase(TrakBox trak)
		{
			Track = trak;
			TimeScale = Track.Mdia.Mdhd.Timescale;
			Stts = Track.Mdia.Minf.Stbl.Stts;
		}

		public abstract bool HandleChunk(ChunkEntry chunkEntry, Span<byte> chunkData);

		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					FrameFilter?.Dispose();
				}
				Disposed = true;
			}
		}
	}
}
