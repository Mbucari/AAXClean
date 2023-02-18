using Mpeg4Lib.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	public class Stsz32 : StszBase
	{
		public override int MaxSize => _sampleSizes.Max();
		public override long TotalSize => _sampleSizes.Sum(s => (long)s);
		public override int GetSizeAtIndex(int index) => _sampleSizes[index];
		public override void AddSampleSize(int sampleSize) => _sampleSizes.Add(sampleSize);
		public override long SumFirstNSizes(int firstN) => _sampleSizes.Take(firstN).Sum(s => (long)s);

		private readonly List<int> _sampleSizes;

		public Stsz32(Stream file, BoxHeader header, Box parent)
			: base(file, header, parent)
		{
			SampleSizes = _sampleSizes = new List<int>((int)SampleCount);

			for (int i = 0; i < SampleCount; i++)
			{
				int sampleSize = file.ReadInt32BE();
				_sampleSizes.Add(sampleSize);
			}
		}

		public static StszBase CreateBlank(Box parent)
		{
			int size = 8 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stsz");

			var stszBox = new Stsz32(new byte[] { 0, 0, 0, 0 }, header, parent);

			parent.Children.Add(stszBox);
			return stszBox;
		}

		protected Stsz32(byte[] versionFlags, BoxHeader header, Box parent)
			: base(versionFlags, header, parent)
		{
			SampleSizes = _sampleSizes = new List<int>();
		}
	}

	public class Stsz16 : StszBase
	{
		private const string HeaderName = "stz2";
		public override int MaxSize => _sampleSizes.Max();
		public override long TotalSize => _sampleSizes.Sum(s => (long)s);
		public override int GetSizeAtIndex(int index) => _sampleSizes[index];
		public override void AddSampleSize(int sampleSize) => _sampleSizes.Add((short)sampleSize);
		public override long SumFirstNSizes(int firstN) => _sampleSizes.Take(firstN).Sum(s => (long)s);
		public int FieldSize { get; }

		private readonly List<short> _sampleSizes;

		public Stsz16(Stream file, BoxHeader header, Box parent)
			: base(file, header, parent)
		{
			SampleSizes = _sampleSizes = new List<short>((int)SampleCount);
			FieldSize = (byte)SampleSize;

			if (FieldSize != 16) throw new InvalidDataException("field_size must be 16 bits");

			for (int i = 0; i < SampleCount; i++)
			{
				file.ReadInt16BE();
				_sampleSizes.Add(file.ReadInt16BE());
			}
			Header.Type = HeaderName;
		}

		public static StszBase CreateBlank(Box parent)
		{
			int size = 8 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stsz");

			var stszBox = new Stsz16(new byte[] { 0, 0, 0, 0 }, header, parent);

			parent.Children.Add(stszBox);
			return stszBox;
		}
		private Stsz16(byte[] versionFlags, BoxHeader header, Box parent)
			: base(versionFlags, header, parent)
		{
			SampleSizes = _sampleSizes = new List<short>();
		}
	}

	public abstract class StszBase : FullBox
	{
		public override long RenderSize => base.RenderSize + 8 + SampleSizes.Count * 4;
		public uint SampleSize { get; }
		public uint SampleCount { get; set; }
		public IList SampleSizes { get; protected init; }

		protected StszBase(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }

		public abstract int MaxSize { get; }
		public abstract long TotalSize { get; }
		public abstract int GetSizeAtIndex(int index);
		public abstract void AddSampleSize(int sampleSize);
		public abstract long SumFirstNSizes(int firstN);

		protected StszBase(Stream file, BoxHeader header, Box parent)
			: base(file, header, parent)
		{
			SampleSize = file.ReadUInt32BE();
			SampleCount = file.ReadUInt32BE();

			if (SampleSize > 0) return;
			if (SampleCount > int.MaxValue)
				throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} samples");
		}

		/// <summary>
		/// Retrieves the frame sizes for <paramref name="numFrames"/> frames starting at <paramref name="firstFrameIndex"/>
		/// </summary>
		/// <param name="firstFrameIndex">The first frame to retrieve the size of</param>
		/// <param name="numFrames">The number of frames to retrieve sizes of</param>
		/// <returns>A tuple containing the size of each frame in an int[] and the total size of all the frames. </returns>
		public (int[] frameSizes, int framesSizeTotal) GetFrameSizes(uint firstFrameIndex, uint numFrames)
		{
			int[] frameSizes = new int[numFrames];
			int framesSizeTotal = 0;

			for (uint i = 0; i < numFrames; i++)
			{
				if (i + firstFrameIndex >= SampleSizes.Count)
				{
					//This handels a case where the last Stsc entry was not written correctly.
					//This is only necessary to correct for an early error in Mpeg4Lib when
					//decrypting to m4b. This bug was introduced to Libation in 5.1.9 and was
					//fixed in 5.4.4. All m4b files created in affected versions will fail to
					//convert to mp3 without this check.
					int[] correctFrameSizes = new int[i];
					Array.Copy(frameSizes, 0, correctFrameSizes, 0, i);
					return (frameSizes, framesSizeTotal);
				}

				frameSizes[i] = GetSizeAtIndex((int)(i + firstFrameIndex));
				framesSizeTotal += frameSizes[i];
			}

			return (frameSizes, framesSizeTotal);
		}

		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE(SampleSize);
			file.WriteUInt32BE((uint)SampleSizes.Count);
			foreach (int sampleSize in SampleSizes)
			{
				file.WriteInt32BE(sampleSize);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				SampleSizes.Clear();
			base.Dispose(disposing);
		}
	}
}
