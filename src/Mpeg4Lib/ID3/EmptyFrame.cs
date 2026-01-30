using System.IO;

namespace Mpeg4Lib.ID3;

internal class EmptyFrame : Frame
{
	private static string GetEmptyFrameId(int version) => version is 0x200 ? "\0\0\0" : "\0\0\0\0";
	public static int GetEmptyFrameSize(int version) => version is 0x200 ? 6 : 10;
	public EmptyFrame(Frame parent)
		: this(new FrameHeader(GetEmptyFrameId(parent.Version), 0, parent.Version), parent)
	{ }
	public EmptyFrame(Header header, Frame parent) : base(header, parent) { }

	public override void Render(Stream file) { }
}
