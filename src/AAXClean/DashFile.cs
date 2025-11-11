using AAXClean.Chunks;
using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.Buffers;
using System.IO;
using System.Linq;

namespace AAXClean;

public class DashFile : Mp4File
{
	public MoofBox FirstMoof { get; }
	public MdatBox FirstMdat => Mdat;
	public SidxBox Sidx => TopLevelBoxes.OfType<SidxBox>().Single();

	public override TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.GetChildOrThrow<MvexBox>().GetChildOrThrow<MehdBox>().FragmentDuration / TimeScale);

	private new MdatBox Mdat => base.Mdat;

	public TencBox Tenc { get; }

	public byte[]? Key { get; private set; }

	protected override uint CalculateBitrate()
	{
		var totalSize = Sidx.Segments.Sum(s => (long)s.ReferenceSize) * 8;
		var totalDuration = Sidx.Segments.Sum(s => (long)s.SubsegmentDuration);
		var bitRate = totalSize * Sidx.Timescale / totalDuration;
		return (uint)bitRate;
	}

	public DashFile(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
		: this(File.Open(fileName, FileMode.Open, access, share)) { }
	public DashFile(Stream file) : this(file, file.Length) { }
	public DashFile(Stream file, long fileLength) : base(file, fileLength)
	{
		if (FileType != FileType.Dash)
			throw new ArgumentException($"This instance of {nameof(Mp4File)} is not a Dash file.");

		FirstMoof = TopLevelBoxes.OfType<MoofBox>().Single();

		var audioSampleEntry = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry
			?? throw new InvalidDataException($"The audio track doesn't contain an {nameof(AudioSampleEntry)}");

		var sinf = audioSampleEntry.GetChildOrThrow<SinfBox>();

		if (sinf.SchemeType?.Type != SchmBox.SchemeType.Cenc)
			throw new NotSupportedException($"Only {nameof(SchmBox.SchemeType.Cenc)} dash files are currently supported.");

		var tenc = sinf.SchemeInformation?.TrackEncryption
			?? throw new NotSupportedException($"{nameof(AAXClean)} doesn't know how to decrypt a dash without a tenc atom");

		var stsd = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd;
		audioSampleEntry.Children.Remove(sinf);
		audioSampleEntry.Header.ChangeAtomName(sinf.OriginalFormat.DataFormat);
		foreach (var pssh in Moov.GetChildren<PsshBox>().ToArray())
			Moov.Children.Remove(pssh);

		if (AudioSampleEntry.Dec3 is not null || AudioSampleEntry.Dac4 is not null)
		{
			Ftyp = FtypBox.Create("mp42", 0);
			Ftyp.CompatibleBrands.Add("dby1");
			Ftyp.CompatibleBrands.Add("iso8");
			Ftyp.CompatibleBrands.Add("isom");
			Ftyp.CompatibleBrands.Add("mp41");
			Ftyp.CompatibleBrands.Add("M4A ");
			Ftyp.CompatibleBrands.Add("M4B ");
		}
		else
		{
			Ftyp = FtypBox.Create("isom", 0x200);
			Ftyp.CompatibleBrands.Add("iso2");
			Ftyp.CompatibleBrands.Add("mp41");
			Ftyp.CompatibleBrands.Add("M4A ");
			Ftyp.CompatibleBrands.Add("M4B ");
		}

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
	{
		if (Key is null)
			throw new InvalidOperationException($"This instance of {nameof(DashFile)} does not have a decryption key set.");
		return new DashFilter(Key);
	}

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
