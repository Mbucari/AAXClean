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
                int blockSize = fsize & 0xffffff0;
                if (blockSize == 0)
                    continue;

                int remSize = fsize - blockSize;

                byte[] encBlocks = infile.ReadBlock(blockSize);

                using var xform = CreateTransform(key, iv);
                using var cStream = new CryptoStream(new MemoryStream(encBlocks), xform, CryptoStreamMode.Read);

                var decFrame = cStream.ReadBlock(blockSize);

                //aac bitstream error detection
                //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/movenc.c  in ff_mov_write_packet
                if ((AV_RB16(decFrame) & 0xfff0) == 0xfff0)
                    return false;

                outfile.Write(decFrame);

                outfile.Write(infile.ReadBlock(remSize));
            }

            return true;
        }

        //Defined at
        //http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
        private static ushort AV_RB16(byte[] frame)
        {
            return (ushort)(frame[0] << 8 | frame[1]);
        }

        private static ICryptoTransform CreateTransform(byte[] key, byte[] iv)
        {
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;

            return aes.CreateDecryptor(key, iv);
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
