using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AAXClean.Boxes
{
	internal class MoovBox : Box
	{
		internal MoovBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			LoadChildren(file);
		}

		public MvhdBox Mvhd => GetChild<MvhdBox>();
		public TrakBox AudioTrack => Tracks.Where(t => t.GetChild<MdiaBox>()?.GetChild<HdlrBox>()?.HandlerType == "soun").FirstOrDefault();
		public TrakBox TextTrack => Tracks.Where(t => t.GetChild<MdiaBox>()?.GetChild<HdlrBox>()?.HandlerType == "text").FirstOrDefault();

		public AppleListBox iLst => GetChild<UdtaBox>().GetChild<MetaBox>().GetChild<AppleListBox>();
		public IEnumerable<TrakBox> Tracks => GetChildren<TrakBox>();

		protected override void Render(Stream file)
		{
			return;
		}
	}
}
