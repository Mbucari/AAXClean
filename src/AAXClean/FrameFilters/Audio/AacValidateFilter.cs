using System;

namespace AAXClean.FrameFilters.Audio
{
	internal class AacValidateFilter : FrameTransformBase<FrameEntry, FrameEntry>
	{
		protected override int InputBufferSize => 1000;
		public override FrameEntry PerformFiltering(FrameEntry input)
		{
			if (!ValidateFrame(input.FrameData.Span))
				throw new Exception("Aac error!");

			return input;
		}

		private bool ValidateFrame(ReadOnlySpan<byte> frame) => (AV_RB16(frame) & 0xfff0) != 0xfff0;

		//Defined at
		//http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
		private static ushort AV_RB16(ReadOnlySpan<byte> frame)
		{
			return (ushort)(frame[0] << 8 | frame[1]);
		}
	}
}
