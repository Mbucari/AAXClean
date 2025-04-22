using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Descriptors;

internal class SLConfigDescriptor : BaseDescriptor
{
	private readonly byte[] Blob;
	public int Predefined { get; set; }
	public override int InternalSize => base.InternalSize + 1 + Blob.Length;

	public SLConfigDescriptor(Stream file, DescriptorHeader header) : base(file, header)
	{
		Predefined = file.ReadByte();
		Blob = file.ReadBlock(Header.TotalBoxSize - Header.HeaderSize - 1);
	}
	private SLConfigDescriptor(byte predefined, byte[] blob) : base(6)
	{
		Predefined = predefined;
		Blob = blob;
	}

	public static SLConfigDescriptor CreateMp4()
		=> new SLConfigDescriptor(2, []);

	public override void Render(Stream file)
	{
		file.WriteByte((byte)Predefined);
		file.Write(Blob);
	}
}
