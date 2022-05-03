using AAXClean.Chunks;
using System;
using System.IO;

namespace AAXClean.FrameFilters
{
	internal class PassthroughFilter : IFrameFilter
	{
		protected readonly Stream OutputStream;
		static readonly byte[] ZeroBytes = new byte[1024];

		public PassthroughFilter(Stream outputStream)
		{
			OutputStream = outputStream;
		}
		public void Dispose() { }

		public bool FilterFrame(ChunkEntry cEntry, uint frameIndex, uint frameDelta, Span<byte> audioFrame)
		{
			if (cEntry.FirstFrameIndex == frameIndex)
			{
				if (OutputStream.Position > cEntry.ChunkOffset)
					return false;
				WriteZeroUntilPosition(cEntry.ChunkOffset);
			}

			OutputStream.Write(audioFrame);

			return true;
		}

		private void WriteZeroUntilPosition(long desiredPosition)
		{
			long padSize = desiredPosition - OutputStream.Position;

			while (padSize > 0)
			{
				long zerosToWrite = Math.Min(ZeroBytes.Length, padSize);
				OutputStream.Write(ZeroBytes, 0, (int)zerosToWrite);
				padSize -= zerosToWrite;
			}
		}
	}
}
