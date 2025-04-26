using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class AudioSampleEntry : SampleEntry
{
	public override long RenderSize => base.RenderSize + 20;

	private readonly byte[] reserved;
	private readonly byte[] reserved_2;
	public ushort ChannelCount { get; set; }
	public ushort SampleSize { get; }
	public short PreDefined { get; }
	public ushort SampleRate { get; set; }
	private readonly ushort SampleRate_loworder;

	/// <summary>
	/// Only AAC files have ESDS. EC-3 and AC-4 files do not.
	/// </summary>
	public EsdsBox? Esds => GetChild<EsdsBox>();
	public Dec3Box? Dec3 => GetChild<Dec3Box>();
	public Dac4Box? Dac4 => GetChild<Dac4Box>();

	public AudioSampleEntry(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		reserved = file.ReadBlock(8);
		ChannelCount = file.ReadUInt16BE();
		SampleSize = file.ReadUInt16BE();
		PreDefined = file.ReadInt16BE();
		reserved_2 = file.ReadBlock(2);
		SampleRate = file.ReadUInt16BE();
		SampleRate_loworder = file.ReadUInt16BE();
		LoadChildren(file);
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		file.Write(reserved);
		file.WriteUInt16BE(ChannelCount);
		file.WriteUInt16BE(SampleSize);
		file.WriteInt16BE(PreDefined);
		file.Write(reserved_2);
		file.WriteUInt16BE(SampleRate);
		file.WriteUInt16BE(SampleRate_loworder);
	}
}
