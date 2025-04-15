using Mpeg4Lib.Util;

namespace AAXClean.FrameFilters.Audio;

internal class DashFilter : AacValidateFilter
{
    protected override int InputBufferSize => 1000;
    public byte[] Key { get; }
    AesCtr AesCtr { get; }

    public DashFilter(byte[] key)
    {
        Key = key;
        AesCtr = new AesCtr(key);
    }

    public override FrameEntry PerformFiltering(FrameEntry input)
    {
        if (input is DashFrameEntry dashEntry)
        {
            var frameData = dashEntry.FrameData.Span;
            AesCtr.SetIV(dashEntry.IV);
            AesCtr.Decrypt(frameData, frameData);
        }
        return base.PerformFiltering(input);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && !Disposed)
            AesCtr.Dispose();
        base.Dispose(disposing);
    }
}
