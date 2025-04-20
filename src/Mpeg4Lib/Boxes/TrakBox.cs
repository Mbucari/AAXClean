using System.IO;

namespace Mpeg4Lib.Boxes;

public class TrakBox : Box
{
	public TrakBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}

	public TkhdBox Tkhd => GetChildOrThrow<TkhdBox>();
	public MdiaBox Mdia => GetChildOrThrow<MdiaBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}