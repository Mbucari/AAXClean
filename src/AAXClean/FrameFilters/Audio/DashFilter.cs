using Mpeg4Lib.Util;

namespace AAXClean.FrameFilters.Audio;

internal class DashFilter : AacValidateFilter
{
	protected override int InputBufferSize => 1000;
	public byte[]? Key { get; }
	private AesCtr? AesCtr { get; }

	public DashFilter(byte[]? key)
	{
		Key = key;
		AesCtr = key is null ? null : new AesCtr(key);
	}

	public override FrameEntry PerformFiltering(FrameEntry input)
	{
		if (input.ExtraData is byte[] iv)
		{
			if (AesCtr is null)
				throw new System.NullReferenceException("AesCtr is null but the frame entry has an IV.");
			var frameData = input.FrameData.Span;
			AesCtr.Decrypt(iv, frameData, frameData);
		}
		return base.PerformFiltering(input);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && !Disposed)
			AesCtr?.Dispose();
		base.Dispose(disposing);
	}
}
