using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes;

public class HdlrBox : FullBox
{
	public bool HasNullTerminator { get; set; }
	public override long RenderSize => base.RenderSize + 20 + Encoding.UTF8.GetByteCount(HandlerName) + (HasNullTerminator ? 1 : 0);
	public uint PreDefined { get; }
	public string HandlerType { get; }
	private readonly byte[] Reserved;
	public string HandlerName { get; set; }
	public HdlrBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
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

	private HdlrBox(string type, string? name, IBox parent)
		: base([0, 0, 0, 0], new BoxHeader(8, "hdlr"), parent)
	{
		ArgumentException.ThrowIfNullOrEmpty(type, nameof(type));
		if (Encoding.UTF8.GetByteCount(type) != 4)
			throw new ArgumentException($"Type '{type}' must be exactly 4 UTF-8 characters long.", nameof(type));

		HandlerType = type;
		Reserved = new byte[12];
		HandlerName = name ?? "";
		HasNullTerminator = true;
	}

	public static HdlrBox Create(string type, string? name, byte[] reservedData, IBox parent)
	{
		ArgumentNullException.ThrowIfNull(reservedData, nameof(reservedData));
		ArgumentNullException.ThrowIfNull(parent, nameof(parent));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(reservedData.Length, 12, nameof(reservedData));

		var hdlr = new HdlrBox(type, name, parent);
		Array.Copy(reservedData, 0, hdlr.Reserved, 0, reservedData.Length);
		parent.Children.Add(hdlr);
		return hdlr;
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
