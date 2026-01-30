using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class TfdtBox : FullBox
{
	public override long RenderSize => base.RenderSize + (Version == 1 ? 8 : 4);
	public long BaseMediaDecodeTime { get; }
	public TfdtBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		BaseMediaDecodeTime = Version == 1 ? file.ReadInt64BE() : file.ReadUInt32BE();
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		if (Version == 1)
			file.WriteInt64BE(BaseMediaDecodeTime);
		else
			file.WriteUInt32BE((uint)BaseMediaDecodeTime);
	}
}
