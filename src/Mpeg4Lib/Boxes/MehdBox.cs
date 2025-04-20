using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class MehdBox : FullBox
{
	public override long RenderSize => base.RenderSize + (Version == 1 ? 8 : 4);
	public ulong FragmentDuration { get; }
	public MehdBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		if (Version == 1)
			FragmentDuration = file.ReadUInt64BE();
		else
			FragmentDuration = file.ReadUInt32BE();
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		if (Version == 1)
			file.WriteUInt64BE(FragmentDuration);
		else
			file.WriteUInt32BE((uint)FragmentDuration);
	}
}
