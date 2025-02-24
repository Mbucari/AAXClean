using System;
using System.Security.Cryptography;

namespace Mpeg4Lib.Util
{
	public class AesCryptoTransform : IDisposable
	{

		public int DecryptCbc(ReadOnlySpan<byte> input, Span<byte> output)
			=> Aes.DecryptCbc(input, IV, output, PaddingMode.None);
		private Aes Aes { get; }
		private byte[] IV { get; }

		public AesCryptoTransform(byte[] key, byte[] iv)
		{
			Aes = Aes.Create();
			Aes.Mode = CipherMode.CBC;
			Aes.Padding = PaddingMode.None;
			Aes.Key = key;
			IV = iv;
		}

		public void Dispose()
		{
			Aes.Dispose();
		}
	}
}
