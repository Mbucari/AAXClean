using AAXClean.Chunks;
using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;
using AAXClean.FrameFilters.Text;
using Mpeg4Lib;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.IO;
using System.Linq;
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

	public class Mp4File : Mpeg4Lib.Mpeg4File
	{
#pragma warning disable CS0618 // Type or member is obsolete
		[Obsolete("Use MetadataItems property instead.")]
		public AppleTags AppleTags  => lazyAppleTags.Value;

		private readonly Lazy<AppleTags> lazyAppleTags;
#pragma warning restore CS0618

		public FileType FileType { get; }	

		public SampleRate SampleRate => (SampleRate)TimeScale;

		public Mp4File(Stream file, long fileSize) : base(file, fileSize)
		{
#pragma warning disable CS0618 // Type or member is obsolete
			lazyAppleTags = new(() => new AppleTags(Moov.ILst ?? Moov.CreateEmptyMetadata()));
#pragma warning restore CS0618

			FileType = Ftyp.CompatibleBrands.Any(b => b == "dash")
				? FileType.Dash
				: Ftyp.MajorBrand switch
				{
					"aax " => FileType.Aax,
					"aaxc" => FileType.Aaxc,
					_ => FileType.Mpeg4
				};
		}

		public Mp4File(Stream file) : this(file, file.Length) { }

		public Mp4File(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
			: this(File.Open(fileName, FileMode.Open, access, share)) { }

		public virtual FrameTransformBase<FrameEntry, FrameEntry> GetAudioFrameFilter()
			=> new AacValidateFilter();

		public static Mp4Operation RelocateMoovAsync(string mp4FilePath)
		{
			ProgressTracker tracker = new();
			Mp4Operation? moovMover = new(t => RelocateMoovToBeginningAsync(mp4FilePath, tracker, t.Token), null, t => { });
			tracker.ProgressUpdated += (_, _) => moovMover.OnProgressUpdate(new ConversionProgressEventArgs(TimeSpan.Zero, tracker.TotalDuration, tracker.Position, tracker.Speed));
			return moovMover;
		}

		/// <summary>
		/// Save all metadata changes to the input stream. Stream must be readable, writable, and seekable.
		/// </summary>
		/// <param name="keepMoovInFront">Controls where the <see cref="MoovBox"/> is saved when the  <see cref="MoovBox"/> is in the beginning of the file but there is not enough space to save it in the same position.
		/// <para/>if <see cref="true"/>, the <see cref="MdatBox"/> is shifted to make room for the <see cref="MoovBox"/>.
		/// <para/>if <see cref="false"/>, the original <see cref="MoovBox"/> is replaced with a <see cref="FreeBox"/> and the new <see cref="MoovBox"/> is written at the end of the file.
		/// </param>
		/// 
		public Mp4Operation SaveAsync(bool keepMoovInFront = true)
		{
			ProgressTracker tracker = new() { TotalDuration = Duration };
			Mp4Operation operation = new(t => SaveAsync(keepMoovInFront, tracker, t.Token), this, t => { });
			tracker.ProgressUpdated += (_, _) => operation.OnProgressUpdate(new ConversionProgressEventArgs(TimeSpan.Zero, tracker.TotalDuration, tracker.Position, tracker.Speed));
			return operation;
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
	}
}
