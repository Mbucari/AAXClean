using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class TencBox : FullBox
{
	public override long RenderSize => base.RenderSize + 20 + ((DefaultIsProtected && DefaultPerSampleIvSize == 0) ? 1 + DefaultConstantIvSize : 0);
	public byte DefaultCryptByteBlock { get; }
	public byte DefaultSkipByteBlock { get; }
	public bool DefaultIsProtected { get; }
	public byte DefaultPerSampleIvSize { get; }
	public Guid DefaultKID { get; }
	public byte DefaultConstantIvSize { get; }
	public byte[] DefaultConstantIv { get; }
	public TencBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		file.ReadByte();
		if (Version == 0)
			file.ReadByte();
		else
		{
			var value = file.ReadByte();

			DefaultCryptByteBlock = (byte)(value >> 4);
			DefaultSkipByteBlock = (byte)(value & 0xf);
		}

		DefaultIsProtected = file.ReadByte() != 0;
		DefaultPerSampleIvSize = (byte)file.ReadByte();
		DefaultKID = new Guid(file.ReadBlock(16), bigEndian: true);

		if (DefaultIsProtected && DefaultPerSampleIvSize == 0)
		{
			DefaultConstantIvSize = (byte)file.ReadByte();
			DefaultConstantIv = file.ReadBlock(DefaultConstantIvSize);
		}
		else
			DefaultConstantIv = Array.Empty<byte>();
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		short value = 0;
		if (Version != 0)
		{
			value = (short)((DefaultCryptByteBlock << 4) | (DefaultSkipByteBlock & 0xf));
		}
		file.WriteInt16BE(value);
		file.WriteByte((byte)(DefaultIsProtected ? 1 : 0));
		file.WriteByte(DefaultPerSampleIvSize);
		file.Write(DefaultKID.ToByteArray(bigEndian: true));
		if (DefaultIsProtected && DefaultPerSampleIvSize == 0)
		{
			file.WriteByte((byte)DefaultConstantIv.Length);
			file.Write(DefaultConstantIv);
		}
	}
}
