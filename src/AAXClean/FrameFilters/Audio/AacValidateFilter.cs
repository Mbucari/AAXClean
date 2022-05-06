using System;

namespace AAXClean.FrameFilters.Audio
{
	internal class AacValidateFilter : FrameTransformBase<FrameEntry, FrameEntry>
	{
		protected override FrameEntry PerformFiltering(FrameEntry input)
		{
			if (!ValidateFrame(input.FrameData.Span))
				throw new Exception("Aac error!");

			return input;
		}
		protected virtual bool ValidateFrame(Span<byte> frame) => (AV_RB16(frame) & 0xfff0) != 0xfff0;

		//Defined at
		//http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
		private static ushort AV_RB16(Span<byte> frame)
		{
			return (ushort)(frame[0] << 8 | frame[1]);
		}

	}
}
