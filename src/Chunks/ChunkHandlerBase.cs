using AAXClean.Boxes;
using AAXClean.FrameFilters;
using System;

namespace AAXClean.Chunks
{
	internal abstract class ChunkHandlerBase : IDisposable
	{
		public TrakBox Track { get; }
		public TimeSpan ProcessPosition => Stts.FrameToTime(TimeScale, LastFrameProcessed);
		public IFrameFilter FrameFilter { get; set; }
		public bool Success { get; protected set; } = true;

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
		public bool HandleChunk(ChunkEntry chunkEntry, Span<byte> chunkData)
		{
			for (int start = 0, f = 0; f < chunkEntry.FrameSizes.Length; start += chunkEntry.FrameSizes[f], f++, LastFrameProcessed++)
			{
				Span<byte> frameData = chunkData.Slice(start, chunkEntry.FrameSizes[f]);

				Success = HandleFrame(chunkEntry, LastFrameProcessed, frameData);

				if (!Success) break;
			}
			return Success;
		}
		public abstract bool HandleFrame(ChunkEntry cEntry, uint frameIndex, Span<byte> frameData);
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
