using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Linq;

namespace AAXClean.Chunks
{
    internal class AavdChunkHandler : Mp4AudioChunkHandler
    {
        public byte[] Key { get; }
        public byte[] IV { get; }

        public AavdChunkHandler(uint timeScale, TrakBox trak, byte[] key, byte[] iv, bool seekable = false) :base(timeScale, trak, seekable)
        {
            Key = key;
            IV = iv;
        }
        public override byte[] ReadBlock(Stream file, int size)
        {
            byte[] encData =  base.ReadBlock(file, size);

            Crypto.DecryptInPlace(Key, IV, encData);

            return encData;
        }
    }
}
