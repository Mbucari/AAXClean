using AAXClean.Boxes;
using AAXClean.Util;
using System;

namespace AAXClean.Chunks
{
    internal sealed class AavdChunkHandler : Mp4AudioChunkHandler
    {
        private bool disposed = false;
        private AesCryptoTransform Aes { get; }

        public AavdChunkHandler(TrakBox trak, byte[] key, byte[] iv) : base(trak)
        {
            if (key is null || key.Length != 16)
                throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
            if (iv is null || iv.Length != 16)
                throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

            Aes = new AesCryptoTransform(key, iv);
        }
        protected override bool ValidateFrame(Span<byte> audioFrame)
        {
            if (audioFrame.Length >= 0x10)
            {
                Aes.TransformFinal(audioFrame.Slice(0, audioFrame.Length & 0x7ffffff0), audioFrame);
            }

            return base.ValidateFrame(audioFrame);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Aes.Dispose();
                disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
