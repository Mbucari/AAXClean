using System.IO;
using System.Text;

namespace Mpeg4Lib.ID3;

public class TXXXFrame : Frame
{
	public override int Size
		=> IsUnicode(FieldName) || IsUnicode(FieldValue)
		? 1 + UnicodeLength(FieldName) + 2 + UnicodeLength(FieldValue)
		: 1 + FieldName.Length + 1 + FieldValue.Length;
	
	public string FieldName { get; }
	public string FieldValue { get; }
	public TXXXFrame(Stream file, Header header, Frame parent) : base(header, parent)
	{
		var startPos = file.Position;
		bool unicode = file.ReadByte() == 1;
		FieldName = ReadNullTerminatedString(file, unicode);
		FieldValue = ReadSizeString(file, unicode, (int)(startPos + header.Size - file.Position));
	}

	public override string ToString() => FieldName;

	public override void Render(Stream file)
	{
		if (IsUnicode(FieldName) || IsUnicode(FieldValue))
		{
			file.WriteByte(1);
			file.Write(UnicodeBytes(FieldName));
			file.Write(stackalloc byte[2]);
			file.Write(UnicodeBytes(FieldValue));
		}
		else
		{
			file.WriteByte(0);
			file.Write(Encoding.ASCII.GetBytes(FieldName));
			file.WriteByte(0);
			file.Write(Encoding.ASCII.GetBytes(FieldValue));
		}
	}
}
