using System;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.FrameFilters
{
	internal class PassthroughFilter : FrameFinalBase<FrameEntry>
	{
		protected readonly Stream OutputStream;
		private static readonly byte[] ZeroBytes = new byte[1024];
		public Queue<FrameEntry> Chapters { get; set; }

		public PassthroughFilter(Stream outputStream)
		{
			OutputStream = outputStream;
		}

		protected override void PerformFiltering(FrameEntry input)
		{
			lock (Chapters)
			{

				if (input.Chunk.FirstFrameIndex == input.FrameIndex)
				{
					//Start of new audio chunk

					if (Chapters.Count > 0)
					{
						while (Chapters.Count > 0 && Chapters.Peek().Chunk.ChunkOffset < input.Chunk.ChunkOffset)
						{
							var chFrame = Chapters.Dequeue();
							WriteZeroUntilPosition(chFrame.Chunk.ChunkOffset);
							OutputStream.Write(chFrame.FrameData.Span);
						}
					}

					WriteZeroUntilPosition(input.Chunk.ChunkOffset);
				}

				OutputStream.Write(input.FrameData.Span);
			}
		}

		private void WriteZeroUntilPosition(long desiredPosition)
		{
			long padSize = desiredPosition - OutputStream.Position;

			if (padSize < 0)
				throw new Exception("Desired offset if before current stream position");

			while (padSize > 0)
			{
				long zerosToWrite = Math.Min(ZeroBytes.Length, padSize);
				OutputStream.Write(ZeroBytes, 0, (int)zerosToWrite);
				padSize -= zerosToWrite;
			}
		}

	}
}
