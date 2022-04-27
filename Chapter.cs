using System;
using System.IO;
using System.Text;
using AAXClean.Util;

namespace AAXClean
{
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
        public override string ToString()
        {
            return $"{Title} {{{StartOffset.TotalSeconds:F6} - {EndOffset.TotalSeconds:F6}}}";
        }

        //This is constant folr UTF-8 text
        //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/movenc.c
        private static readonly byte[] encd = { 0, 0, 0, 0xc, (byte)'e', (byte)'n', (byte)'c', (byte)'d', 0, 0, 1, 0 };
    }
}
