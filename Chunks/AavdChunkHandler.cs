using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Linq;

namespace AAXClean.Chunks
{
    internal class AavdChunkHandler : AudioChunkHandler
    {
        public byte[] Key { get; }
        public byte[] IV { get; }

        public AavdChunkHandler(uint timeScale, TrakBox trak, byte[] key, byte[] iv, bool seekable = false) :base(timeScale, trak, seekable)
        {
            Key = key;
            IV = iv;
        }

        public override bool PreprocessSample(byte[] audioSample)
        {
            Crypto.DecryptInPlace(Key, IV, audioSample);

            return (AV_RB16(audioSample) & 0xfff0) != 0xfff0;
        }

        //Defined at
        //http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
        private static ushort AV_RB16(byte[] frame)
        {
            return (ushort)(frame[0] << 8 | frame[1]);
        }

      
    }
}
