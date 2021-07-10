using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Text;

namespace AAXClean.Chunks
{
    internal class TextChunk : MdatChunk
    {
        public uint TimeMs { get; }
        public uint ChapterIndex { get; }
        public uint Stream_1 { get; }
        public uint TextLength_1 { get; }
        public uint Stream_2 { get; }
        public uint TextLength_2 { get; }
        public short TextSize { get; }
        public string ChapterTitle { get; }

        public byte[] Encd_data { get; }
        public TextChunk(Stream file, BoxHeader header) : base(file, header)
        {
            TimeMs = file.ReadUInt32BE();
            ChapterIndex = file.ReadUInt32BE();
            Stream_1 = file.ReadUInt32BE();
            TextLength_1 = file.ReadUInt32BE();
            Stream_2 = file.ReadUInt32BE();
            TextLength_2 = file.ReadUInt32BE();

            TextSize = file.ReadInt16BE();
            byte[] title = file.ReadBlock(TextSize);

            ChapterTitle = Encoding.ASCII.GetString(title);

            Encd_data = file.ReadBlock((int)(TextLength_1 - TextSize - 2));
        }

        public int ChapterChunkOffset => (int)Header.TotalBoxSize;
        public long Save(Stream file)
        {
            long beginPos = file.Position;
            file.WriteHeader(Header);
            file.WriteUInt32BE(TimeMs);
            file.WriteUInt32BE(ChapterIndex);
            file.WriteUInt32BE(Stream_1);
            file.WriteUInt32BE(TextLength_1);
            file.WriteUInt32BE(Stream_2);
            file.WriteUInt32BE(TextLength_2);
            file.WriteInt16BE(TextSize);
            file.Write(Encoding.ASCII.GetBytes(ChapterTitle));
            file.Write(Encd_data);
            return beginPos;
        }

        public (string title, TimeSpan duration) GetChapter(TrakBox chapterTrack)
        {
            var chapterIndex = chapterTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.IndexOf((uint)(Position + ChapterChunkOffset));

            if (chapterIndex < 0)
                throw new Exception($"{ChapterTitle} offset {Position + ChapterChunkOffset:X8} not found in stts box.");

            double timeScale = chapterTrack.Mdia.Mdhd.Timescale;
            var duration = chapterTrack.Mdia.Minf.Stbl.Stts.Samples[chapterIndex].SampleDelta / timeScale;

            return (ChapterTitle, TimeSpan.FromSeconds(duration));
        }
    }
}
