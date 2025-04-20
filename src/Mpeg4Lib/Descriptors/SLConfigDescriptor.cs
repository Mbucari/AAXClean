using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Descriptors;

internal class SLConfigDescriptor : BaseDescriptor
{
	public override uint RenderSize => base.RenderSize + 1 + (uint)Blob.Length;
	private readonly byte[] Blob;
	public int Predefined { get; set; }

	public SLConfigDescriptor(Stream file) : base(6, file)
	{
		Predefined = file.ReadByte();
		Blob = file.ReadBlock(Size - 1);
	}
	public override void Render(Stream file)
	{
		file.WriteByte((byte)Predefined);
		file.Write(Blob);
	}
}
