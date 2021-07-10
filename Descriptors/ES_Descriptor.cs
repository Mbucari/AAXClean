using AAXClean.Util;
using System.IO;

namespace AAXClean.Descriptors
{
    //ES_Descriptor ISO/IEC 14496-1
    //https://stackoverflow.com/a/61158659/3335599
    internal class ES_Descriptor : BaseDescriptor
    {
        public override uint RenderSize => base.RenderSize + GetLength();
        public ushort ES_ID { get; }
        private byte EsFlags { get; }

        private int StreamDependenceFlag => EsFlags >> 7;
        private int URL_Flag => (EsFlags >> 6) & 1;
        private int OCRstreamFlag => (EsFlags >> 6) & 1;
        private ushort DependsOn_ES_ID { get; }
        private byte URLlength { get; }
        private byte[] URLstring { get; }
        private ushort OCR_ES_Id { get; }

        public DecoderConfigDescriptor DecoderConfig => GetChild<DecoderConfigDescriptor>();

        public ES_Descriptor(Stream file) : base(0x3, file)
        {
            ES_ID = file.ReadUInt16BE();
            EsFlags = (byte)file.ReadByte();

            if (StreamDependenceFlag == 1)
            {
                DependsOn_ES_ID = file.ReadUInt16BE();
            }
            if (URL_Flag == 1)
            {
                URLlength = (byte)file.ReadByte();
                URLstring = file.ReadBlock(URLlength);
            }
            if (OCRstreamFlag == 1)
            {
                OCR_ES_Id = file.ReadUInt16BE();
            }
            //Currently only supported child is DecoderConfigDescriptor.
            //Any additional children will be loaded as UnknownDescriptor
            LoadChildren(file);
        }

        private uint GetLength()
        {
            int length = 3;
            if (StreamDependenceFlag == 1)
                length += 2;
            if (URL_Flag == 1)
            {
                length += 1 + URLlength;
            }
            if (OCRstreamFlag == 1)
                length += 2;

            return (uint)length;
        }

        public override void Render(Stream file)
        {
            file.WriteUInt16BE(ES_ID);
            file.WriteByte(EsFlags);
            if (StreamDependenceFlag == 1)
            {
                file.WriteUInt16BE(DependsOn_ES_ID);
            }
            if (URL_Flag == 1)
            {
                file.WriteByte(URLlength);
                file.Write(URLstring);
            }
            if (OCRstreamFlag == 1)
            {
                file.WriteUInt16BE(OCR_ES_Id);
            }
        }
    }
}
