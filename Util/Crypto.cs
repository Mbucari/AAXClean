using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AAXClean.Util
{
    internal class Crypto
    {
        internal static void DecryptInPlace(byte[] key, byte[] iv, byte[] encryptedBlocks)
        {
            DecryptInPlace(key, iv, encryptedBlocks, encryptedBlocks.Length);
        }
        internal static void DecryptInPlace(byte[] key, byte[] iv, byte[] encryptedBlocks, int length)
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;

            using var cbcDecryptor = aes.CreateDecryptor(key, iv);

            using var cStream = new CryptoStream(new MemoryStream(encryptedBlocks), cbcDecryptor, CryptoStreamMode.Read);

            cStream.Read(encryptedBlocks, 0, length & 0x7ffffff0);
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
