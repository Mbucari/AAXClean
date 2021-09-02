using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
	internal class AppleDataBox : Box
	{
		public enum FlagType : uint
		{
			/// <summary>
			///    The box contains UTF-8 text.
			/// </summary>
			ContainsText = 0x01,

			/// <summary>
			///    The box contains binary data.
			/// </summary>
			ContainsData = 0x00,

			/// <summary>
			///    The box contains data for a tempo box.
			/// </summary>
			ForTempo = 0x15,

			/// <summary>
			///    The box contains a raw JPEG image.
			/// </summary>
			ContainsJpegData = 0x0D,

			/// <summary>
			///    The box contains a raw PNG image.
			/// </summary>
			ContainsPngData = 0x0E,

			/// <summary>
			///    The box contains a raw BMP image.
			/// </summary>
			ContainsBmpData = 0x1B

		}

		public override long RenderSize => base.RenderSize + 8 + Data.Length;
		public FlagType DataType { get; }
		public uint Flags { get; }
		public byte[] Data { get; set; }

		public static void Create(Box parent, byte[] data, FlagType type)
		{
			int size = data.Length + 8 /* empty Box size*/;
			var header = new BoxHeader((uint)size, "data");

			var dataBox = new AppleDataBox(header, parent, data, type);

			parent.Children.Add(dataBox);
		}
		private AppleDataBox(BoxHeader header, Box parent, byte[] data, FlagType type) : base(header, parent)
		{
			DataType = type;
			Flags = 0;
			Data = data;
		}
		internal AppleDataBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			DataType = (FlagType)file.ReadUInt32BE();
			Flags = file.ReadUInt32BE();
			long length = Header.FilePosition + Header.TotalBoxSize - file.Position;
			Data = file.ReadBlock((int)length);
		}
		protected override void Render(Stream file)
		{
			file.WriteUInt32BE((uint)DataType);
			file.WriteUInt32BE(Flags);
			file.Write(Data);
		}
	}
}
