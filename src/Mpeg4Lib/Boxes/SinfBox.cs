using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

public class SinfBox : Box
{
	public SinfBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}
	public FrmaBox OriginalFormat => GetChildOrThrow<FrmaBox>();
	public SchmBox? SchemeType => GetChildren<SchmBox>()?.SingleOrDefault();
	public SchiBox? SchemeInformation => GetChildren<SchiBox>()?.SingleOrDefault();
	protected override void Render(Stream file)
	{
		return;
	}
}
