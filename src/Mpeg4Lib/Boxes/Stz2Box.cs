using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	public class Stz2Box : FullBox, IStszBox
	{
		public override long RenderSize => base.RenderSize + 8 + SampleCount * FieldSize / 8 + (FieldSize == 4 && SampleCount % 2 == 1 ? 1 : 0);
		public uint SampleSize { get; }
		public int SampleCount => SampleSizes.Count;
		public List<ushort> SampleSizes { get; protected init; }
		public int MaxSize => SampleSizes.Max();
		public long TotalSize => SampleSizes.Sum(s => (long)s);
		public int GetSizeAtIndex(int index) => SampleSizes[index];
		public long SumFirstNSizes(int firstN) => SampleSizes.Take(firstN).Sum(s => (long)s);
		public int FieldSize => (int)SampleSize & 0xff;

		public Stz2Box(Stream file, BoxHeader header, IBox parent)
			: base(file, header, parent)
		{
			SampleSize = file.ReadUInt32BE();
			var sampleCount = file.ReadUInt32BE();

			if (FieldSize != 16 && FieldSize != 8 && FieldSize != 4)
				throw new InvalidDataException($"Stsz field size ({FieldSize}). Valid values are 4, 8, or 16.");
			if (sampleCount > int.MaxValue)
				throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} samples");

			SampleSizes = new List<ushort>((int)sampleCount);

			for (int i = 0; i < sampleCount; i++)
			{
				switch (FieldSize)
				{
					case 16:
						SampleSizes.Add(file.ReadUInt16BE());
						break;
					case 8:
						SampleSizes.Add((ushort)file.ReadByte());
						break;
					case 4:
						//If the value 4 is used, then each byte contains two values
						var twoValues = file.ReadByte();
						SampleSizes.Add((ushort)(twoValues >> 4));

						//if the sizes do not fill an integral number of bytes, the last byte is padded with zeros.
						if ((twoValues & 7) > 0)
							SampleSizes.Add((ushort)(twoValues & 7));
						break;
				}
			}
		}

		private Stz2Box(byte[] versionFlags, BoxHeader header, IBox parent, List<ushort> sampleSizes, int fieldSize)
			: base(versionFlags, header, parent)
		{
			if (fieldSize != 16 && fieldSize != 8 && fieldSize != 4)
				throw new InvalidDataException($"Stsz field size ({fieldSize}). Valid values are 4, 8, or 16.");

			SampleSize = (uint)fieldSize;
			SampleSizes = sampleSizes;
		}

		public static Stz2Box CreateBlank(IBox parent, List<ushort> sampleSizes, int fieldSize)
		{
			int size = 8 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stz2");

			var stszBox = new Stz2Box(new byte[] { 0, 0, 0, 0 }, header, parent, sampleSizes, fieldSize);

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
			file.Write(stackalloc byte[3]); //reserved
			file.WriteByte((byte)FieldSize);
			file.WriteInt32BE(SampleSizes.Count);
			for (int i = 0; i < SampleSizes.Count; i++)
			{
				switch (FieldSize)
				{
					case 16:
						file.WriteUInt16BE(SampleSizes[i]);
						break;
					case 8:
						file.WriteByte((byte)SampleSizes[i]);
						break;
					case 4:
						var twoValues = SampleSizes[i] << 4;
						if (i + 1 < SampleSizes.Count)
							twoValues |= SampleSizes[i + 1];
						i++;
						file.WriteByte((byte)twoValues);
						break;
				}
			}
		}
	}
}
