using AAXClean.Chunks;
using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;
using AAXClean.FrameFilters.Text;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean
{
	public enum ConversionResult
	{
		Cancelled,
		Failed,
		NoErrorsDetected
	}
	public enum FileType
	{
		Aax,
		Aaxc,
		Mpeg4
	}

	public enum SampleRate : int
	{
		_96000 = 96000,
		_88200 = 88200,
		_64000 = 64000,
		_48000 = 48000,
		_44100 = 44100,
		_32000 = 32000,
		_24000 = 24000,
		_22050 = 22050,
		_16000 = 16000,
		_12000 = 12000,
		_11025 = 11025,
		_8000 = 8000,
		_7350 = 7350
	}

	public class Mp4File : Box
	{
		public ChapterInfo Chapters { get; set; }

		public event EventHandler<ConversionProgressEventArgs> ConversionProgressUpdate;
		public AppleTags AppleTags { get; }
		public Stream InputStream => inputStream;
		public FileType FileType { get; }
		public TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
		public uint MaxBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate;
		public uint AverageBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate;
		public uint TimeScale => Moov.AudioTrack.Mdia.Mdhd.Timescale;
		public int AudioChannels => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.ChannelConfiguration;
		public ushort AudioSampleSize => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize;
		public byte[] AscBlob => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.AscBlob;

		public FtypBox Ftyp { get; set; }
		public MoovBox Moov { get; }
		public MdatBox Mdat { get; }

		private readonly TrackedReadStream inputStream;

		public Mp4File(Stream file, long fileSize) : base(new BoxHeader(fileSize, "MPEG"), null)
		{
			inputStream = new TrackedReadStream(file, fileSize);
			LoadChildren(InputStream);
			Ftyp = GetChild<FtypBox>();
			Moov = GetChild<MoovBox>();
			Mdat = GetChild<MdatBox>();

			FileType = Ftyp.MajorBrand switch
			{
				"aax " => FileType.Aax,
				"aaxc" => FileType.Aaxc,
				_ => FileType.Mpeg4
			};

			if (Moov.ILst is not null)
				AppleTags = new AppleTags(Moov.ILst);
		}
		public Mp4File(Stream file) : this(file, file.Length) { }

		public Mp4File(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
			: this(File.Open(fileName, FileMode.Open, access, share)) { }

		public virtual FrameTransformBase<FrameEntry, FrameEntry> GetAudioFrameFilter()
			=> new AacValidateFilter();

		public void Save()
		{
			if (Moov.Header.FilePosition < Mdat.Header.FilePosition)
				throw new Exception("Does not support editing moov before mdat");

			InputStream.Position = Moov.Header.FilePosition;
			Moov.Save(InputStream);

			if (InputStream.Position < InputStream.Length)
			{
				int freeSize = (int)Math.Max(8, InputStream.Length - InputStream.Position);

				FreeBox.Create(freeSize, this).Save(InputStream);
			}
		}

		public ConversionResult ConvertToMp4a(Stream outputStream, ChapterInfo userChapters = null, bool trimOutputToChapters = false)
			=> ConvertToMp4aAsync(outputStream, userChapters, trimOutputToChapters).GetAwaiter().GetResult();

		public async Task<ConversionResult> ConvertToMp4aAsync(Stream outputStream, ChapterInfo userChapters = null, bool trimOutputToChapters = false)
		{
			ConversionResult result;
			using FrameTransformBase<FrameEntry, FrameEntry> f1 = GetAudioFrameFilter();

			using LosslessFilter f2 = new(outputStream, this);

			f1.LinkTo(f2);

			if (Moov.TextTrack is null || userChapters is not null)
			{
				f2.SetChapterDelegate(() => userChapters);
				result = await ProcessAudio(trimOutputToChapters, userChapters.StartOffset, userChapters.EndOffset, (Moov.AudioTrack, f1));
			}
			else
			{
				using ChapterFilter c1 = new(TimeScale);
				f2.SetChapterDelegate(() => c1.Chapters);
				result = await ProcessAudio((Moov.AudioTrack, f1), (Moov.TextTrack, c1));
			}

			if (result == ConversionResult.NoErrorsDetected)
				Chapters = f2.Chapters;

			outputStream.Close();

			return result;
		}
		public ConversionResult ConvertToMultiMp4a(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback, bool trimOutputToChapters = false)
			=> ConvertToMultiMp4aAsync(userChapters, newFileCallback, trimOutputToChapters).GetAwaiter().GetResult();

		public async Task<ConversionResult> ConvertToMultiMp4aAsync(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback, bool trimOutputToChapters = false)
		{
			using FrameTransformBase<FrameEntry, FrameEntry> f1 = GetAudioFrameFilter();
			using LosslessMultipartFilter f2 = new
				(userChapters,
				Ftyp,
				Moov,
				newFileCallback);

			f1.LinkTo(f2);

			return await ProcessAudio(trimOutputToChapters, userChapters.StartOffset, userChapters.EndOffset, (Moov.AudioTrack, f1));
		}

		private void OnProgressUpdate(ConversionProgressEventArgs args)
		{
			ConversionProgressUpdate?.Invoke(this, args);
		}

		public ChapterInfo GetChapterInfo() => GetChapterInfoAsync().GetAwaiter().GetResult();
		public async Task<ChapterInfo> GetChapterInfoAsync()
		{
			ChapterFilter c1 = new(TimeScale);
			await ProcessAudio((Moov.TextTrack, c1));

			Chapters ??= c1.Chapters;
			return c1.Chapters;
		}

		public ChapterInfo GetChaptersFromMetadata()
		{
			TrakBox textTrak = Moov.TextTrack;

			//Get chapter names from metadata box in chapter track
			List<string> chapterNames =
				textTrak
				?.GetChild<UdtaBox>()
				?.GetChild<MetaBox>()
				?.GetChild<AppleListBox>()
				?.Children
				?.Cast<AppleTagBox>()
				?.Where(b => b.Header.Type == "©nam")
				?.Select(b => Encoding.UTF8.GetString(b.Data.Data))
				?.ToList();

			if (chapterNames is null) return null;

			IReadOnlyList<SttsBox.SampleEntry> sampleTimes = textTrak.Mdia.Minf.Stbl.Stts.Samples;

			if (sampleTimes.Count != chapterNames.Count) return null;

			ChunkEntryList cEntryList = new(textTrak);

			if (cEntryList.Count != chapterNames.Count) return null;

			ChapterBuilder builder = new(TimeScale);

			for (int i = 0; i < chapterNames.Count; i++)
			{
				ChunkEntry cEntry = cEntryList[i];
				builder.AddChapter(cEntry.ChunkIndex, chapterNames[(int)cEntry.ChunkIndex], (int)sampleTimes[i].FrameDelta);
			}

			ChapterInfo chlist = builder.ToChapterInfo();

			Chapters ??= chlist;

			return chlist;
		}

		private CancellationTokenSource CancellationSource;
		private Task<ConversionResult> ReaderTask;

		public async Task<ConversionResult> ProcessAudio(params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			return await ProcessAudio(false, TimeSpan.Zero, TimeSpan.Zero, filters);
		}

		public async Task<ConversionResult> ProcessAudio(bool doTimeFilter, TimeSpan startTime, TimeSpan endTime, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			CunkReader reader = new(InputStream)
			{
				TotalDuration = Duration,
				OnProggressUpdateDelegate = OnProgressUpdate
			};

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			CancellationSource = new CancellationTokenSource();

			ReaderTask = Task.Run(() =>reader.RunAsync(CancellationSource.Token, doTimeFilter, startTime, endTime));

			return await ReaderTask;
		}

		protected (long audioSize, uint avgBitrate) CalculateAudioSizeAndBitrate()
		{
			//Calculate the actual average bitrate because aaxc file is wrong.
			long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s) * 8;
			double duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
			uint avgBitrate = (uint)(audioBits * TimeScale / duration);

			return (audioBits / 8, avgBitrate);
		}

		public async Task<ConversionResult> CancelAsync()
		{
			if (CancellationSource is not null)
			{
				CancellationSource.Cancel();
				if (ReaderTask is not null)
				{
					return await ReaderTask;
				}
			}
			return ConversionResult.Cancelled;
		}

		public void Close()
		{
			InputStream?.Close();
		}

		private bool _disposed = false;
		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
				Close();

			_disposed = true;
			base.Dispose(disposing);
		}

		protected override void Render(Stream file) => throw new NotImplementedException();
	}
}
