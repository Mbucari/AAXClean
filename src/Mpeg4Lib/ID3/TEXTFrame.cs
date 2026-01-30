using System.IO;
using System.Text;

namespace Mpeg4Lib.ID3;

public class TEXTFrame : Frame
{
	public override int Size => 1 + (Text is null ? 0 : IsUnicode(Text) ? UnicodeLength(Text) : Text.Length);
	public byte EncodingFlag { get; set; }
	public string? Text { get; set; }
	private TEXTFrame(Header header, Frame parent) : base(header, parent) { }
	public TEXTFrame(Stream file, Header header, Frame parent) : base(header, parent)
	{
		EncodingFlag = (byte)file.ReadByte();
		Text = ReadSizeString(file, EncodingFlag == 1, Header.Size - 1);
	}

	public static TEXTFrame Create(Frame parent, string frameId, string text)
	{
		var tit2 = new TEXTFrame(new FrameHeader(frameId, 0, parent.Version), parent)
		{
			Text = text
		};

		parent?.Children.Add(tit2);
		return tit2;
	}

	public override void Render(Stream file)
	{
		if (Text is null)
		{
			file.WriteByte(0);
		}
		else if (IsUnicode(Text))
		{
			file.WriteByte(1);
			file.Write(UnicodeBytes(Text));
		}
		else
		{
			file.WriteByte(0);
			file.Write(Encoding.ASCII.GetBytes(Text));
		}
	}
}
