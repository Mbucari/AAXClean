using System.IO;

namespace Mpeg4Lib.Boxes;

public class TrafBox : Box
{
	public TrafBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
	{
		LoadChildren(file);
	}

    public TfhdBox Tfhd => GetChild<TfhdBox>();
    public TrunBox Trun => GetChild<TrunBox>();
    protected override void Render(Stream file)
	{
		return;
	}
}