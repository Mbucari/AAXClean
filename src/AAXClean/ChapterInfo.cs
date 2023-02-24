using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AAXClean
{
	public class ChapterInfo : IEnumerable<Chapter>
	{
		public TimeSpan StartOffset { get; }
		public TimeSpan EndOffset => Count == 0 ? StartOffset : _chapterList.Max(c => c.EndOffset);

		private readonly List<Chapter> _chapterList = new();
		public IReadOnlyList<Chapter> Chapters => _chapterList;
		public int Count => _chapterList.Count;
		public int RenderSize => _chapterList.Sum(c => c.RenderSize);

		public ChapterInfo(TimeSpan offsetFromBeginning = default) => StartOffset = offsetFromBeginning;

		public void AddChapter(string title, TimeSpan duration)
		{
			TimeSpan starttime = Count == 0 ? StartOffset : _chapterList[^1].EndOffset;

			_chapterList.Add(new Chapter(title, starttime, duration));
		}
		public void Add(string title, TimeSpan duration) => AddChapter(title, duration);

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
