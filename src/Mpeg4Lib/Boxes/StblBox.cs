using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class StblBox : Box
	{
		public StblBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			LoadChildren(file);
		}
		public StsdBox Stsd => GetChild<StsdBox>();
		public SttsBox Stts => GetChild<SttsBox>();
		public StcoBox Stco => GetChild<StcoBox>();
		public Co64Box Co64 => GetChild<Co64Box>();
		public StszBox Stsz => GetChild<StszBox>();
		public StscBox Stsc => GetChild<StscBox>();

		protected override void Render(Stream file)
		{
			return;
		}
	}
}