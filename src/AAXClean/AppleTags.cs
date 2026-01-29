using Mpeg4Lib;
using Mpeg4Lib.Boxes;
using System;

namespace AAXClean
{
	[Obsolete("Use Mpeg4Lib.AppleTags instead.")]
	public sealed class AppleTags(AppleListBox appleListBox) : MetadataItems(appleListBox)
	{
		[Obsolete("Use TrackNumber property instead.")]
		public (int trackNum, int trackCount)? Tracks
		{
			get
				=> TrackNumber is TrackNumber track
				? (track.Track, track.TotalTracks)
				: null;
			set
			{
				TrackNumber? track = value.HasValue ? new TrackNumber((ushort)value.Value.trackNum, (ushort)value.Value.trackCount) : null;
				AppleListBox.EditOrAddTag(TAG_NAME_TRACK_NUMBER, track);
			}
		}


		[Obsolete("Use Mpeg4Lib.AppleTags.FromFile() instead.")]
		public static new AppleTags? FromFile(string mp4File)
			=> MetadataItems.FromFile(mp4File) is { } tags ? new AppleTags(tags.AppleListBox) : null;
	}
}
