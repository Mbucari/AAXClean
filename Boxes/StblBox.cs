using System.IO;

namespace AAXClean.Boxes
{
    internal class StblBox : Box
    {
        internal StblBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
        {
            LoadChildren(file);
        }
        public StsdBox Stsd => GetChild<StsdBox>();
        public SttsBox Stts => GetChild<SttsBox>();
        public StcoBox Stco => GetChild<StcoBox>();
        public StszBox Stsz => GetChild<StszBox>();
        public StscBox Stsc => GetChild<StscBox>();

        protected override void Render(Stream file)
        {
            return;
        }
    }
}