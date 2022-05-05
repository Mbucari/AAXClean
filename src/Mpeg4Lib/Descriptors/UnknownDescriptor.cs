using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Descriptors
{
	public class UnknownDescriptor : BaseDescriptor
	{
		public override uint RenderSize => base.RenderSize + (uint)Blob.Length;
		private readonly byte[] Blob;
		public UnknownDescriptor(byte tag, Stream file) : base(tag, file)
		{
			Blob = file.ReadBlock(Size);
		}
		public override void Render(Stream file)
		{
			file.Write(Blob);
		}
	}
}
