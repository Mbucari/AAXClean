using Mpeg4Lib.Util;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mpeg4Lib.Boxes;

public class Stz2Box : FullBox, IStszBox
{
	public override long RenderSize => base.RenderSize + 8 + SampleCount * FieldSize / 8 + (FieldSize == 4 && SampleCount % 2 == 1 ? 1 : 0);
	public int SampleCount => SampleSizes.Count;
	public List<ushort> SampleSizes { get; protected init; }
	public int MaxSize => SampleSizes.Max();
	public long TotalSize => SampleSizes.Sum(s => (long)s);
	public int GetSizeAtIndex(int index) => SampleSizes[index];
	public long SumFirstNSizes(int firstN) => SampleSizes.Take(firstN).Sum(s => (long)s);
	public int FieldSize { get; }

	public Stz2Box(Stream file, BoxHeader header, IBox? parent)
		: base(file, header, parent)
	{
		Span<byte> reserved = stackalloc byte[4];
		file.ReadExactly(reserved);
		FieldSize = reserved[3];
		var sampleCount = file.ReadUInt32BE();

		if (FieldSize is not (8 or 16))
			throw new InvalidDataException($"Stsz field size ({FieldSize}). Valid values are 4, 8, or 16.");
		if (sampleCount > int.MaxValue)
			throw new NotSupportedException($"Mpeg4Lib does not support MPEG-4 files with more than {int.MaxValue} samples");

		SampleSizes = new List<ushort>((int)sampleCount);
		CollectionsMarshal.SetCount(SampleSizes, (int)sampleCount);
		Span<ushort> shortSpan = CollectionsMarshal.AsSpan(SampleSizes);

		if (FieldSize is 16)
		{
			file.ReadExactly(MemoryMarshal.AsBytes(shortSpan));
			if (BitConverter.IsLittleEndian)
			{
				BinaryPrimitives.ReverseEndianness(shortSpan, shortSpan);
			}
		}
		else
		{
			Span<byte> bytes = file.ReadBlock((int)sampleCount);
			for (int i = 0; i < sampleCount; i++)
			{
				shortSpan[i] = bytes[i];
			}
		}
	}

	private Stz2Box(byte[] versionFlags, BoxHeader header, IBox parent, List<ushort> sampleSizes, int fieldSize)
		: base(versionFlags, header, parent)
	{
		if (fieldSize != 16 && fieldSize != 8 && fieldSize != 4)
			throw new InvalidDataException($"Stsz field size ({fieldSize}). Valid values are 4, 8, or 16.");

		FieldSize = fieldSize;
		SampleSizes = sampleSizes;
	}

	public static Stz2Box CreateBlank(IBox parent, List<ushort> sampleSizes)
	{
		int size = 8 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "stz2");

		var stszBox = new Stz2Box([0, 0, 0, 0], header, parent, sampleSizes, 16);

		parent.Children.Add(stszBox);
		return stszBox;
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		Span<byte> fieldSize = stackalloc byte[4];
		Span<ushort> shortSpan = CollectionsMarshal.AsSpan(SampleSizes);
		fieldSize[3] = (byte)(shortSpan.AllLessThanOrEqual(byte.MaxValue) ? 8 : 16);
		file.Write(fieldSize);
		file.WriteInt32BE(SampleSizes.Count);

		if (fieldSize[3] is 16)
		{
			if (BitConverter.IsLittleEndian)
			{
				BinaryPrimitives.ReverseEndianness(shortSpan, shortSpan);
			}
			file.Write(MemoryMarshal.AsBytes(shortSpan));
		}
		else
		{
			foreach (var size in SampleSizes)
			{
				file.WriteByte((byte)size);
			}
		}
	}
}
