using AAXClean.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAXClean
{
    public sealed class AppleTags
    {
        private readonly AppleListBox IList;
        internal IEnumerable<AppleTagBox> Tags => IList.GetChildren<AppleTagBox>();
        public IEnumerable<string> TagNames => Tags.Select(t => t.Header.Type);
        internal AppleTags(AppleListBox appleListBox)
        {
            IList = appleListBox;
        }

        public string FirstAuthor => Performers?.Split(';')?[0];
        public string TitleSansUnabridged => Title?.Replace(" (Unabridged)", "");

        public string BookCopyright => _copyright is not null && _copyright.Length > 0 ? _copyright[0] : default;
        public string RecordingCopyright => _copyright is not null && _copyright.Length > 1 ? _copyright[1] : default;
        private string[] _copyright => Copyright?.Replace("&#169;", "©")?.Split(';');

        public string Title { get => GetTagString("©nam"); set => EditOrAddTag("©nam", value); }
        public string Performers { get => GetTagString("©ART"); set => EditOrAddTag("©ART", value); }
        public string AlbumArtists { get => GetTagString("aART"); set => EditOrAddTag("aART", value); }
        public string Album { get => GetTagString("©alb"); set => EditOrAddTag("©alb", value); }
        public string Generes { get => GetTagString("©gen"); set => EditOrAddTag("©gen", value); }
        public string ProductID { get => GetTagString("prID"); set => EditOrAddTag("prID", value); }
        public string Comment { get => GetTagString("©cmt"); set => EditOrAddTag("©cmt", value); }
        public string LongDescription { get => GetTagString("©des"); set => EditOrAddTag("©des", value); }
        public string Copyright { get => GetTagString("cprt"); set => EditOrAddTag("cprt", value); }
        public string Publisher { get => GetTagString("©pub"); set => EditOrAddTag("©pub", value); }
        public string Year { get => GetTagString("©day"); set => EditOrAddTag("©day", value); }
        public string Narrator { get => GetTagString("©nrt"); set => EditOrAddTag("©nrt", value); }
        public string Asin { get => GetTagString("CDEK"); set => EditOrAddTag("CDEK", value); }
        public string ReleaseDate { get => GetTagString("rldt"); set => EditOrAddTag("rldt", value); }
        public byte[] Cover { get => GetTagBytes("covr"); set => EditOrAddTag("covr", value, AppleDataType.ContainsJpegData); }

        public void EditOrAddTag(string name, string data)
        {
            EditOrAddTag(name, Encoding.UTF8.GetBytes(data), AppleDataType.ContainsText);
        }

        public void EditOrAddTag(string name, byte[] data)
        {
            EditOrAddTag(name, data, AppleDataType.ContainsData);
        }

        private void EditOrAddTag(string name, byte[] data, AppleDataType type)
        {
            var tag = Tags.Where(t => t.Header.Type == name).FirstOrDefault();

            if (tag is null)
            {
                AddTag(name, data, type);
            }
            else if (tag.Data.DataType == type)
            {
                tag.Data.Data = data;
            }
        }

        public void AddTag(string name, string data)
        {
            AddTag(name, Encoding.UTF8.GetBytes(data), AppleDataType.ContainsText);
        }

        public void AddTag(string name, byte[] data)
        {
            AddTag(name, data, AppleDataType.ContainsData);
        }

        private void AddTag(string name, byte[] data, AppleDataType type)
        {
            AppleTagBox.Create(IList, name, data, type);
        }

        public string GetTagString(string name)
        {
            var tag = GetTagBytes(name);
            if (tag is null) return null;

            return Encoding.UTF8.GetString(tag);
        }

        public byte[] GetTagBytes(string name)
        {
            var tag = Tags.Where(t => t.Header.Type == name).FirstOrDefault();

            if (tag is null) return null;

            var tagData = tag.GetChild<AppleDataBox>();

            if (tagData is null) return null;

            return tagData.Data;
        }

        [Obsolete("This method has been deprecated. Please use the Cover property.")]
        public void SetCoverArt(byte[] coverArt)
        {
            Cover = coverArt;
        }
    }
}
