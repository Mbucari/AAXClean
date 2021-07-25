using System;

namespace AAXClean.AudioFilters
{
    interface IFrameFilter : IDisposable
    {
        bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] audioFrame);
        void Close();
    }
}
