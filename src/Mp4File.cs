using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.Collections.Generic;
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
        public Stream InputStream { get; }
        public FileType FileType { get; }
        public TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
        public uint MaxBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate;
        public uint AverageBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate;
        public uint TimeScale => Moov.AudioTrack.Mdia.Mdhd.Timescale;
        public int AudioChannels => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.ChannelConfiguration;
        public ushort AudioSampleSize => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize;
        public byte[] AscBlob => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob;

        public bool InputStreamCanSeek { get; protected set; }

        internal FtypBox Ftyp { get; set; }
        internal MoovBox Moov { get; }
        internal MdatBox Mdat { get; }

        private bool isCancelled = false;

        public Mp4File(Stream file, long fileSize) : base(new BoxHeader(fileSize, "MPEG"), null)
        {
            LoadChildren(file);
            InputStream = file;
            Ftyp = GetChild<FtypBox>();
            Moov = GetChild<MoovBox>();
            Mdat = GetChild<MdatBox>();

            FileType = Ftyp.MajorBrand switch
            {
                "aax " => FileType.Aax,
                "aaxc" => FileType.Aaxc,
                _ => FileType.Mpeg4
            };

            if (Moov.iLst is not null)
                AppleTags = new AppleTags(Moov.iLst);
        }
        public Mp4File(Stream file) : this(file, file.Length)
        {
            InputStreamCanSeek = file.CanSeek;
        }

        public Mp4File(string fileName, FileAccess access = FileAccess.Read) : this(File.Open(fileName, FileMode.Open, access))
        {
            InputStreamCanSeek = true;
        }

        internal virtual Mp4AudioChunkHandler GetAudioChunkHandler()
            => new Mp4AudioChunkHandler(TimeScale, Moov.AudioTrack);

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
            using var audioHandler = GetAudioChunkHandler();
            audioHandler.AudioFilter = audioFilter;
            if (Moov.TextTrack is null)
            {
                ProcessAudio(audioHandler);
                Chapters ??= userChapters;
            }
            else
            {
                var chapterHandler = new ChapterChunkHandler(TimeScale, Moov.TextTrack);
                ProcessAudio(audioHandler, chapterHandler);
                Chapters = userChapters ?? chapterHandler.Chapters;
            }
            audioFilter.Chapters = Chapters;

            return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
        }

        public ConversionResult ConvertToMp4a(Stream outputStream, ChapterInfo userChapters = null)
        {
            ConversionResult result;
            using (var losslessFilter = new LosslessFilter(outputStream, this))
            {
                result = FilterAudio(losslessFilter, userChapters);
            }
            outputStream.Close();
            return result;
        }
        public void ConvertToMultiMp4a(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback)
        {
            using var audioFilter = new LosslessMultipartFilter(
                userChapters,
                newFileCallback,
                Ftyp,
                Moov);

            FilterAudio(audioFilter, userChapters);
        }

        public ChapterInfo GetChapterInfo()
        {
            var chapterHandler = new ChapterChunkHandler(TimeScale, Moov.TextTrack);

            isCancelled = false;

            foreach (var chunk in new MpegChunkCollection(InputStream, chapterHandler))
            {
                if (isCancelled)
                    break;

                Span<byte> buff = new byte[chunk.Entry.ChunkSize];
                InputStream.ReadNextChunk(chunk.Entry.ChunkOffset, buff);
                isCancelled  =!chunk.Handler.ChunkAvailable(buff, chunk.Entry);
            }
            Chapters ??= chapterHandler.Chapters;
            return chapterHandler.Chapters;
        }

        private void ProcessAudio(Mp4AudioChunkHandler audioHandler, params IChunkHandler[] chunkHandlers)
        {
            var handlers = new List<IChunkHandler>();
            handlers.Add(audioHandler);
            handlers.AddRange(chunkHandlers);

            var beginProcess = DateTime.Now;
            var nextUpdate = beginProcess;

            isCancelled = false;


            foreach (var chunk in new MpegChunkCollection(InputStream, handlers.ToArray()))
            {
                if (isCancelled)
                    return;

                Span<byte> buff = new byte[chunk.Entry.ChunkSize];
                InputStream.ReadNextChunk(chunk.Entry.ChunkOffset, buff);
                isCancelled = !chunk.Handler.ChunkAvailable(buff, chunk.Entry);

                //Throttle update so it doesn't bog down UI
                if (DateTime.Now > nextUpdate)
                {
                    TimeSpan position = audioHandler.ProcessPosition;
                    var speed = position / (DateTime.Now - beginProcess);
                    ConversionProgressUpdate?.Invoke(this, new ConversionProgressEventArgs(position, speed));

                    nextUpdate = DateTime.Now.AddMilliseconds(200);
                }
            }
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
            GC.Collect();
        }

        protected override void Render(Stream file) => throw new NotImplementedException();
    }
}
