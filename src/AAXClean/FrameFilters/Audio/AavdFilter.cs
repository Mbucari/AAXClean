using Mpeg4Lib.Util;
using System;

namespace AAXClean.FrameFilters.Audio
{
	internal class AavdFilter : AacValidateFilter
	{
		private readonly AesCryptoTransform aesCryptoTransform;

		public AavdFilter(byte[] key, byte[] iv)
		{
			if (key is null || key.Length != 16)
				throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
			if (iv is null || iv.Length != 16)
				throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

			aesCryptoTransform = new AesCryptoTransform(key, iv);
		}
		protected override bool ValidateFrame(Span<byte> frame)
		{
			if (frame.Length >= 0x10)
			{
				aesCryptoTransform.TransformFinal(frame.Slice(0, frame.Length & 0x7ffffff0), frame);
			}

			return base.ValidateFrame(frame);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				aesCryptoTransform.Dispose();
			base.Dispose(disposing);
		}
	}
}
