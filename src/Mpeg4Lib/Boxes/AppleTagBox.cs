using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class AppleTagBox : Box
	{
		public static void Create(AppleListBox parent, string name, byte[] data, AppleDataType dataType)
		{
			int size = data.Length + 2 + 8 /* empty Box size*/ ;
			BoxHeader header = new BoxHeader((uint)size, name);

			AppleTagBox tagBox = new AppleTagBox(header, parent);
			AppleDataBox.Create(tagBox, data, dataType);

			parent.Children.Add(tagBox);
		}

		private AppleTagBox(BoxHeader header, Box parent) : base(header, parent) { }

		public AppleTagBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			LoadChildren(file);
		}
		public AppleDataBox Data => GetChild<AppleDataBox>();
		protected override void Render(Stream file)
		{
			return;
		}
	}
}
