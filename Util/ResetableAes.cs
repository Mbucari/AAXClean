using System;
using System.Security.Cryptography;

namespace AAXClean.Util
{
    internal class ResetableAes : IDisposable
    {
        /// <summary>
        /// Reset the initialization vector to its original state.
        /// </summary>
        internal Action Reset { get; }
        private Aes Aes { get; }
        private ICryptoTransform AesTransform { get; }
        public ResetableAes(byte[] key, byte[] iv, CipherMode mode, PaddingMode padding)
        {
            Aes = Aes.Create();
            Aes.Mode = mode;
            Aes.Padding = padding;
            AesTransform = Aes.CreateDecryptor(key, iv);

            var symmetricCipherProperty = AesTransform.GetType().GetProperty("BasicSymmetricCipher", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var basicSymmetricCipherBCrypt = symmetricCipherProperty.GetValue(AesTransform);

            Reset = basicSymmetricCipherBCrypt
                .GetType()
                .GetMethod("Reset", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .CreateDelegate<Action>(basicSymmetricCipherBCrypt);
        }

        public void DecryptInPlace(byte[] encryptedBlocks)
            => AesTransform.TransformBlock(encryptedBlocks, 0, encryptedBlocks.Length & 0x7ffffff0, encryptedBlocks, 0);

        public void Dispose()
        {
            Aes.Dispose();
            AesTransform.Dispose();
        }
    }
}
