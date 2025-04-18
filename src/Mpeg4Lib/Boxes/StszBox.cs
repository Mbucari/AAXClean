using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	/*
	 * When reading stsz from a stream, begin by assuming all sample sizes are <= ushort.MaxValue
	 * to save on memory. If a sample size > ushort.MaxValue is encountered, convert to a List<int>.
	 */
	public class StszBox : FullBox, IStszBox
	{
		public override long RenderSize => base.RenderSize + 8 + SampleCount * sizeof(int);
		public uint SampleSize { get; }
		public int SampleCount => sampleSizes_32?.Count ?? sampleSizes_16.Count;
		public int MaxSize => sampleSizes_32?.Max() ?? sampleSizes_16.Max();
		public long TotalSize => sampleSizes_32?.Sum(s => (long)s) ?? sampleSizes_16.Sum(s => (long)s);
		public int GetSizeAtIndex(int index) => sampleSizes_32 is null ? sampleSizes_16[index] : sampleSizes_32[index];
		public long SumFirstNSizes(int firstN) => sampleSizes_32?.Take(firstN).Sum(s => (long)s) ?? sampleSizes_16.Take(firstN).Sum(s => (long)s);

		private readonly List<int> sampleSizes_32;
		private readonly List<ushort> sampleSizes_16;

		public StszBox(Stream file, BoxHeader header, IBox parent)
			: base(file, header, parent)
		{
			SampleSize = file.ReadUInt32BE();
			var sampleCount = file.ReadUInt32BE();

			if (SampleSize > 0) return;
			if (sampleCount > int.MaxValue)
				throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} samples");

			sampleSizes_16 = new List<ushort>((int)sampleCount);

			for (int i = 0; i < sampleCount; i++)
			{
				var value = file.ReadInt32BE();

				if (value > ushort.MaxValue && sampleSizes_32 is null)
				{
					//We encountered a sample size larger than ushort.MaxValue,
					//so we need the full 32-bit stsz box sizes.
					sampleSizes_32 = new List<int>((int)sampleCount);
					sampleSizes_32.AddRange(sampleSizes_16.Select(s => (int)s));
				}

				if (sampleSizes_32 is null)
					sampleSizes_16.Add((ushort)value);
				else
					sampleSizes_32.Add(value);
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

			var stszBox = new StszBox(new byte[] { 0, 0, 0, 0 }, header, parent, sampleSizes);

			parent.Children.Add(stszBox);
			return stszBox;
		}

		public static StszBox CreateBlank(IBox parent, List<ushort> sampleSizes)
		{
			int size = 8 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stsz");

			var stszBox = new StszBox(new byte[] { 0, 0, 0, 0 }, header, parent, sampleSizes);

			parent.Children.Add(stszBox);
			return stszBox;
		}

		public (int[] frameSizes, int framesSizeTotal) GetFrameSizes(uint firstFrameIndex, uint numFrames)
		{
			int[] frameSizes = new int[numFrames];
			int framesSizeTotal = 0;

			for (uint i = 0; i < numFrames; i++)
			{
				frameSizes[i] = GetSizeAtIndex((int)(i + firstFrameIndex));
				framesSizeTotal += frameSizes[i];
			}

			return (frameSizes, framesSizeTotal);
		}

		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE(SampleSize);
			file.WriteUInt32BE((uint)SampleCount);

			foreach (var sampleSize in sampleSizes_32 ?? sampleSizes_16.Select(s => (int)s))
			{
				file.WriteInt32BE(sampleSize);
			}
		}
	}
}
