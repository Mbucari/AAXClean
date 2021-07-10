using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
    internal class AudioSampleEntry : SampleEntry
    {
        public override uint RenderSize => base.RenderSize + 20;

        readonly byte[] reserved;
        readonly byte[] reserved_2;
        public ushort ChannelCount { get; }
        public ushort SampleSize { get; }
        public short PreDefined { get; }
        public ushort SampleRate { get; }
        private ushort SampleRate_loworder { get; }

        public EsdsBox Esds => GetChild<EsdsBox>();

        internal AudioSampleEntry(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            reserved = file.ReadBlock(8);
            ChannelCount = file.ReadUInt16BE();
            SampleSize = file.ReadUInt16BE();
            PreDefined = file.ReadInt16BE();
            reserved_2 = file.ReadBlock(2);
            SampleRate = file.ReadUInt16BE();
            SampleRate_loworder = file.ReadUInt16BE();
            LoadChildren(file);
        }
        protected override void Render(Stream file)
        {
            base.Render(file);
            file.Write(reserved);
            file.WriteUInt16BE(ChannelCount);
            file.WriteUInt16BE(SampleSize);
            file.WriteInt16BE(PreDefined);
            file.Write(reserved_2);
            file.WriteUInt16BE(SampleRate);
            file.WriteUInt16BE(SampleRate_loworder);
        }
    }
}
