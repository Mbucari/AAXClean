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

        internal virtual Mp4AudioChunkHandler AudioChunkHandler => new Mp4AudioChunkHandler(TimeScale, Moov.AudioTrack, InputStreamCanSeek);
        internal FtypBox Ftyp { get; set; }
        internal MoovBox Moov { get; }
        internal MdatBox Mdat { get; }

        public bool InputStreamCanSeek { get; protected set; }

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
        public Mp4File(Stream file) : this(file, file.Length) 
        {
            InputStreamCanSeek = file.CanSeek;
        }

        public Mp4File(string fileName, FileAccess access = FileAccess.Read) : this(File.Open(fileName, FileMode.Open, access)) 
        {
            InputStreamCanSeek = true;
        }

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

        public ConversionResult ConvertToMp3(Stream outputStream, NAudio.Lame.LameConfig lameConfig = null, ChapterInfo userChapters = null)
        {
            var audioHandler = AudioChunkHandler;

            double channelDown = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.ChannelCount == 1 ? 1 : 0.5;

            lameConfig ??= new NAudio.Lame.LameConfig
            {
                ABRRateKbps = (int)(CalculateAudioSizeAndBitrate().avgBitrate * channelDown / 1024),
                Mode = NAudio.Lame.MPEGMode.Mono,
                VBR = NAudio.Lame.VBRMode.ABR,
            };

            lameConfig.ID3 ??= AacToMp3Filter.GetDefaultMp3Tags(AppleTags);

            using var aacToMp3Filter = new AacToMp3Filter(
                outputStream,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize,
                lameConfig);

            audioHandler.FrameFilter = aacToMp3Filter;
            var chapterHandler = new ChapterChunkHandler(TimeScale, Moov.TextTrack);

            ProcessAudio(audioHandler, chapterHandler);

            aacToMp3Filter.Close();
            outputStream.Close();

            Chapters = userChapters ?? chapterHandler.Chapters; 

            return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
        }

        public void ConvertToMultiMp3(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback, NAudio.Lame.LameConfig lameConfig = null)
        {
            var audioHandler = AudioChunkHandler;

            double channelDown = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.ChannelCount == 1 ? 1 : 0.5;

            lameConfig ??= new NAudio.Lame.LameConfig
            {
                ABRRateKbps = (int)(CalculateAudioSizeAndBitrate().avgBitrate * channelDown / 1024),
                Mode = NAudio.Lame.MPEGMode.JointStereo,
                VBR = NAudio.Lame.VBRMode.ABR,
            };

            lameConfig.ID3 ??= AacToMp3Filter.GetDefaultMp3Tags(AppleTags);

            using var audioFilter = new AacToMp3MultipartFilter(
                userChapters, 
                newFileCallback, 
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize,
                lameConfig);

            audioHandler.FrameFilter = audioFilter;

            ProcessAudio(audioHandler);
            audioFilter.Close();
        }

        public ConversionResult ConvertToMp4a(Stream outputStream, ChapterInfo userChapters = null)
        {
            var audioHandler = AudioChunkHandler;

            var chapters = Mp4aToMp4a(audioHandler, outputStream, Ftyp, Moov, userChapters);

            Chapters = chapters;

            return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
        }

        public void ConvertToMultiMp4a(ChapterInfo userChapters, Action<NewSplitCallback> newFileCallback)
        {
            var audioHandler = AudioChunkHandler;

            using var audioFilter = new LosslessMultipartFilter(
                userChapters,
                newFileCallback,
                Ftyp,
                Moov);

            audioHandler.FrameFilter = audioFilter;

            ProcessAudio(audioHandler);
            audioFilter.Close();
        }


        public ChapterInfo GetChapterInfo()
        {
            var chapterHandler = new ChapterChunkHandler(TimeScale, Moov.TextTrack, seekable: true);
            var chunkReader = new TrakChunkReader(InputStream, chapterHandler);

            isCancelled = false;

            while (!isCancelled && chunkReader.NextChunk()) ;
            return chapterHandler.Chapters; 
        }

        internal ChapterInfo Mp4aToMp4a(Mp4AudioChunkHandler audioHandler, Stream outputStream, FtypBox ftyp, MoovBox moov, ChapterInfo userChapters = null)
        {
            (uint audioSize, _) = CalculateAudioSizeAndBitrate();

            var losslessFilter = new LosslessFilter(outputStream);
            audioHandler.FrameFilter = losslessFilter;
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

        public IEnumerable<(TimeSpan start,TimeSpan end)> DetectSilence(double decibels, TimeSpan minDuration)
        {
            if (decibels >= 0 || decibels < -90)
                throw new ArgumentException($"{nameof(decibels)} must fall in [-90,0)");
            if (minDuration.TotalSeconds * TimeScale < 2)
                throw new ArgumentException($"{nameof(minDuration)} must be no shorter than 2 audio samples.");

            var audioHandler = AudioChunkHandler;

            SilenceDetect sil = new SilenceDetect(
                decibels,
                minDuration,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.Blob,
                audioHandler.Track.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.SampleSize);
            audioHandler.FrameFilter = sil;

            ProcessAudio(audioHandler);

            sil.Close();

            return sil.Silences;
        }

        private void ProcessAudio(Mp4AudioChunkHandler audioHandler, params IChunkHandler[] chunkHandlers)
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

        protected (uint audioSize, uint avgBitrate) CalculateAudioSizeAndBitrate()
        {
            //Calculate the actual average bitrate because aaxc file is wrong.
            long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => (long)s) * 8;
            double duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
            uint avgBitrate = (uint)(audioBits * TimeScale / duration);

            return ((uint)(audioBits / 8), avgBitrate);
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
