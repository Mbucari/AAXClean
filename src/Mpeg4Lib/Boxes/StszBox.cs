using Mpeg4Lib.Util;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mpeg4Lib.Boxes;

/*
 * When reading stsz from a stream, begin by assuming all sample sizes are <= ushort.MaxValue
 * to save on memory. If a sample size > ushort.MaxValue is encountered, convert to a List<int>.
 */
public class StszBox : FullBox, IStszBox
{
	public override long RenderSize => base.RenderSize + 8 + SampleCount * sizeof(int);
	public int SampleSize { get; }
	private int origSampleCount;
	public int SampleCount => sampleSizes_32?.Count ?? sampleSizes_16?.Count ?? origSampleCount;
	public int MaxSize => sampleSizes_32?.Max() ?? sampleSizes_16?.Max() ?? SampleSize;
	public long TotalSize => sampleSizes_32?.Sum(s => (long)s) ?? sampleSizes_16?.Sum(s => (long)s) ?? SampleSize * origSampleCount;
	public int GetSizeAtIndex(int index) => sampleSizes_32?[index] ?? sampleSizes_16?[index] ?? SampleSize;
	public long SumFirstNSizes(int firstN) => sampleSizes_32?.Take(firstN).Sum(s => (long)s) ?? sampleSizes_16?.Take(firstN).Sum(s => (long)s) ?? (long)SampleSize * firstN;

	private readonly List<int>? sampleSizes_32;
	private readonly List<ushort>? sampleSizes_16;

	unsafe public StszBox(Stream file, BoxHeader header, IBox? parent)
		: base(file, header, parent)
	{
		SampleSize = file.ReadInt32BE();
		var sampleCountU = file.ReadUInt32BE();

		//Technically we're losing half the capacity by using a List<T> with int.MaxValue capacity, but at
		//1024 sample per frame and 44100 Hz, this still allows for > 577 days of audio.
		if (sampleCountU > int.MaxValue)
			throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} samples");

		origSampleCount = (int)sampleCountU;
		if (SampleSize > 0)
			return;

		var buffSize = origSampleCount * sizeof(int);
		sampleSizes_32 = new(origSampleCount);
		CollectionsMarshal.SetCount(sampleSizes_32, origSampleCount);
		Span<int> intListSpan = CollectionsMarshal.AsSpan(sampleSizes_32);

		file.ReadExactly(MemoryMarshal.AsBytes(intListSpan));

		if (BitConverter.IsLittleEndian)
		{
			BinaryPrimitives.ReverseEndianness(intListSpan, intListSpan);
		}

		if (intListSpan.AllLessThanOrEqual(ushort.MaxValue))
		{
			sampleSizes_16 = new(origSampleCount);
			CollectionsMarshal.SetCount(sampleSizes_16, origSampleCount);
			Span<ushort> shortListSpan = CollectionsMarshal.AsSpan(sampleSizes_16);
			for (int i = 0; i < origSampleCount; i++)
			{
				shortListSpan[i] = (ushort)intListSpan[i];
			}
			CollectionsMarshal.SetCount(sampleSizes_32, 0);
			sampleSizes_32 = null;
		}
	}

	private StszBox(byte[] versionFlags, BoxHeader header, IBox parent, List<int> sampleSizes)
		: base(versionFlags, header, parent)
	{
		sampleSizes_32 = sampleSizes;
	}

	private StszBox(byte[] versionFlags, BoxHeader header, IBox parent, List<ushort> sampleSizes)
		: base(versionFlags, header, parent)
	{
		sampleSizes_16 = sampleSizes;
	}

	public static StszBox CreateBlank(IBox parent, List<int> sampleSizes)
	{
		int size = 8 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "stsz");

		var stszBox = new StszBox([0, 0, 0, 0], header, parent, sampleSizes);

		parent.Children.Add(stszBox);
		return stszBox;
	}

	public static StszBox CreateBlank(IBox parent, List<ushort> sampleSizes)
	{
		int size = 8 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "stsz");

		var stszBox = new StszBox([0, 0, 0, 0], header, parent, sampleSizes);

		parent.Children.Add(stszBox);
		return stszBox;
	}

	protected unsafe override void Render(Stream file)
	{
		base.Render(file);
		file.WriteInt32BE(SampleSize);
		file.WriteUInt32BE((uint)SampleCount);

		if (sampleSizes_32 is not null)
		{
			Span<int> intSpan = CollectionsMarshal.AsSpan(sampleSizes_32);
			if (BitConverter.IsLittleEndian)
			{
				BinaryPrimitives.ReverseEndianness(intSpan, intSpan);
			}
			file.Write(MemoryMarshal.AsBytes(intSpan));
		}
		else if (sampleSizes_16 is not null)
		{
			foreach (var size in sampleSizes_16)
			{
				file.WriteInt32BE(size);
			}
		}
	}
}
