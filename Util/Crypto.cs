using System;
using System.Security.Cryptography;

namespace AAXClean.Util
{
    internal class Crypto
    {
        internal static void DecryptInPlace(byte[] key, byte[] iv, byte[] encryptedBlocks)
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;

            using var cbcDecryptor = aes.CreateDecryptor(key, iv);

            cbcDecryptor.TransformBlock(encryptedBlocks, 0, encryptedBlocks.Length & 0x7ffffff0, encryptedBlocks, 0);
        }

        internal static byte[] Sha1(params (byte[] bytes, int start, int length)[] blocks)
        {
            using SHA1 sha = SHA1.Create();
            int i = 0;
            for (; i < blocks.Length - 1; i++)
            {
                sha.TransformBlock(blocks[i].bytes, blocks[i].start, blocks[i].length, null, 0);
            }
            sha.TransformFinalBlock(blocks[i].bytes, blocks[i].start, blocks[i].length);
            return sha.Hash;
        }
    }
}
