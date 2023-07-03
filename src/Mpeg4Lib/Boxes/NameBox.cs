using Mpeg4Lib.Util;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes
{
	public class NameBox : FullBox
	{
		public override long RenderSize => base.RenderSize + Encoding.UTF8.GetByteCount(Name);
		public string Name { get; set; }
		public NameBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
		{
			var stringSize = RemainingBoxLength(file);
			var stringData = file.ReadBlock((int)stringSize);
			Name = Encoding.UTF8.GetString(stringData);
		}

		public static void Create(IBox parent, string name)
		{
			int size = Encoding.UTF8.GetByteCount(name) + 12 /* empty FullBox size*/;
			BoxHeader header = new BoxHeader((uint)size, "name");

			NameBox nameBox = new NameBox(header, parent, name);

			parent.Children.Add(nameBox);
		}

		private NameBox(BoxHeader header, IBox parent, string name) : base(new byte[4], header, parent)
		{
			Name = name;
		}

		protected override void Render(Stream file)
		{
			base.Render(file);
			file.Write(Encoding.UTF8.GetBytes(Name));
		}
	}
}
