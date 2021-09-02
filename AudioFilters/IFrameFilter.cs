using System;

namespace AAXClean.AudioFilters
{
    interface IFrameFilter : IDisposable
    {
        bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioFrame);
        void Close();
    }
}
