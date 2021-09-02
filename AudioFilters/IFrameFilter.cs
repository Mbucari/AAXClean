using System;

namespace AAXClean.AudioFilters
{
	internal interface IFrameFilter : IDisposable
	{
		bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioFrame);
		void Close();
	}
}
