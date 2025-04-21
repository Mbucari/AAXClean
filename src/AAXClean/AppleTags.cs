using Mpeg4Lib.Boxes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AAXClean
{
	public sealed class AppleTags
	{

		private readonly AppleListBox IList;
		public IEnumerable<string> TagNames => IList.Tags.Select(t => t.Header.Type);
		public AppleTags(AppleListBox appleListBox)
		{
			IList = appleListBox;
		}

		public AppleListBox AppleListBox => IList;
		public string? FirstAuthor => Artist?.Split(';')?[0];
		public string? TitleSansUnabridged => Title?.Replace(" (Unabridged)", "");

		public string? BookCopyright => _copyright is not null && _copyright.Length > 0 ? _copyright[0] : default;
		public string? RecordingCopyright => _copyright is not null && _copyright.Length > 1 ? _copyright[1] : default;
		private string[]? _copyright => Copyright?.Replace("&#169;", "©")?.Split(';');

		public string? Title { get => IList.GetTagString("©nam"); set => IList.EditOrAddTag("©nam", value); }
		public string? Artist { get => IList.GetTagString("©ART"); set => IList.EditOrAddTag("©ART", value); }
		public string? AlbumArtists { get => IList.GetTagString("aART"); set => IList.EditOrAddTag("aART", value); }
		public string? Album { get => IList.GetTagString("©alb"); set => IList.EditOrAddTag("©alb", value); }
		public string? Generes { get => IList.GetTagString("©gen"); set => IList.EditOrAddTag("©gen", value); }
		public string? ProductID { get => IList.GetTagString("prID"); set => IList.EditOrAddTag("prID", value); }
		public string? Comment { get => IList.GetTagString("©cmt"); set => IList.EditOrAddTag("©cmt", value); }
		public string? LongDescription { get => IList.GetTagString("©des"); set => IList.EditOrAddTag("©des", value); }
		public string? Copyright { get => IList.GetTagString("cprt"); set => IList.EditOrAddTag("cprt", value); }
		public string? Publisher { get => IList.GetTagString("©pub"); set => IList.EditOrAddTag("©pub", value); }
		public string? Year { get => IList.GetTagString("©day"); set => IList.EditOrAddTag("©day", value); }
		public string? Narrator { get => IList.GetTagString("©nrt"); set => IList.EditOrAddTag("©nrt", value); }
		public string? Asin { get => IList.GetTagString("CDEK"); set => IList.EditOrAddTag("CDEK", value); }
		public string? ReleaseDate { get => IList.GetTagString("rldt"); set => IList.EditOrAddTag("rldt", value); }
		public string? Acr { get => IList.GetTagString("AACR"); set => IList.EditOrAddTag("AACR", value); }
		public string? Version { get => IList.GetTagString("VERS"); set => IList.EditOrAddTag("VERS", value); }
		public byte[]? Cover { get => IList.GetTagBytes("covr"); set => IList.EditOrAddTag("covr", value, AppleDataType.JPEG); }

		[DisallowNull]
		public (int trackNum, int trackCount)? Tracks
		{
			get
			{
				var data = IList.GetTagBytes("trkn");
				if (data is null) return null;

				int trackNum = (data[2] << 8) | data[3];
				int trackCount = (data[4] << 8) | data[5];

				return (trackNum, trackCount);
			}
			set
			{
				if (value is not (int trackNum, int trackCount))
					throw new ArgumentNullException(nameof(Tracks), "Cannot set to null");

				var data = new byte[8];

				data[3] = (byte)trackNum;
				data[2] = (byte)(trackNum >> 8);

				data[5] = (byte)trackCount;
				data[4] = (byte)(trackCount >> 8);

				IList.EditOrAddTag("trkn", data);
			}
		}

		public static AppleTags? FromFile(string mp4File)
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

			return ilst is null ? null : new AppleTags(ilst);
		}
	}
}
