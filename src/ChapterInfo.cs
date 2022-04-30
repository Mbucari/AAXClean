using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AAXClean
{
    public class ChapterInfo : IEnumerable<Chapter>
    {
        private readonly List<Chapter> _chapterList = new();
        public IReadOnlyList<Chapter> Chapters => _chapterList;
        public int Count => _chapterList.Count;
        public int RenderSize => _chapterList.Sum(c => c.RenderSize);

        public void AddChapter(string title, TimeSpan duration)
        {
            TimeSpan starttime = _chapterList.Count == 0 ? TimeSpan.FromSeconds(0) : _chapterList[^1].EndOffset;

            _chapterList.Add(new Chapter(title, starttime, duration));
        }

        public IEnumerator<Chapter> GetEnumerator()
        {
            return _chapterList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
