using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Chunks
{
    class Mp4aChunkHandler : AudioChunkHandler
    {
        public Mp4aChunkHandler(uint timeScale, TrakBox trak, bool seekable = false) : base(timeScale, trak, seekable) { }

        public override bool PreprocessSample(byte[] audioSample)
        {
            return true;
        }
    }
}
