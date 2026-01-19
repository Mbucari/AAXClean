using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private readonly AudioSampleEntry AudioSampleEntry;
		private readonly ChunkOffsetList AudioChunks = new();
		private readonly ChunkOffsetList TextChunks = new();
		//Since we're only working with audio files, no frame will ever be larger than ushort.MaxValue.
		//Use shorts to save memory.
		private readonly List<ushort> AudioSampleSizes = new();
		private readonly List<int> TextSampleSizes = new();
		private readonly object lockObj = new();
		private uint CurrentFrameDuration;
		private uint FrameDurationCount;

		public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov)
		{
			ArgumentNullException.ThrowIfNull(outputFile, nameof(outputFile));
			ArgumentNullException.ThrowIfNull(ftyp, nameof(ftyp));
			ArgumentNullException.ThrowIfNull(moov, nameof(moov));
			if (!outputFile.CanWrite)
				throw new ArgumentException("The stream is not writable", nameof(outputFile));

			OutputFile = outputFile;
			Moov = MakeBlankMoov(moov);

			Stts = Moov.AudioTrack.Mdia.Minf.Stbl.Stts;
			Stsc = Moov.AudioTrack.Mdia.Minf.Stbl.Stsc;

			AudioSampleEntry = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry
				?? throw new InvalidDataException($"Audio track's stsd box does not contain an {nameof(AudioSampleEntry)}");

			ftyp.Save(OutputFile);
			mdatStart = OutputFile.Position;

			//Placeholder mdat header
			OutputFile.WriteUInt32BE(0);
			OutputFile.WriteType("mdat");
			OutputFile.WriteInt64BE(0);
		}

		public Mp4aWriter(Stream outputFile, FtypBox ftyp, MoovBox moov, byte[] ascBytes)
			: this(outputFile, ftyp, moov)
		{
			ArgumentNullException.ThrowIfNull(ascBytes, nameof(ascBytes));

			AudioSampleEntry.Header.ChangeAtomName("mp4a");

			if (AudioSampleEntry.Esds is EsdsBox esds)
				AudioSampleEntry.Children.Remove(esds);

			if (AudioSampleEntry.Dec3 is Dec3Box dec3)
				AudioSampleEntry.Children.Remove(dec3);

			esds = EsdsBox.CreateEmpty(AudioSampleEntry);

			var asc = esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig;

			asc.AscBlob = ascBytes;
			if (asc.ChannelConfiguration > 2)
				throw new NotSupportedException($"Only supports maximum of 2-channel audio. (Channels={asc.ChannelConfiguration})");
			AudioSampleEntry.ChannelCount = (ushort)asc.ChannelConfiguration;

			SetTimeScale((uint)asc.SamplingFrequency);
		}

		private void SetTimeScale(uint timeScale)
		{
			Debug.Assert(timeScale <= ushort.MaxValue);
			AudioSampleEntry.SampleRate = (ushort)timeScale;
			Moov.AudioTrack.Mdia.Mdhd.Timescale = timeScale;
			if (Moov.TextTrack is not null)
			{
				Moov.TextTrack.Mdia.Mdhd.Timescale = Moov.AudioTrack.Mdia.Mdhd.Timescale;
			}
		}

		private void SetDuration(ulong duration)
		{
			Moov.Mvhd.Duration = duration * Moov.Mvhd.Timescale / Moov.AudioTrack.Mdia.Mdhd.Timescale;

			Moov.AudioTrack.Mdia.Mdhd.Duration = duration;
			Moov.AudioTrack.Tkhd.Duration = Moov.Mvhd.Duration;

			if (Moov.TextTrack is not null)
			{
				Moov.TextTrack.Mdia.Mdhd.Duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
				Moov.TextTrack.Tkhd.Duration = Moov.Mvhd.Duration;
			}
		}

		protected virtual void SaveMoov()
		{
			Moov.Save(OutputFile);
		}

		public void Close()
		{
			lock (lockObj)
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

			WriteChapterMetadata(chapterTitles);

			Stsc.Samples.Add(new StscBox.StscChunkEntry(CurrentChunk, SamplesPerChunk, 1));
			Stts.Samples.Add(new SttsBox.SampleEntry(FrameDurationCount, CurrentFrameDuration));
			FrameDurationCount = 0;
			Debug.Assert(AudioSampleSizes.Count == Stts.Samples.Sum(s => s.FrameCount));
			IStszBox stsz = StszBox.CreateBlank(Moov.AudioTrack.Mdia.Minf.Stbl, AudioSampleSizes);

			IChunkOffsets.Create(Moov.AudioTrack.Mdia.Minf.Stbl, AudioChunks);

			if (Moov.TextTrack is not null)
			{
				IChunkOffsets.Create(Moov.TextTrack.Mdia.Minf.Stbl, TextChunks);
			}

			SetDuration((ulong)Stts.Samples.Sum(s => (decimal)s.FrameCount * s.FrameDelta));

			(uint maxBitRate, uint avgBitrate)
				= CalculateBitrate(
					Moov.AudioTrack.Mdia.Mdhd.Timescale,
					Moov.AudioTrack.Mdia.Mdhd.Duration,
					stsz,
					Stts);

			if (AudioSampleEntry.Esds is EsdsBox esds)
			{
				esds.ES_Descriptor.DecoderConfig.MaxBitrate = maxBitRate;
				esds.ES_Descriptor.DecoderConfig.AverageBitrate = avgBitrate;
			}

			if (AudioSampleEntry.GetChild<BtrtBox>() is null)
				BtrtBox.Create(0, maxBitRate, avgBitrate, AudioSampleEntry);

			SaveMoov();
			Closed = true;
		}

		private static (uint maxOneSecondBitrate, uint avgBitrate) CalculateBitrate(double timeScale, ulong duration, IStszBox stsz, SttsBox stts)
		{
			//Calculate the actual average bitrate because aaxc file is wrong.
			long audioBits = stsz.TotalSize * 8;
			uint avgBitrate = (uint)(audioBits * timeScale / duration);

			//Expand the stts sample table to one sample duration per frame.
			//Audio frame sizes are always small (on the order of 1000), so cast to ushort
			//to save on memory.
			var frameDeltas = stts.EnumerateFrameDeltas().Select(d => (ushort)d).ToArray();

			if (stts.SampleTimeCount != stsz.SampleCount || stts.SampleTimeCount != frameDeltas.Length)
				throw new InvalidOperationException($"The number of sample deltas ({stts.SampleTimeCount}) doesn't match the number of sample sizes ({stsz.SampleCount}).");

			long currentWindowSampleSpan = 0;
			long windowSizeInBytes = 0;
			double maxOneSecondBitrate = 0;

			for (int i = 0, beginIndex = 0; i < stsz.SampleCount; i++)
			{
				while (currentWindowSampleSpan > timeScale)
				{
					double bitrate = windowSizeInBytes * 8 * timeScale / currentWindowSampleSpan;
					if (bitrate > maxOneSecondBitrate)
						maxOneSecondBitrate = bitrate;

					windowSizeInBytes -= stsz.GetSizeAtIndex(beginIndex);
					currentWindowSampleSpan -= frameDeltas[beginIndex];
					beginIndex++;
				}

				windowSizeInBytes += stsz.GetSizeAtIndex(i);
				currentWindowSampleSpan += frameDeltas[i];
			}

			return ((uint)Math.Round(maxOneSecondBitrate), avgBitrate);
		}

		private readonly List<string> chapterTitles = new();

		public void WriteChapter(ChapterEntry entry)
		{
			if (Moov.TextTrack is null) return;

			if (Moov.TextTrack.Mdia.Minf.Stbl.Stsz is null)
				StszBox.CreateBlank(Moov.TextTrack.Mdia.Minf.Stbl, TextSampleSizes);

			if (!Moov.TextTrack.Mdia.Minf.Stbl.Stsc.Samples.Any())
				Moov.TextTrack.Mdia.Minf.Stbl.Stsc.Samples.Add(new StscBox.StscChunkEntry(1, 1, 1));

			chapterTitles.Add(entry.Title);

			Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Add(new SttsBox.SampleEntry(sampleCount: 1, entry.SamplesInFrame));
			TextSampleSizes.Add(entry.FrameData.Length);
			TextChunks.Add(OutputFile.Position);

			OutputFile.Write(entry.FrameData.Span);
		}

		private void WriteChapterMetadata(IEnumerable<string> chapterTitles)
		{
			if (Moov.TextTrack is null) return;

			AppleListBox? chapterNames =
				Moov.TextTrack
				?.GetChild<UdtaBox>()
				?.GetChild<MetaBox>()
				?.GetChild<AppleListBox>();

			if (chapterNames is null) return;

			chapterNames.Children.Clear();

			foreach (var title in chapterTitles)
			{
				chapterNames.AddTag("©nam", title);
				chapterNames.AddTag("©cmt", title);
			}
		}

		public void RemoveTextTrack()
		{
			if (Moov.TextTrack is null || !Moov.Children.Remove(Moov.TextTrack))
				return;

			uint trackNum = 1;
			Dictionary<uint, uint> trackRemap = [];
			foreach (var t in Moov.GetChildren<TrakBox>().OrderBy(t => t.Tkhd.TrackID))
			{
				trackRemap[t.Tkhd.TrackID] = trackNum;
				t.Tkhd.TrackID = trackNum++;
			}

			Moov.Mvhd.NextTrackID = trackNum;

			foreach (var track in Moov.GetChildren<TrakBox>().Select(c => c.GetChild<TrefBox>()).OfType<TrefBox>())
			{
				foreach (var tref in track.References.ToArray())
				{
					if (tref.Header.Type == "chap")
					{
						track.References.Remove(tref);
						continue;
					}

					//Update track id to remapped values
					foreach (var tid in tref.TrackIds.Order().ToArray())
					{
						if (!trackRemap.TryGetValue(tid, out var remap))
							tref.TrackIds.Remove(tid);
						else if (remap != tid)
						{
							tref.TrackIds.Remove(tid);
							tref.TrackIds.Add(remap);
						}
					}

					if (tref.TrackIds.Count == 0)
						track.References.Remove(tref);
				}

				if (track.References.Count == 0)
					track.Parent!.Children.Remove(track);
			}
		}

		public void AddFrame(Span<byte> frame, bool newChunk, uint frameDelta)
		{
			lock (lockObj)
			{
				if (Closing) return;

				if (newChunk)
				{
					AudioChunks.Add(OutputFile.Position);

					if (SamplesPerChunk > 0 && SamplesPerChunk != LastSamplesPerChunk)
					{
						Stsc.Samples.Add(new StscBox.StscChunkEntry(CurrentChunk, SamplesPerChunk, 1));

						LastSamplesPerChunk = SamplesPerChunk;
					}
					SamplesPerChunk = 0;
					CurrentChunk++;
				}

				AudioSampleSizes.Add((ushort)frame.Length);

				if (CurrentFrameDuration == 0)
				{
					CurrentFrameDuration = frameDelta;
				}
				else if (CurrentFrameDuration != frameDelta)
				{
					Stts.Samples.Add(new SttsBox.SampleEntry(FrameDurationCount, CurrentFrameDuration));
					FrameDurationCount = 0;
					CurrentFrameDuration = frameDelta;
				}

				FrameDurationCount++;
				SamplesPerChunk++;
			}

			OutputFile.Write(frame);
		}

		private static MoovBox MakeBlankMoov(MoovBox moov)
		{
			SttsBox? t1 = null;
			StscBox? t2 = null;
			IStszBox? t3 = null;
			IChunkOffsets? t4 = null;

			if (moov.TextTrack is not null)
			{
				t1 = moov.TextTrack.Mdia.Minf.Stbl.Stts;
				t2 = moov.TextTrack.Mdia.Minf.Stbl.Stsc;
				t3 = moov.TextTrack.Mdia.Minf.Stbl.Stsz;
				t4 = moov.TextTrack.Mdia.Minf.Stbl.COBox;

				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t1);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t2);
				if (t3 is not null)
					moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t3);
				moov.TextTrack.Mdia.Minf.Stbl.Children.Remove(t4);
			}

			SttsBox a1 = moov.AudioTrack.Mdia.Minf.Stbl.Stts;
			StscBox a2 = moov.AudioTrack.Mdia.Minf.Stbl.Stsc;
			IStszBox? a3 = moov.AudioTrack.Mdia.Minf.Stbl.Stsz;
			IChunkOffsets a4 = moov.AudioTrack.Mdia.Minf.Stbl.COBox;

			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a1);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a2);
			if (a3 is not null)
				moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a3);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Remove(a4);

			MvexBox? mvex = moov.GetChild<MvexBox>();
			if (mvex is not null)
				moov.Children.Remove(mvex);

			MemoryStream ms = new();

			moov.Save(ms);

			if (mvex is not null)
				moov.Children.Add(mvex);

			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a1);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a2);
			if (a3 is not null)
				moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a3);
			moov.AudioTrack.Mdia.Minf.Stbl.Children.Add(a4);

			if (moov.TextTrack is not null)
			{
				if (t1 is not null)
					moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t1);
				if (t2 is not null)
					moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t2);
				if (t3 is not null)
					moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t3);
				if (t4 is not null)
					moov.TextTrack.Mdia.Minf.Stbl.Children.Add(t4);
			}

			ms.Position = 0;

			MoovBox newMoov = BoxFactory.CreateBox<MoovBox>(ms, null);

			// Create chunk offset and sample size boxes when closing the file
			// so we know whether to create stco/co64 and stsz/stz2
			SttsBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);
			StscBox.CreateBlank(newMoov.AudioTrack.Mdia.Minf.Stbl);

			newMoov.AudioTrack.Mdia.Mdhd.CreationTime = newMoov.AudioTrack.Mdia.Mdhd.ModificationTime = DateTimeOffset.UtcNow;

			if (newMoov.TextTrack is not null)
			{
				SttsBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);
				StscBox.CreateBlank(newMoov.TextTrack.Mdia.Minf.Stbl);

				newMoov.TextTrack.Mdia.Mdhd.CreationTime = newMoov.TextTrack.Mdia.Mdhd.ModificationTime = newMoov.AudioTrack.Mdia.Mdhd.CreationTime;
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
				AudioSampleSizes.Clear();
				AudioChunks.Clear();
				TextChunks.Clear();
				disposed = true;
			}
		}
		#endregion
	}
}
