using System.IO;

namespace Mpeg4Lib.Boxes;

public class SchiBox : Box
{
	public SchiBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
	{
		LoadChildren(file);
	}
	public TencBox TrackEncryption => GetChild<TencBox>();
	protected override void Render(Stream file)
	{
		return;
	}
}
