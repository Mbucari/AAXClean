using System.IO;
using System.Text;

namespace Mpeg4Lib.Boxes;

//https://mutagen.readthedocs.io/en/latest/api/mp4.html
public class FreeformTagBox : AppleTagBox
{
	public MeanBox? Mean => GetChild<MeanBox>();
	public NameBox? Name => GetChild<NameBox>();

	public static FreeformTagBox Create(AppleListBox parent, string domain, string tagName, byte[] data, AppleDataType dataType)
	{
		int size
			= 12 /* empty FullBox size*/ + Encoding.UTF8.GetByteCount(domain)
			+ 12 /* empty FullBox size*/ + Encoding.UTF8.GetByteCount(tagName)
			+ 8 /* empty Box size*/ + data.Length + 2;

		BoxHeader header = new BoxHeader((uint)size, "----");

		var tagBox = new FreeformTagBox(header, parent);
		MeanBox.Create(tagBox, domain);
		NameBox.Create(tagBox, tagName);
		AppleDataBox.Create(tagBox, data, dataType);

		parent.Children.Add(tagBox);
		return tagBox;
	}

	protected FreeformTagBox(BoxHeader header, IBox parent) : base(header, parent) { }

	public FreeformTagBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}
}
