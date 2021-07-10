using System.IO;

namespace AAXClean.Boxes
{
    internal class AppleTagBox : Box
    {
        public static AppleTagBox Create(AppleListBox parent, string name, byte[] data)
        {
            int size = data.Length + 12 /* empty FullBox size*/ + 8 /* empty Box size*/ ;
            var header = new BoxHeader((uint)size, name);

            var tagBox = new AppleTagBox(header, parent);
            parent.Children.Add(tagBox);
            return tagBox;
        }
        private AppleTagBox(BoxHeader header, Box parent) : base(header, parent) { }

        internal AppleTagBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            LoadChildren(file);
        }
        public AppleDataBox Data => GetChild<AppleDataBox>();
        protected override void Render(Stream file)
        {
            return;
        }
    }
}
