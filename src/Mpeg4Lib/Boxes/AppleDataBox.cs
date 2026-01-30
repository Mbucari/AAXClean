using Mpeg4Lib.Util;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class AppleDataBox : Box
{
	public override long RenderSize => base.RenderSize + 8 + Data.Length;
	public AppleDataType DataType { get; }
	public uint Flags { get; }
	public byte[] Data { get; set; }

	[DebuggerHidden]
	private string DebuggerDisplay
		=> DataType is AppleDataType.Utf_8 ? $"[UTF-8]: '{Encoding.UTF8.GetString(Data)}'"
		: DataType is AppleDataType.Utf_16 ? $"[UTF-16]: '{Encoding.Unicode.GetString(Data)}'"
		: $"[{DataType}]: {Data.Length} bytes";

	public string ReadAsString()
	{
		return DataType switch
		{
			AppleDataType.Utf_8 => Encoding.UTF8.GetString(Data),
			AppleDataType.Utf_16 => Encoding.Unicode.GetString(Data),
			_ => throw new InvalidDataException($"Cannot read AppleDataBox of type {DataType} as string."),
		};
	}

	public static AppleDataBox Create(IBox parent, byte[] data, AppleDataType type)
	{
		int size = data.Length + 8 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "data");

		AppleDataBox dataBox = new AppleDataBox(header, parent, data, type);

		parent.Children.Add(dataBox);
		return dataBox;
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
