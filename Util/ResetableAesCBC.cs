using System;
using System.Security.Cryptography;

namespace AAXClean.Util
{
    internal class ResetableAesCBC : IDisposable
    {
        /// <summary>
        /// Reset the initialization vector to its original state.
        /// </summary>
        internal Action Reset { get; }
        private Aes Aes { get; }
        private ICryptoTransform AesTransform { get; }
        public ResetableAesCBC(byte[] key, byte[] iv)
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

            Reset = 
                basicSymmetricCipherBCrypt
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
