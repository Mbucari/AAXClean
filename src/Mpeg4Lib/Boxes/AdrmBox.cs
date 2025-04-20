using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class AdrmBox : Box
{
	public override long RenderSize => base.RenderSize + beginBlob.Length + DrmBlob.Length + middleBlob.Length + Checksum.Length + endBlob.Length;
	private readonly byte[] beginBlob;
	public byte[] DrmBlob { get; }
	private readonly byte[] middleBlob;
	public byte[] Checksum { get; }
	private readonly byte[] endBlob;

	public AdrmBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		beginBlob = file.ReadBlock(8);
		DrmBlob = file.ReadBlock(56);
		middleBlob = file.ReadBlock(4);
		Checksum = file.ReadBlock(20);
		long len = RemainingBoxLength(file);
		endBlob = file.ReadBlock((int)len);
	}

	protected override void Render(Stream file)
	{
		file.Write(beginBlob);
		file.Write(DrmBlob);
		file.Write(middleBlob);
		file.Write(Checksum);
		file.Write(endBlob);
	}
}
