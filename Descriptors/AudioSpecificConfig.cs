using AAXClean.Util;
using System.IO;

namespace AAXClean.Descriptors
{
    //ISO/IEC 14496-1 (MPEG-4 Systems) Section 1.6 (pp 52)
    //Only supports audio object IDs 1,2,3,4,6,7,17,19,20,21,22,23
    //and only parses data through the dependsOnCoreCoder flag in GASpecificConfig (Subpart 4)
    internal class AudioSpecificConfig : BaseDescriptor
    {
        public override uint RenderSize => base.RenderSize + (uint)Blob.Length;
        private byte[] Blob { get; }

        public AudioSpecificConfig(Stream file) : base(0x5, file)
        {
            Blob = file.ReadBlock(Size);
        }
        public override void Render(Stream file)
        {
            file.Write(Blob);
        }

        public int AudioObjectType
        {
            get
            {
                return Blob[0] >> 3;
            }
            set
            {
                Blob[0] = (byte)(value << 3 | Blob[0] & 7);
            }
        }
        public int SamplingFrequencyIndex
        {
            get
            {
                return (Blob[0] & 7) << 1 | Blob[1] >> 7;
            }
            set
            {
                Blob[0] = (byte)(value >> 1 | Blob[0] & 0xf8);
                Blob[1] = (byte)(value << 7 | Blob[1] & 0x7f);
            }
        }
        public int ChannelConfiguration
        {
            get
            {
                return (Blob[1] >> 3) & 7;
            }
            set
            {
                Blob[1] = (byte)(value << 3 | Blob[0] & 0x87);
            }
        }

        //GASpecificConfig in ISO/IEC 14496-3 Subpart 4 (pp 487)
        public int FrameLengthFlag
        {
            get
            {
                return Blob[1] >> 2 & 1;
            }
            set
            {
                Blob[1] = (byte)(value << 2 | Blob[1] & 0xfb);
            }
        }
        public int DependsOnCoreCoder
        {
            get
            {
                return Blob[1] >> 1 & 1;
            }
            set
            {
                Blob[1] = (byte)(value << 1 | Blob[1] & 0xfd);
            }
        }
    }
}
