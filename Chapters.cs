using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AAXClean
{
    public class ChapterInfo : IEnumerable<Chapter>
    {
        private readonly List<Chapter> _chapterList = new();
        public IEnumerable<Chapter> Chapters => _chapterList.AsEnumerable();
        public int Count => _chapterList.Count;
        public int RenderSize => _chapterList.Sum(c => c.RenderSize);
        public ChapterInfo() { }

        public void AddChapter(Chapter chapter)
        {
            if (chapter == null)
                throw new ArgumentNullException(nameof(chapter));

            _chapterList.Add(chapter);
        }
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
    public class Chapter
    {
        public string Title { get; }
        public TimeSpan StartOffset { get; }
        public TimeSpan EndOffset { get; }

        public int RenderSize => 2 + Title.Length + 12;
        public Chapter(string title, TimeSpan start, TimeSpan duration)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            Title = title;
            StartOffset = start;
            EndOffset = start + duration;
        }
        public Chapter(string title, long startOffsetMs, long lengthMs)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            if (startOffsetMs < 0)
                throw new ArgumentNullException(nameof(startOffsetMs));
            // do not validate lengthMs for '> 0'. It is valid to set sections this way. eg: 11-22-63 [B005UR3VFO] by Stephen King

            Title = title;
            StartOffset = TimeSpan.FromMilliseconds(startOffsetMs);
            EndOffset = StartOffset + TimeSpan.FromMilliseconds(lengthMs);
        }
        public Chapter(string title, double startTimeSec, double endTimeSec)
            : this(title, (long)(startTimeSec * 1000), (long)((endTimeSec - startTimeSec) * 1000))
        {
        }
    }
}
