﻿using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Descriptors;

public class DecoderConfigDescriptor : BaseDescriptor
{
	public override uint RenderSize => base.RenderSize + 13;
	public byte ObjectTypeIndication { get; }
	private readonly byte[] Blob;
	public uint MaxBitrate { get; set; }
	public uint AverageBitrate { get; set; }
	public AudioSpecificConfig AudioSpecificConfig => GetChildOrThrow<AudioSpecificConfig>();

	public DecoderConfigDescriptor(Stream file) : base(0x4, file)
	{
		ObjectTypeIndication = (byte)file.ReadByte();
		Blob = file.ReadBlock(4);
		MaxBitrate = file.ReadUInt32BE();
		AverageBitrate = file.ReadUInt32BE();

		//Currently only supported child is DecoderSpecificInfo, and the only
		//supported DecoderSpecificInfo is AudioSpecificConfig.
		//Any additional children will be loaded as UnknownDescriptor
		LoadChildren(file);
	}

	public override void Render(Stream file)
	{
		file.WriteByte(ObjectTypeIndication);
		file.Write(Blob);
		file.WriteUInt32BE(MaxBitrate);
		file.WriteUInt32BE(AverageBitrate);
	}
}
