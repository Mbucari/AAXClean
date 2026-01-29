using System.Diagnostics;
using System.IO;

namespace Mpeg4Lib.Boxes;

//https://mutagen.readthedocs.io/en/latest/api/mp4.html

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class FreeformTagBox : AppleTagBox
{
	public MeanBox? Mean => GetChild<MeanBox>();
	public NameBox? Name => GetChild<NameBox>();
	[DebuggerHidden]
	private string DebuggerDisplay => $"----:{Mean?.ReverseDnsDomain}:{Name?.Name}'";

	public static FreeformTagBox Create(AppleListBox? parent, string domain, string tagName, byte[] data, AppleDataType dataType)
	{
		BoxHeader header = new BoxHeader(8, "----");

		var tagBox = new FreeformTagBox(header, parent);
		MeanBox.Create(tagBox, domain);
		NameBox.Create(tagBox, tagName);
		AppleDataBox.Create(tagBox, data, dataType);

		parent?.Children.Add(tagBox);
		return tagBox;
	}

	protected FreeformTagBox(BoxHeader header, IBox? parent) : base(header, parent) { }

	public FreeformTagBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}
}
