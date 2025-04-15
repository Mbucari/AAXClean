using AAXClean.Chunks;
using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.Buffers;
using System.IO;
using System.Linq;

#nullable enable
namespace AAXClean;

public class DashFile : Mp4File
{
    public MoofBox FirstMoof { get; }
    public MdatBox FirstMdat => Mdat;
    public SidxBox Sidx => topLevelBoxes.OfType<SidxBox>().Single();

    public override TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.GetChild<MvexBox>().GetChild<MehdBox>().FragmentDuration / TimeScale);

    private new MdatBox Mdat => base.Mdat;

    public TencBox Tenc { get; }

    public byte[]? Key { get; private set; }

    public DashFile(Stream file) : this(file, file.Length) { }
    public DashFile(Stream file, long fileLength) : base(file, fileLength)
    {
        if (FileType != FileType.Dash)
            throw new ArgumentException($"This instance of {nameof(Mp4File)} is not a Dash file.");

        FirstMoof = topLevelBoxes.OfType<MoofBox>().Single();
        var sinf = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.GetChild<SinfBox>();

        if (sinf.SchemeType?.Type != SchmBox.SchemeType.Cenc)
            throw new NotSupportedException($"Only {nameof(SchmBox.SchemeType.Cenc)} dash files are currently supported.");

        if (sinf.SchemeInformation?.TrackEncryption is not TencBox tenc)
            throw new NotSupportedException($"{nameof(AAXClean)} doesn't know how to decrypt a dash without a tenc atom");

        var stsd = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd;
        stsd.AudioSampleEntry.Children.Remove(sinf);
        stsd.AudioSampleEntry.Header.ChangeAtomName(sinf.OriginalFormat.DataFormat);
        foreach(var pssh in Moov.GetChildren<PsshBox>().ToArray())
            Moov.Children.Remove(pssh);

        Tenc = tenc;
    }

    public void SetDecryptionKey(string keyId, string decryptionKey)
    {
        if (string.IsNullOrWhiteSpace(keyId) || keyId.Length != AesCtr.AES_BLOCK_SIZE * 2)
            throw new ArgumentException($"{nameof(keyId)} must be {AesCtr.AES_BLOCK_SIZE} bytes long.");
        if (string.IsNullOrWhiteSpace(decryptionKey) || decryptionKey.Length != AesCtr.AES_BLOCK_SIZE * 2)
            throw new ArgumentException($"{nameof(decryptionKey)} must be {AesCtr.AES_BLOCK_SIZE} bytes long.");

        byte[] keyIdBts = Convert.FromHexString(keyId);
        byte[] decryptionKeyBts = Convert.FromHexString(decryptionKey);

        SetDecryptionKey(keyIdBts, decryptionKeyBts);
    }

    protected override IChunkReader CreateChunkReader(Stream inputStream, TimeSpan startTime, TimeSpan endTime)
        => new DashChunkReader(this, inputStream, startTime, endTime);

    public override FrameTransformBase<FrameEntry, FrameEntry> GetAudioFrameFilter()
        => new DashFilter(Key);

    public void SetDecryptionKey(byte[] keyId, byte[] decryptionKey)
    {
        if (keyId is null || keyId.Length != AesCtr.AES_BLOCK_SIZE)
            throw new ArgumentException($"{nameof(keyId)} must be {AesCtr.AES_BLOCK_SIZE} bytes long.");
        if (decryptionKey is null || decryptionKey.Length != AesCtr.AES_BLOCK_SIZE)
            throw new ArgumentException($"{nameof(decryptionKey)} must be {AesCtr.AES_BLOCK_SIZE} bytes long.");

        var keyUUID = new Guid(keyId, bigEndian: true);

        if (keyUUID != Tenc.DefaultKID)
            throw new InvalidOperationException($"Supplied keyId does not match dash default keyId: {Convert.ToHexString(Tenc.DefaultKID.ToByteArray(bigEndian: true))}");
    
        Key = decryptionKey;
    }
}
