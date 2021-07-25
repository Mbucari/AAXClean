using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AAXClean
{
    public class ChapterInfo : IEnumerable<Chapter>
    {
        private readonly List<Chapter> _chapterList = new();
        public IEnumerable<Chapter> Chapters => _chapterList.AsEnumerable();
        public int Count => _chapterList.Count;
        public int RenderSize => _chapterList.Sum(c => c.RenderSize);
        public ChapterInfo() { }

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
        public TimeSpan Duration { get; }
        public TimeSpan EndOffset => StartOffset + Duration;

        internal int RenderSize => 2 + Encoding.UTF8.GetByteCount(Title) + encd.Length;
        public Chapter(string title, TimeSpan start, TimeSpan duration)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            Title = title;
            StartOffset = start;
            Duration = duration;
        }

        internal void WriteChapter(Stream output)
        {
            byte[] title = Encoding.UTF8.GetBytes(Title);

            output.WriteInt16BE((short)title.Length);
            output.Write(title);
            output.Write(encd);
        }

        //This is constant folr UTF-8 text
        //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/movenc.c
        private static readonly byte[] encd = { 0, 0, 0, 0xc, (byte)'e', (byte)'n', (byte)'c', (byte)'d', 0, 0, 1, 0 };
    }
}
