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
		Mpeg4
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

		public Mp4File(Stream file, long fileSize)
		{
			inputStream = new TrackedReadStream(file, fileSize);

			var boxes = Mpeg4Util.LoadTopLevelBoxes(inputStream);
			Ftyp = boxes.OfType<FtypBox>().Single();
			Moov = boxes.OfType<MoovBox>().Single();
			Mdat = boxes.OfType<MdatBox>().Single();

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
		public void Save()
		{
			if (!InputStream.CanRead ||!InputStream.CanWrite || !InputStream.CanSeek)
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

				Mdat.ShiftMdat(InputStream, sizeChange);
				InputStream.SetLength(InputStream.Position);

				InputStream.Position = Moov.Header.FilePosition;
				Moov.ShiftChunkOffsets(sizeChange);
				Moov.Save(InputStream);
			}

			InputStream.Close();
		}

		public Mp4Operation ConvertToMp4aAsync(Stream outputStream, ChapterInfo userChapters = null, bool trimOutputToChapters = false)
		{
			FrameTransformBase<FrameEntry, FrameEntry> f1 = GetAudioFrameFilter();

			LosslessFilter f2 = new(outputStream, this);

			f1.LinkTo(f2);

			if (Moov.TextTrack is null || userChapters is not null)
			{
				f2.SetChapterDelegate(() => userChapters);

				void continuation(Task t)
				{
					f1.Dispose();
					if (t.IsCompletedSuccessfully)
						Chapters = f2.Chapters;

					outputStream.Close();
				}

				return ProcessAudio(trimOutputToChapters, userChapters.StartOffset, userChapters.EndOffset, continuation, (Moov.AudioTrack, f1));
			}
			else
			{
				ChapterFilter c1 = new(TimeScale);
				f2.SetChapterDelegate(() => c1.Chapters);

				void continuation(Task t)
				{
					f1.Dispose();
					c1.Dispose();
					if (t.IsCompletedSuccessfully)
						Chapters = f2.Chapters;

					outputStream.Close();
				}

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

			void continuation(Task t) => f1.Dispose();

			return ProcessAudio(trimOutputToChapters, userChapters.StartOffset, userChapters.EndOffset, continuation, (Moov.AudioTrack, f1));
		}

		public Mp4Operation<ChapterInfo> GetChapterInfoAsync()
		{
			ChapterFilter c1 = new(TimeScale);

			ChapterInfo continuation(Task t)
			{

				Chapters ??= c1.Chapters;
				c1.Dispose();
				return Chapters;
			}

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

			startTime = doTimeFilter ? startTime : TimeSpan.Zero;
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


		public void Close()
		{
			InputStream?.Close();
		}
	}
}
