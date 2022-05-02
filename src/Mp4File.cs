using AAXClean.FrameFilters.Audio;
using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.IO;
using System.Linq;

namespace AAXClean
{
	public enum ConversionResult
	{
		Failed,
		NoErrorsDetected
	}
	public enum FileType
	{
		Aax,
		Aaxc,
		Mpeg4
	}
	public class Mp4File : Box
	{
		public ChapterInfo Chapters { get; internal set; }

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

		internal FtypBox Ftyp { get; set; }
		internal MoovBox Moov { get; }
		internal MdatBox Mdat { get; }

		private readonly TrackedReadStream inputStream;
		private bool isCancelled = false;

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

		public Mp4File(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read) : this(File.Open(fileName, FileMode.Open, access, share)) { }

		internal virtual Mp4AudioChunkHandler GetAudioChunkHandler()
			=> new(Moov.AudioTrack);

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

		public ConversionResult FilterAudio(AudioFilterBase audioFilter, ChapterInfo userChapters = null)
		{
			using Mp4AudioChunkHandler audioHandler = GetAudioChunkHandler();
			audioHandler.FrameFilter = audioFilter;
			if (Moov.TextTrack is null)
			{
				ProcessAudio(audioHandler);
				Chapters ??= userChapters;
			}
			else
			{
				ChapterChunkHandler chapterHandler = new ChapterChunkHandler(Moov.TextTrack);
				ProcessAudio(audioHandler, chapterHandler);
				Chapters = userChapters ?? chapterHandler.Chapters;
			}
			audioFilter.Chapters = Chapters;

			return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
		}

		public ConversionResult ConvertToMp4a(Stream outputStream, ChapterInfo userChapters = null)
		{
			ConversionResult result;
			using (LosslessFilter losslessFilter = new LosslessFilter(outputStream, this))
			{
				result = FilterAudio(losslessFilter, userChapters);
			}
			outputStream.Close();
			return result;
		}

		public void ConvertToMultiMp4a(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback)
		{
			using LosslessMultipartFilter audioFilter = new LosslessMultipartFilter(
				userChapters,
				newFileCallback,
				Ftyp,
				Moov);

			FilterAudio(audioFilter, userChapters);
		}

		public ChapterInfo GetChapterInfo()
		{
			ChapterChunkHandler chapterHandler = new ChapterChunkHandler(Moov.TextTrack);

			isCancelled = false;

			Span<byte> chunkBuffer = new byte[1024];
			foreach (TrackChunk chunk in new MpegChunkEnumerable(chapterHandler))
			{
				if (isCancelled)
					break;

				Span<byte> chunkdata = chunkBuffer.Slice(0, chunk.Entry.ChunkSize);
				InputStream.ReadNextChunk(chunk.Entry.ChunkOffset, chunkdata);
				isCancelled = !chunk.Handler.HandleChunk(chunk.Entry, chunkdata);
			}
			Chapters ??= chapterHandler.Chapters;
			return chapterHandler.Chapters;
		}

		private void ProcessAudio(params ChunkHandlerBase[] chunkHandlers)
		{
			DateTime beginProcess = DateTime.Now;
			DateTime nextUpdate = beginProcess;

			isCancelled = false;

			Span<byte> chunkBuffer = new byte[4 * 1024 * 1024];
			foreach (TrackChunk chunk in new MpegChunkEnumerable(chunkHandlers))
			{
				if (isCancelled)
					break;

				Span<byte> chunkdata = chunkBuffer.Slice(0, chunk.Entry.ChunkSize);
				InputStream.ReadNextChunk(chunk.Entry.ChunkOffset, chunkdata);
				isCancelled = !chunk.Handler.HandleChunk(chunk.Entry, chunkdata);

				//Throttle update so it doesn't bog down UI
				if (DateTime.Now > nextUpdate)
				{
					TimeSpan position = chunk.Handler.ProcessPosition;
					double speed = position / (DateTime.Now - beginProcess);
					ConversionProgressUpdate?.Invoke(this, new ConversionProgressEventArgs { TotalDuration = Duration, ProcessPosition = position, ProcessSpeed = speed });

					nextUpdate = DateTime.Now.AddMilliseconds(200);
				}
			}

			ConversionProgressUpdate?.Invoke(this, new ConversionProgressEventArgs { TotalDuration = Duration, ProcessPosition = Duration, ProcessSpeed = Duration / (DateTime.Now - beginProcess) });
		}

		protected (long audioSize, uint avgBitrate) CalculateAudioSizeAndBitrate()
		{
			//Calculate the actual average bitrate because aaxc file is wrong.
			long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s) * 8;
			double duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
			uint avgBitrate = (uint)(audioBits * TimeScale / duration);

			return (audioBits / 8, avgBitrate);
		}

		public void Cancel()
		{
			isCancelled = true;
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
