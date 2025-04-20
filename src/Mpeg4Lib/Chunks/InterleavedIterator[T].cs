using Mpeg4Lib.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Mpeg4Lib.Chunks;

public static class EnumerableExtensions
{
	public static IEnumerable<ChunkEntry> ChunkEntries(this TrakBox track)
		=> new ChunkEntryList(track);

	public static IEnumerable<TResult> InterleaveBy<TSource, TResult, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, IEnumerable<TResult>> selector,
		Func<TResult, TKey> keySelector)
		where TKey : IComparable<TKey>
	{
		ArgumentNullException.ThrowIfNull(source, nameof(source));
		ArgumentNullException.ThrowIfNull(selector, nameof(selector));
		ArgumentNullException.ThrowIfNull(keySelector, nameof(keySelector));

		var comparer = Comparer<TResult>.Create((x, y) => keySelector(x).CompareTo(keySelector(y)));
		return new InterleavedIterator<TResult>(source.Select(s => selector(s)).ToArray(), comparer);
	}

	private class InterleavedIterator<T> : IEnumerable<T>
	{
		private IEnumerable<T>[] Enumerables { get; }
		private Comparer<T> Comparer { get; }

		public InterleavedIterator(IEnumerable<T>[] enumerables, Comparer<T> comparer)
		{
			Enumerables = enumerables;
			Comparer = comparer;
		}

		public IEnumerator<T> GetEnumerator()
		{
			IEnumerator<T>?[] enumerators
				= Enumerables
				.Select(e => e.GetEnumerator())
				.Select(e => e.MoveNext() ? e : null)
				.ToArray();

			while (GetNextValue(enumerators, out var currentIndex, out var currentEnumerator))
			{
				yield return currentEnumerator.Current;
				if (!currentEnumerator.MoveNext())
					enumerators[currentIndex] = null;
			}
		}

		private bool GetNextValue(IEnumerator<T>?[] enumerators, out int minIndex, [NotNullWhen(true)] out IEnumerator<T>? minValue)
		{
			minIndex = -1;
			minValue = null;

			for (int i = 0; i < enumerators.Length; i++)
			{
				if (enumerators[i] is IEnumerator<T> ei && (minValue is null || Comparer.Compare(minValue.Current, ei.Current) > 0))
					(minIndex, minValue) = (i, ei);
			}

			return minIndex != -1;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
