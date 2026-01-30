using System;
using System.IO;

namespace Mpeg4Lib.ID3;

public class Id3Header : Header
{
	public override string Identifier => "ID3";
	public ushort Version { get; set; }
	public Flags Flags { get; set; }
	public override int HeaderSize => 10;

	public static Id3Header? Create(Stream file)
	{
		try
		{
			Span<byte> bts = stackalloc byte[4];
			file.ReadExactly(bts);

			if (bts[0] == 'I' && bts[1] == 'D' && bts[2] == '3' //"ID3"
				&& bts[3] is 2 or 3 or 4)  //ID3v2.2, ID3v2.3, or ID3v2.4
			{
				ushort version = (ushort)(bts[3] << 8 | file.ReadByte());
				return new Id3Header(version, file);
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	internal Id3Header(ushort version, Stream file)
	{
		Version = version;
		Flags = new((byte)file.ReadByte());
		Size = UnSyncSafify(ReadUInt32BE(file));
	}

	public override void Render(Stream stream, int renderSize, ushort version)
	{
		stream.Write("ID3"u8); //"ID3"
		stream.WriteByte((byte)(version >> 8));
		stream.WriteByte((byte)(version & 0xff));
		stream.Write(Flags.ToBytes());

		WriteUInt32BE(stream, SyncSafify(renderSize));
	}
}
