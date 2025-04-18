using AAXClean.FrameFilters;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Chunks;
using Mpeg4Lib.Util;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable enable
namespace AAXClean.Chunks;

internal class DashChunkReader : ChunkReader
{
	private DashFile Dash { get; }

	public DashChunkReader(DashFile dash, Stream inputStream, TimeSpan startTime, TimeSpan endTime)
		: base(inputStream, startTime, endTime)
	{
		Dash = dash;
	}

	protected override FrameEntry CreateFrameEntry(ChunkEntry chunk, int frameInChunk, uint frameDelta, Memory<byte> frameData)
	{
		var entry = base.CreateFrameEntry(chunk, frameInChunk, frameDelta, frameData);

		if (chunk.ExtraData is not byte[][] IVs || IVs.Length <= frameInChunk)
			throw new InvalidDataException($"The dash chunk entry does not contain IVs");

		entry.ExtraData = IVs[frameInChunk];
		return entry;
	}

	public override void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter)
	{
		if (Tracks.Count > 0)
			throw new InvalidOperationException($"The {nameof(DashChunkReader)} currently only supports a single track.");
		base.AddTrack(trak, filter);
	}

	private (MoofBox, MdatBox, long) SkipToFirstMoof(long minimumSample)
	{
		long startPosition = Dash.FirstMoof.Header.FilePosition;
		long dataOffset = 0;
		long runningSamples = 0;

		if (Dash.Sidx.Segments.Any(s => s.ReferenceType || !s.StartsWithSAP || s.SapType != 1 || s.SapDeltaTime != 0))
			throw new InvalidOperationException($"AAXClean doesn't know how to inrepret segment index boxes other than " +
				$"{nameof(SidxBox.Segment.SapType)} = 1, " +
				$"{nameof(SidxBox.Segment.SapDeltaTime)} = 0, " +
				$"{nameof(SidxBox.Segment.StartsWithSAP)} = 1, " +
				$"{nameof(SidxBox.Segment.ReferenceType)} = 0");

		foreach (var segment in Dash.Sidx.Segments)
		{
			if (minimumSample < runningSamples + segment.SubsegmentDuration)
				break;

			dataOffset += segment.ReferenceSize;
			runningSamples += segment.SubsegmentDuration;
		}

		if (dataOffset == 0)
		{
			return (Dash.FirstMoof, Dash.FirstMdat, runningSamples);
		}
		else
		{
			InputStream.SeekToOffset(startPosition + dataOffset);
			var firstMoof = (MoofBox)BoxFactory.CreateBox(InputStream, parent: null);
			var firstMdat = (MdatBox)BoxFactory.CreateBox(InputStream, parent: null);

			return (firstMoof, firstMdat, runningSamples);
		}
	}

	protected override IEnumerable<TrackChunk> EnumerateChunks()
	{
		long minimumSample = (long)(StartTime.TotalSeconds * GetTrackTimescale(0));
		long maximumSample = (long)(EndTime.TotalSeconds * GetTrackTimescale(0));

		var (moofBox, mdatBox, startSample) = SkipToFirstMoof(minimumSample);

		var totalDataSize = Dash.Sidx.Segments.Sum(s => s.ReferenceSize);
		var endOfFile = Dash.FirstMoof.Header.FilePosition + totalDataSize;

		while (InputStream.Position < endOfFile)
		{
			if (startSample > maximumSample)
				yield break; //No more samples in range

			var trackChunk = ValidateMdatSize(moofBox, mdatBox, startSample);
			startSample += trackChunk.Entry.FrameDurations.Sum(d => d);
			if (startSample > minimumSample)
			{
				yield return trackChunk;
			}
			else
			{
				//Skip over mdat to next moof
				var endOfMdat = InputStream.Position + mdatBox.Header.TotalBoxSize - mdatBox.Header.HeaderSize;
				InputStream.SeekToOffset(endOfMdat);
			}

			if (InputStream.Position < endOfFile)
			{
				moofBox = (MoofBox)BoxFactory.CreateBox(InputStream, parent: null);
				mdatBox = (MdatBox)BoxFactory.CreateBox(InputStream, parent: null);
			}
		}
	}

	private TrackChunk ValidateMdatSize(MoofBox moofBox, MdatBox mdatBox, long startSample)
	{
		var trun = moofBox.Traf.Trun;
		var chunkDataSize = trun.Samples.Sum(s => s.SampleSize ?? 0);
		if (chunkDataSize != mdatBox.Header.TotalBoxSize - mdatBox.Header.HeaderSize)
			throw new InvalidDataException("Mdat box size doesn't match sample sizes in track fragment run box");

		var frameSizes = trun.Samples.Select(s => s.SampleSize ?? 0).ToArray();

		var frameDurations
			= trun.sample_duration_present ? trun.Samples.Select(s => s.SampleDuration).OfType<uint>().ToArray()
			: moofBox.Traf.Tfhd.DefaultSampleDuration is uint sampleDuration ? Enumerable.Repeat(sampleDuration, trun.Samples.Length).ToArray()
			: throw new InvalidOperationException("Trun sample infos don't contain sample durations and no default sample duration is set.");

		if (frameDurations.Length != frameSizes.Length)
			throw new InvalidDataException($"The number of frame sizes ({frameSizes.Length}) does not match the number of durations ({frameDurations.Length}) in fragment {moofBox.Mfhd.SequenceNumber}");

		var ivs = moofBox.Traf.GetChild<SencBox>().IVs;

		if (frameSizes.Length != ivs.Length)
			throw new InvalidDataException($"The number of IVs ({ivs.Length}) does not match the number of samples ({frameSizes.Length}) in fragment {moofBox.Mfhd.SequenceNumber}");

		return new TrackChunk()
		{
			TrackNum = 0,
			Entry = new ChunkEntry
			{
				ChunkIndex = (uint)moofBox.Mfhd.SequenceNumber,
				ChunkOffset = InputStream.Position,
				ChunkSize = chunkDataSize,
				FirstSample = startSample,
				FrameSizes = frameSizes,
				FrameDurations = frameDurations,
				ExtraData = ivs
			},
		};
	}
}
