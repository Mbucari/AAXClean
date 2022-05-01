using System.IO;

namespace AAXClean.Boxes
{
	internal class TrakBox : Box
	{
		internal TrakBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			LoadChildren(file);
		}

		public TkhdBox Tkhd => GetChild<TkhdBox>();
		public MdiaBox Mdia => GetChild<MdiaBox>();
		protected override void Render(Stream file)
		{
			return;
		}
	}
}