using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class FreeBox : Box
	{
		public override long RenderSize => base.RenderSize + Header.TotalBoxSize - Header.HeaderSize;

		public static FreeBox Create(int size, Box parent)
		{
			BoxHeader header = new BoxHeader((uint)size, "free");
			FreeBox free = new FreeBox(header, parent);
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
			for (uint i = Header.HeaderSize; i < Header.TotalBoxSize; i++)
				file.WriteByte(0);
		}

	}
}
