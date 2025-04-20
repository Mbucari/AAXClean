using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class AppleDataBox : Box
{
	public override long RenderSize => base.RenderSize + 8 + Data.Length;
	public AppleDataType DataType { get; }
	public uint Flags { get; }
	public byte[] Data { get; set; }

	public static void Create(IBox parent, byte[] data, AppleDataType type)
	{
		int size = data.Length + 8 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "data");

		AppleDataBox dataBox = new AppleDataBox(header, parent, data, type);

		parent.Children.Add(dataBox);
	}
	private AppleDataBox(BoxHeader header, IBox parent, byte[] data, AppleDataType type)
		: base(header, parent)
	{
		DataType = type;
		Flags = 0;
		Data = data;
	}
	public AppleDataBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		DataType = (AppleDataType)file.ReadUInt32BE();
		Flags = file.ReadUInt32BE();
		long length = RemainingBoxLength(file);
		Data = file.ReadBlock((int)length);
	}
	protected override void Render(Stream file)
	{
		file.WriteUInt32BE((uint)DataType);
		file.WriteUInt32BE(Flags);
		file.Write(Data);
	}
}
