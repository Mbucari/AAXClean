using AAXClean.FrameFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AAXClean
{
	public class ChapterEntry
	{
		public Memory<byte> FrameData { get; init; }
		public uint SamplesInFrame { get; init; }
		public string Title { get; init; }
	}

	/// <summary>
	/// Chapters to be written to an mp4 file.
	/// </summary>
	public class ChapterQueue
	{
		private int subtractNext = 0;
		private readonly double SampleScaleFactor;
		private readonly SampleRate OutputSampleRate;
		private readonly object lockObj = new();
		private readonly Queue<ChapterEntry> chapterEntries = new();

		public ChapterQueue(SampleRate inputRate, SampleRate outputRate)
		{
			OutputSampleRate = outputRate;
			SampleScaleFactor = (double)outputRate / (double)inputRate;
		}

		public bool TryGetNextChapter(out ChapterEntry chapterEntry)
		{
			lock (lockObj)
			{
				if (chapterEntries.Count > 0)
				{
					chapterEntry = chapterEntries.Dequeue();
					return true;
				}
			}

			chapterEntry = null;
			return false;
		}

		public void AddRange(IEnumerable<Chapter> chapters)
		{
			foreach (var ch in chapters)
				Add(ch);
		}

		/// <summary>
		/// Add a user-defined chapter
		/// </summary>
		public void Add(Chapter chapter)
		{
			byte[] frameData = new byte[chapter.RenderSize];

			using var ms = new MemoryStream(frameData);
			chapter.WriteChapter(ms);
			uint sampleDelta = (uint)(chapter.Duration.TotalSeconds * (int)OutputSampleRate);

			lock (lockObj)
			{
				chapterEntries.Enqueue(new ChapterEntry
				{
					FrameData = frameData,
					SamplesInFrame = sampleDelta,
					Title = chapter.Title
				});
			}
		}

		/// <summary>
		/// Add a chapter read directly from the timed text track.
		/// </summary>
		public void Add(FrameEntry entry)
		{
			Span<byte> frameData = entry.FrameData.Span;
			int size = frameData[1] | frameData[0];
			string title = Encoding.UTF8.GetString(frameData.Slice(2, size));

			//Takes care of 'negative' sample deltas in malformed Stts entries (e.g. Broken Angels)
			var sif = (int)entry.SamplesInFrame;

			lock (lockObj)
			{
				chapterEntries.Enqueue(new ChapterEntry
				{
					FrameData = entry.FrameData,
					SamplesInFrame = (uint)(Math.Max(0, sif + subtractNext) * SampleScaleFactor),
					Title = title
				});
			}

			subtractNext = sif < 0 ? sif : 0;
		}
	}
}
