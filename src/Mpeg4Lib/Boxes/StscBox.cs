using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.IO;

namespace Mpeg4Lib.Boxes;

public readonly record struct ChunkFrames
{
	public uint FirstFrameIndex { get; internal init; }
	public uint NumberOfFrames { get; internal init; }
}

public class StscBox : FullBox
{
	public override long RenderSize => base.RenderSize + 4 + EntryCount * 3 * 4;
	public int EntryCount => Samples.Count;
	public List<StscChunkEntry> Samples { get; } = new List<StscChunkEntry>();

	public static StscBox CreateBlank(IBox parent)
	{
		int size = 4 + 12 /* empty Box size*/;
		BoxHeader header = new BoxHeader((uint)size, "stsc");

		StscBox stscBox = new StscBox([0, 0, 0, 0], header, parent);

		parent.Children.Add(stscBox);
		return stscBox;
	}

	private StscBox(byte[] versionFlags, BoxHeader header, IBox parent)
		: base(versionFlags, header, parent) { }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="file"></param>
	/// <param name="header"></param>
	/// <param name="parent"></param>
	public StscBox(Stream file, BoxHeader header, IBox? parent)
		: base(file, header, parent)
	{
		var entryCount = file.ReadUInt32BE();

		for (int i = 0; i < entryCount; i++)
		{
			Samples.Add(new StscChunkEntry(file));
		}
	}

	/// <summary>
	/// Effectively expand the Stsc table to one entry per chunk. Table size is 8 * <paramref name="numChunks"/> bytes.
	/// </summary>
	/// <param name="numChunks">The number of chunks in the track</param>
	/// <returns>A zero-base table of frame indices and sizes for each chunk index</returns>
	public ChunkFrames[] CalculateChunkFrameTable(uint numChunks)
	{
		ChunkFrames[] table = new ChunkFrames[numChunks];

		uint firstFrameIndex = 0;
		int lastStscIndex = 0;

		for (uint chunk = 1; chunk <= numChunks; chunk++)
		{
			if (lastStscIndex + 1 < Samples.Count && chunk == Samples[lastStscIndex + 1].FirstChunk)
				lastStscIndex++;

			table[chunk - 1] = new() { FirstFrameIndex = firstFrameIndex, NumberOfFrames = Samples[lastStscIndex].SamplesPerChunk };
			firstFrameIndex += Samples[lastStscIndex].SamplesPerChunk;
		}

		return table;
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteUInt32BE((uint)Samples.Count);
		foreach (StscChunkEntry sample in Samples)
		{
			file.WriteUInt32BE(sample.FirstChunk);
			file.WriteUInt32BE(sample.SamplesPerChunk);
			file.WriteUInt32BE(sample.SampleDescriptionIndex);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && !Disposed)
			Samples.Clear();
		base.Dispose(disposing);
	}

	public class StscChunkEntry
	{
		public StscChunkEntry(Stream file)
		{
			FirstChunk = file.ReadUInt32BE();
			SamplesPerChunk = file.ReadUInt32BE();
			SampleDescriptionIndex = file.ReadUInt32BE();
		}
		public StscChunkEntry(uint firstChunk, uint samplesPerChunk, uint sampleDesIndex)
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
