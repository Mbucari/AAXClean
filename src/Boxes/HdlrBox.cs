using AAXClean.Util;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AAXClean.Boxes
{
	internal class HdlrBox : FullBox
	{
		public bool HasNullTerminator { get; }
		public override long RenderSize => base.RenderSize + 20 + Encoding.ASCII.GetByteCount(HandlerName) + (HasNullTerminator ? 1 : 0);
		public uint PreDefined { get; }
		public string HandlerType { get; }
		private readonly byte[] Reserved;
		public string HandlerName { get; }
		internal HdlrBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			long endPos = Header.FilePosition + Header.TotalBoxSize;

			PreDefined = file.ReadUInt32BE();
			HandlerType = Encoding.ASCII.GetString(file.ReadBlock(4));
			Reserved = file.ReadBlock(12);

			List<byte> blist = new List<byte>();
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
			HandlerName = Encoding.ASCII.GetString(blist.ToArray());
		}
		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE(PreDefined);
			file.WriteDWord(Encoding.ASCII.GetBytes(HandlerType));
			file.Write(Reserved);
			file.Write(Encoding.ASCII.GetBytes(HandlerName));
			if (HasNullTerminator)
				file.WriteByte(0);
		}
	}
}
