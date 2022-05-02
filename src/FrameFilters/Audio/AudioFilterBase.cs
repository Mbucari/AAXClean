using AAXClean.Chunks;
using System;

namespace AAXClean.FrameFilters.Audio
{
	public abstract class AudioFilterBase : IFrameFilter
	{
		public bool Closed { get; protected set; }
		public abstract bool FilterFrame(ChunkEntry cEntry, uint frameIndex, Span<byte> audioFrame);
		public abstract void Close();
		public void Dispose() => Dispose(true);

		protected bool Disposed { get; private set; }
		internal virtual ChapterInfo Chapters { get; set; }
		protected virtual void Dispose(bool disposing)
		{
			if (Disposed)
			{
				return;
			}
			Disposed = true;
		}
	}
}
