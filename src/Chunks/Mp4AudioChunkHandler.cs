using AAXClean.Boxes;
using System;

namespace AAXClean.Chunks
{
	internal class Mp4AudioChunkHandler : ChunkHandlerBase
	{
		public bool Success { get; private set; } = true;
		public Mp4AudioChunkHandler(TrakBox trak) : base(trak) { }

		protected virtual bool ValidateFrame(Span<byte> frame) => (AV_RB16(frame) & 0xfff0) != 0xfff0;

		public override bool HandleChunk(ChunkEntry chunkEntry, Span<byte> chunkData)
		{
			int framePosition = 0;
			for (uint fIndex = 0; fIndex < chunkEntry.FrameSizes.Length; fIndex++)
			{
				LastFrameProcessed = chunkEntry.FirstFrameIndex + fIndex;

				Span<byte> frame = chunkData.Slice(framePosition, chunkEntry.FrameSizes[fIndex]);

				Success = ValidateFrame(frame) && FrameFilter?.FilterFrame(chunkEntry, LastFrameProcessed, frame) == true;

				if (!Success)
					return false;

				framePosition += chunkEntry.FrameSizes[fIndex];
			}
			return true;
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
