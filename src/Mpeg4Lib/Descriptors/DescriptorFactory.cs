using System.IO;

namespace Mpeg4Lib.Descriptors;

public static class DescriptorFactory
{
	public static BaseDescriptor CreateDescriptor(Stream file)
	{

		var header = new DescriptorHeader(file);

		return header.TagID switch
		{
			3 => new ES_Descriptor(file, header),
			4 => new DecoderConfigDescriptor(file, header),
			5 => new AudioSpecificConfig(file, header),
			6 => new SLConfigDescriptor(file, header),
			_ => new UnknownDescriptor(file, header),
		};
	}
}
