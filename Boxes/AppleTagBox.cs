using System.IO;

namespace AAXClean.Boxes
{
    internal class AppleTagBox : Box
    {
        public static void Create(AppleListBox parent, string name, byte[] data, AppleDataBox.FlagType dataType)
        {
            int size = data.Length + 2 + 8 /* empty Box size*/ ;
            var header = new BoxHeader((uint)size, name);

            var tagBox = new AppleTagBox(header, parent);
            AppleDataBox.Create(tagBox, data, dataType);

            parent.Children.Add(tagBox);
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
