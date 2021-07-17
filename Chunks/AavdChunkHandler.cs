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
            if (key is null || key.Length != 16)
                throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
            if (iv is null || iv.Length != 16)
                throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

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
