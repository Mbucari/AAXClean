using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mpeg4Lib.ID3;

public abstract class Frame
{
	public Header Header { get; }
	protected Frame Parent { get; }
	public virtual int Size => Children.Sum(b => b.Size);
	public List<Frame> Children { get; } = new();
	public virtual ushort Version => Parent.Version;

	public Frame(Header header, Frame parent)
	{
		Header = header;
		Parent = parent;
	}

	public virtual void Save(Stream file, ushort version)
	{
		Header.Render(file, Size, version);

		Render(file);

		foreach (var child in Children)
			child.Save(file, version);
	}

	public override string ToString() => Header.ToString();

	protected void LoadChildren(Stream file, long endPosition)
	{
		var origPosition = file.Position;

		while (file.Position < endPosition
			&& origPosition == file.Position
			&& TagFactory.CreateTag(file, this, out var lengthRead) is Frame child and not EmptyFrame)
		{
			origPosition += lengthRead;
			Children.Add(child);
		}
		Header.SeekForwardToPosition(file, endPosition);
	}

	public static string ReadSizeString(Stream file, bool unicode, int bytes)
	{
		var buff= new byte[bytes];
		file.ReadExactly(buff);

		return (unicode ? Encoding.Unicode : Encoding.ASCII).GetString(buff);
	}

	public static string ReadNullTerminatedString(Stream file, bool unicode)
	{
		List<byte> lst = new List<byte>();
		if (unicode)
		{
			var blob = new byte[2];
			file.ReadExactly(blob);
			while (blob[0] != 0 || blob[1] != 0)
			{
				lst.AddRange(blob);
				file.ReadExactly(blob);
			}
			return Encoding.Unicode.GetString(lst.ToArray());
		}
		else
		{
			byte b = (byte)file.ReadByte();
			while (b != 0)
			{
				lst.Add(b);
				b = (byte)file.ReadByte();
			}
			return Encoding.ASCII.GetString(lst.ToArray());
		}
	}

	public abstract void Render(Stream file);

	public static bool IsUnicode(string str) => Encoding.UTF8.GetByteCount(str) != str.Length;

	/// <summary>
	/// String length without null terminator
	/// </summary>
	public static int UnicodeLength(string str)
	{
		if (str.Length == 0) return 4;

		int strLen = Encoding.Unicode.GetByteCount(str);
		var c0 = str[0];

		if (c0 != '\ufffe' && c0 != '\ufeff')
			strLen += 2;

		return strLen;
	}

	/// <summary>
	/// String without null terminator
	/// </summary>
	public static byte[] UnicodeBytes(string str)
	{
		int strLen = str.Length;
		if (strLen == 0) return Encoding.Unicode.GetPreamble();
		var c0 = str[0];

		if (c0 == '\ufffe' || c0 == '\ufeff')
		{
			return Encoding.Unicode.GetBytes(str);
		}
		else
		{
			var bts = new byte[2 * (strLen + 1)];
			var preamble = Encoding.Unicode.GetPreamble();
			Array.Copy(preamble, bts, preamble.Length);

			Encoding.Unicode.GetBytes(str, 0, str.Length, bts, preamble.Length);

			return bts;
		}
	}
}
