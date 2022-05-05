using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class MinfBox : Box
	{
		public MinfBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			LoadChildren(file);
		}
		public StblBox Stbl => GetChild<StblBox>();
		protected override void Render(Stream file)
		{
			return;
		}
	}
}