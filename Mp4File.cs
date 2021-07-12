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
    public enum DecryptionResult
    {
        Failed,
        NoErrorsDetected
    }
    public enum DecryptionStatus
    {
        NotStarted,
        Working,
        Completed
    }
    public class Mp4File : Box
    {
        public event EventHandler<DecryptionProgressEventArgs> DecryptionProgressUpdate;
        public event EventHandler<DecryptionResult> DecryptionComplete;
        public AppleTags AppleTags { get; }
        public Stream InputStream { get; }
        public ChapterInfo Chapters { get; private set; }
        public DecryptionStatus Status { get; private set; } = DecryptionStatus.NotStarted;

        public TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
        public uint MaxBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate;
        public uint AverageBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate;
        public uint TimeScale => Moov.AudioTrack.Mdia.Mdhd.Timescale;
        public int AudioChannels => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.ChannelConfiguration;

        private FtypBox Ftyp { get; }
        private MoovBox Moov { get; }
        private MdatBox Mdat { get; }

        public Mp4File(Stream file, long fileSize) : base(new BoxHeader((uint)fileSize, "MPEG"), null)
        {
            LoadChildren(file);
            InputStream = file;
            Ftyp = GetChild<FtypBox>();
            Moov = GetChild<MoovBox>();
            Mdat = GetChild<MdatBox>();

            if (Moov.iLst is not null)
                AppleTags = new AppleTags(Moov.iLst);
        }
        public Mp4File(Stream file) : this(file, file.Length) { }

        public Mp4File(string fileName, FileAccess access = FileAccess.Read) : this(File.Open(fileName, FileMode.Open, access)) { }

        private bool isCancelled = false;


        public Mp4File DecryptAaxc(FileStream outputStream, string audible_key, string audible_iv, ChapterInfo userChapters = null)
        {
            if (string.IsNullOrWhiteSpace(audible_key) || audible_key.Length != 32)
                throw new ArgumentException($"{nameof(audible_key)} must be 16 bytes long.");
            if (string.IsNullOrWhiteSpace(audible_iv) || audible_iv.Length != 32)
                throw new ArgumentException($"{nameof(audible_iv)} must be 16 bytes long.");

            byte[] key = Enumerable.Range(0, audible_key.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(audible_key.Substring(x, 2), 16))
                     .ToArray();

            byte[] iv = Enumerable.Range(0, audible_iv.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(audible_iv.Substring(x, 2), 16))
                     .ToArray();

            return DecryptAaxc(outputStream, key, iv, userChapters);
        }

        public Mp4File DecryptAaxc(FileStream outputStream, byte[] key, byte[] iv, ChapterInfo userChapters = null)
        {
            if (!outputStream.CanSeek || !outputStream.CanWrite)
                throw new IOException($"{nameof(outputStream)} must be writable and seekable.");
            if (Ftyp.MajorBrand != "aaxc")
                throw new ArgumentException($"This instance of {nameof(Mp4File)} is not an Aaxc file.");
            if (key is null || key.Length != 16)
                throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
            if (iv is null || iv.Length != 16)
                throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

            bool insertChapters = userChapters != null;

            Status = DecryptionStatus.Working;

            if (insertChapters)
            {
                Chapters = userChapters;
                //Aaxc files repeat the chapter titles in a metadata track, but they
                //aren't necessary for media players and they will contradict the new
                //chapter titles, so we remove them.
                var textUdta = Moov.TextTrack.GetChild<UdtaBox>();

                if (textUdta is not null)
                    Moov.TextTrack.Children.Remove(textUdta);
            }
            else
            {
                Chapters = new ChapterInfo();
            }

            PatchAaxc();
            CalculateAndAddBitrate();

            //Write ftyp to output file
            Ftyp.Save(outputStream);

            //Write an mdat header with placeholder size.
            outputStream.WriteUInt32BE(0);
            outputStream.Write(Encoding.ASCII.GetBytes("mdat"));

            List<uint> audioChunkOffsets = new();

            #region Decryption Loop

            var beginProcess = DateTime.Now;
            var nextUpdate = beginProcess;
            uint framesProcessed = 0;
            var decryptSuccess = true;
            isCancelled = false;

            var mdatChunk = Mdat.FirstEntry;
            do
            {
                switch (mdatChunk)
                {
                    case AavdChunk aavd:
                        {
                            audioChunkOffsets.Add((uint)outputStream.Position);
                            decryptSuccess = aavd.DecryptSave(key, iv, InputStream, outputStream);
                            framesProcessed += (uint)aavd.NumFrames;

                            //Throttle update so it doesn't bog down UI
                            if (DateTime.Now > nextUpdate)
                            {
                                TimeSpan position = FrameToTime(framesProcessed);
                                var speed = position / (DateTime.Now - beginProcess);
                                DecryptionProgressUpdate?.Invoke(this, new DecryptionProgressEventArgs(position, speed));

                                nextUpdate = DateTime.Now.AddMilliseconds(200);
                            }

                            break;
                        }
                    case TextChunk text:
                        {
                            if (insertChapters) continue;

                            //read chapter info into Chapters for writing at the end of mdat
                            (string title, TimeSpan duration) = text.GetChapter(Moov.TextTrack);
                            Chapters.AddChapter(title, duration);

                            break;
                        }
                    default:
                        continue;
                }

            } while (!isCancelled && decryptSuccess && (mdatChunk = mdatChunk.GetNext(InputStream)) != null);

            #endregion

            //Reset all chunk offsets with their new positions.
            Moov.AudioTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Clear();
            Moov.AudioTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.AddRange(audioChunkOffsets);

            //Write chapters to end of mdat and update moov
            WriteChapters(outputStream, Chapters);

            //write final mdat size
            uint mdatSize = (uint)(outputStream.Position - Ftyp.Header.TotalBoxSize);
            outputStream.Position = Ftyp.Header.TotalBoxSize;
            outputStream.WriteUInt32BE(mdatSize);
            outputStream.Position = Ftyp.Header.TotalBoxSize + mdatSize;

            //write moov to end of file
            Moov.Save(outputStream);

            outputStream.Close();
            InputStream.Close();

            //Update status and events
            Status = DecryptionStatus.Completed;
            var decryptionResult = decryptSuccess ? DecryptionResult.NoErrorsDetected : DecryptionResult.Failed;
            DecryptionComplete?.Invoke(this, decryptionResult);

            return decryptionResult switch
            {
                DecryptionResult.NoErrorsDetected => new Mp4File(outputStream.Name, FileAccess.ReadWrite),
                _ => null
            };
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
        public void Close()
        {
            InputStream?.Close();
        }
        public void Cancel()
        {
            isCancelled = true;
        }

        /// <returns>Number of audio bytes</returns>
        private uint CalculateAndAddBitrate()
        {

            //Calculate the actual average bitrate because aaxc file is wrong.
            long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => s) * 8;
            double duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
            uint avgBitrate = (uint)(audioBits * TimeScale / duration);

            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate = avgBitrate;

            //Remove extra Free boxes
            List<Box> children = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children;
            for (int i = children.Count - 1; i >= 0; i--)
            {
                if (children[i] is FreeBox)
                    children.RemoveAt(i);
            }
            //Add a btrt box to the audio sample description.
            BtrtBox.Create(0, MaxBitrate, avgBitrate, Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry);
            return (uint)(audioBits / 8);
        }
        private void PatchAaxc()
        {
            Ftyp.MajorBrand = "isom";
            Ftyp.MajorVersion = 0x200;
            Ftyp.CompatibleBrands.Clear();
            Ftyp.CompatibleBrands.Add("iso2");
            Ftyp.CompatibleBrands.Add("mp41");
            Ftyp.CompatibleBrands.Add("M4A ");
            Ftyp.CompatibleBrands.Add("M4B ");

            //This is the flag that, if set, prevents cover art from loading on android.
            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.DependsOnCoreCoder = 0;
            //Must change the audio type from aavd to mp4a
            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Header.Type = "mp4a";
        }
        private void WriteChapters(Stream output, ChapterInfo chapters)
        {
            Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Clear();
            Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Clear();
            Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Clear();

            foreach (var c in chapters)
            {
                uint sampleDelta = (uint)((c.EndOffset - c.StartOffset).TotalSeconds * TimeScale);
                byte[] title = Encoding.UTF8.GetBytes(c.Title);

                Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Add(new SttsBox.SampleEntry(sampleCount:1, sampleDelta));
                Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Add((uint)(2 + title.Length + encd.Length));
                Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Add((uint)output.Position);

                output.WriteInt16BE((short)title.Length);
                output.Write(title);
                output.Write(encd);
            }
        }

        //This is constant folr UTF-8 text
        //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/movenc.c
        private readonly byte[] encd = { 0, 0, 0, 0xc, (byte)'e', (byte)'n', (byte)'c', (byte)'d', 0, 0, 1, 0 };

        //Gets the playback timestamp of an audio frame.
        private TimeSpan FrameToTime(uint sampleNum)
        {
            double beginDelta = 0;

            foreach (var entry in Moov.AudioTrack.Mdia.Minf.Stbl.Stts.Samples)
            {
                if (sampleNum > entry.SampleCount)
                {
                    beginDelta += entry.SampleCount * entry.SampleDelta;
                    sampleNum -= entry.SampleCount;
                }
                else
                {
                    beginDelta += sampleNum * entry.SampleDelta;
                    break;
                }
            }

            return TimeSpan.FromSeconds(beginDelta / TimeScale);
        }
        protected override void Render(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
