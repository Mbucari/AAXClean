using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class StblBox : Box
	{
		public StblBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
		{
			LoadChildren(file);
		}
		public StsdBox Stsd => GetChild<StsdBox>();
		public SttsBox Stts => GetChild<SttsBox>();
		public IChunkOffsets COBox => GetChild<StcoBox>() ?? (IChunkOffsets)GetChild<Co64Box>();
		public IStszBox Stsz => GetChild<StszBox>() ?? (IStszBox)GetChild<Stz2Box>();
		public StscBox Stsc => GetChild<StscBox>();

		protected override void Render(Stream file)
		{
			return;
		}
	}
}