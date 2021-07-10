using System.IO;

namespace AAXClean.Boxes
{
    internal class AppleListBox : Box
    {
        internal AppleListBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            long endPos = Header.FilePosition + Header.TotalBoxSize;

            while (file.Position < endPos)
            {
                var appleTag = new AppleTagBox(file, new BoxHeader(file), this);

                if (appleTag.Header.TotalBoxSize == 0)
                    break;
                Children.Add(appleTag);
            }
        }

        protected override void Render(Stream file)
        {
            return;
        }
    }
}
