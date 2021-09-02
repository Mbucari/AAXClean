using System.IO;

namespace AAXClean.Descriptors
{
	internal static class DescriptorFactory
	{
		public static BaseDescriptor CreateDescriptor(Stream file)
		{
			var tagID = file.ReadByte();

			return tagID switch
			{
				3 => new ES_Descriptor(file),
				4 => new DecoderConfigDescriptor(file),
				5 => new AudioSpecificConfig(file),
				_ => new UnknownDescriptor((byte)tagID, file),
			};
		}
	}
}
