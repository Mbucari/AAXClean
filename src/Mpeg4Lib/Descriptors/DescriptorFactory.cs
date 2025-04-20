using System.IO;

namespace Mpeg4Lib.Descriptors;

public static class DescriptorFactory
{
	public static BaseDescriptor CreateDescriptor(Stream file)
	{
		int tagID = file.ReadByte();

		return tagID switch
		{
			3 => new ES_Descriptor(file),
			4 => new DecoderConfigDescriptor(file),
			5 => new AudioSpecificConfig(file),
			6 => new SLConfigDescriptor(file),
			_ => new UnknownDescriptor((byte)tagID, file),
		};
	}
}
