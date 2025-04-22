using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

public class SttsBox : FullBox
{
	public override long RenderSize => base.RenderSize + 4 + EntryCount * 2 * 4;
	public int EntryCount => Samples.Count;
	public List<SampleEntry> Samples { get; } = new List<SampleEntry>();

	public static SttsBox CreateBlank(IBox parent)
	{
		int size = 4 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "stts");

		SttsBox sttsBox = new SttsBox([0, 0, 0, 0], header, parent);

		parent.Children.Add(sttsBox);
		return sttsBox;
	}
	private SttsBox(byte[] versionFlags, BoxHeader header, IBox? parent)
		: base(versionFlags, header, parent) { }

	public SttsBox(Stream file, BoxHeader header, IBox? parent)
		: base(file, header, parent)
	{
		var entryCount = file.ReadUInt32BE();

		for (int i = 0; i < entryCount; i++)
		{
			Samples.Add(new SampleEntry(file));
		}
	}

	public uint SampleTimeCount => (uint)Samples.Sum(s => s.FrameCount);

	public IEnumerable<uint> EnumerateFrameDeltas(uint startAt = 0)
	{
		foreach (SampleEntry entry in Samples)
		{
			while (startAt < entry.FrameCount)
			{
				yield return entry.FrameDelta;
				startAt++;
			}

			startAt -= entry.FrameCount;
		}
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

			beginDelta += entry.FrameCount * (ulong)entry.FrameDelta;
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
