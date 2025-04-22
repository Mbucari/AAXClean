using Mpeg4Lib.Util;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Descriptors
{
	//ES_Descriptor ISO/IEC 14496-1 Section 7.2.6.5 (pp 35)
	//https://stackoverflow.com/a/61158659/3335599
	public class ES_Descriptor : BaseDescriptor
	{
		public ushort ES_ID { get; }
		private readonly byte EsFlags;

		private int StreamDependenceFlag => EsFlags >> 7;
		private int URL_Flag => (EsFlags >> 6) & 1;
		private int OCRstreamFlag => (EsFlags >> 5) & 1;
		public int StreamPriority => EsFlags & 31;

		private readonly ushort DependsOn_ES_ID;
		private readonly byte URLlength;
		private readonly byte[]? URLstring;
		private readonly ushort OCR_ES_Id;

		public DecoderConfigDescriptor DecoderConfig => GetChildOrThrow<DecoderConfigDescriptor>();

		public override int InternalSize => base.InternalSize + GetLength();

		public ES_Descriptor(Stream file, DescriptorHeader header) : base(file, header)
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

		private ES_Descriptor() :base(0x3)
		{
			ES_ID = 0;
			EsFlags = 0;
		}

		public static ES_Descriptor CreateAudio()
		{
			var descriptor = new ES_Descriptor();
			var decoder = DecoderConfigDescriptor.CreateAudio();
			var slConfig = SLConfigDescriptor.CreateMp4();
			descriptor.Children.Add(decoder);
			descriptor.Children.Add(slConfig);
			return descriptor;
		}

		private int GetLength()
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

			return length;
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
