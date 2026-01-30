using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Mpeg4Lib.ID3;

[Flags]
public enum ChapterFlags
{
	TopLevel = 1,
	Ordered = 2
}

public class CHAPFrame : Frame
{
	public override int Size => Encoding.ASCII.GetByteCount(ChapterID) + 1 + 4 * 4 + Children.Sum(c => c.Size + 10);
	public string ChapterID { get; set; }
	public TimeSpan StartTime { get; set; }
	public TimeSpan EndTime { get; set; }
	public uint ByteStart { get; set; }
	public uint ByteEnd { get; set; }

	public CHAPFrame(Frame parent, TimeSpan startTime, TimeSpan endTime, int chNum, string title, string? subtitle = null, string? subtitle2 = null)
		: base(new FrameHeader("CHAP", 0, parent.Version), parent)
	{
		ChapterID = title;
		StartTime = startTime;
		EndTime = endTime;
		ByteStart = uint.MaxValue;
		ByteEnd = uint.MaxValue;

		if (subtitle is not null)
			TEXTFrame.Create(this, "TIT2", subtitle);

		if (subtitle2 is not null)
			TEXTFrame.Create(this, "TIT3", subtitle2);
	}

	public CHAPFrame(Stream file, Header header, Frame parent) : base(header, parent)
	{
		var endPosition = file.Position + Header.Size;
		ChapterID = ReadNullTerminatedString(file, false);
		StartTime = TimeSpan.FromMilliseconds(Header.ReadUInt32BE(file));
		EndTime = TimeSpan.FromMilliseconds(Header.ReadUInt32BE(file));
		ByteStart = Header.ReadUInt32BE(file);
		ByteEnd = Header.ReadUInt32BE(file);
		LoadChildren(file, endPosition);
	}

	public override void Render(Stream file)
	{
		file.Write(Encoding.ASCII.GetBytes(ChapterID));
		file.WriteByte(0);
		Header.WriteUInt32BE(file, (uint)Math.Round(StartTime.TotalMilliseconds));
		Header.WriteUInt32BE(file, (uint)Math.Round(EndTime.TotalMilliseconds));
		Header.WriteUInt32BE(file, ByteStart);
		Header.WriteUInt32BE(file, ByteEnd);
	}
}
