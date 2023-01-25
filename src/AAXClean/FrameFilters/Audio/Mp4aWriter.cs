using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AAXClean.FrameFilters.Audio
{
	public class Mp4aWriter
	{
		public Stream OutputFile { get; }

		public readonly MoovBox Moov;

		private long LastSamplesPerChunk = -1;
		private uint SamplesPerChunk = 0;
		private uint CurrentChunk = 0;
		private bool Closed;
		private bool Closing;

		private readonly long mdatStart;
		private readonly SttsBox Stts;
		private readonly StscBox Stsc;
		private readonly StszBox Stsz;
		private readonly List<ChunkOffsetEntry> AudioChunks = new();
		private readonly List<ChunkOffsetEntry> TextChunks = new();
		private readonly object lockObj = new();

		private const int AAC_TIME_DOMAIN_SAMPLES = 1024;
		private static readonly int[] asc_samplerates = { 96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350 };

		public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov)
		{
			OutputFile = outputFile;
			Moov = MakeBlankMoov(moov);

			Stts = Moov.AudioTrack.Mdia.Minf.Stbl.Stts;
			Stsc = Moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
			Stsz = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz;

			ftyp.Save(OutputFile);
			mdatStart = OutputFile.Position;

			//Placeholder mdat header
			OutputFile.WriteUInt32BE(0);
			OutputFile.WriteType("mdat");
			OutputFile.WriteInt64BE(0);
		}

		public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov, bool co64, int sampleRate, int channels)
			: this(outputFile, ftyp, moov)
		{
			int sampleRateIndex = Array.IndexOf(asc_samplerates, sampleRate);

			if (sampleRateIndex < 0)
				throw new NotSupportedException($"Unsupported sample rate: {sampleRate}");
			if (channels > 2)
				throw new NotSupportedException($"Only supports maximum of 2-channel audio. (Channels={channels})");

			Moov.AudioTrack.Mdia.Mdhd.Timescale = (uint)sampleRate;
			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleRate = (ushort)sampleRate;
			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.SamplingFrequencyIndex = sampleRateIndex;
			//Channel Configuration only equals number of channels for stereo and mono.
			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.ChannelConfiguration = channels;

			if (Moov.TextTrack is not null)
			{
				Moov.TextTrack.Mdia.Mdhd.Timescale = (uint)sampleRate;
			}
		}

		public void Close()
		{
			lock(lockObj)
			{
				if (Closing) return;
				Closing = true;
			}

			if (Closed || !OutputFile.CanWrite) return;

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

			Stsc.Samples.Add(new StscBox.StscChunkEntry(CurrentChunk, SamplesPerChunk, 1));

			Stts.EntryCount = 1;
			Stts.Samples.Add(new SttsBox.SampleEntry((uint)Stsz.SampleSizes.Count, AAC_TIME_DOMAIN_SAMPLES));

			Stsz.SampleCount = (uint)Stsz.SampleSizes.Count;
			Stsc.EntryCount = (uint)Stsc.Samples.Count;

			IChunkOffsets cobox_audio
				= mdatSize > uint.MaxValue
				? Co64Box.CreateBlank(Moov.AudioTrack.Mdia.Minf.Stbl, AudioChunks)
				: StcoBox.CreateBlank(Moov.AudioTrack.Mdia.Minf.Stbl, AudioChunks);

			if (Moov.TextTrack is not null)
			{
				IChunkOffsets cobox_text
				= mdatSize > uint.MaxValue
				? Co64Box.CreateBlank(Moov.TextTrack.Mdia.Minf.Stbl, TextChunks)
				: StcoBox.CreateBlank(Moov.TextTrack.Mdia.Minf.Stbl, TextChunks);
			}

			Moov.AudioTrack.Mdia.Mdhd.Duration = (ulong)Stsz.SampleCount * AAC_TIME_DOMAIN_SAMPLES;
			Moov.Mvhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration * Moov.Mvhd.Timescale / Moov.AudioTrack.Mdia.Mdhd.Timescale;
			Moov.AudioTrack.Tkhd.Duration = Moov.Mvhd.Duration;

			(uint maxBitRate, uint avgBitrate)
				= CalculateBitrate(
					Moov.AudioTrack.Mdia.Mdhd.Timescale,
					Moov.AudioTrack.Mdia.Mdhd.Duration,
					Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes);

			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate = maxBitRate;
			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate = avgBitrate;

			var btrt = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.GetChild<BtrtBox>();

			if (btrt is null)
			{
				BtrtBox.Create(0, maxBitRate, avgBitrate, Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry);
			}
			else
			{
				btrt.MaxBitrate = maxBitRate;
				btrt.AvgBitrate = avgBitrate;
			}

			if (Moov.TextTrack is not null)
			{
				Moov.TextTrack.Mdia.Mdhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
				Moov.TextTrack.Tkhd.Duration = Moov.Mvhd.Duration;
			}

			Moov.Save(OutputFile);
			Closed = true;

			static (uint maxOneSecondBitrate, uint avgBitrate) CalculateBitrate(double timeScale, ulong duration, List<int> sampleSizes)
			{
				//Calculate the actual average bitrate because aaxc file is wrong.
				long audioBits = sampleSizes.Sum(s => (long)s) * 8;
				uint avgBitrate = (uint)(audioBits * timeScale / duration);

				int framesPerSec = (int)Math.Round(timeScale / AAC_TIME_DOMAIN_SAMPLES);
				int currentMovingSum = sampleSizes.Take(framesPerSec).Sum();
				int currentMax = currentMovingSum;

				for (int i = framesPerSec; i < sampleSizes.Count; i++)
				{
					var lose = sampleSizes[i - framesPerSec];
					var gain = sampleSizes[i];

					currentMovingSum += gain - lose;

					if (currentMovingSum > currentMax)
						currentMax = currentMovingSum;
				}

				double maxOneSecondBitrate = currentMax * timeScale * 8 / framesPerSec / AAC_TIME_DOMAIN_SAMPLES;

				return ((uint)Math.Round(maxOneSecondBitrate), avgBitrate);
			}
		}

		public void WriteChapters(ChapterInfo chapters)
		{
			if (Moov.TextTrack is null) return;

			Moov.TextTrack.Mdia.Minf.Stbl.Stsc.EntryCount = 1;
			Moov.TextTrack.Mdia.Minf.Stbl.Stsc.Samples.Add(new StscBox.StscChunkEntry(1, 1, 1));

			uint entIndex = 0;
			foreach (Chapter c in chapters)
			{
				uint sampleDelta = (uint)(c.Duration.TotalSeconds * Moov.AudioTrack.Mdia.Mdhd.Timescale);

				Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Add(new SttsBox.SampleEntry(sampleCount: 1, sampleDelta));
				Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Add(c.RenderSize);

				TextChunks.Add(new ChunkOffsetEntry { EntryIndex = entIndex++, ChunkOffset = OutputFile.Position });

				c.WriteChapter(OutputFile);
			}
			Moov.TextTrack.Mdia.Minf.Stbl.Stts.EntryCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Count;
			Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleCount = (uint)Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Count;

			AppleListBox chapterNames =
				Moov.TextTrack
				?.GetChild<UdtaBox>()
				?.GetChild<MetaBox>()
				?.GetChild<AppleListBox>();

			if (chapterNames is null) return;

			chapterNames.Children.Clear();

			foreach (Chapter c in chapters)
			{
				chapterNames.AddTag("©nam", c.Title);
				chapterNames.AddTag("©cmt", c.Title);
			}
		}

		public void RemoveTextTrack()
		{
			Moov.Children.Remove(Moov.TextTrack);

			var trefBox = Moov.AudioTrack.Children.OfType<UnknownBox>().First(u => u.Header.Type == "tref");
			if (trefBox != null)
				Moov.AudioTrack.Children.Remove(trefBox);
			Moov.Mvhd.NextTrackID--;
		}

		public void AddFrame(Span<byte> frame, bool newChunk)
		{
			lock (lockObj)
			{
				if (Closing) return;

				if (newChunk)
				{
					AudioChunks.Add(new ChunkOffsetEntry { EntryIndex = CurrentChunk, ChunkOffset = OutputFile.Position });

					if (SamplesPerChunk > 0 && SamplesPerChunk != LastSamplesPerChunk)
					{
						Stsc.Samples.Add(new StscBox.StscChunkEntry(CurrentChunk, SamplesPerChunk, 1));

						LastSamplesPerChunk = SamplesPerChunk;
					}
					SamplesPerChunk = 0;
					CurrentChunk++;
				}

				Stsz.SampleSizes.Add(frame.Length);
				SamplesPerChunk++;
			}

			OutputFile.Write(frame);
		}		

		private static MoovBox MakeBlankMoov(MoovBox moov)
		{
			SttsBox t1 = null;
			StscBox t2 = null;
			StszBox t3 = null;
			Box t4 = null;

			bool hasTextTrack = false;
			if (moov.TextTrack != null)
			{
				hasTextTrack = true;
				t1 = moov.TextTrack.Mdia.Minf.Stbl.Stts;
				t2 = moov.TextTrack.Mdia.Minf.Stbl.Stsc;
				t3 = moov.TextTrack.Mdia.Minf.Stbl.Stsz;
				t4 = (Box)moov.TextTrack.Mdia.Minf.Stbl.COBox;

				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t1);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t2);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t3);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t4);
			}

			SttsBox a1 = moov.AudioTrack.Mdia.Minf.Stbl.Stts;
			StscBox a2 = moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
			StszBox a3 = moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
			Box a4 = (Box)moov.AudioTrack.Mdia.Minf.Stbl.COBox;

			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a1);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a2);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a3);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a4);

			MemoryStream ms = new();

			moov.Save(ms);

			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a1);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a2);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a3);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a4);

			if (hasTextTrack)
			{
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t1);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t2);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t3);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t4);
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

			return newMoov;
		}

		#region IDisposable
		private bool disposed = false;
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Mp4aWriter()
		{
			Dispose(disposing: false);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				Close();
				Stsc?.Samples.Clear();
				Stsz?.SampleSizes.Clear();
				AudioChunks.Clear();
				TextChunks.Clear();
			}
		}
		#endregion
	}
}
