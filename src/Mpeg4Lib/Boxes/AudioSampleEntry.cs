﻿using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class AudioSampleEntry : SampleEntry
{
	public override long RenderSize => base.RenderSize + 20;

	private readonly byte[] reserved;
	private readonly byte[] reserved_2;
	public ushort ChannelCount { get; }
	public ushort SampleSize { get; }
	public short PreDefined { get; }
	public ushort SampleRate { get; set; }
	private readonly ushort SampleRate_loworder;

	public EsdsBox Esds => GetChildOrThrow<EsdsBox>();

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
