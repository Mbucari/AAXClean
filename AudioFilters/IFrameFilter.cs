using AAXClean.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    interface IFrameFilter : IDisposable
    {
        bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] audioFrame);
        void Close();
    }
}
