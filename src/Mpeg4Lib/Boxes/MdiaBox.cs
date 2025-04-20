using System.IO;

namespace Mpeg4Lib.Boxes;

public class MdiaBox : Box
{
	public MdiaBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}
	public MdhdBox Mdhd => GetChildOrThrow<MdhdBox>();
	public HdlrBox Hdlr => GetChildOrThrow<HdlrBox>();
	public MinfBox Minf => GetChildOrThrow<MinfBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}