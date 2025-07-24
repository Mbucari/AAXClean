using Mpeg4Lib.Util;
using System.Diagnostics;
using System.IO;

namespace Mpeg4Lib.Boxes;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class SampleEntry : Box
{
	public override long RenderSize => base.RenderSize + 8;

	private readonly byte[] Reserved;
	public ushort DataReferenceIndex { get; }
	public SampleEntry(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		Reserved = file.ReadBlock(6);
		DataReferenceIndex = file.ReadUInt16BE();
	}

	protected override void Render(Stream file)
	{
		file.Write(Reserved);
		file.WriteUInt16BE(DataReferenceIndex);
	}

	private string DebuggerDisplay => $"[{Header.Type}] - " + Header.Type switch
	{
		"text" => "Text SampleEntry",
		"mp4s" => "MpegSampleEntry",
		"mp4v" => "MP4VisualSampleEntry",
		"mp4a" => "MP4AudioSampleEntry",
		"aavd" => "Audible AAX(C) Protected AudioSampleEntry",
		"encv" => "Protected VisualSampleEntry",
		"enca" => "Protected AudioSampleEntry",
		"ec-3" => "EC3SampleEntry",
		"ac-4" => "AC4SampleEntry",
		_ => $"[UNKNOWN]"
	};
}
