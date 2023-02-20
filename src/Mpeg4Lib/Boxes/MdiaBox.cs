using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class MdiaBox : Box
	{
		public MdiaBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
		{
			LoadChildren(file);
		}
		public MdhdBox Mdhd => GetChild<MdhdBox>();
		public HdlrBox Hdlr => GetChild<HdlrBox>();
		public MinfBox Minf => GetChild<MinfBox>();
		protected override void Render(Stream file)
		{
			return;
		}
	}
}