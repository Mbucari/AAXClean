using System;
using System.Security.Cryptography;

namespace AAXClean.FrameFilters.Audio;

internal class AavdFilter : AacValidateFilter
{
	private const int AES_BLOCK_SIZE = 16;
	private readonly Aes Aes;
	private readonly byte[] IV;

	public AavdFilter(byte[] key, byte[] iv)
	{
		if (key is null || key.Length != AES_BLOCK_SIZE)
			throw new ArgumentException($"{nameof(key)} must be {AES_BLOCK_SIZE} bytes long.");
		if (iv is null || iv.Length != AES_BLOCK_SIZE)
			throw new ArgumentException($"{nameof(iv)} must be {AES_BLOCK_SIZE} bytes long.");

		Aes = Aes.Create();
		Aes.Key = key;
		IV = iv;
	}

	public override FrameEntry PerformFiltering(FrameEntry input)
	{
		if (input.FrameData.Length >= 0x10)
		{
			var encBlocks = input.FrameData.Slice(0, input.FrameData.Length & 0x7ffffff0).Span;
			Aes.DecryptCbc(encBlocks, IV, encBlocks, PaddingMode.None);
		}
		return base.PerformFiltering(input);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && !Disposed)
			Aes.Dispose();
		base.Dispose(disposing);
	}
}
