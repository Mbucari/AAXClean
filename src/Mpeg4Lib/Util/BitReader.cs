using System;
using System.Linq;

namespace Mpeg4Lib.Util;

internal class BitReader
{
    private readonly byte[] bytes;
    public BitReader(byte[] data)
    {
        bytes = data;
    }
    private int byteIndex = 0;
    private int bitIndex = 0;

    public int Position
    {
        get => byteIndex * 8 + bitIndex;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(Position));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, bytes.Length * 8, nameof(Position));

            byteIndex = value / 8;
            bitIndex = value % 8;
        }
    }
    public int Length => bytes.Length * 8;

    public uint Read(int numBits)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(numBits, 0, nameof(numBits));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(numBits, sizeof(uint) * 8, nameof(numBits));

        uint value = 0;
        while (numBits > 0)
        {
            if (byteIndex >= bytes.Length)
                throw new InvalidOperationException("Not enough data to read the requested number of bits.");

            int bitsToRead = Math.Min(numBits, 8 - bitIndex);
            uint mask = (1u << bitsToRead) - 1;
            value <<= bitsToRead;
            value |= ((uint)bytes[byteIndex] >> (8 - bitIndex - bitsToRead)) & mask;
            numBits -= bitsToRead;
            bitIndex += bitsToRead;
            if (bitIndex == 8)
            {
                byteIndex++;
                bitIndex = 0;
            }
        }
        return value;
    }

    public void CopyTo(BitWriter writer)
    {
        if (bitIndex != 0)
        {
            var toRead = 8 - bitIndex;
            var value = Read(toRead);
            writer.Write(value, toRead);
        }
        while (byteIndex < bytes.Length)
        {
            writer.Write(bytes[byteIndex++], 8);
        }
    }
}

public class BitWriter
{
    private int byteIndex = 0;
    private int bitIndex = 0;
    public int Position => byteIndex * 8 + bitIndex;
    private byte[] bytes = [];

    public byte[] ToByteArray() => bytes.ToArray();

    public void Write(uint value, int numBits)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(numBits, 0, nameof(numBits));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(numBits, sizeof(uint) * 8, nameof(numBits));

        while (numBits > 0)
        {
            if (bitIndex == 0)
                Array.Resize(ref bytes, byteIndex + 1);

            int bitsToWrite = Math.Min(numBits, 8 - bitIndex);
            value &= numBits == 32 ? uint.MaxValue : (1u << numBits) - 1;

            bytes[byteIndex] |= (byte)((value >> (numBits - bitsToWrite)) << (8 - bitIndex - bitsToWrite));

            numBits -= bitsToWrite;
            bitIndex += bitsToWrite;
            if (bitIndex == 8)
            {
                byteIndex++;
                bitIndex = 0;
            }
        }
    }
}
