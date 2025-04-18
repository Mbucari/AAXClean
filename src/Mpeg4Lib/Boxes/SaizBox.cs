using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class SaizBox : FullBox
{
	public override long RenderSize => base.RenderSize + ((Flags & 1) == 1 ? 8 : 0) + 5 + (DefaultInfoSampleSize == 0 ? SampleInfoSizes.Length : 0);
	public uint AuxInfoType { get; }
	public uint AuxInfoTypeParameter { get; }
	public byte DefaultInfoSampleSize { get; }
	public byte[] SampleInfoSizes { get; }
	public SaizBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
	{
		if ((Flags & 1) == 1)
		{
			AuxInfoType = file.ReadUInt32BE();
			AuxInfoTypeParameter = file.ReadUInt32BE();
		}

		DefaultInfoSampleSize = (byte)file.ReadByte();
		var sampleCount = file.ReadInt32BE();
		SampleInfoSizes = DefaultInfoSampleSize == 0 ? file.ReadBlock(sampleCount) : Array.Empty<byte>();
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		if ((Flags & 1) == 1)
		{
			file.WriteUInt32BE(AuxInfoType);
			file.WriteUInt32BE(AuxInfoTypeParameter);
		}
		file.WriteByte(DefaultInfoSampleSize);
		file.WriteInt32BE(SampleInfoSizes.Length);

		if (DefaultInfoSampleSize == 0)
			file.Write(SampleInfoSizes);
	}
}
