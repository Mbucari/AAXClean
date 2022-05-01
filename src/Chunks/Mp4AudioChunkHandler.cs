using AAXClean.AudioFilters;
using AAXClean.Boxes;
using System;
using System.Collections.Generic;

namespace AAXClean.Chunks
{
	internal class Mp4AudioChunkHandler : IChunkHandler
	{
		public bool Success { get; private set; } = true;
		public double TimeScale { get; }
		public AudioFilterBase AudioFilter { get; set; }
		public TrakBox Track { get; }
		public TimeSpan ProcessPosition => FrameToTime(LastFrameProcessed);

		private uint LastFrameProcessed;
		private IEnumerable<SttsBox.SampleEntry> Samples;

		public Mp4AudioChunkHandler(TrakBox trak)
		{
			Track = trak;
			TimeScale = Track.Mdia.Mdhd.Timescale;
			Samples = Track.Mdia.Minf.Stbl.Stts.Samples;
		}

		protected virtual bool ValidateFrame(Span<byte> frame) => (AV_RB16(frame) & 0xfff0) != 0xfff0;

		public bool ChunkAvailable(ChunkEntry chunkEntry, Span<byte> chunkData)
		{
			int framePosition = 0;
			for (uint fIndex = 0; fIndex < chunkEntry.FrameSizes.Length; fIndex++)
			{
				LastFrameProcessed = chunkEntry.FirstFrameIndex + fIndex;

				Span<byte> frame = chunkData.Slice(framePosition, chunkEntry.FrameSizes[fIndex]);

				Success = ValidateFrame(frame) && AudioFilter?.FilterFrame(chunkEntry.ChunkIndex, LastFrameProcessed, frame) == true;

				if (!Success)
					return false;

				framePosition += chunkEntry.FrameSizes[fIndex];
			}
			return true;
		}

		/// <summary>
		/// Gets the playback timestamp of an audio frame.
		/// </summary>
		internal TimeSpan FrameToTime(uint frameIndex)
		{
			double beginDelta = 0;

			foreach (SttsBox.SampleEntry entry in Samples)
			{
				if (frameIndex > entry.FrameCount)
				{
					beginDelta += (ulong)entry.FrameCount * entry.FrameDelta;
					frameIndex -= entry.FrameCount;
				}
				else
				{
					beginDelta += (ulong)frameIndex * entry.FrameDelta;
					break;
				}
			}
			return TimeSpan.FromSeconds(beginDelta / TimeScale);
		}

		//Defined at
		//http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
		private static ushort AV_RB16(Span<byte> frame)
		{
			return (ushort)(frame[0] << 8 | frame[1]);
		}

		private bool disposed = false;
		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Dispose managed resources.
					AudioFilter?.Dispose();
					Samples = null;
				}

				// Call the appropriate methods to clean up
				// unmanaged resources here.
				disposed = true;
			}
		}
	}
}
