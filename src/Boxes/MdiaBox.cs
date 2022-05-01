using System.IO;

namespace AAXClean.Boxes
{
	internal class MdiaBox : Box
	{
		internal MdiaBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
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