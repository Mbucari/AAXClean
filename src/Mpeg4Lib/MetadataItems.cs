using Mpeg4Lib.Boxes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mpeg4Lib;

public class MetadataItems
{
	public const string TAG_NAME_TITLE = "©nam";
	public const string TAG_NAME_PRODUCER = "©prd";
	public const string TAG_NAME_ARTIST = "©ART";
	public const string TAG_NAME_ALBUMARTIST = "aART";
	public const string TAG_NAME_ALBUM = "©alb";
	public const string TAG_NAME_GENRES = "©gen";
	public const string TAG_NAME_PRODUCTID = "prID";
	public const string TAG_NAME_COMMENT = "©cmt";
	public const string TAG_NAME_LONGDESCRIPTION = "©des";
	public const string TAG_NAME_COPYRIGHT = "cprt";
	public const string TAG_NAME_PUBLISHER = "©pub";
	public const string TAG_NAME_YEAR = "©day";
	public const string TAG_NAME_NARRATOR = "©nrt";
	public const string TAG_NAME_ASIN = "CDEK";
	public const string TAG_NAME_RELEASEDATE = "rldt";
	public const string TAG_NAME_ACR = "AACR";
	public const string TAG_NAME_VERSION = "VERS";
	public const string TAG_NAME_ENCODER = "©too";
	public const string TAG_NAME_COVER = "covr";
	public const string TAG_NAME_TRACK_NUMBER = "trkn";
	public const string TAG_NAME_DISK_NUMBER = "disk";

	public AppleListBox AppleListBox { get; }
	public IEnumerable<string> TagNames => AppleListBox.Tags.Select(t => t.Header.Type);
	public MetadataItems(AppleListBox appleListBox)
	{
		AppleListBox = appleListBox;
	}
	public string? FirstAuthor => Artist?.Split(';')?[0];
	public string? TitleSansUnabridged => Title?.Replace(" (Unabridged)", "");

	public string? BookCopyright => _copyright is not null && _copyright.Length > 0 ? _copyright[0] : default;
	public string? RecordingCopyright => _copyright is not null && _copyright.Length > 1 ? _copyright[1] : default;
	private string[]? _copyright => Copyright?.Replace("&#169;", "©")?.Split(';');
	public string? Title { get => AppleListBox.GetTagString(TAG_NAME_TITLE); set => AppleListBox.EditOrAddTag(TAG_NAME_TITLE, value); }
	public string? Producer { get => AppleListBox.GetTagString(TAG_NAME_PRODUCER); set => AppleListBox.EditOrAddTag(TAG_NAME_PRODUCER, value); }
	public string? Artist { get => AppleListBox.GetTagString(TAG_NAME_ARTIST); set => AppleListBox.EditOrAddTag(TAG_NAME_ARTIST, value); }
	public string? AlbumArtists { get => AppleListBox.GetTagString(TAG_NAME_ALBUMARTIST); set => AppleListBox.EditOrAddTag(TAG_NAME_ALBUMARTIST, value); }
	public string? Album { get => AppleListBox.GetTagString(TAG_NAME_ALBUM); set => AppleListBox.EditOrAddTag(TAG_NAME_ALBUM, value); }
	public string? Generes { get => AppleListBox.GetTagString(TAG_NAME_GENRES); set => AppleListBox.EditOrAddTag(TAG_NAME_GENRES, value); }
	public string? ProductID { get => AppleListBox.GetTagString(TAG_NAME_PRODUCTID); set => AppleListBox.EditOrAddTag(TAG_NAME_PRODUCTID, value); }
	public string? Comment { get => AppleListBox.GetTagString(TAG_NAME_COMMENT); set => AppleListBox.EditOrAddTag(TAG_NAME_COMMENT, value); }
	public string? LongDescription { get => AppleListBox.GetTagString(TAG_NAME_LONGDESCRIPTION); set => AppleListBox.EditOrAddTag(TAG_NAME_LONGDESCRIPTION, value); }
	public string? Copyright { get => AppleListBox.GetTagString(TAG_NAME_COPYRIGHT); set => AppleListBox.EditOrAddTag(TAG_NAME_COPYRIGHT, value); }
	public string? Publisher { get => AppleListBox.GetTagString(TAG_NAME_PUBLISHER); set => AppleListBox.EditOrAddTag(TAG_NAME_PUBLISHER, value); }
	public string? Year { get => AppleListBox.GetTagString(TAG_NAME_YEAR); set => AppleListBox.EditOrAddTag(TAG_NAME_YEAR, value); }
	public string? Narrator { get => AppleListBox.GetTagString(TAG_NAME_NARRATOR); set => AppleListBox.EditOrAddTag(TAG_NAME_NARRATOR, value); }
	public string? Asin { get => AppleListBox.GetTagString(TAG_NAME_ASIN); set => AppleListBox.EditOrAddTag(TAG_NAME_ASIN, value); }
	public string? ReleaseDate { get => AppleListBox.GetTagString(TAG_NAME_RELEASEDATE); set => AppleListBox.EditOrAddTag(TAG_NAME_RELEASEDATE, value); }
	public string? Acr { get => AppleListBox.GetTagString(TAG_NAME_ACR); set => AppleListBox.EditOrAddTag(TAG_NAME_ACR, value); }
	public string? Version { get => AppleListBox.GetTagString(TAG_NAME_VERSION); set => AppleListBox.EditOrAddTag(TAG_NAME_VERSION, value); }
	public string? Encoder { get => AppleListBox.GetTagString(TAG_NAME_ENCODER); set => AppleListBox.EditOrAddTag(TAG_NAME_ENCODER, value); }
	public byte[]? Cover { get => AppleListBox.GetTagBox(TAG_NAME_COVER)?.Data.Data; set => SetCoverArt(value); }
	public AppleDataType? CoverFormat => AppleListBox.GetTagBox(TAG_NAME_COVER)?.Data.DataType;
	public TrackNumber? TrackNumber { get => AppleListBox.GetTagData<TrackNumber>(TAG_NAME_TRACK_NUMBER); set => AppleListBox.EditOrAddTag(TAG_NAME_TRACK_NUMBER, value); }
	public DiskNumber? DiskNumber { get => AppleListBox.GetTagData<DiskNumber>(TAG_NAME_DISK_NUMBER); set => AppleListBox.EditOrAddTag(TAG_NAME_DISK_NUMBER, value); }

	public IEnumerable<FreeformTagBox> GetFreeformTagBoxes()
		=> AppleListBox.Tags.OfType<FreeformTagBox>();

	private void SetCoverArt(byte[]? coverArtBytes)
	{
		if (coverArtBytes is null)
			AppleListBox.RemoveTag(TAG_NAME_COVER);
		else if (coverArtBytes.Length >= 2 && coverArtBytes[0] == 0x42 && coverArtBytes[1] == 0x4D)
			editOrAdd(AppleDataType.BMP);
		else if (coverArtBytes.Length >= 3 && coverArtBytes[0] == 0xFF && coverArtBytes[1] == 0xD8 && coverArtBytes[2] == 0xFF)
			editOrAdd(AppleDataType.JPEG);
		else if (coverArtBytes.Length >= 8 && coverArtBytes.SequenceEqual(new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a }))
			editOrAdd(AppleDataType.PNG);
		else
			throw new InvalidDataException("Image data is not a jpeg, PNG, or windows bitmap.");

		void editOrAdd(AppleDataType dataType)
		{
			if (AppleListBox.GetTagBox(TAG_NAME_COVER) is { } tagBox && tagBox.Data.DataType != dataType)
			{
				//Allow changing data type by removing and re-adding
				AppleListBox.RemoveTag(tagBox);
			}
			AppleListBox.EditOrAddTag(TAG_NAME_COVER, coverArtBytes, dataType);
		}
	}

	public static MetadataItems? FromFile(string mp4File)
	{
		using var file = System.IO.File.Open(mp4File, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

		BoxHeader header;
		do
		{
			header = new BoxHeader(file);

			if (header.Type is "moov")
				continue;
			else if (header.Type is "udta")
				break;
			else
				file.Position += header.TotalBoxSize - header.HeaderSize;

		} while (file.Position < file.Length);

		if (header?.Type is not "udta") return null;

		var ilst = new UdtaBox(file, header, null)?.GetChild<MetaBox>()?.GetChild<AppleListBox>();

		return ilst is null ? null : new MetadataItems(ilst);
	}
}
