using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class MvhdBox : HeaderBox
{
	public override long RenderSize => base.RenderSize + 4 + 4 + 2 + 2 + 8 + Matrix.Length + Pre_defined.Length + 4;
	public uint Timescale { get; set; }
	public int Rate { get; }
	public short Volume { get; }
	public ushort Reserved { get; }
	public ulong Reserved2 { get; }
	public byte[] Matrix { get; }
	public byte[] Pre_defined { get; }
	public uint NextTrackID { get; set; }

	public MvhdBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		Rate = file.ReadInt32BE();
		Volume = file.ReadInt16BE();
		Reserved = file.ReadUInt16BE();
		Reserved2 = file.ReadUInt64BE();
		Matrix = file.ReadBlock(4 * 9);
		Pre_defined = file.ReadBlock(4 * 6);
		NextTrackID = file.ReadUInt32BE();
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteInt32BE(Rate);
		file.WriteInt16BE(Volume);
		file.WriteUInt16BE(Reserved);
		file.WriteUInt64BE(Reserved2);
		file.Write(Matrix);
		file.Write(Pre_defined);
		file.WriteUInt32BE(NextTrackID);
	}

	protected override void ReadBeforeDuration(Stream file)
	{
		Timescale = file.ReadUInt32BE();
	}

	protected override void WriteBeforeDuration(Stream file)
	{
		file.WriteUInt32BE(Timescale);
	}
}
