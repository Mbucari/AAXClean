using Mpeg4Lib.Util;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mpeg4Lib.Boxes;

/// <summary>
/// A compact list of chunk offsets that can store both 32-bit and 64-bit offsets.
/// </summary>
public class ChunkOffsetList : ICollection<long>
{
	public int Count => chunkOffsets32.Count + (chunkOffsets64?.Count ?? 0);

	public bool IsReadOnly => false;

	private readonly List<uint> chunkOffsets32;
	private List<long>? chunkOffsets64;
	public ChunkOffsetList()
	{
		chunkOffsets32 = [];
	}
	private ChunkOffsetList(int capacity)
	{
		chunkOffsets32 = new List<uint>(capacity);
	}

	public void Clear()
	{
		chunkOffsets32.Clear();
		chunkOffsets64?.Clear();
		chunkOffsets64 = null;
	}

	public void Sort()
	{
		if (chunkOffsets64 is not null)
		{
			chunkOffsets64.Sort();
			int index = FindLast32bit(CollectionsMarshal.AsSpan(chunkOffsets64));
			if (index >= 0)
			{
				//Move 32-bit offsets back to chunkOffsets32
				int countToMove = index + 1;
				uint[] toMove = new uint[countToMove];
				for (int i = 0; i < countToMove; i++)
				{
					toMove[i] = (uint)chunkOffsets64[i];
				}
				chunkOffsets32.AddRange(toMove);
				chunkOffsets64.RemoveRange(0, countToMove);
				if (chunkOffsets64.Count == 0)
					chunkOffsets64 = null;
			}
		}

		chunkOffsets32.Sort();
	}

	public long GetOffsetAtIndex(int index)
	{
		if (index < chunkOffsets32.Count)
			return chunkOffsets32[index];
		else if (chunkOffsets64 is not null)
		{
			index -= chunkOffsets32.Count;
			if (index < chunkOffsets64.Count)
				return chunkOffsets64[index];
		}

		throw new IndexOutOfRangeException($"Index {index} is out of range for chunk offsets.");
	}

	public void SetOffsetAtIndex(int index, long value)
	{
		if (index < chunkOffsets32.Count)
		{
			if (value > uint.MaxValue)
			{
				//Element at i is greater than 32-bit.
				//Move it and all following 32-bit offsets
				//into the 64-bit offset list.
				int toMove = chunkOffsets32.Count - index;
				long[] longs = new long[toMove];
				for (int i = 0; i < toMove; i++)
				{
					longs[i] = chunkOffsets32[index + i];
				}

				CollectionsMarshal.SetCount(chunkOffsets32, index);
				chunkOffsets64 ??= new List<long>(longs.Length);
				chunkOffsets64.InsertRange(0, longs);
			}
			else
			{
				chunkOffsets32[index] = (uint)value;
			}
			return;
		}
		else if (chunkOffsets64 is not null)
		{
			index -= chunkOffsets32.Count;
			if (index < chunkOffsets64.Count)
			{
				chunkOffsets64[index] = value;
				return;
			}
		}
		throw new IndexOutOfRangeException($"Index {index} is out of range for chunk offsets.");
	}

	/// <summary>
	/// Reads 32-bit chunk offsets from the given stream, fixes overflows into 64-bit offsets, and sorts the offsets.
	/// </summary>
	public static ChunkOffsetList Read32(Stream file, uint entryCount)
	{
		ChunkOffsetList list = new((int)entryCount);
		CollectionsMarshal.SetCount(list.chunkOffsets32, (int)entryCount);
		Span<uint> span = CollectionsMarshal.AsSpan(list.chunkOffsets32);
		file.ReadExactly(MemoryMarshal.AsBytes(span));
		if (BitConverter.IsLittleEndian)
		{
			BinaryPrimitives.ReverseEndianness(span, span);
		}

		long lastChunkOffset = 0;
		for (int i = 0; i < list.chunkOffsets32.Count; i++)
		{
			long chunkOffset = list.chunkOffsets32[i];

			//Seems some files incorrectly use stco box with offsets > uint.MAXVALUE (e.g. 50 Self Help Books).
			//This causes the offsets to uint to overflow. Unfottnately, somtimes chapters are out of order and
			//chunkOffset is supposed to be less than lastChunkOffset (e.g. Altered Carbon). To attempt to
			//detect this, only assume it's an overflow if lastChunkOffset - chunkOffset > uint.MaxValue / 2
			if (chunkOffset < lastChunkOffset && lastChunkOffset - chunkOffset > uint.MaxValue / 2)
			{
				if (list.chunkOffsets64 is null)
				{
					int count = list.chunkOffsets32.Count - i;
					list.chunkOffsets64 = new List<long>(count);
				}
				chunkOffset += 1L << 32;
				list.chunkOffsets32.RemoveAt(i--);
				list.chunkOffsets64.Add(chunkOffset);
			}
			lastChunkOffset = chunkOffset;
		}
		list.chunkOffsets32.Sort();
		list.chunkOffsets64?.Sort();
		return list;
	}

	public unsafe static ChunkOffsetList Read64(Stream file, uint entryCount)
	{
		nint pLongs = Marshal.AllocHGlobal(sizeof(long) * (nint)entryCount);
		try
		{
			Span<long> longsSpan = new(pLongs.ToPointer(), (int)entryCount);
			file.ReadExactly(MemoryMarshal.AsBytes(longsSpan));
			if (BitConverter.IsLittleEndian)
			{
				BinaryPrimitives.ReverseEndianness(longsSpan, longsSpan);
			}
			longsSpan.Sort();

			int _32BitCount = FindLast32bit(longsSpan) + 1;

			ChunkOffsetList list = new(_32BitCount);
			CollectionsMarshal.SetCount(list.chunkOffsets32, _32BitCount);
			Span<uint> span = CollectionsMarshal.AsSpan(list.chunkOffsets32);
			for (int i = 0; i < _32BitCount; i++)
			{
				span[i] = (uint)longsSpan[i];
			}
			Span<long> remainder = longsSpan[_32BitCount..];
			list.chunkOffsets64 = new List<long>(remainder.Length);
			CollectionsMarshal.SetCount(list.chunkOffsets64, remainder.Length);
			Span<long> span64 = CollectionsMarshal.AsSpan(list.chunkOffsets64);
			remainder.CopyTo(span64);
			return list;
		}
		finally
		{
			Marshal.FreeHGlobal(pLongs);
		}
	}

	static int FindLast32bit(ReadOnlySpan<long> longsSpan)
	{
		int l = 0;
		int r = longsSpan.Length - 1;

		while (l <= r)
		{
			int mid = l + (r - l) / 2;
			long midValue = longsSpan[mid];
			if (midValue > uint.MaxValue)
			{
				r = mid - 1;
			}
			else if (midValue == uint.MaxValue || mid >= r || longsSpan[mid + 1] >= uint.MaxValue)
			{
				return mid;
			}
			else
			{
				l = mid + 1;
			}
		}
		return -1;
	}

	public void Write32(Stream file)
	{
		if (chunkOffsets64 != null && chunkOffsets64.Count > 0)
			throw new InvalidOperationException("Cannot write 32-bit chunk offsets when 64-bit offsets are present.");

		Span<uint> span = CollectionsMarshal.AsSpan(chunkOffsets32);
		if (BitConverter.IsLittleEndian)
		{
			BinaryPrimitives.ReverseEndianness(span, span);
		}
		file.Write(MemoryMarshal.AsBytes(span));
		if (BitConverter.IsLittleEndian)
		{
			BinaryPrimitives.ReverseEndianness(span, span);
		}
	}

	public void Write64(Stream file)
	{
		foreach (uint offset32 in chunkOffsets32)
		{
			//Expand 32-bit offsets to 64-bit
			file.WriteInt64BE(offset32);
		}
		Span<long> span64 = CollectionsMarshal.AsSpan(chunkOffsets64);
		if (BitConverter.IsLittleEndian)
		{
			BinaryPrimitives.ReverseEndianness(span64, span64);
		}
		file.Write(MemoryMarshal.AsBytes(span64));
		if (BitConverter.IsLittleEndian)
		{
			BinaryPrimitives.ReverseEndianness(span64, span64);
		}
	}

	public int IndexOf(long item)
	{
		if (item <= uint.MaxValue)
		{
			int index32 = chunkOffsets32.IndexOf((uint)item);
			if (index32 >= 0)
				return index32;
		}
		else if (chunkOffsets64 is not null)
		{
			int index64 = chunkOffsets64.IndexOf(item);
			if (index64 >= 0)
				return chunkOffsets32.Count + index64;
		}
		return -1;
	}

	public void RemoveAt(int index)
	{
		if (index < chunkOffsets32.Count)
		{
			chunkOffsets32.RemoveAt(index);
			return;
		}
		else if (chunkOffsets64 is not null)
		{
			index -= chunkOffsets32.Count;
			if (index < chunkOffsets64.Count)
			{
				chunkOffsets64.RemoveAt(index);
				return;
			}
		}
		throw new IndexOutOfRangeException();
	}

	public void Add(long offset)
	{
		if (offset > uint.MaxValue)
		{
			chunkOffsets64 ??= new List<long>();
			chunkOffsets64.Add(offset);
		}
		else
			chunkOffsets32.Add((uint)offset);
	}

	public bool Contains(long item) => IndexOf(item) >= 0;

	public bool Remove(long item)
	{
		int index = IndexOf(item);
		if (index >= 0)
		{
			RemoveAt(index);
			return true;
		}
		return false;
	}

	public IEnumerator<long> GetEnumerator() => chunkOffsets32.ConvertAll(i => (long)i).Concat(chunkOffsets64 ?? []).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public void CopyTo(long[] array, int arrayIndex)
	{

		for (int i = 0; i < chunkOffsets32.Count; i++, arrayIndex++)
		{
			array[arrayIndex] = chunkOffsets32[i];
		}
		if (chunkOffsets64 is not null)
		{
			for (int i = 0; i < chunkOffsets64.Count; i++, arrayIndex++)
			{
				array[arrayIndex] = chunkOffsets64[i];
			}
		}
	}
}
