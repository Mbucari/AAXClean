using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class MetaBox : FullBox
	{
		public MetaBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			LoadChildren(file);
		}
	}
}
