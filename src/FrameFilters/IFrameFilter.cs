using AAXClean.Chunks;
using System;

namespace AAXClean.FrameFilters
{
	public interface IFrameFilter : IDisposable
	{
		bool FilterFrame(ChunkEntry cEntry, uint frameIndex, uint frameDelta, Span<byte> frameData);
	}
}
