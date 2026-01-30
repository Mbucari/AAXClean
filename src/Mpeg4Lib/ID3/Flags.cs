using System;
using System.Linq;

namespace Mpeg4Lib.ID3;

public class Flags
{
	public int Size => _flags.Length;
	private byte[] _flags;
	public byte[] ToBytes() => _flags.ToArray();
	public Flags(params byte[] flags)
	{
		_flags = flags;
	}
	public Flags(ushort flags)
	{
		_flags = [(byte)(flags >> 8), (byte)(flags & 0xff)];
	}
	public bool this[int index]
	{
		get
		{
			if (index < 0 || index >= _flags.Length * 8)
				throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the range of the flags.");
			return (_flags[index / 8] & (1 << (7 - (index % 8)))) != 0;
		}
		set
		{
			if (index < 0 || index >= _flags.Length * 8)
				throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the range of the flags.");
			if (value)
				_flags[index / 8] |= (byte)(1 << (7 - (index % 8)));
			else
				_flags[index / 8] &= (byte)~(1 << (7 - (index % 8)));
		}
	}
}
