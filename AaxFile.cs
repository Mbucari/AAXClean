using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Chunks;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean
{
    public enum OutputFormat
    {
        Mp4a,
        Mp3
    }
    public class AaxFile : Mp4File
    {
        public ConversionStatus Status { get; private set; } = ConversionStatus.NotStarted;

        public AaxFile(Stream file, long fileSize) : base(file, fileSize) 
        {
            if (FileType != FileType.Aax && FileType != FileType.Aaxc)
                throw new ArgumentException($"This instance of {nameof(Mp4File)} is not an Aax or Aaxc file.");
        }
        public AaxFile(Stream file) : this(file, file.Length) { }
        public AaxFile(string fileName, FileAccess access = FileAccess.Read) : this(File.Open(fileName, FileMode.Open, access)) { }

        public ConversionResult DecryptAax(FileStream outputStream, string activationBytes, OutputFormat format, ChapterInfo userChapters = null)
        {
            if (string.IsNullOrWhiteSpace(activationBytes) || activationBytes.Length != 8)
                throw new ArgumentException($"{nameof(activationBytes)} must be 4 bytes long.");

            byte[] actBytes = ByteUtil.BytesFromHexString(activationBytes);

            return DecryptAax(outputStream, actBytes, format, userChapters);
        }
        public ConversionResult DecryptAax(FileStream outputStream, byte[] activationBytes, OutputFormat format, ChapterInfo userChapters = null)
        {
            if (activationBytes is null || activationBytes.Length != 4)
                throw new ArgumentException($"{nameof(activationBytes)} must be 4 bytes long.");
            if (FileType != FileType.Aax)
                throw new Exception($"This instance of {nameof(AaxFile)} is not an {FileType.Aax} file.");

            var adrm = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.GetChild<AdrmBox>();

            if (adrm is null)
                throw new Exception($"This instance of {nameof(AaxFile)} does not contain an adrm box.");

            //Adrm key derrivation from 
            //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/mov.c in mov_read_adrm

            var intermediate_key = Crypto.Sha1(
               (audible_fixed_key, 0, audible_fixed_key.Length),
               (activationBytes, 0, activationBytes.Length));

            var intermediate_iv = Crypto.Sha1(
                (audible_fixed_key, 0, audible_fixed_key.Length),
                (intermediate_key, 0, intermediate_key.Length),
                (activationBytes, 0, activationBytes.Length));

            var calculatedChecksum = Crypto.Sha1(
                (intermediate_key, 0, 16),
                (intermediate_iv, 0, 16));

            if (!ByteUtil.BytesEqual(calculatedChecksum, adrm.Checksum))
                return ConversionResult.Failed;

            var drmBlob = ByteUtil.CloneBytes(adrm.DrmBlob);

            Crypto.DecryptInPlace(
                ByteUtil.CloneBytes(intermediate_key, 0, 16),
                ByteUtil.CloneBytes(intermediate_iv, 0, 16),
                drmBlob);

            if (!ByteUtil.BytesEqual(drmBlob, 0, activationBytes, 0, 4, true))
                return ConversionResult.Failed;

            byte[] file_key = ByteUtil.CloneBytes(drmBlob, 8, 16);

            var file_iv = Crypto.Sha1(
                (drmBlob, 26, 16),
                (file_key, 0, 16),
                (audible_fixed_key, 0, 16));

            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children.Remove(adrm);

            var aabd = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children.FirstOrDefault(b => b.Header.Type == "aabd");
            if (aabd is not null)
                Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children.Remove(aabd);

            return DecryptAaxc(
                outputStream,
                file_key,
                ByteUtil.CloneBytes(file_iv, 0, 16),
                format,
                userChapters);
        }

        //Constant key
        //https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/mov.c
        private static readonly byte[] audible_fixed_key = { 0x77, 0x21, 0x4d, 0x4b, 0x19, 0x6a, 0x87, 0xcd, 0x52, 0x00, 0x45, 0xfd, 0x20, 0xa5, 0x1d, 0x67 };

        public ConversionResult DecryptAaxc(FileStream outputStream, string audible_key, string audible_iv, OutputFormat format, ChapterInfo userChapters = null)
        {
            if (string.IsNullOrWhiteSpace(audible_key) || audible_key.Length != 32)
                throw new ArgumentException($"{nameof(audible_key)} must be 16 bytes long.");
            if (string.IsNullOrWhiteSpace(audible_iv) || audible_iv.Length != 32)
                throw new ArgumentException($"{nameof(audible_iv)} must be 16 bytes long.");

            byte[] key = ByteUtil.BytesFromHexString(audible_key);

            byte[] iv = ByteUtil.BytesFromHexString(audible_iv);

            return DecryptAaxc(outputStream, key, iv, format, userChapters);
        }

        public ConversionResult DecryptAaxc(FileStream outputStream, byte[] key, byte[] iv, OutputFormat format, ChapterInfo userChapters = null)
        {
            if (!outputStream.CanWrite)
                throw new IOException($"{nameof(outputStream)} must be writable.");
            if (key is null || key.Length != 16)
                throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
            if (iv is null || iv.Length != 16)
                throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

            Status = ConversionStatus.Working;

            var audioHandler = new AavdChunkHandler(TimeScale, Moov.AudioTrack, key, iv);

            Chapters = format switch
            {
                OutputFormat.Mp4a => AaxcToMp4(audioHandler, outputStream, userChapters),
                OutputFormat.Mp3 => AaxToMp3(audioHandler, outputStream, userChapters),
                _ => throw new NotImplementedException($"Unrecognized {nameof(OutputFormat)}"),
            };
            
            InputStream.Close();

            Status = ConversionStatus.Completed;

            return audioHandler.Success && !isCancelled ? ConversionResult.NoErrorsDetected : ConversionResult.Failed;
        }

        private ChapterInfo AaxToMp3(AudioChunkHandler audioHandler, Stream outputStream, ChapterInfo userChapters)
        {
            return Mp4aToMp3(audioHandler, outputStream, userChapters);
        }

        private ChapterInfo AaxcToMp4(AudioChunkHandler audioHandler, Stream outputStream, ChapterInfo userChapters)
        {
            (uint _, uint avgBitrate) = CalculateAudioSizeAndBitrate();

            var ftyp = FtypBox.Create(32, null);
            ftyp.MajorBrand = "isom";
            ftyp.MajorVersion = 0x200;
            ftyp.CompatibleBrands.Clear();
            ftyp.CompatibleBrands.Add("iso2");
            ftyp.CompatibleBrands.Add("mp41");
            ftyp.CompatibleBrands.Add("M4A ");
            ftyp.CompatibleBrands.Add("M4B ");

            //This is the flag that, if set, prevents cover art from loading on android.
            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.DependsOnCoreCoder = 0;
            //Must change the audio type from aavd to mp4a
            Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Header.Type = "mp4a";
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

            return Mp4aToMp4a(audioHandler, outputStream, ftyp, Moov, userChapters);
        }
    }
}
