using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AAXClean.Chunks
{
	internal class TrakChunkReader
	{
		private Stream InputStream { get; }
		private Queue<(long, ChunkTable)> ChunkQueue { get; }

		/// <summary>
		/// Reads track all chuncks sequentially
		/// </summary>
		/// <param name="file">input mp4 stream from which to read chunks</param>
		/// <param name="handlers">Codec handlers</param>
		public TrakChunkReader(Stream file, params IChunkHandler[] handlers)
		{
			InputStream = file;

			List<(long chunkOffset, ChunkTable)> combinedChunkList = new();

			foreach (var handler in handlers)
			{
				List<(long chunkOffset, ChunkTable)> chunkList = new();

				var chunkTable = new ChunkTable(handler.Track.Mdia.Minf.Stbl, handler);

				if (handler.Track.Mdia.Minf.Stbl.Stco is null)
					chunkList.AddRange(handler.Track.Mdia.Minf.Stbl.Co64.ChunkOffsets.Select(co => (co, chunkTable)));
				else
				{
					long lastOffset = 0;
					foreach (var co in handler.Track.Mdia.Minf.Stbl.Stco.ChunkOffsets)
					{
						if (co < lastOffset && lastOffset - co > uint.MaxValue / 2)
						{
							//Seems some files incorrectly use stco box with offsets > uint.MAXVALUE. This causes
							//the offsets to wrap around. Unfottnately, somtimes chapters are out of order and
							//co is supposed to be less than lastOffset (e.g. Altered Carbon). To attempt to detect
							//this, only assume it's a wrap around if lastOffset - co > uint.MaxValue / 2
							lastOffset = (1L << 32) + co;
						}
						else
							lastOffset = co;

						chunkList.Add((lastOffset, chunkTable));

					}
				}

				chunkTable.SetChunkOffsets(chunkList.Select(c => c.chunkOffset).ToList());

				combinedChunkList.AddRange(chunkList);
			}

			combinedChunkList.Sort((o1, o2) => o1.chunkOffset.CompareTo(o2.chunkOffset));

			ChunkQueue = new Queue<(long, ChunkTable)>(combinedChunkList);
		}

		/// <summary>
		/// Reads the next available chunk.
		/// </summary>
		/// <returns></returns>
		public bool NextChunk()
		{
			if (ChunkQueue.Count == 0) return false;

			(long chunkOffset, ChunkTable table) = ChunkQueue.Dequeue();

			if (InputStream.Position < chunkOffset)
			{
				//Unknown Track or data type. Read past it to next known chunk.
				if (table.Handler.InputStreamSeekable)
					InputStream.Position = chunkOffset;
				else
					InputStream.ReadBlock((int)(chunkOffset - InputStream.Position));
			}
			else if (InputStream.Position > chunkOffset)
			{
				if (table.Handler.InputStreamSeekable)
					InputStream.Position = chunkOffset;
				else
					throw new Exception($"Input stream position 0x{InputStream.Position:X8} is past the chunk offset 0x{chunkOffset:X8} and is not seekable.");
			}

			(int totalChunkSize, uint frameIndex, int[] frameSizes) = table.GetChunkFrames(chunkOffset);

			Span<byte> chunkBuffer = new byte[totalChunkSize];
			InputStream.Read(chunkBuffer);


			return table.Handler.ChunkAvailable(chunkBuffer, table.CurrentChunkIndex, frameIndex, totalChunkSize, frameSizes);
		}

		private class ChunkTable
		{
			public IChunkHandler Handler { get; }
			public uint CurrentChunkIndex { get; private set; } = 0;
			private int[] SampleSizes { get; }
			private StscBox.ChunkEntry[] SamplesToChunks { get; }

			private Dictionary<long, uint> ChunkOffsetToChunkIndex;
			public ChunkTable(StblBox stbl, IChunkHandler handler)
			{
				Handler = handler;

				SampleSizes = stbl.Stsz.SampleSizes.ToArray();

				SamplesToChunks = stbl.Stsc.Samples.ToArray();
			}

			/// <summary>
			/// Create a map of chunk offsets to chunk indices. This is necessary because some Audible titles
			/// (eg. Broken Angels: B002V8H59I) have the chunk offsets out of order. <see cref="TrakChunkReader"/>
			/// reads sorts chunk offsets and reads them in order, but we need the chunk offset INDEX in order
			/// to calculate the frame index and frame sizes.
			/// </summary>
			public void SetChunkOffsets(List<long> chunkOffsets)
			{
				ChunkOffsetToChunkIndex = new();

				for (int i = 0; i < chunkOffsets.Count; i++)
				{
					ChunkOffsetToChunkIndex[chunkOffsets[i]] = (uint)i;
				}
			}

			public (int totalChunkSize, uint frameIndex, int[] frameSizes) GetChunkFrames(long chunkOffset)
			{
				return GetChunkFrames(ChunkOffsetToChunkIndex[chunkOffset]);
			}

			public (int totalChunkSize, uint frameIndex, int[] frameSizes) GetChunkFrames(uint chunkIndex)
			{
				CurrentChunkIndex = chunkIndex;
				(uint firstFrame, uint numFrames) = NumSamplesInChunk(chunkIndex);

				int[] frameSizes = new int[numFrames];
				int totalChunkSize = 0;

				for (uint i = 0; i < numFrames; i++)
				{
					if (i + firstFrame >= SampleSizes.Length)
					{
						//This handels a case whwere the last Stsc entry was not written correctly.
						int[] correctFrameSizes = new int[i];
						Array.Copy(frameSizes, 0, correctFrameSizes, 0, i);
						return (totalChunkSize, firstFrame, correctFrameSizes);
					}

					frameSizes[i] = SampleSizes[i + firstFrame];
					totalChunkSize += frameSizes[i];
				}

				return (totalChunkSize, firstFrame, frameSizes);
			}

			private (uint firstFrame, uint numFrames) NumSamplesInChunk(uint chunk)
			{
				//Mp4 uses one-based counting
				chunk++;

				uint firstFrame = 0, numFrames = 0;

				for (int i = 0; i < SamplesToChunks.Length; i++)
				{
					if (i + 1 == SamplesToChunks.Length || chunk < SamplesToChunks[i + 1].FirstChunk)
					{
						firstFrame += (chunk - SamplesToChunks[i].FirstChunk) * SamplesToChunks[i].SamplesPerChunk;
						numFrames = SamplesToChunks[i].SamplesPerChunk;
						break;
					}
					firstFrame += (SamplesToChunks[i + 1].FirstChunk - SamplesToChunks[i].FirstChunk) * SamplesToChunks[i].SamplesPerChunk;
				}

				return (firstFrame, numFrames);
			}
		}
	}
}
