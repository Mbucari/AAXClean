using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes
{
	public class HdlrBox : FullBox
	{
		public bool HasNullTerminator { get; set; }
		public override long RenderSize => base.RenderSize + 20 + Encoding.UTF8.GetByteCount(HandlerName) + (HasNullTerminator ? 1 : 0);
		public uint PreDefined { get; }
		public string HandlerType { get; }
		private readonly byte[] Reserved;
		public string HandlerName { get; set; }
		public HdlrBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
		{
			long endPos = Header.FilePosition + Header.TotalBoxSize;

			PreDefined = file.ReadUInt32BE();
			HandlerType = Encoding.UTF8.GetString(file.ReadBlock(4));
			Reserved = file.ReadBlock(12);

			List<byte> blist = new();
			while (file.Position < endPos)
			{
				byte lastByte = (byte)file.ReadByte();

				if (lastByte == 0)
				{
					HasNullTerminator = true;
					break;
				}

				blist.Add(lastByte);
			}
			HandlerName = Encoding.UTF8.GetString(blist.ToArray());
		}
		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE(PreDefined);
			file.Write(Encoding.UTF8.GetBytes(HandlerType));
			file.Write(Reserved);
			file.Write(Encoding.UTF8.GetBytes(HandlerName));
			if (HasNullTerminator)
				file.WriteByte(0);
		}
	}
}
