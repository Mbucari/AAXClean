using System.IO;

namespace Mpeg4Lib.ID3;

public class Id3ExtendedHeader : Frame
{
	private Id3ExtendedHeader(Header header, Frame parent) : base(header, parent) { }
	public override void Render(Stream file) { }
	public static Id3ExtendedHeader Create(Stream file, Id3Tag parent)
	{
		var header = new ExtendedHeader(file, parent.Version);
		return new Id3ExtendedHeader(header, parent);
	}
}

public class ExtendedHeader : Header
{
	public override string Identifier { get; } = string.Empty;
	public override int HeaderSize => GetExtendedFlagsSize();
	public Flags ExtendedFlags { get; set; }
	public uint SizeOfPadding { get; set; }
	public bool TagIsUpdate { get; set; }
	public byte TagRestrictions { get; set; }
	public uint CRC32 { get; set; }
	public int Version { get; }
	public ExtendedHeader(Stream file, int version)
	{
		Version = version;
		long headerSize, originalPosition = file.Position;
		if (version >= 0x400)
		{
			headerSize = UnSyncSafify(ReadUInt32BE(file));
			var numFlagBytes = file.ReadByte();
			ExtendedFlags = new Flags(ReadBlock(file, numFlagBytes));
			if (ExtendedFlags[1]) // Tag is an update
				TagIsUpdate = file.ReadByte() != 0;
			if (ExtendedFlags[2] && file.ReadByte() == 5) // CRC32 present
				CRC32 = ((uint)file.ReadByte() << 28) | (uint)UnSyncSafify(ReadUInt32BE(file));
			if (ExtendedFlags[3] && file.ReadByte() == 1) // Tag restrictions
				TagRestrictions = (byte)file.ReadByte();
		}
		else
		{
			headerSize = ReadUInt32BE(file);
			ExtendedFlags = new Flags(ReadUInt16BE(file));
			SizeOfPadding = ReadUInt32BE(file);
			if (ExtendedFlags[0])
				CRC32 = ReadUInt32BE(file);
		}
		// Skip padding if any
		SeekForwardToPosition(file, originalPosition + headerSize);
	}

	private int GetExtendedFlagsSize()
	{
		if (ExtendedFlags is null)
		{
			return 0;
		}
		if (Version >= 0x400)
		{
			var size = 5 + ExtendedFlags.Size;
			if (ExtendedFlags[1])
				size++; //Tag is an update
			if (ExtendedFlags[2])
				size += 6; // CRC32
			if (ExtendedFlags[3])
				size += 2; // Tag restrictions
			return size;
		}
		else
		{
			return ExtendedFlags[0] ? 10 : 6; // Extended header size
		}
	}

	public override void Render(Stream stream, int renderSize, ushort version)
	{
		if (version >= 0x400)
		{
			WriteUInt32BE(stream, SyncSafify(HeaderSize + renderSize));
			stream.WriteByte((byte)ExtendedFlags.Size);
			stream.Write(ExtendedFlags.ToBytes());

			if (ExtendedFlags[1])
				stream.WriteByte((byte)(TagIsUpdate ? 1 : 0));
			if (ExtendedFlags[2])
			{
				// CRC32 present
				stream.WriteByte(5);
				stream.WriteByte((byte)(CRC32 >> 28));
				WriteUInt32BE(stream, SyncSafify((int)(CRC32 & 0xfffffff)));
			}
			if (ExtendedFlags[3])
			{
				// Tag restrictions
				stream.WriteByte(1);
				stream.WriteByte(TagRestrictions);
			}
		}
		else
		{
			WriteUInt32BE(stream, (uint)(HeaderSize + renderSize));
			stream.Write(ExtendedFlags.ToBytes());
			WriteUInt32BE(stream, SizeOfPadding);
			if (ExtendedFlags[0])
				WriteUInt32BE(stream, CRC32);
		}
	}
	public override string ToString() =>"ExtendedHeader";
}
