using AAXClean.Chunks;
using System;

namespace AAXClean.FrameFilters
{
	public interface IFrameFilter : IDisposable
	{
		bool Closed { get; }
		void Close();
		bool FilterFrame(ChunkEntry eEntry, uint frameIndex, Span<byte> audioFrame);
	}
}
