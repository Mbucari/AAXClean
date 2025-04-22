using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Descriptors;

public class UnknownDescriptor : BaseDescriptor
{
	private readonly byte[] Blob;
	public override int InternalSize => base.InternalSize + Blob.Length;
	public UnknownDescriptor(Stream file, DescriptorHeader header) : base(file, header)
	{
		Blob = file.ReadBlock(Header.TotalBoxSize - Header.HeaderSize);
	}
	public override void Render(Stream file)
	{
		file.Write(Blob);
	}
}
