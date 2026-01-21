using AAXClean.Chunks;
using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;
using AAXClean.FrameFilters.Text;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Chunks;
using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean
{
	public enum FileType
	{
		Aax,
		Aaxc,
		Mpeg4,
		Dash
	}

	public enum SampleRate : int
	{
		Hz_96000 = 96000,
		Hz_88200 = 88200,
		Hz_64000 = 64000,
		Hz_48000 = 48000,
		Hz_44100 = 44100,
		Hz_32000 = 32000,
		Hz_24000 = 24000,
		Hz_22050 = 22050,
		Hz_16000 = 16000,
		Hz_12000 = 12000,
		Hz_11025 = 11025,
		Hz_8000 = 8000,
		Hz_7350 = 7350
	}

	public class Mp4File : IDisposable
	{
		public ChapterInfo? Chapters { get; set; }
		public AppleTags AppleTags { get; }
		public Stream InputStream { get; }
		public FileType FileType { get; }
		public virtual TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
		public int MaxBitrate => (int)(AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.MaxBitrate ?? 0);

		private int? m_timescale = null;
		private int? m_audioChannels = null;
		private int? m_averageBitrate = null;

		public int TimeScale => m_timescale ??=
			AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.AudioSpecificConfig.SamplingFrequency ??
			AudioSampleEntry.Dec3?.SampleRate ??
			AudioSampleEntry.Dac4?.SampleRate ??
			(int)Moov.AudioTrack.Mdia.Mdhd.Timescale;

		public int AudioChannels => m_audioChannels ??=
			AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.AudioSpecificConfig.ChannelConfiguration ??
			AudioSampleEntry.Dec3?.NumberOfChannels ??
			AudioSampleEntry.Dac4?.NumberOfChannels ??
			AudioSampleEntry.ChannelCount;

		public int AverageBitrate => m_averageBitrate ??=
			(int)(AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.AverageBitrate ??
			AudioSampleEntry.Dec3?.AverageBitrate ??
			AudioSampleEntry?.Dac4?.AverageBitrate ??
			CalculateBitrate());

		protected virtual uint CalculateBitrate()
		{
			var totalSize = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz?.TotalSize;
			if (!totalSize.HasValue || totalSize.Value == 0)
				return 0;
			return (uint)Math.Round(totalSize.Value * 8 / Duration.TotalSeconds, 0);
		}

		public SampleRate SampleRate => (SampleRate)TimeScale;

		public FtypBox Ftyp { get; set; }
		public MoovBox Moov { get; }
		public MdatBox Mdat { get; }

		public List<IBox> TopLevelBoxes { get; }
		public AudioSampleEntry AudioSampleEntry { get; }

		public Mp4File(Stream file, long fileSize)
		{
			InputStream = file.CanSeek ? file : new TrackedReadStream(file, fileSize);

			TopLevelBoxes = Mpeg4Util.LoadTopLevelBoxes(InputStream);
			Ftyp = TopLevelBoxes.OfType<FtypBox>().Single();
			Moov = TopLevelBoxes.OfType<MoovBox>().Single();
			Mdat = TopLevelBoxes.OfType<MdatBox>().Single();

			if (Ftyp.CompatibleBrands.Any(b => b == "dash"))
				FileType = FileType.Dash;
			else
				FileType = Ftyp.MajorBrand switch
				{
					"aax " => FileType.Aax,
					"aaxc" => FileType.Aaxc,
					_ => FileType.Mpeg4
				};

			AppleTags = new AppleTags(Moov.ILst ?? Moov.CreateEmptyMetadata());
			AudioSampleEntry = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry
				?? throw new InvalidOperationException("The audio track's AudioSampleEntry is null");
		}

		public Mp4File(Stream file) : this(file, file.Length) { }

		public Mp4File(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
			: this(File.Open(fileName, FileMode.Open, access, share)) { }

		public virtual FrameTransformBase<FrameEntry, FrameEntry> GetAudioFrameFilter()
			=> new AacValidateFilter();

		/// <summary>
		/// Save all metadata changes to the input stream. Stream must be readable, writable, and seekable.
		/// </summary>
		public async Task SaveAsync()
		{
			if (!InputStream.CanRead || !InputStream.CanWrite || !InputStream.CanSeek)
				throw new InvalidOperationException($"{nameof(InputStream)} must be readable, writable and seekable to save");

			InputStream.Position = Moov.Header.FilePosition;

			//Remove Free boxes and work with net size change
			foreach (var box in Moov.GetFreeBoxes())
				box.Parent?.Children.Remove(box);

			var sizeChange = Moov.RenderSize - Moov.Header.TotalBoxSize;

			if (sizeChange == 0)
			{
				Moov.Save(InputStream);
			}
			else if (Moov.Header.FilePosition > Mdat.Header.FilePosition)
			{
				//Moov is at the end, so just write it
				Moov.Save(InputStream);
				InputStream.SetLength(InputStream.Position);
			}
			else if (FreeBox.MinSize + sizeChange <= 0)
			{
				//Moov is at the beginning and srank more than the minimum Free box size
				//We can accomodate the change with a Free box
				FreeBox.Create((int)-sizeChange, Moov);
				Moov.Save(InputStream);
			}
			else
			{
				//Moov is at the beginning and it either grew or it shrank less than FreeBox.MinSize
				//Shift Mdat by sizeChange to fit the new Moov exactly.

				await Mdat.ShiftMdatAsync(InputStream, sizeChange);
				InputStream.SetLength(InputStream.Position);

				InputStream.Position = Moov.Header.FilePosition;
				Moov.ShiftChunkOffsets(sizeChange);
				Moov.Save(InputStream);
			}

			InputStream.Close();
		}

		public static Mp4Operation RelocateMoovAsync(string mp4FilePath)
		{
			ProgressTracker tracker = new();
			Mp4Operation? moovMover = new(t => Mpeg4Util.RelocateMoovToBeginningAsync(mp4FilePath, t.Token, tracker), null, t => { });
			tracker.ProgressUpdated += (_, _) => moovMover.OnProgressUpdate(new ConversionProgressEventArgs(TimeSpan.Zero, tracker.TotalDuration, tracker.Position, tracker.Speed));
			return moovMover;
		}

		public Mp4Operation ConvertToMp4aAsync(Stream outputStream, ChapterInfo? userChapters = null)
		{
			var start = userChapters?.StartOffset ?? TimeSpan.Zero;
			var end = userChapters?.EndOffset ?? TimeSpan.MaxValue;
			ChapterQueue chapterQueue = new(SampleRate, SampleRate);

			if (userChapters is not null)
			{
				if (Moov.TextTrack is null)
					Moov.CreateEmptyTextTrack();
				chapterQueue.AddRange(userChapters);
			}

			FrameTransformBase<FrameEntry, FrameEntry> filter1 = GetAudioFrameFilter();
			LosslessFilter filter2 = new(outputStream, this, chapterQueue);
			filter1.LinkTo(filter2);

			if (Moov.TextTrack is not null && userChapters is null)
			{
				ChapterFilter c1 = new();

				c1.ChapterRead += (_, e) => chapterQueue.Add(e);

				void continuation(Task t)
				{
					filter1.Dispose();
					c1.Dispose();
					outputStream.Close();
				}

				return ProcessAudio(start, end, continuation, (Moov.AudioTrack, filter1), (Moov.TextTrack, c1));
			}
			else
			{
				void continuation(Task t)
				{
					filter1.Dispose();
					outputStream.Close();
				}

				return ProcessAudio(start, end, continuation, (Moov.AudioTrack, filter1));
			}
		}

		public Mp4Operation ConvertToMultiMp4aAsync(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback)
		{
			FrameTransformBase<FrameEntry, FrameEntry> f1 = GetAudioFrameFilter();
			LosslessMultipartFilter f2 = new
				(userChapters,
				Ftyp,
				Moov,
				newFileCallback);

			f1.LinkTo(f2);

			void continuation(Task t) => f1.Dispose();

			return ProcessAudio(userChapters.StartOffset, userChapters.EndOffset, continuation, (Moov.AudioTrack, f1));
		}

		public Mp4Operation<ChapterInfo?> GetChapterInfoAsync()
		{
			if (Moov.TextTrack is not TrakBox textTrack)
				return Mp4Operation<ChapterInfo?>.FromCompleted(this, null);

			ChapterFilter chapterFilter = new();

			ChapterQueue chapterQueue = new(SampleRate, SampleRate);
			chapterFilter.ChapterRead += (s, e) => chapterQueue.Add(e);

			ChapterInfo? continuation(Task t)
			{
				ChapterInfo chapters = new();

				while (chapterQueue.TryGetNextChapter(out var ch))
					chapters.AddChapter(ch.Title, TimeSpan.FromSeconds(ch.SamplesInFrame / (double)SampleRate));

				chapterFilter.Dispose();

				Chapters ??= chapters;
				return chapters;
			}

			return ProcessAudio(TimeSpan.Zero, TimeSpan.MaxValue, continuation, (Moov.TextTrack, chapterFilter));
		}

		public ChapterInfo? GetChaptersFromMetadata()
		{
			TrakBox? textTrak = Moov.TextTrack;

			//Get chapter names from metadata box in chapter track
			List<string>? chapterNames =
				textTrak
				?.GetChild<UdtaBox>()
				?.GetChild<MetaBox>()
				?.GetChild<AppleListBox>()
				?.Children
				?.OfType<AppleTagBox>()
				?.Where(b => b.Header.Type == "©nam")
				?.Select(b => Encoding.UTF8.GetString(b.Data.Data))
				?.ToList();

			if (chapterNames is null) return null;

			IReadOnlyList<SttsBox.SampleEntry> sampleTimes = textTrak!.Mdia.Minf.Stbl.Stts.Samples;

			if (sampleTimes.Count != chapterNames.Count) return null;

			var cEntryList = new ChunkEntryList(textTrak).OrderBy(s => s.ChunkOffset).ToList();

			if (cEntryList.Count != chapterNames.Count) return null;

			ChapterInfo chapterInfo = new();

			int subtractNext = 0;

			for (int i = 0; i < chapterNames.Count; i++)
			{
				var sif = (int)sampleTimes[i].FrameDelta;

				TimeSpan duration = TimeSpan.FromSeconds(Math.Max(0d, sif + subtractNext) / TimeScale);
				chapterInfo.AddChapter(chapterNames[(int)cEntryList[i].ChunkIndex], duration);
				subtractNext = sif < 0 ? sif : 0;
			}

			Chapters ??= chapterInfo;

			return chapterInfo;
		}

		protected virtual IChunkReader CreateChunkReader(Stream inputStream, TimeSpan startTime, TimeSpan endTime)
			=> new ChunkReader(inputStream, startTime, endTime);

		private static TimeSpan Min(TimeSpan t1, TimeSpan t2) => t1 > t2 ? t2 : t1;
		public virtual Mp4Operation ProcessAudio(TimeSpan startTime, TimeSpan endTime, Action<Task> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			IChunkReader reader = CreateChunkReader(InputStream, startTime, Min(Duration, endTime));

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			var operation = new Mp4Operation(reader.RunAsync, this, continuation);
			reader.OnProgressUpdateDelegate = operation.OnProgressUpdate;
			return operation;
		}

		public Mp4Operation<TResult> ProcessAudio<TResult>(TimeSpan startTime, TimeSpan endTime, Func<Task, TResult> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			IChunkReader reader = CreateChunkReader(InputStream, startTime, Min(Duration, endTime));

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			var operation = new Mp4Operation<TResult>(reader.RunAsync, this, continuation);
			reader.OnProgressUpdateDelegate = operation.OnProgressUpdate;
			return operation;
		}

		protected bool Disposed { get; private set; }
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Mp4File()
		{
			Dispose(disposing: false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
			{
				InputStream.Dispose();
				foreach (var box in TopLevelBoxes)
					box.Dispose();
			}
			Disposed = true;
		}
	}
}
