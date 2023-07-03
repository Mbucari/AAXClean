using System;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes
{
	public class AppleTagBox : Box
	{
		public static AppleTagBox Create(AppleListBox parent, string name, byte[] data, AppleDataType dataType)
		{
			if (Encoding.ASCII.GetByteCount(name) != 4)
				throw new ArgumentOutOfRangeException($"{nameof(name)} must be exactly 4 bytes long");

			int size = data.Length + 2 + 8 /* empty Box size*/ ;
			BoxHeader header = new BoxHeader((uint)size, name);

			AppleTagBox tagBox = new AppleTagBox(header, parent);
			AppleDataBox.Create(tagBox, data, dataType);

			parent.Children.Add(tagBox);
			return tagBox;
		}

		protected AppleTagBox(BoxHeader header, IBox parent) : base(header, parent) { }

		public AppleTagBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
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
