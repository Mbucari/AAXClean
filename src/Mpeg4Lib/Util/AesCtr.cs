using System;
using System.Security.Cryptography;

namespace Mpeg4Lib.Util;

//https://github.com/FFmpeg/FFmpeg/blob/master/libavutil/aes_ctr.c
public unsafe struct AesCtr : IDisposable
{
	public const int AES_BLOCK_SIZE = 16;

	private readonly ICryptoTransform Encryptor;
	private readonly Aes Aes;
	public AesCtr(byte[] key)
	{
		ArgumentNullException.ThrowIfNull(key, nameof(key));
		if (key.Length != AES_BLOCK_SIZE)
			throw new ArgumentException($"{nameof(key)} must be exactly {AES_BLOCK_SIZE} bytes long.");

		Aes = Aes.Create();
		Aes.Key = key;
		Aes.Padding = PaddingMode.None;
		Aes.Mode = CipherMode.ECB;
		Encryptor = Aes.CreateEncryptor(key, new byte[16]);
	}


	public unsafe void Decrypt(byte[] iv, ReadOnlySpan<byte> source, Span<byte> destination)
	{
		if (destination.Length < source.Length)
			throw new ArithmeticException($"Destination array is not long enough. (Parameter '{nameof(destination)}')");

		int data_pos = 0;
		fixed (byte* pD = destination)
		{
			fixed (byte* pS = source)
			{
				uint* pD32 = (uint*)pD;
				uint* pS32 = (uint*)pS;

				while (data_pos < source.Length)
				{
					//Crt always encrypts with an empty IV, and the only way to reset the IV after encrypting a block
					//is by using TransformFinalBlock. 
					var encrypted_counter = Encryptor.TransformFinalBlock(iv, 0, AES_BLOCK_SIZE);
					IncrementBE(iv);

					int length = int.Min(source.Length - data_pos, AES_BLOCK_SIZE);

					fixed (byte* pEc = encrypted_counter)
					{
						uint* pEc32 = (uint*)pEc;

						int i = 0, dwordLength = length / sizeof(uint);
						for (; i < dwordLength; i++)
						{
							*pD32++ = pEc32[i] ^ *pS32++;
						}
						i <<= 2;
						data_pos += i;
						for (; i < length; i++)
						{
							*(pD + data_pos) = (byte)(pEc[i] ^ *(pS + data_pos));
							data_pos++;
						}
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
