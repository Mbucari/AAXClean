using AAXClean.Util;
using System.IO;

namespace AAXClean.Descriptors
{
	//ISO/IEC 14496-1 (MPEG-4 Systems) Section 1.6 (pp 52)
	//Only supports audio object IDs 1,2,3,4,6,7,17,19,20,21,22,23
	//and only parses data through the dependsOnCoreCoder flag in GASpecificConfig (Subpart 4)
	internal class AudioSpecificConfig : BaseDescriptor
	{
		public override uint RenderSize => base.RenderSize + (uint)AscBlob.Length;
		public byte[] AscBlob { get; }

		public AudioSpecificConfig(Stream file) : base(0x5, file)
		{
			AscBlob = file.ReadBlock(Size);
		}
		public override void Render(Stream file)
		{
			file.Write(AscBlob);
		}

		public int AudioObjectType
		{
			get
			{
				return AscBlob[0] >> 3;
			}
			set
			{
				AscBlob[0] = (byte)(value << 3 | AscBlob[0] & 7);
			}
		}
		public int SamplingFrequencyIndex
		{
			get
			{
				return (AscBlob[0] & 7) << 1 | AscBlob[1] >> 7;
			}
			set
			{
				AscBlob[0] = (byte)(value >> 1 | AscBlob[0] & 0xf8);
				AscBlob[1] = (byte)(value << 7 | AscBlob[1] & 0x7f);
			}
		}
		public int ChannelConfiguration
		{
			get
			{
				return (AscBlob[1] >> 3) & 7;
			}
			set
			{
				AscBlob[1] = (byte)(value << 3 | AscBlob[0] & 0x87);
			}
		}

		//GASpecificConfig in ISO/IEC 14496-3 Subpart 4 (pp 487)
		public int FrameLengthFlag
		{
			get
			{
				return AscBlob[1] >> 2 & 1;
			}
			set
			{
				AscBlob[1] = (byte)(value << 2 | AscBlob[1] & 0xfb);
			}
		}
		public int DependsOnCoreCoder
		{
			get
			{
				return AscBlob[1] >> 1 & 1;
			}
			set
			{
				AscBlob[1] = (byte)(value << 1 | AscBlob[1] & 0xfd);
			}
		}
	}
}
