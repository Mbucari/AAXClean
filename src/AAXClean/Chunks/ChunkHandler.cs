using Mpeg4Lib.Boxes;
using AAXClean.FrameFilters;
using System;

namespace AAXClean.Chunks
{
	internal class ChunkHandler : IDisposable
	{
		public TrakBox Track { get; }
		public TimeSpan ProcessPosition => Stts.FrameToTime(TimeScale, LastFrameProcessed);
		public IFrameFilter FrameFilter { get; }
		public bool Success { get; private set; }

		private readonly SttsBox Stts;
		private readonly double TimeScale;
		private uint LastFrameProcessed;

		protected bool Disposed = false;

		public ChunkHandler(TrakBox trak, IFrameFilter frameFilter)
		{
			Track = trak;
			FrameFilter = frameFilter;
			Stts = Track.Mdia.Minf.Stbl.Stts;
			TimeScale = Track.Mdia.Mdhd.Timescale;
		}

		public bool HandleChunk(ChunkEntry chunkEntry, Span<byte> chunkData)
		{
			for (int start = 0, f = 0; f < chunkEntry.FrameSizes.Length; start += chunkEntry.FrameSizes[f], f++, LastFrameProcessed++)
			{
				Span<byte> frameData = chunkData.Slice(start, chunkEntry.FrameSizes[f]);

				Success = HandleFrame(chunkEntry, LastFrameProcessed, Stts.FrameToFrameDelta(LastFrameProcessed), frameData);

				if (!Success) break;
			}
			return Success;
		}
		public virtual bool HandleFrame(ChunkEntry cEntry, uint frameIndex, uint frameDelta, Span<byte> frameData)
		{
			return FrameFilter?.FilterFrame(cEntry, frameIndex, frameDelta, frameData) ?? false;
		}

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
