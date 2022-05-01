using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;

namespace AAXClean.AudioFilters
{
	internal class Mp4aWriter
	{
		internal Stream OutputFile { get; }

		private const int AAC_TIME_DOMAIN_SAMPLES = 1024;

		private readonly long mdatStart;
		private readonly MoovBox Moov;
		private readonly SttsBox Stts;
		private readonly StscBox Stsc;
		private readonly StszBox Stsz;
		private readonly StcoBox Stco;
		private readonly Co64Box Co64;
		private readonly bool IsCo64;

		private long LastSamplesPerChunk = -1;
		private uint SamplesPerChunk = 0;
		private uint CurrentChunk = 0;
		private bool Closed;

		public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov, bool co64)
		{
			OutputFile = outputFile;
			IsCo64 = co64;
			Moov = MakeBlankMoov(moov, IsCo64);

			Stts = Moov.AudioTrack.Mdia.Minf.Stbl.Stts;
			Stsc = Moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
			Stco = Moov.AudioTrack.Mdia.Minf.Stbl.Stco;
			Stsz = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
			Co64 = Moov.AudioTrack.Mdia.Minf.Stbl.Co64;

			ftyp.Save(OutputFile);
			mdatStart = OutputFile.Position;

			//Placeholder mdat header
			OutputFile.WriteUInt32BE(0);
			OutputFile.WriteType("mdat");
			if (IsCo64)
				outputFile.WriteInt64BE(0);
		}

		public void Close()
		{
			if (Closed) return;

			long mdatEnd = OutputFile.Position;

			long mdatSize = mdatEnd - mdatStart;

			OutputFile.Position = mdatStart;

			if (mdatSize <= uint.MaxValue)
				OutputFile.WriteUInt32BE((uint)mdatSize);
			else
			{
				OutputFile.WriteUInt32BE(1);
			}
			OutputFile.WriteType("mdat");

			if (mdatSize > uint.MaxValue)
				OutputFile.WriteInt64BE(mdatSize);


			OutputFile.Position = mdatEnd;


			Stsc.Samples.Add(new StscBox.ChunkEntry(CurrentChunk, SamplesPerChunk, 1));

			Stts.EntryCount = 1;
			Stts.Samples.Add(new SttsBox.SampleEntry((uint)Stsz.SampleSizes.Count, AAC_TIME_DOMAIN_SAMPLES));

			Stsz.SampleCount = (uint)Stsz.SampleSizes.Count;
			Stsc.EntryCount = (uint)Stsc.Samples.Count;

			if (IsCo64)
				Co64.EntryCount = (uint)Co64.ChunkOffsets.Count;
			else
				Stco.EntryCount = (uint)Stco.ChunkOffsets.Count;

			Moov.AudioTrack.Mdia.Mdhd.Duration = (ulong)Stsz.SampleCount * AAC_TIME_DOMAIN_SAMPLES;
			Moov.Mvhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration * Moov.Mvhd.Timescale / Moov.AudioTrack.Mdia.Mdhd.Timescale;
			Moov.AudioTrack.Tkhd.Duration = Moov.Mvhd.Duration;

			Moov.Save(OutputFile);
			Closed = true;
		}

		public void WriteChapters(ChapterInfo chapters)
		{
			if (Moov.TextTrack is null) return;

			Moov.TextTrack.Mdia.Minf.Stbl.Stsc.EntryCount = 1;
			Moov.TextTrack.Mdia.Minf.Stbl.Stsc.Samples.Add(new StscBox.ChunkEntry(1, 1, 1));

			uint entIndex = 0;
			foreach (Chapter c in chapters)
			{
				uint sampleDelta = (uint)(c.Duration.TotalSeconds * Moov.AudioTrack.Mdia.Mdhd.Timescale);

				Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Add(new SttsBox.SampleEntry(sampleCount: 1, sampleDelta));
				Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Add(c.RenderSize);

				if (IsCo64)
					Moov.TextTrack.Mdia.Minf.Stbl.Co64.ChunkOffsets.Add(new ChunkOffsetEntry { EntryIndex = entIndex++, ChunkOffset = OutputFile.Position });
				else
					Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Add(new ChunkOffsetEntry { EntryIndex = entIndex++, ChunkOffset = (uint)OutputFile.Position });

				c.WriteChapter(OutputFile);
			}
			Moov.TextTrack.Mdia.Minf.Stbl.Stts.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Count;
			Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Count;
			if (IsCo64)
				Moov.TextTrack.Mdia.Minf.Stbl.Co64.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Co64.ChunkOffsets.Count;
			else
				Moov.TextTrack.Mdia.Minf.Stbl.Stco.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Count;

		}

		public void RemoveTextTrack()
		{
			Moov.Children.Remove(Moov.TextTrack);

			Moov.Mvhd.NextTrackID--;
		}

		public void AddFrame(Span<byte> frame, bool newChunk)
		{
			if (newChunk)
			{

				if (IsCo64)
					Co64.ChunkOffsets.Add(new ChunkOffsetEntry { EntryIndex = CurrentChunk, ChunkOffset = OutputFile.Position });
				else
					Stco.ChunkOffsets.Add(new ChunkOffsetEntry { EntryIndex = CurrentChunk, ChunkOffset = (uint)OutputFile.Position });

				if (SamplesPerChunk > 0 && SamplesPerChunk != LastSamplesPerChunk)
				{
					Stsc.Samples.Add(new StscBox.ChunkEntry(CurrentChunk, SamplesPerChunk, 1));

					LastSamplesPerChunk = SamplesPerChunk;
				}
				SamplesPerChunk = 0;
				CurrentChunk++;
			}

			Stsz.SampleSizes.Add(frame.Length);
			SamplesPerChunk++;

			OutputFile.Write(frame);
		}

		public void Dispose()
		{
			Close();
			Stsc?.Samples.Clear();
			Stsz?.SampleSizes.Clear();
			Stco?.ChunkOffsets.Clear();
			Co64?.ChunkOffsets.Clear();
		}

		private static MoovBox MakeBlankMoov(MoovBox moov, bool co64)
		{
			SttsBox t1 = null;
			StscBox t2 = null;
			StszBox t3 = null;
			StcoBox t4 = null;
			Co64Box t5 = null;
			UdtaBox ttUdata = null;

			bool hasTextTrack = false;
			if (moov.TextTrack != null)
			{
				hasTextTrack = true;
				t1 = moov.TextTrack.Mdia.Minf.Stbl.Stts;
				t2 = moov.TextTrack.Mdia.Minf.Stbl.Stsc;
				t3 = moov.TextTrack.Mdia.Minf.Stbl.Stsz;
				t4 = moov.TextTrack.Mdia.Minf.Stbl.Stco;
				t5 = moov.TextTrack.Mdia.Minf.Stbl.Co64;

				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t1);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t2);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t3);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t4);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t5);

				ttUdata = moov.TextTrack.GetChild<UdtaBox>();
				moov.TextTrack.Children.Remove(ttUdata);
			}

			SttsBox a1 = moov.AudioTrack.Mdia.Minf.Stbl.Stts;
			StscBox a2 = moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
			StszBox a3 = moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
			StcoBox a4 = moov.AudioTrack.Mdia.Minf.Stbl.Stco;
			Co64Box a5 = moov.AudioTrack.Mdia.Minf.Stbl.Co64;

			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a1);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a2);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a3);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a4);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a5);

			MemoryStream ms = new();

			moov.Save(ms);

			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a1);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a2);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a3);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a4);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a5);

			if (hasTextTrack)
			{
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t1);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t2);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t3);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t4);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t5);
				moov.TextTrack.Children.Add(ttUdata);
			}

			ms.Position = 0;

			MoovBox newMoov = new(ms, new BoxHeader(ms), null);

			SttsBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
			StscBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
			StszBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);

			if (hasTextTrack)
			{
				SttsBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
				StscBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
				StszBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
			}

			if (co64)
			{
				Co64Box.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
				if (hasTextTrack)
					Co64Box.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
			}
			else
			{
				StcoBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
				if (hasTextTrack)
					StcoBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
			}

			return newMoov;
		}
	}
}
