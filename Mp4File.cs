using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AAXClean
{
    public enum ConversionResult
    {
        Failed,
        NoErrorsDetected
    }
    public enum ConversionStatus
    {
        NotStarted,
        Working,
        Completed
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

        protected bool isCancelled = false;
        internal FtypBox Ftyp { get; }
        internal MoovBox Moov { get; }
        internal MdatBox Mdat { get; }

        public Mp4File(Stream file, long fileSize) : base(new BoxHeader((uint)fileSize, "MPEG"), null)
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
        public Mp4File(Stream file) : this(file, file.Length) { }

        public Mp4File(string fileName, FileAccess access = FileAccess.Read) : this(File.Open(fileName, FileMode.Open, access)) { }

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

        public (uint audioSize, uint avgBitrate) CalculateAudioSizeAndBitrate()
        {
            //Calculate the actual average bitrate because aaxc file is wrong.
            long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s) * 8;
            double duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
            uint avgBitrate = (uint)(audioBits * TimeScale / duration);

            return ((uint)(audioBits / 8), avgBitrate);
        }

        public ConversionResult ConvertToMp3(Stream outputStream)
        {
            var audioHandler = new Mp4aChunkHandler(TimeScale, Moov.AudioTrack, seekable: true);

            var chapters = Mp4aToMp3(audioHandler, outputStream);

            Chapters = chapters;

            return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
        }

        public ConversionResult ConvertToMp4a(Stream outputStream)
        {
            var audioHandler = new Mp4aChunkHandler(TimeScale, Moov.AudioTrack, seekable: true);

            var chapters = Mp4aToMp4a(audioHandler, outputStream, Ftyp, Moov);

            Chapters = chapters;

            return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
        }

        internal ChapterInfo Mp4aToMp3(AudioChunkHandler audioHandler, Stream outputStream, ChapterInfo userChapters = null)
        {
            (_, uint avgBitrate) = CalculateAudioSizeAndBitrate();

            var aacToMp3Filter = new AacToMp3Filter(
                outputStream,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize,
                (int)(avgBitrate / 1024),
                AppleTags);

            audioHandler.SampleFilter = aacToMp3Filter;
            var chapterHandler = new ChapterChunkHandler(TimeScale, Moov.TextTrack);

            ProcessAudio(audioHandler, chapterHandler);

            aacToMp3Filter.Close();
            outputStream.Close();

            return userChapters ?? chapterHandler.Chapters;
        }

        internal ChapterInfo Mp4aToMp4a(AudioChunkHandler audioHandler, Stream outputStream, FtypBox ftyp, MoovBox moov, ChapterInfo userChapters = null)
        {
            (uint audioSize, _) = CalculateAudioSizeAndBitrate();

            var losslessFilter = new LosslessFilter(outputStream);
            audioHandler.SampleFilter = losslessFilter;
            var chapterHandler = new ChapterChunkHandler(TimeScale, moov.TextTrack);

            uint chaptersSize;

            if (userChapters is null)
            {
                chaptersSize = (uint)moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => s);
            }
            else
            {
                chaptersSize = (uint)userChapters.RenderSize;
                //Aaxc files repeat the chapter titles in a metadata track, but they
                //aren't necessary for media players and they will contradict the new
                //chapter titles, so we remove them.
                var textUdta = moov.TextTrack.GetChild<UdtaBox>();

                if (textUdta is not null)
                    moov.TextTrack.Children.Remove(textUdta);
            }

            ftyp.Save(outputStream);

            //Calculate mdat size and write mdat header.
            uint mdatSize = 8 + audioSize + chaptersSize;
            outputStream.WriteUInt32BE(mdatSize);
            outputStream.WriteType("mdat");

            ProcessAudio(audioHandler, chapterHandler);

            var chapters = userChapters ?? chapterHandler.Chapters;
            //Write chapters to end of mdat and update moov
            chapters.WriteChapters(moov.TextTrack, TimeScale, outputStream);

            moov.AudioTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Clear();
            moov.AudioTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.AddRange(losslessFilter.ChunkOffsets);

            //write moov to end of file
            moov.Save(outputStream);

            losslessFilter.Close();
            outputStream.Close();
            return chapters;
        }

        private void ProcessAudio(AudioChunkHandler audioHandler, params IChunkHandler[] chunkHandlers)
        {
            var handlers = new List<IChunkHandler>();
            handlers.Add(audioHandler);
            handlers.AddRange(chunkHandlers);

            var chunkReader = new TrakChunkReader(InputStream, handlers.ToArray());

            var beginProcess = DateTime.Now;
            var nextUpdate = beginProcess;

            isCancelled = false;

            while (!isCancelled && chunkReader.NextChunk())
            {
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
