using Mpeg4Lib.Util;
using System;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

public class SencBox : FullBox
{
	public override long RenderSize => base.RenderSize + 4 + IVs.Sum(iv => iv.Length);
	public bool UseSubSampleEncryption => (Flags & 2) == 2;
	public byte[][] IVs { get; }

	public SencBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
	{
		var sampleCount = file.ReadInt32BE();

		if (UseSubSampleEncryption)
			throw new NotSupportedException(nameof(UseSubSampleEncryption));

		var ivSize = (int)((header.TotalBoxSize - 16) / sampleCount);

		IVs = new byte[sampleCount][];

		for (int i = 0; i < sampleCount; i++)
		{
			IVs[i] = file.ReadBlock(ivSize);
		}
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteInt32BE(IVs.Length);
		foreach (var iv in IVs)
			file.Write(iv);
	}
}
