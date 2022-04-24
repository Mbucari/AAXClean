using AAXClean.Util;
using System.IO;

namespace AAXClean.Descriptors
{
    internal class DecoderConfigDescriptor : BaseDescriptor
    {
        public override uint RenderSize => base.RenderSize + 13;
        public byte ObjectTypeIndication { get; }
        private byte[] Blob { get; }
        public uint MaxBitrate { get; set; }
        public uint AverageBitrate { get; set; }

        public AudioSpecificConfig AudioConfig => GetChild<AudioSpecificConfig>();
        public DecoderConfigDescriptor(Stream file) : base(0x4, file)
        {
            ObjectTypeIndication = (byte)file.ReadByte();
            Blob = file.ReadBlock(4);
            MaxBitrate = file.ReadUInt32BE();
            AverageBitrate = file.ReadUInt32BE();

            //Currently only supported child is DecoderSpecificInfo, and the only
            //supported DecoderSpecificInfo is AudioSpecificConfig.
            //Any additional children will be loaded as UnknownDescriptor
            LoadChildren(file);
        }

        public override void Render(Stream file)
        {
            file.WriteByte(ObjectTypeIndication);
            file.Write(Blob);
            file.WriteUInt32BE(MaxBitrate);
            file.WriteUInt32BE(AverageBitrate);
        }
    }
}
