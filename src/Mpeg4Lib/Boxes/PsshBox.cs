using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class PsshBox : FullBox
{
	public override long RenderSize => base.RenderSize + 16 + sizeof(int) + InitData.Length + ExtraData.Length;
	public Guid ProtectionSystemId { get; }
	public byte[] InitData { get; }
	public byte[] ExtraData { get; }

	public PsshBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		ProtectionSystemId = new Guid(file.ReadBlock(16), bigEndian: true);
		int initDataSize = file.ReadInt32BE();
		InitData = file.ReadBlock(initDataSize);

		var remaining = (int)(header.TotalBoxSize - header.HeaderSize - 4 - 16 - sizeof(int) - initDataSize);
		ExtraData = file.ReadBlock(remaining);
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		file.Write(ProtectionSystemId.ToByteArray(bigEndian: true));
		file.WriteInt32BE(InitData.Length);
		file.Write(InitData);
		file.Write(ExtraData);
	}
}
