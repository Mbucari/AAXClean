using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class AppleTagBox : Box
{
	[DebuggerHidden]
	private string DebuggerDisplay => $"[AppleTag]: {Header.Type}";
	public static AppleTagBox Create(AppleListBox parent, string name, byte[] data, AppleDataType dataType)
	{
		if (Encoding.ASCII.GetByteCount(name) != 4)
			throw new ArgumentOutOfRangeException($"{nameof(name)} must be exactly 4 bytes long");

		int size = data.Length + 2 + 8 /* empty Box size*/ ;
		BoxHeader header = new BoxHeader((uint)size, name);

		AppleTagBox tagBox = new AppleTagBox(header, parent);
		AppleDataBox.Create(tagBox, data, dataType);

		parent.Children.Add(tagBox);
		return tagBox;
	}

	protected AppleTagBox(BoxHeader header, IBox? parent) : base(header, parent) { }

	public AppleTagBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
	{
		LoadChildren(file);
	}
	public AppleDataBox Data => GetChildOrThrow<AppleDataBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}
