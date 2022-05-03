using AAXClean.Chunks;
using System;
using System.IO;

namespace AAXClean.FrameFilters
{
	internal class PassthroughFilter : IFrameFilter
	{
		protected readonly Stream OutputStream;
		public bool Closed { get; private set; }

		static readonly byte[] ZeroBytes = new byte[16384];

		public PassthroughFilter(Stream outputStream)
		{
			OutputStream = outputStream;
		}

		public void Close() { }

		public void Dispose() { }

		public virtual bool FilterFrame(ChunkEntry cEntry, uint frameIndex, Span<byte> audioFrame)
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
		protected void WriteZeroUntilPosition(long desiredPosition)
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
