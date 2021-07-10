using System.IO;

namespace AAXClean.Boxes
{
    internal class MetaBox : FullBox
    {
        internal MetaBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
        {
            LoadChildren(file);
        }
    }
}
