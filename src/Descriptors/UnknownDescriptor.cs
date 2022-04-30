using AAXClean.Util;
using System.IO;

namespace AAXClean.Descriptors
{
    internal class UnknownDescriptor : BaseDescriptor
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
