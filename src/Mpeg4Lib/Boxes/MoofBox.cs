using System.IO;

namespace Mpeg4Lib.Boxes;

public class MoofBox : Box
{
	public MoofBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
	{
		LoadChildren(file);
	}

	public MfhdBox Mfhd => GetChild<MfhdBox>();
	public TrafBox Traf => GetChild<TrafBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}
