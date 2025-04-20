﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	public class MoovBox : Box
	{
		public MoovBox(Stream file, BoxHeader header) : base(header, null)
		{
			LoadChildren(file);
		}

		public MvhdBox Mvhd => GetChildOrThrow<MvhdBox>();
		public TrakBox AudioTrack => Tracks.Where(t => t.GetChild<MdiaBox>()?.GetChild<HdlrBox>()?.HandlerType == "soun").First();
		public TrakBox? TextTrack => Tracks.Where(t => t.GetChild<MdiaBox>()?.GetChild<HdlrBox>()?.HandlerType == "text").FirstOrDefault();
		public AppleListBox? ILst => GetChild<UdtaBox>()?.GetChild<MetaBox>()?.GetChild<AppleListBox>();
		public IEnumerable<TrakBox> Tracks => GetChildren<TrakBox>();

		/// <summary>
		/// Adjust the chunk offsets in all tracks
		/// </summary>
		/// <param name="shiftVector">The size and direction of the shift</param>
		public void ShiftChunkOffsets(long shiftVector)
		{
			foreach (var track in Tracks)
			{
				foreach (var co in track.Mdia.Minf.Stbl.COBox.ChunkOffsets)
					co.ChunkOffset += shiftVector;
			}
		}

		protected override void Render(Stream file)
		{
			return;
		}
	}
}
