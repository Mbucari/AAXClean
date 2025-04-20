using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public abstract class FullBox : Box
{
	public override long RenderSize => base.RenderSize + 4;
	public byte Version => VersionFlags[0];
	public int Flags => VersionFlags[1] << 16 | VersionFlags[2] << 8 | VersionFlags[3];

	protected byte[] VersionFlags { get; }
	public FullBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		VersionFlags = file.ReadBlock(4);
	}
	public FullBox(byte[] versionFlags, BoxHeader header, IBox? parent) : base(header, parent)
	{
		VersionFlags = versionFlags;
	}
	protected override void Render(Stream file)
	{
		file.Write(VersionFlags);
	}
}
