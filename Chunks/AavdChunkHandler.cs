using AAXClean.Boxes;
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
		protected override bool ValidateFrame(byte[] audioFrame)
        {
            if (audioFrame.Length >= 0x10)
            {
                AesTransform.TransformBlock(audioFrame, 0, audioFrame.Length & 0x7ffffff0, audioFrame, 0);
                AesTransform.TransformFinalBlock(emptyArray, 0, 0);
            }

            return base.ValidateFrame(audioFrame);
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
