using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class MetaBox : FullBox
	{
		public MetaBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
		{
			LoadChildren(file);
		}

		private MetaBox(IBox parent)
			: base([0,0,0,0], new BoxHeader(8,"meta"), parent) { }

		public static MetaBox CreateEmpty(IBox parent)
		{
			var meta = new MetaBox(parent);
			parent.Children.Add(meta);
			return meta;
		}
	}
}
