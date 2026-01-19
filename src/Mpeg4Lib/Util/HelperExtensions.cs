using System;
using System.Runtime.Intrinsics;

namespace Mpeg4Lib.Util;

internal static class HelperExtensions
{
	public static unsafe bool AllLessThanOrEqual<T>(this Span<T> ints, T value) where T : unmanaged, IComparable<T>
	{
		int checkedCount;
		bool result;
		if (Vector512.IsHardwareAccelerated)
			result = AllLessThanOrEqual_512(ints, value, out checkedCount);
		else if (Vector256.IsHardwareAccelerated)
			result = AllLessThanOrEqual_256(ints, value, out checkedCount);
		else
		{
			result = true;
			checkedCount = 0;
		}

		if (!result)
			return false;

		for (int i = checkedCount; i < ints.Length; i++)
		{
			if (ints[i].CompareTo(value) == 1)
				return false;
		}
		return true;
	}

	private static unsafe bool AllLessThanOrEqual_256<T>(Span<T> ints, T value, out int checkedcount) where T : unmanaged, IComparable<T>
	{
		int vecSize = 256 / 8 / sizeof(T);
		var numVectors = ints.Length / vecSize;

		Vector256<T> comparand = Vector256.Create(value);

		fixed (T* pInts = ints)
		{
			for (int i = 0; i < numVectors; i++)
			{
				Vector256<T> intVector = Vector256.Load(pInts + vecSize * i);
				if (!Vector256.LessThanOrEqualAll(intVector, comparand))
				{
					checkedcount = vecSize * i;
					return false;
				}
			}
		}
		checkedcount = vecSize * numVectors;
		return true;
	}

	private static unsafe bool AllLessThanOrEqual_512<T>(Span<T> ints, T value, out int checkedcount) where T : unmanaged, IComparable<T>
	{
		int vecSize = 512 / 8 / sizeof(T);
		var numVectors = ints.Length / vecSize;

		Vector512<T> comparand = Vector512.Create(value);

		fixed (T* pInts = ints)
		{
			for (int i = 0; i < numVectors; i++)
			{
				Vector512<T> intVector = Vector512.Load(pInts + vecSize * i);
				if (!Vector512.LessThanOrEqualAll(intVector, comparand))
				{
					checkedcount = vecSize * i;
					return false;
				}
			}
		}
		checkedcount = vecSize * numVectors;
		return true;
	}
}
