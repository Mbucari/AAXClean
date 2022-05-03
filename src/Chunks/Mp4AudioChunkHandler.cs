using AAXClean.Boxes;
using AAXClean.FrameFilters;
using System;

namespace AAXClean.Chunks
{
	internal class Mp4AudioChunkHandler : ChunkHandlerBase
	{
		public Mp4AudioChunkHandler(TrakBox trak, IFrameFilter frameFilter = null) : base(trak)
		{
			FrameFilter = frameFilter;
		}

		protected virtual bool ValidateFrame(Span<byte> frame) => (AV_RB16(frame) & 0xfff0) != 0xfff0;

		public override bool HandleFrame(ChunkEntry cEntry, uint frameIndex, Span<byte> frameData)
		{
			return ValidateFrame(frameData) && (FrameFilter?.FilterFrame(cEntry, LastFrameProcessed, frameData) ?? false);
		}

		//Defined at
		//http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
		private static ushort AV_RB16(Span<byte> frame)
		{
			return (ushort)(frame[0] << 8 | frame[1]);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
