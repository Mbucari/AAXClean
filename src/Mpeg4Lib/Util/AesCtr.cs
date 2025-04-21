using System;
using System.Security.Cryptography;

namespace Mpeg4Lib.Util;

//https://github.com/FFmpeg/FFmpeg/blob/master/libavutil/aes_ctr.c
public unsafe struct AesCtr : IDisposable
{
	public const int AES_BLOCK_SIZE = 16;

	private readonly ICryptoTransform Encryptor;
	private readonly Aes Aes;
	private readonly byte[] encrypted_counter = new byte[AES_BLOCK_SIZE];

	public AesCtr(byte[] key)
	{
		ArgumentNullException.ThrowIfNull(key, nameof(key));
		if (key.Length != AES_BLOCK_SIZE)
			throw new ArgumentException($"{nameof(key)} must be exactly {AES_BLOCK_SIZE} bytes long.");

		Aes = Aes.Create();
		Aes.Padding = PaddingMode.None;
		Aes.Mode = CipherMode.ECB;
		Encryptor = Aes.CreateEncryptor(key, null);
	}

	public unsafe void Decrypt(byte[] iv, ReadOnlySpan<byte> source, Span<byte> destination)
	{
		ArgumentNullException.ThrowIfNull(iv, nameof(iv));
		ArgumentOutOfRangeException.ThrowIfNotEqual(iv.Length, AES_BLOCK_SIZE, nameof(iv));

		if (destination.Length < source.Length)
			throw new ArithmeticException($"Destination array is not long enough. (Parameter '{nameof(destination)}')");

		const int AES_NUM_DWORDS = AES_BLOCK_SIZE / sizeof(uint);

		fixed (byte* pD = destination)
		{
			fixed (byte* pS = source)
			{
				fixed (byte* pEc = encrypted_counter)
				{
					uint* pD32 = (uint*)pD;
					uint* pS32 = (uint*)pS;
					uint* pEc32 = (uint*)pEc;

					int data_pos = 0, count = source.Length;

					while (count >= AES_BLOCK_SIZE)
					{
						Encryptor.TransformBlock(iv, 0, AES_BLOCK_SIZE, encrypted_counter, 0);
						IncrementBE(iv);

						for (int i = 0; i < AES_NUM_DWORDS; i++)
							*pD32++ = pEc32[i] ^ *pS32++;

						data_pos += AES_BLOCK_SIZE;
						count -= AES_BLOCK_SIZE;
					}

					if (count > 0)
					{
						Encryptor.TransformBlock(iv, 0, AES_BLOCK_SIZE, encrypted_counter, 0);

						for (int i = 0; i < count; i++, data_pos++)
							pD[data_pos] = (byte)(pEc[i] ^ pS[data_pos]);
					}
				}
			}
		}
	}

	private static void IncrementBE(byte[] data)
	{
		int i = data.Length - 1;
		do
		{
			data[i]++;
		} while (data[i] == 0 && i-- > 0);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private bool isDisposed;
	private void Dispose(bool disposing)
	{
		if (disposing & !isDisposed)
		{
			Encryptor.Dispose();
			Aes.Dispose();
			isDisposed = true;
		}
	}
}
