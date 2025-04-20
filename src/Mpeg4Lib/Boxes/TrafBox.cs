using System.IO;

namespace Mpeg4Lib.Boxes;

public class TrafBox : Box
{
	public TrafBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}

	public TfhdBox Tfhd => GetChildOrThrow<TfhdBox>();
	public TfdtBox? Tfdt => GetChild<TfdtBox>();
	public TrunBox? Trun => GetChild<TrunBox>();
	public SaizBox? Saiz => GetChild<SaizBox>();
	public SaioBox? Saio => GetChild<SaioBox>();
	public SencBox? Senc => GetChild<SencBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}