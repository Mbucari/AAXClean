using Mpeg4Lib.Util;
using System;
using System.IO;
using System.Text;

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
		var bts = Encoding.UTF8.GetBytes(DataFormat);
		Array.Resize(ref bts, 4);
		file.Write(bts);
	}
}
