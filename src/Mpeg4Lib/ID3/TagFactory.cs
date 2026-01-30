using System.IO;

namespace Mpeg4Lib.ID3;

internal class TagFactory
{
	public static Frame CreateTag(Stream file, Frame parent, out int lengthRead)
	{
		var startPos = file.Position;
		var frameHeader = FrameHeader.Create(file, parent.Version);
		var frame = CreateTagInternal(frameHeader, file, parent);
		lengthRead = (int)(file.Position - startPos);
		return frame;
	}

	private static Frame CreateTagInternal(FrameHeader frameHeader, Stream file, Frame parent)
	{

		if (parent.Version >= 0x300 && frameHeader.Identifier.StartsWith('T') && frameHeader.Identifier is not "TXXX")
			return new TEXTFrame(file, frameHeader, parent);

		return frameHeader.Identifier switch
		{
			"TXXX" => new TXXXFrame(file, frameHeader, parent),
			"APIC" => new APICFrame(file, frameHeader, parent),
			"CHAP" => new CHAPFrame(file, frameHeader, parent),
			"CTOC" => new CTOCFrame(file, frameHeader, parent),
			"\0\0\0" or "\0\0\0\0" => new EmptyFrame(frameHeader, parent),
			_ => new UnknownFrame(file, frameHeader, parent),
		};
	}
}
