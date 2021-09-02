using System.IO;

namespace AAXClean.Boxes
{
	internal class MinfBox : Box
	{
		internal MinfBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
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