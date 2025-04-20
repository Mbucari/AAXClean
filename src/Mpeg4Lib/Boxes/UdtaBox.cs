using System.IO;

namespace Mpeg4Lib.Boxes;

public class UdtaBox : Box
{
	public UdtaBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}

	protected override void Render(Stream file)
	{
		return;
	}
}
