using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Security.Cryptography;

namespace AAXClean.Chunks
{
    internal sealed class AavdChunkHandler : Mp4AudioChunkHandler
    {
        private Aes Aes { get; }
        private ICryptoTransform AesTransform { get; }

        private static readonly byte[] emptyArray = Array.Empty<byte>();

        public AavdChunkHandler(uint timeScale, TrakBox trak, byte[] key, byte[] iv, bool seekable = false) : base(timeScale, trak, seekable)
        {
            if (key is null || key.Length != 16)
                throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
            if (iv is null || iv.Length != 16)
                throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

            Aes = Aes.Create();
            Aes.Mode = CipherMode.CBC;
            Aes.Padding = PaddingMode.None;
            AesTransform = Aes.CreateDecryptor(key, iv);
        }

        public override byte[] ReadBlock(Stream file, int size)
        {
            byte[] encData = base.ReadBlock(file, size);

            AesTransform.TransformBlock(encData, 0, encData.Length & 0x7ffffff0, encData, 0);
            AesTransform.TransformFinalBlock(emptyArray, 0, 0);
            return encData;
        }

        bool disposed = false;
        protected override void Dispose(bool disposing)
		{
            if (disposing && !disposed)
			{
                Aes.Dispose();
                AesTransform.Dispose();
                disposed = true;
            }

			base.Dispose(disposing);
		}
	}
}
