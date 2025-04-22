using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class TkhdBox : HeaderBox
{
	public override long RenderSize => base.RenderSize + 2 * 4 + 8 + 4 * 2 + Matrix.Length + 2 * 4;
	public uint TrackID { get; set; }
	public short Layer { get; }
	public short AlternateGroup { get; set; }
	public short Volume { get; }
	public ushort Reserved3 { get; }
	public byte[] Matrix { get; }
	public uint Width { get; }
	public uint Height { get; }

	public TkhdBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		_ = file.ReadUInt64BE(); //Reserved2
		Layer = file.ReadInt16BE();
		AlternateGroup = file.ReadInt16BE();
		Volume = file.ReadInt16BE();
		Reserved3 = file.ReadUInt16BE();
		Matrix = file.ReadBlock(4 * 9);
		Width = file.ReadUInt32BE();
		Height = file.ReadUInt32BE();
	}

	protected override void Render(Stream file)
	{
		base.Render(file);

		file.WriteUInt64BE(0); //Reserved2
		file.WriteInt16BE(Layer);
		file.WriteInt16BE(AlternateGroup);
		file.WriteInt16BE(Volume);
		file.WriteUInt16BE(Reserved3);
		file.Write(Matrix);
		file.WriteUInt32BE(Width);
		file.WriteUInt32BE(Height);
	}

	protected override void ReadBeforeDuration(Stream file)
	{
		TrackID = file.ReadUInt32BE();
		_ = file.ReadUInt32BE(); //Reserved
	}

	protected override void WriteBeforeDuration(Stream file)
	{
		file.WriteUInt32BE(TrackID);
		file.WriteUInt32BE(0); //Reserved
	}
}
