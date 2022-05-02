using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
	internal class StscBox : FullBox
	{
		public override long RenderSize => base.RenderSize + 4 + Samples.Count * 3 * 4;
		internal uint EntryCount { get; set; }
		internal List<ChunkEntry> Samples { get; } = new List<ChunkEntry>();

		internal static StscBox CreateBlank(Box parent)
		{
			int size = 4 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stsc");

			StscBox stscBox = new StscBox(new byte[] { 0, 0, 0, 0 }, header, parent);

			parent.Children.Add(stscBox);
			return stscBox;
		}

		private StscBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }

		internal StscBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			EntryCount = file.ReadUInt32BE();

			for (int i = 0; i < EntryCount; i++)
			{
				Samples.Add(new ChunkEntry(file));
			}
		}

		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE((uint)Samples.Count);
			foreach (ChunkEntry sample in Samples)
			{
				file.WriteUInt32BE(sample.FirstChunk);
				file.WriteUInt32BE(sample.SamplesPerChunk);
				file.WriteUInt32BE(sample.SampleDescriptionIndex);
			}
		}

		private bool _disposed = false;
		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				Samples.Clear();
			}

			_disposed = true;

			base.Dispose(disposing);
		}

		/// <summary>
		/// Effectively expand the Stsc table to one entry per chunk. Table size is 8 * <paramref name="numChunks"/> bytes.
		/// </summary>
		/// <returns>A zero-base table of frame indices and sizes for each chunk index</returns>
		public (uint firstFrameIndex, uint numFrames)[] CalculateChunkFrameTable()
		{
			(uint firstFrameIndex, uint numFrames)[] table = new (uint, uint)[EntryCount];

			uint firstFrameIndex = 0;
			int lastStscIndex = 0;

			for (uint chunk = 1; chunk <= EntryCount; chunk++)
			{
				if (lastStscIndex + 1 < Samples.Count && chunk == Samples[lastStscIndex + 1].FirstChunk)
					lastStscIndex++;

				table[chunk - 1] = (firstFrameIndex, Samples[lastStscIndex].SamplesPerChunk);
				firstFrameIndex += Samples[lastStscIndex].SamplesPerChunk;
			}

			return table;
		}

		public class ChunkEntry
		{
			public ChunkEntry(Stream file)
			{
				FirstChunk = file.ReadUInt32BE();
				SamplesPerChunk = file.ReadUInt32BE();
				SampleDescriptionIndex = file.ReadUInt32BE();
			}
			public ChunkEntry(uint firstChunk, uint samplesPerChunk, uint sampleDesIndex)
			{
				FirstChunk = firstChunk;
				SamplesPerChunk = samplesPerChunk;
				SampleDescriptionIndex = sampleDesIndex;
			}
			public uint FirstChunk { get; }
			public uint SamplesPerChunk { get; }
			public uint SampleDescriptionIndex { get; }
		}
	}
}
