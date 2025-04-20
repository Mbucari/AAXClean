using System.IO;

namespace Mpeg4Lib.Boxes;

public class MinfBox : Box
{
	public MinfBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}
	public StblBox Stbl => GetChildOrThrow<StblBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}