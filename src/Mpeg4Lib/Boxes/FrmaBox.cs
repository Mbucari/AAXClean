using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class FrmaBox : Box
{
	public override long RenderSize => base.RenderSize + 4;
	public string DataFormat { get; }
	public FrmaBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		DataFormat = file.ReadType();
	}

	protected override void Render(Stream file)
	{
		file.WriteType(DataFormat);
	}
}
