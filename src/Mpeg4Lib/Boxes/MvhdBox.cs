using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class MvhdBox : FullBox
	{
		public override long RenderSize => base.RenderSize + 3 * (Version == 0 ? 4 : 8) + 4 + 4 + 2 + 2 + 8 + Matrix.Length + Pre_defined.Length + 4;
		public ulong CreationTime { get; }
		public ulong ModificationTime { get; }
		public uint Timescale { get; set; }
		public ulong Duration { get; set; }
		public int Rate { get; }
		public short Volume { get; }
		public ushort Reserved { get; }
		public ulong Reserved2 { get; }
		public byte[] Matrix { get; }
		public byte[] Pre_defined { get; }
		public uint NextTrackID { get; set; }

		public MvhdBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
		{
			if (Version == 0)
			{
				CreationTime = file.ReadUInt32BE();
				ModificationTime = file.ReadUInt32BE();
				Timescale = file.ReadUInt32BE();
				Duration = file.ReadUInt32BE();
			}
			else
			{
				CreationTime = file.ReadUInt64BE();
				ModificationTime = file.ReadUInt64BE();
				Timescale = file.ReadUInt32BE();
				Duration = file.ReadUInt64BE();
			}
			Rate = file.ReadInt32BE();
			Volume = file.ReadInt16BE();
			Reserved = file.ReadUInt16BE();
			Reserved2 = file.ReadUInt64BE();
			Matrix = file.ReadBlock(4 * 9);
			Pre_defined = file.ReadBlock(4 * 6);
			NextTrackID = file.ReadUInt32BE();
		}
		protected override void Render(Stream file)
		{
			base.Render(file);
			if (Version == 0)
			{
				file.WriteUInt32BE((uint)CreationTime);
				file.WriteUInt32BE((uint)ModificationTime);
				file.WriteUInt32BE(Timescale);
				file.WriteUInt32BE((uint)Duration);
			}
			else
			{
				file.WriteUInt64BE(CreationTime);
				file.WriteUInt64BE(ModificationTime);
				file.WriteUInt32BE(Timescale);
				file.WriteUInt64BE(Duration);
			}
			file.WriteInt32BE(Rate);
			file.WriteInt16BE(Volume);
			file.WriteUInt16BE(Reserved);
			file.WriteUInt64BE(Reserved2);
			file.Write(Matrix);
			file.Write(Pre_defined);
			file.WriteUInt32BE(NextTrackID);
		}
	}
}
