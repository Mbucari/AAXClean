using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class FreeBox : Box
	{
		public const int MinSize = 8;
		public override long RenderSize => base.RenderSize + Header.TotalBoxSize - Header.HeaderSize;

		public static FreeBox Create(int freeSize, Box parent)
		{
			BoxHeader header = new BoxHeader(freeSize, "free");
			FreeBox free = new FreeBox(header, parent);
			parent.Children.Add(free);
			return free;
		}

		private FreeBox(BoxHeader header, Box parent) : base(header, parent) { }

		public FreeBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			for (uint i = Header.HeaderSize; i < Header.TotalBoxSize; i++)
				file.ReadByte();
		}

		protected override void Render(Stream file)
		{
			file.Write(new byte[Header.TotalBoxSize - Header.HeaderSize]);
		}
	}
}
