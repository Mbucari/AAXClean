using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    interface ISampleFilter : IDisposable
    {
        void FilterSample(uint chunkIndex, uint frameIndex, byte[] audioSample);
        void Close();
    }
}
