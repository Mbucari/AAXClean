using System.IO;

namespace AAXClean.Boxes
{
    internal class UdtaBox : Box
    {
        internal UdtaBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            LoadChildren(file);
        }

        protected override void Render(Stream file)
        {
            return;
        }
    }
}
