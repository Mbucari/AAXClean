using System.IO;
using System.Text;

namespace Mpeg4Lib.ID3;

public class APICFrame : Frame
{
	public override int Size
	{
		get
		{
			var fixedSize = 1 + ImageFormat.Length + 1 + 1 + Image.Length;

			if (IsUnicode(Description))
				return UnicodeLength(Description) + 2 + fixedSize;
			else
				return Description.Length + 1 + fixedSize;
		}
	}
	public string ImageFormat { get; set; }
	public string Description { get; set; }
	public byte Type { get; set; }
	public byte[] Image { get; set; }

	public APICFrame(Stream file, Header header, Frame parent) : base(header, parent)
	{
		var startPos = file.Position;
		var textEncoding = file.ReadByte();
		ImageFormat = ReadNullTerminatedString(file, false);
		Description = ReadNullTerminatedString(file, textEncoding == 1);
		Type = (byte)file.ReadByte();
		Image = new byte[(int)(startPos + header.Size - file.Position)];
		file.ReadExactly(Image);
	}

	public override void Render(Stream file)
	{
		int txtFormat = IsUnicode(Description) ? 1 : 0;

		file.WriteByte((byte)txtFormat);

		file.Write(Encoding.ASCII.GetBytes(ImageFormat));
		file.WriteByte(0);
		file.WriteByte(Type);

		if (txtFormat == 0)
		{
			file.Write(Encoding.ASCII.GetBytes(Description));
			file.WriteByte(0);
		}
		else
		{
			file.Write(UnicodeBytes(Description));
			file.Write(stackalloc byte[2]);
		}
		file.Write(Image);
	}
}
