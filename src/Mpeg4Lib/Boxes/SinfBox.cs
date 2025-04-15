using System.IO;
using System.Linq;

#nullable enable
namespace Mpeg4Lib.Boxes;

public class SinfBox : Box
{
    public SinfBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
    {
        LoadChildren(file);
    }
    public FrmaBox OriginalFormat => GetChild<FrmaBox>();
    public SchmBox? SchemeType => GetChildren<SchmBox>()?.SingleOrDefault();
    public SchiBox? SchemeInformation => GetChildren<SchiBox>()?.SingleOrDefault();
    protected override void Render(Stream file)
    {
        return;
    }
}
