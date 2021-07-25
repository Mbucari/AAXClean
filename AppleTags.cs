using AAXClean.Boxes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAXClean
{
    public class AppleTags
    {
        private AppleListBox iList { get; }
        internal IEnumerable<AppleTagBox> Tags => iList.GetChildren<AppleTagBox>();
        public IEnumerable<string> TagNames => Tags.Select(t => t.Header.Type);
        internal AppleTags(AppleListBox appleListBox)
        {
            iList = appleListBox;
        }

        public string FirstAuthor => Performers?.Split(';')?[0];
        public string TitleSansUnabridged => Title?.Replace(" (Unabridged)", "");

        public string BookCopyright => _copyright is not null && _copyright.Length > 0 ? _copyright[0] : default;
        public string RecordingCopyright => _copyright is not null && _copyright.Length > 1 ? _copyright[1] : default;
        private string[] _copyright => Copyright?.Replace("&#169;", string.Empty)?.Replace("(P)", string.Empty)?.Split(';');

        public string Title => GetTagString("©nam");
        public string Performers => GetTagString("©ART");
        public string AlbumArtists => GetTagString("aART");
        public string Album => GetTagString("©alb");
        public string Generes => GetTagString("©gen");
        public string ProductID => GetTagString("prID");
        public string Comment => GetTagString("©cmt");
        public string LongDescription => GetTagString("©des");
        public string Copyright => GetTagString("cprt");
        public string Publisher => GetTagString("©pub");
        public string Year => GetTagString("©day");
        public string Narrator => GetTagString("©nrt");
        public string Asin => GetTagString("CDEK");
        public string ReleaseDate => GetTagString("rldt");
        public byte[] Cover => GetTag("covr");

        public void AddTag(string name, string data)
        {
            AddTag(name, Encoding.UTF8.GetBytes(data), AppleDataBox.FlagType.ContainsText);
        }
        public void AddTag(string name, byte[] data)
        {
            AddTag(name, data, AppleDataBox.FlagType.ContainsData);
        }
        private void AddTag(string name, byte[] data, AppleDataBox.FlagType type)
        {
            AppleTagBox.Create(iList, name, data, type);
        }

        public string GetTagString(string name)
        {
            var tag = GetTag(name);
            if (tag is null) return null;

            return Encoding.UTF8.GetString(tag);
        }

        public byte[] GetTag(string name)
        {
            var tag = Tags.Where(t => t.Header.Type == name).FirstOrDefault();

            if (tag is null) return null;

            var tagData = tag.GetChild<AppleDataBox>();

            if (tagData is null) return null;

            return tagData.Data;
        }

        public void SetCoverArt(byte[] coverArt)
        {
            if (coverArt is null) return;

            var covr = Tags.Where(t => t.Header.Type == "covr").FirstOrDefault();

            if (covr is not null)
                covr.Data.Data = coverArt;
            else
                AddTag("covr", coverArt, AppleDataBox.FlagType.ContainsJpegData);
        }
    }
}
