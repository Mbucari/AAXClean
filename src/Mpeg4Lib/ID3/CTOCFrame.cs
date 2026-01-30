using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mpeg4Lib.ID3;

public class CTOCFrame : Frame
{
	public override int Size => Encoding.ASCII.GetByteCount(ElementID) + 3 + ChildElementIDs.Sum(c => Encoding.ASCII.GetByteCount(c) + 1) + Children.Sum(c => c.Size + c.Header.HeaderSize);
	public string ElementID { get; private init; }
	public ChapterFlags ChapterFlags { get; private init; }
	public List<string> ChildElementIDs { get; } = new();
	public void Add(CHAPFrame chapter) => ChildElementIDs.Add(chapter.ChapterID);

	public CTOCFrame(Frame parent, ChapterFlags chapterFlags, string elementId = "TOC1")
		: base(new FrameHeader("CTOC", 0, parent.Version), parent)
	{
		ChapterFlags = chapterFlags;
		ElementID = elementId;
	}

	public CTOCFrame(Stream file, Header header, Frame parent) : base(header, parent)
	{
		var endPosition = file.Position + Header.Size;
		ElementID = ReadNullTerminatedString(file, false);
		ChapterFlags = (ChapterFlags)file.ReadByte();
		var elemIdCount = file.ReadByte();

		for (int i = 0; i < elemIdCount; i++)
			ChildElementIDs.Add(ReadNullTerminatedString(file, false));

		LoadChildren(file, endPosition);
	}

	public override void Render(Stream file)
	{
		file.Write(Encoding.ASCII.GetBytes(ElementID));
		file.WriteByte(0);
		file.WriteByte((byte)ChapterFlags);
		file.WriteByte((byte)ChildElementIDs.Count);

		foreach (var ch in ChildElementIDs)
		{
			file.Write(Encoding.ASCII.GetBytes(ch));
			file.WriteByte(0);
		}
	}
}
