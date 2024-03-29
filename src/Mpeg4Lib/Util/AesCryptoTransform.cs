﻿using System;
using System.Security.Cryptography;

namespace Mpeg4Lib.Util
{
	public class AesCryptoTransform : IDisposable
	{
		public TransformDelegate Transform { get; }
		public TransformDelegate TransformFinal { get; }
		public delegate int TransformDelegate(ReadOnlySpan<byte> input, Span<byte> output);

		private Aes Aes { get; }
		private ICryptoTransform AesTransform { get; }

		public AesCryptoTransform(byte[] key, byte[] iv)
		{
			Aes = Aes.Create();
			Aes.Mode = CipherMode.CBC;
			Aes.Padding = PaddingMode.None;
			AesTransform = Aes.CreateDecryptor(key, iv);

			object basicSymmetricCipherBCrypt =
				AesTransform
				.GetType()
				.GetProperty("BasicSymmetricCipher", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
				.GetValue(AesTransform);

			Type bCryptType = basicSymmetricCipherBCrypt.GetType();
			Type[] methodSig = new Type[] { typeof(ReadOnlySpan<byte>), typeof(Span<byte>) };

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
