using System;
using System.Security.Cryptography;

#nullable enable
namespace Mpeg4Lib.Util;

//https://github.com/FFmpeg/FFmpeg/blob/master/libavutil/aes_ctr.c
public class AesCtr : IDisposable
{
	public const int AES_BLOCK_SIZE = 16;
	private int block_offset = 0;
	private readonly Aes aes;
	private readonly byte[] counter = new byte[AES_BLOCK_SIZE];
	private readonly byte[] encrypted_counter = new byte[AES_BLOCK_SIZE];
	private static readonly byte[] ClearIV = new byte[AES_BLOCK_SIZE];

	public AesCtr(byte[] key)
	{
		ArgumentNullException.ThrowIfNull(key, nameof(key));
		if (key.Length != AES_BLOCK_SIZE)
			throw new ArgumentException($"{nameof(key)} must be exactly {AES_BLOCK_SIZE} bytes long.");

		aes = Aes.Create();
		aes.Key = key;
	}

	public void SetIV(byte[] iv)
	{
		ArgumentNullException.ThrowIfNull(iv, nameof(iv));
		if (iv.Length != AES_BLOCK_SIZE)
			throw new ArgumentException($"{nameof(iv)} must be exactly {AES_BLOCK_SIZE} bytes long.");
		Array.Copy(iv, counter, iv.Length);
		block_offset = 0;
	}

	public void Decrypt(ReadOnlySpan<byte> source, Span<byte> destination)
	{
		if (destination.Length < source.Length)
			throw new ArithmeticException($"Destination array is not long enough. (Parameter '{nameof(destination)}')");

		int data_pos = 0;

		while (data_pos < source.Length)
		{
			if (block_offset == 0)
			{
				if (!aes.TryEncryptCbc(counter, ClearIV, encrypted_counter, out var bytesWritten, PaddingMode.None))
					throw new CryptographicException($"{nameof(aes.TryEncryptCbc)} unexpectedly produced a ciphertext with the incorrect length.");
				IncrementBE(counter);
			}

			int encrypted_counter_pos = block_offset;
			int cur_end_pos = data_pos + AES_BLOCK_SIZE - block_offset;
			cur_end_pos = int.Min(cur_end_pos, source.Length);
			block_offset += cur_end_pos - data_pos;
			block_offset &= AES_BLOCK_SIZE - 1;

			for (; data_pos < cur_end_pos; encrypted_counter_pos++, data_pos++)
			{
				destination[data_pos] = (byte)(encrypted_counter[encrypted_counter_pos] ^ source[data_pos]);
			}
		}
	}

	private static void IncrementBE(byte[] data)
	{
		int carry = 1;
		for (int i = data.Length - 1; i >= 0 && carry > 0; i--)
		{
			var u = data[i] + carry;
			data[i] = (byte)u;
			carry = u >> 8;
		}
	}

	public void Dispose() => aes.Dispose();
}
