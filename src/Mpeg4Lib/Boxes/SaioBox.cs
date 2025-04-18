using Mpeg4Lib.Util;
using System.IO;

#nullable enable
namespace Mpeg4Lib.Boxes;

public interface ISaioBox : IBox
{
	uint AuxInfoType { get; }
	uint AuxInfoTypeParameter { get; }
	int EntryCount { get; }
}

public class SaioBox : FullBox, ISaioBox
{
	public override long RenderSize => base.RenderSize + ((Flags & 1) == 1 ? 8 : 0) + 4 + (Version == 0 ? (offsets_32?.Length ?? 0) * 4 : (offsets_64?.Length ?? 0) * 8);
	public uint AuxInfoType { get; }
	public uint AuxInfoTypeParameter { get; }
	public int EntryCount { get; }

	private uint[]? offsets_32;
	private long[]? offsets_64;

	public SaioBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
	{
		if ((Flags & 1) == 1)
		{
			AuxInfoType = file.ReadUInt32BE();
			AuxInfoTypeParameter = file.ReadUInt32BE();
		}

		EntryCount = file.ReadInt32BE();
		if (Version == 0)
		{
			offsets_32 = new uint[EntryCount];
			for (int i = 0; i < EntryCount; i++)
			{
				offsets_32[i] = file.ReadUInt32BE();
			}
		}
		else
		{

			offsets_64 = new long[EntryCount];
			for (int i = 0; i < EntryCount; i++)
			{
				offsets_64[i] = file.ReadInt64BE();
			}
		}
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		if ((Flags & 1) == 1)
		{
			file.WriteUInt32BE(AuxInfoType);
			file.WriteUInt32BE(AuxInfoTypeParameter);
		}
		file.WriteInt32BE(EntryCount);

		if (offsets_32 != null)
		{
			foreach (var offset in offsets_32)
				file.WriteUInt32BE(offset);
		}
		else if (offsets_64 != null)
		{
			foreach (var offset in offsets_64)
				file.WriteInt64BE(offset);
		}
	}
}
