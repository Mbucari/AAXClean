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
    public enum FileType
    {
        Aax,
        Aaxc,
        Mpeg4
    }
    public class Mp4File : Box
    {
        public AppleTags AppleTags { get; }
        public Stream InputStream { get; }
        public FileType FileType { get; }
        public TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
        public uint MaxBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.MaxBitrate;
        public uint AverageBitrate => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AverageBitrate;
        public uint TimeScale => Moov.AudioTrack.Mdia.Mdhd.Timescale;
        public int AudioChannels => Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioConfig.ChannelConfiguration;

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
