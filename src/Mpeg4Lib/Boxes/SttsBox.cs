using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class SttsBox : FullBox
	{
		public override long RenderSize => base.RenderSize + 4 + Samples.Count * 2 * 4;
		public uint EntryCount { get; set; }
		public List<SampleEntry> Samples { get; } = new List<SampleEntry>();

		public static SttsBox CreateBlank(Box parent)
		{
			int size = 4 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stts");

			SttsBox sttsBox = new SttsBox(new byte[] { 0, 0, 0, 0 }, header, parent);

			parent.Children.Add(sttsBox);
			return sttsBox;
		}
		private SttsBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }

		public SttsBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			EntryCount = file.ReadUInt32BE();

			for (int i = 0; i < EntryCount; i++)
			{
				Samples.Add(new SampleEntry(file));
			}
		}

		public uint FrameToFrameDelta(uint frameIndex)
		{
			uint workingIndex = frameIndex;
			foreach (SampleEntry entry in Samples)
			{
				if (workingIndex < entry.FrameCount)
				{
					return entry.FrameDelta;
				}

				workingIndex -= entry.FrameCount;
			}
			throw new IndexOutOfRangeException($"{nameof(frameIndex)} {frameIndex} is larger than the number of frames in {nameof(SttsBox)}");
		}

		/// <summary>
		/// Gets the playback timestamp of an audio frame.
		/// </summary>
		public TimeSpan FrameToTime(double timeScale, ulong frameIndex)
		{
			ulong beginDelta = 0, workingIndex = frameIndex;

			foreach (SampleEntry entry in Samples)
			{
				if (workingIndex <= entry.FrameCount)
				{
					return TimeSpan.FromSeconds((beginDelta + workingIndex * entry.FrameDelta) / timeScale);
				}

				beginDelta += entry.FrameCount * entry.FrameDelta;
				workingIndex -= entry.FrameCount;
			}
			throw new IndexOutOfRangeException($"{nameof(frameIndex)} {frameIndex} is larger than the number of frames in {nameof(SttsBox)}");
		}

		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE((uint)Samples.Count);
			foreach (SampleEntry sample in Samples)
			{
				file.WriteUInt32BE(sample.FrameCount);
				file.WriteUInt32BE(sample.FrameDelta);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				Samples.Clear();
				base.Dispose(disposing);
		}

		public class SampleEntry
		{
			public SampleEntry(Stream file)
			{
				FrameCount = file.ReadUInt32BE();
				FrameDelta = file.ReadUInt32BE();
			}
			public SampleEntry(uint sampleCount, uint sampleDelta)
			{
				FrameCount = sampleCount;
				FrameDelta = sampleDelta;
			}
			public uint FrameCount { get; }
			public uint FrameDelta { get; }
		}
	}

}
