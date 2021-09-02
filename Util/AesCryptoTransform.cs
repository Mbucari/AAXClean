using System;
using System.Security.Cryptography;

namespace AAXClean.Util
{
	internal class AesCryptoTransform : IDisposable
	{
		internal TransformDelegate Transform { get; }
		internal TransformDelegate TransformFinal { get; }
		internal delegate int TransformDelegate(ReadOnlySpan<byte> input, Span<byte> output);

		private Aes Aes { get; }
		private ICryptoTransform AesTransform { get; }

		public AesCryptoTransform(byte[] key, byte[] iv)
		{
			Aes = Aes.Create();
			Aes.Mode = CipherMode.CBC;
			Aes.Padding = PaddingMode.None;
			AesTransform = Aes.CreateDecryptor(key, iv);

			var basicSymmetricCipherBCrypt =
				AesTransform
				.GetType()
				.GetProperty("BasicSymmetricCipher", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
				.GetValue(AesTransform);

			var bCryptType = basicSymmetricCipherBCrypt.GetType();
			var methodSig = new Type[] { typeof(ReadOnlySpan<byte>), typeof(Span<byte>) };

			Transform =
				bCryptType
				.GetMethod("Transform", methodSig)
				.CreateDelegate<TransformDelegate>(basicSymmetricCipherBCrypt);

			TransformFinal =
				bCryptType
				.GetMethod("TransformFinal", methodSig)
				.CreateDelegate<TransformDelegate>(basicSymmetricCipherBCrypt);
		}

		public void Dispose()
		{
			Aes.Dispose();
			AesTransform.Dispose();
		}
	}
}
