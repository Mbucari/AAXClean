using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class UnknownBox : Box
	{
		public override long RenderSize => base.RenderSize + Data.Length;
		public byte[] Data { get; }
		public UnknownBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
		{
			Data = file.ReadBlock((int)(Header.TotalBoxSize - header.HeaderSize));
		}

		public override string ToString()
		{
			return nameof(UnknownBox) + "-" + Header.Type;
		}
		protected override void Render(Stream file)
		{
			file.Write(Data);
		}
	}
}
