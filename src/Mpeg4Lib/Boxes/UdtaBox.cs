using System.IO;

namespace Mpeg4Lib.Boxes;

public class UdtaBox : Box
{
	public UdtaBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}

	private UdtaBox(IBox parent) : base(new BoxHeader(8, "udta"), parent) { }

	public static UdtaBox CreateEmpty(IBox parent)
	{
		var udata = new UdtaBox(parent);
		parent.Children.Add(udata);
		return udata;
	}

	protected override void Render(Stream file)
	{
		return;
	}
}
