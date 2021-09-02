using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;

namespace AAXClean.Chunks
{
    internal sealed class AavdChunkHandler : Mp4AudioChunkHandler
    {
        private ResetableAesCBC ResetableAes { get; }

        public AavdChunkHandler(uint timeScale, TrakBox trak, byte[] key, byte[] iv, bool seekable = false) : base(timeScale, trak, seekable)
        {
            if (key is null || key.Length != 16)
                throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
            if (iv is null || iv.Length != 16)
                throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

            ResetableAes = new(key, iv);
        }
        public override byte[] ReadBlock(Stream file, int size)
        {
            byte[] encData = base.ReadBlock(file, size);

            ResetableAes.Reset();
            ResetableAes.DecryptInPlace(encData);

            return encData;
        }

        bool disposed = false;
        protected override void Dispose(bool disposing)
		{
            if (disposing && !disposed)
			{
                ResetableAes?.Dispose();
                disposed = true;
            }

			base.Dispose(disposing);
		}
	}
}
