using AAXClean.Chunks;
using AAXClean.Boxes;
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
    public class Mp4File : Box
    {
        public event EventHandler<DecryptionProgressEventArgs> DecryptionProgressUpdate;
        public event EventHandler<DecryptionResult> DecryptionComplete;
        public AppleTags AppleTags { get; }
        public Stream InputStream { get; }
        public ChapterInfo Chapters { get; private set; }

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


        public void DecryptAaxc(Stream outputStream, byte[] key, byte[] iv, ChapterInfo userChapters = null)
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

            CalculateAndAddBitrate();
            PatchAaxc();

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

            //Write ftyp to output file
            Ftyp.Save(outputStream);
            long moovPos = outputStream.Position;
            //Write moov to output file. At this ponit the moov size won't change, but
            //we will need to come back and re-write it after we update chunk offsets.
            Moov.Save(outputStream);
            long mdatPos = outputStream.Position;

            //Write an mdat placeholder. We'll come back and overwrite the size at the
            //end when we've calculated it.
            outputStream.WriteUInt32BE(0);
            outputStream.Write(Encoding.ASCII.GetBytes("mdat"));

            //Add chapters to beginning of moov.
            if (insertChapters)
                WriteChapters(outputStream, userChapters);

            List<uint> audioChunkOffsets = new();
            List<uint> textChunkOffsets = new();

            #region Decryption Loop
            var beginProcess = DateTime.Now;
            uint framesProcessed = 0;
            var mdatChunk = Mdat.FirstEntry;
            var decryptSuccess = true;
            do
            {
                switch (mdatChunk)
                {
                    case AavdChunk aavd:
                        {
                            audioChunkOffsets.Add((uint)outputStream.Position);
                            decryptSuccess = aavd.DecryptSave(key, iv, InputStream, outputStream);
                            framesProcessed += (uint)aavd.NumFrames;

                            TimeSpan position = FrameToTime(framesProcessed);
                            var speed = position / (DateTime.Now - beginProcess);
                            DecryptionProgressUpdate?.Invoke(this, new DecryptionProgressEventArgs(position, speed));
                            break;
                        }
                    case TextChunk text:
                        {
                            if (insertChapters) continue;

                            (string title, TimeSpan duration) = text.GetChapter(Moov.TextTrack);
                            Chapters.AddChapter(title, duration);

                            textChunkOffsets.Add((uint)(outputStream.Position + text.ChapterChunkOffset));
                            text.Save(outputStream);
                            break;
                        }
                    default:
                        continue;
                }

            } while (decryptSuccess && (mdatChunk = mdatChunk.GetNext(InputStream)) != null);

            #endregion
            var decryptionResult = decryptSuccess ? DecryptionResult.NoErrorsDetected : DecryptionResult.Failed;

            //Final calculated mdat size
            long mdatSize = outputStream.Position - mdatPos;

            //Reset all chunk offsets with their new positions.
            Moov.AudioTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Clear();
            Moov.AudioTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.AddRange(audioChunkOffsets);

            if (!insertChapters)
            {
                Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Clear();
                Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.AddRange(textChunkOffsets);
            }

            //Re-write the whole moov to commit changes
            outputStream.Position = moovPos;
            Moov.Save(outputStream);
            //Write final mdat size
            outputStream.Position = mdatPos;
            outputStream.WriteUInt32BE((uint)mdatSize);

            outputStream.Close();
            InputStream.Close();

            DecryptionComplete?.Invoke(this, decryptionResult);
        }

        private void CalculateAndAddBitrate()
        {
            uint maxBitrate = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate;

            //Calculate the actual average bitrate because aaxc file is wrong.
            long audioBits = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Sum(s => s) * 8;
            uint timescale = Moov.AudioTrack.Mdia.Mdhd.Timescale;
            uint duration = Moov.AudioTrack.Mdia.Mdhd.Duration;
            uint avgBitrate = (uint)(audioBits * timescale / duration);

            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate = avgBitrate;

            //Remove extra Free boxes
            List<Box> children = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children;
            for (int i = children.Count - 1; i >= 0; i--)
            {
                if (children[i] is FreeBox)
                    children.RemoveAt(i);
            }
            //Add a btrt box to the audio sample description.
            BtrtBox.Create(0, maxBitrate, avgBitrate, Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry);
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
            uint timescale = Moov.AudioTrack.Mdia.Mdhd.Timescale;

            Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Clear();
            Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Clear();
            Moov.TextTrack.Mdia.Minf.Stbl.Stco.ChunkOffsets.Clear();

            foreach (var c in chapters)
            {
                uint sampleDelta = (uint)((c.EndOffset - c.StartOffset).TotalSeconds * timescale);
                byte[] title = Encoding.UTF8.GetBytes(c.Title);

                Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples.Add(new SttsBox.SampleEntry(1, sampleDelta));
                Moov.TextTrack.Mdia.Minf.Stbl.Stsz.SampleSizes.Add((uint)(title.Length + 2 + encd.Length));
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
            long beginDelta = 0;

            foreach (var entry in Moov.TextTrack.Mdia.Minf.Stbl.Stts.Samples)
            {
                if (sampleNum > entry.SampleCount)
                {
                    beginDelta += entry.SampleCount * entry.SampleDelta;
                    sampleNum -= entry.SampleCount;
                }
                else
                {
                    beginDelta += (entry.SampleCount - sampleNum) * entry.SampleDelta;
                    break;
                }
            }
            double timeScale = Moov.AudioTrack.Mdia.Mdhd.Timescale;

            return TimeSpan.FromSeconds(beginDelta / timeScale);
        }
        protected override void Render(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
