using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class VisualSampleEntry : SampleEntry
{
	public override long RenderSize => base.RenderSize +
		pre_defined1.Length +
		reserved.Length +
		pre_defined2.Length +
		sizeof(ushort) * 4 +
		sizeof(uint) * 2 +
		reserved2.Length +
		pre_defined3.Length +
		32;

	private readonly byte[] pre_defined1;
	private readonly byte[] reserved;
	private readonly byte[] pre_defined2;
	public ushort Width { get; }
	public ushort Height { get; }
	public uint HorizontalResolution { get; }
	public uint VerticalResolution { get; }
	private readonly byte[] reserved2;
	public ushort FrameCount { get; }
	public string CompressorName { get; }
	public ushort Depth { get; }
	private readonly byte[] pre_defined3;
	public VisualSampleEntry(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		pre_defined1 = file.ReadBlock(2);
		reserved = file.ReadBlock(2);
		pre_defined2 = file.ReadBlock(sizeof(uint) * 3);
		Width = file.ReadUInt16BE();
		Height = file.ReadUInt16BE();
		HorizontalResolution = file.ReadUInt32BE();
		VerticalResolution = file.ReadUInt32BE();
		reserved2 = file.ReadBlock(4);
		FrameCount = file.ReadUInt16BE();
		var compressorNameBytes = file.ReadBlock(32);
		var displaySize = compressorNameBytes[0];
		if (displaySize > 31)
			throw new InvalidOperationException("Compressor name must be 31 characters or fewer.");
		CompressorName = System.Text.Encoding.UTF8.GetString(compressorNameBytes, 1, displaySize);

		Depth = file.ReadUInt16BE();
		pre_defined3 = file.ReadBlock(2);
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		file.Write(pre_defined1);
		file.Write(reserved);
		file.Write(pre_defined2);
		file.WriteUInt16BE(Width);
		file.WriteUInt16BE(Height);
		file.WriteUInt32BE(HorizontalResolution);
		file.WriteUInt32BE(VerticalResolution);
		file.Write(reserved2);
		file.WriteUInt16BE(FrameCount);
		var compressorNameBytes = new byte[32];
		if (CompressorName.Length > 31)
			throw new InvalidOperationException("Compressor name must be 31 characters or fewer.");

		compressorNameBytes[0] = (byte)CompressorName.Length;
		System.Text.Encoding.UTF8.GetBytes(CompressorName, 0, CompressorName.Length, compressorNameBytes, 1);
		file.Write(compressorNameBytes);
		file.WriteUInt16BE(Depth);
		file.Write(pre_defined3);
	}
}
