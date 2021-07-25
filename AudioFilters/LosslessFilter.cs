using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    class LosslessFilter : IFrameeFilter
    {
        private Mp4aWriter Mp4writer;
        public LosslessFilter(Mp4aWriter mp4writer)
        {
            Mp4writer = mp4writer;
        }

        private long lastChunk = -1;
        public bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] audioSample)
        {
            Mp4writer.AddFrame(audioSample, chunkIndex > lastChunk);
            lastChunk = chunkIndex;
            return true;
        }

        public void Close()
        {
            Mp4writer.Close();
        }

        public void Dispose()
        {
            Mp4writer.Dispose();
        }
    }
}
