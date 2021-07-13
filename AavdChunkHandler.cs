using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean
{
    internal class AavdChunkHandler : IChunkHandler
    {
        public byte[] Key { get; }
        public byte[] IV { get; }
        public Stream OutputStream { get; }
        public bool Success { get; private set; } = true;
        public TimeSpan ProcessPosition => FrameToTime(lastFrameProcessed);
        public TrakBox Track { get; }

        private uint lastFrameProcessed { get; set; }
        private double TimeScale { get; }
        private SttsBox.SampleEntry[] Samples { get; }
        private List<uint> ChunkOffsets { get; }

        public AavdChunkHandler(uint timeScale, TrakBox trak, byte[] key, byte[] iv, Stream outputStream)
        {
            TimeScale = timeScale;
            Track = trak;
            Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
            ChunkOffsets = Track.Mdia.Minf.Stbl.Stco.ChunkOffsets;
            Key = key;
            IV = iv;
            OutputStream = outputStream;
        }

        public void Init()
        {
            ChunkOffsets.Clear();
        }

        public bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int chunkSize, int[] frameSizes)
        {
            ChunkOffsets.Add((uint)OutputStream.Position);
            foreach (var fsize in frameSizes)
            {
                byte[] encBlocks = file.ReadBlock(fsize);

                Crypto.DecryptInPlace(Key, IV, encBlocks);

                //aac bitstream error detection
                //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/movenc.c  in ff_mov_write_packet
                if ((AV_RB16(encBlocks) & 0xfff0) == 0xfff0)
                {
                    Success = false;
                    return Success;
                }

                OutputStream.Write(encBlocks);
            }
            lastFrameProcessed = frameIndex;
            return true;
        }

        //Defined at
        //http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
        private static ushort AV_RB16(byte[] frame)
        {
            return (ushort)(frame[0] << 8 | frame[1]);
        }

        //Gets the playback timestamp of an audio frame.
        private TimeSpan FrameToTime(uint sampleNum)
        {
            double beginDelta = 0;

            foreach (var entry in Samples)
            {
                if (sampleNum > entry.SampleCount)
                {
                    beginDelta += entry.SampleCount * entry.SampleDelta;
                    sampleNum -= entry.SampleCount;
                }
                else
                {
                    beginDelta += sampleNum * entry.SampleDelta;
                    break;
                }
            }

            return TimeSpan.FromSeconds(beginDelta / TimeScale);
        }
    }
}
