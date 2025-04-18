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

	public class Mp4File
	{
		public ChapterInfo Chapters { get; set; }
		public AppleTags AppleTags { get; }
		public Stream InputStream => inputStream;
		public FileType FileType { get; }
		public virtual TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
		public int MaxBitrate => (int)Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate;
		public int AverageBitrate => (int)Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate;
		public int TimeScale => (int)Moov.AudioTrack.Mdia.Mdhd.Timescale;
		public int AudioObjectType => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.AudioObjectType;
		public int AudioChannels => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.ChannelConfiguration;
		public int AudioSampleSize => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize;
		public byte[] AscBlob => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.AscBlob;
		public SampleRate SampleRate => (SampleRate)Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.SamplingFrequency;

		public FtypBox Ftyp { get; set; }
		public MoovBox Moov { get; }
		public MdatBox Mdat { get; }

		private readonly TrackedReadStream inputStream;
		public List<IBox> TopLevelBoxes { get; }

		public Mp4File(Stream file, long fileSize)
		{
			inputStream = new TrackedReadStream(file, fileSize);

			TopLevelBoxes = Mpeg4Util.LoadTopLevelBoxes(inputStream);
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

			if (Moov.ILst is not null)
				AppleTags = new AppleTags(Moov.ILst);
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
				box.Parent.Children.Remove(box);

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
			Mp4Operation moovMover = null;
			moovMover = new Mp4Operation(t => Mpeg4Util.RelocateMoovToBeginningAsync(mp4FilePath, t.Token, progressAction), null, t => { });
			return moovMover;

			void progressAction(TimeSpan totalDuration, TimeSpan processPosition, double processSpeed)
			{
				moovMover.OnProggressUpdate(new ConversionProgressEventArgs(TimeSpan.Zero, totalDuration, processPosition, processSpeed));
			}
		}

		public Mp4Operation ConvertToMp4aAsync(Stream outputStream, ChapterInfo userChapters = null)
		{
			var start = userChapters?.StartOffset ?? TimeSpan.Zero;
			var end = userChapters?.EndOffset ?? TimeSpan.MaxValue;

			FrameTransformBase<FrameEntry, FrameEntry> filter1 = GetAudioFrameFilter();

			ChapterQueue chapterQueue = new(SampleRate, SampleRate);
			LosslessFilter filter2 = new(outputStream, this, chapterQueue);

			filter1.LinkTo(filter2);

			if (Moov.TextTrack is not null && userChapters is not null)
				chapterQueue.AddRange(userChapters);

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

		public Mp4Operation<ChapterInfo> GetChapterInfoAsync()
		{
			ChapterFilter chapterFilter = new();

			ChapterQueue chapterQueue = new(SampleRate, SampleRate);
			chapterFilter.ChapterRead += (s, e) => chapterQueue.Add(e);

			ChapterInfo continuation(Task t)
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
				?.OfType<AppleTagBox>()
				?.Where(b => b.Header.Type == "©nam")
				?.Select(b => Encoding.UTF8.GetString(b.Data.Data))
				?.ToList();

			if (chapterNames is null) return null;

			IReadOnlyList<SttsBox.SampleEntry> sampleTimes = textTrak.Mdia.Minf.Stbl.Stts.Samples;

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

		static TimeSpan Min(TimeSpan t1, TimeSpan t2) => t1 > t2 ? t2 : t1;
		public virtual Mp4Operation ProcessAudio(TimeSpan startTime, TimeSpan endTime, Action<Task> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			IChunkReader reader = CreateChunkReader(InputStream, startTime, Min(Duration, endTime));

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			var operation = new Mp4Operation(reader.RunAsync, this, continuation);
			reader.OnProggressUpdateDelegate = operation.OnProggressUpdate;
			return operation;
		}

		public Mp4Operation<TResult> ProcessAudio<TResult>(TimeSpan startTime, TimeSpan endTime, Func<Task, TResult> continuation, params (TrakBox track, FrameFilterBase<FrameEntry> filter)[] filters)
		{
			IChunkReader reader = CreateChunkReader(InputStream, startTime, Min(Duration, endTime));

			foreach ((TrakBox track, FrameFilterBase<FrameEntry> filter) in filters)
				reader.AddTrack(track, filter);

			var operation = new Mp4Operation<TResult>(reader.RunAsync, this, continuation);
			reader.OnProggressUpdateDelegate = operation.OnProggressUpdate;
			return operation;
		}

		[Obsolete("Call Close() on the input stream")]
		public void Close()
		{
			InputStream?.Close();
		}
	}
}
