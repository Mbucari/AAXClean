using System;
using System.IO;
using System.Text;

namespace Mpeg4Lib.ID3;

public class FrameHeader : Header
{
	public override string Identifier { get; }
	public Flags Flags { get; }
	public override int HeaderSize => Version is 0x200 ? 6 : 10;
	public ushort Version { get; }

	public static FrameHeader Create(Stream file, ushort version)
	{
		var originalPosition = file.Position;
		if (version == 0x200)
		{
			Span<byte> bts2 = stackalloc byte[6];
			file.ReadExactly(bts2);
			var frameID = Encoding.ASCII.GetString(bts2.Slice(0, 3));
			var size = (bts2[3] << 16) | (bts2[4] << 8) | bts2[5];
			return new FrameHeader(frameID, 0, version, size);
		}
		else
		{
			Span<byte> bts = stackalloc byte[4];
			file.ReadExactly(bts);

			var frameID = Encoding.ASCII.GetString(bts);
			var size = version >= 0x400 ? UnSyncSafify(ReadUInt32BE(file)) : (int)ReadUInt32BE(file);

			return new FrameHeader(frameID, ReadUInt16BE(file), version, size);
		}
	}

	public FrameHeader(string frameID, ushort flags, ushort version, int size = 0)
	{
		Version = version;
		Identifier = frameID;
		Flags = new Flags(flags);
		Size = size;
	}

	public override void Render(Stream stream, int renderSize, ushort version)
	{
		stream.Write(Encoding.ASCII.GetBytes(Identifier));
		int size = version >= 0x400 ? (int)SyncSafify(renderSize) : renderSize;

		if (version == 0x200)
		{
			stream.WriteByte((byte)(size >> 16));
			stream.WriteByte((byte)(size >> 8));
			stream.WriteByte((byte)size);
		}
		else
		{
			WriteUInt32BE(stream, (uint)size);
			stream.Write(Flags.ToBytes());
		}
	}
}
