using AAXClean.Boxes;
using AAXClean.Util;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace AAXClean.Chunks
{
    internal class AavdChunk : MdatChunk
    {
        public uint TimeMS { get; }
        public uint FirstFram { get; }
        public int StreamDescriptor { get; }
        public int DataSize { get; }
        public int NumFrames { get; }
        public List<int> FrameSizes { get; } = new List<int>();

        public AavdChunk(Stream file, BoxHeader header) : base(file, header)
        {
            TimeMS = file.ReadUInt32BE();
            FirstFram = file.ReadUInt32BE();
            StreamDescriptor = file.ReadInt32BE();
            DataSize = file.ReadInt32BE();
            NumFrames = file.ReadInt32BE();

            for (int i = 0; i < NumFrames; i++)
            {
                FrameSizes.Add(file.ReadInt32BE());
            }
        }

        public bool DecryptSave(byte[] key, byte[] iv, Stream infile, Stream outfile)
        {
            foreach (var fsize in FrameSizes)
            {
                byte[] encBlocks = infile.ReadBlock(fsize);

                Crypto.DecryptInPlace(key, iv, encBlocks);

                //aac bitstream error detection
                //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/movenc.c  in ff_mov_write_packet
                if ((AV_RB16(encBlocks) & 0xfff0) == 0xfff0)
                    return false;

                outfile.Write(encBlocks);
            }

            return true;
        }

        //Defined at
        //http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
        private static ushort AV_RB16(byte[] frame)
        {
            return (ushort)(frame[0] << 8 | frame[1]);
        }

        public override MdatChunk GetNext(Stream file)
        {
            long nextEntryPos = Position + Header.TotalBoxSize + DataSize;
            if (file.Position != nextEntryPos)
                file.Position = nextEntryPos;

            return MdatFactory.CreateEntry(file);
        }
    }
}
