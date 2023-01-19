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

		public Mp4Operation ConvertToMp4aAsync(Stream outputStream, ChapterInfo userChapters = null, bool trimOutputToChapters = false)
		{
			FrameTransformBase<FrameEntry, FrameEntry> f1 = GetAudioFrameFilter();

			LosslessFilter f2 = new(outputStream, this);

			f1.LinkTo(f2);

			if (Moov.TextTrack is null || userChapters is not null)
			{
				f2.SetChapterDelegate(() => userChapters);

				Action<Task> continuation = t =>
				{
					f1.Dispose();
					f2.Dispose();
					if (t.IsCompletedSuccessfully)
						Chapters = f2.Chapters;

					outputStream.Close();
				};

				return ProcessAudio(trimOutputToChapters, userChapters.StartOffset, userChapters.EndOffset, continuation, (Moov.AudioTrack, f1));
			}
			else
			{
				ChapterFilter c1 = new(TimeScale);
				f2.SetChapterDelegate(() => c1.Chapters);

				Action<Task> continuation = t =>
				{
					f1.Dispose();
					f2.Dispose();
					c1.Dispose();
					if (t.IsCompletedSuccessfully)
						Chapters = f2.Chapters;

					outputStream.Close();
				};

				return ProcessAudio(continuation, (Moov.AudioTrack, f1), (Moov.TextTrack, c1));
			}
		}

		public Mp4Operation ConvertToMultiMp4aAsync(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback, bool trimOutputToChapters = false)
		{
			FrameTransformBase<FrameEntry, FrameEntry> f1 = GetAudioFrameFilter();
			LosslessMultipartFilter f2 = new
				(userChapters,
				Ftyp,
				Moov,
				newFileCallback);

			f1.LinkTo(f2);

			Action<Task> continuation = t =>
			{
				f1.Dispose();
				f2.Dispose();
			};

			return ProcessAudio(trimOutputToChapters, userChapters.StartOffset, userChapters.EndOffset, continuation ,(Moov.AudioTrack, f1));
		}

		public ChapterInfo GetChapterInfo() => GetChapterInfoAsync().GetAwaiter().GetResult();
		public Mp4Operation<ChapterInfo> GetChapterInfoAsync()
		{
			ChapterFilter c1 = new(TimeScale);

			Func<Task, ChapterInfo> continuation = t =>
			{				
				c1.Dispose();

				Chapters ??= c1.Chapters;
				return c1.Chapters;
			};

			return ProcessAudio(continuation, (Moov.TextTrack, c1));
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

		public Mp4Operation ProcessAudio(Action<Task> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			return ProcessAudio(false, TimeSpan.Zero, TimeSpan.Zero, continuation, filters);
		}
		
		public Mp4Operation<TResult> ProcessAudio<TResult>(Func<Task, TResult> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			return ProcessAudio(false, TimeSpan.Zero, TimeSpan.Zero, continuation, filters);
		}

		public Mp4Operation ProcessAudio(bool doTimeFilter, TimeSpan startTime, TimeSpan endTime, Action<Task> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			CunkReader reader = new(InputStream)
			{
				TotalDuration = Duration
			};

			startTime = doTimeFilter ? startTime: TimeSpan.Zero;
			endTime = doTimeFilter ? endTime : TimeSpan.MaxValue;

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			reader.Initialize(startTime, endTime);
			
			var operation = new Mp4Operation(reader.RunAsync, this, continuation);
			reader.OnProggressUpdateDelegate = operation.OnProggressUpdate;
			return operation;
		}

		public Mp4Operation<TResult> ProcessAudio<TResult>(bool doTimeFilter, TimeSpan startTime, TimeSpan endTime, Func<Task, TResult> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			CunkReader reader = new(InputStream)
			{
				TotalDuration = Duration
			};

			startTime = doTimeFilter ? startTime : TimeSpan.Zero;
			endTime = doTimeFilter ? endTime : TimeSpan.MaxValue;

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			reader.Initialize(startTime, endTime);

			var operation = new Mp4Operation<TResult>(reader.RunAsync, this, continuation);
			reader.OnProggressUpdateDelegate = operation.OnProggressUpdate;
			return operation;
		}

		protected (long audioSize, uint avgBitrate) CalculateAudioSizeAndBitrate()
		{
			//Calculate the actual average bitrate because aaxc file is wrong.
			long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s) * 8;
			double duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
			uint avgBitrate = (uint)(audioBits * TimeScale / duration);

			return (audioBits / 8, avgBitrate);
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
