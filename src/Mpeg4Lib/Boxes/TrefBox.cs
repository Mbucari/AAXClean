using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

public interface ITrackReferenceTypeBox : IBox
{
	HashSet<uint> TrackIds { get; set; }
}

public class TrefBox : Box
{
	public override long RenderSize => base.RenderSize + References.Sum(r => r.RenderSize);
	public List<ITrackReferenceTypeBox> References { get; }
	public TrefBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		References = new();
		while (RemainingBoxLength(file) > 0)
			References.Add(new TrackReferenceTypeBox(file, this));
	}

	private TrefBox(IBox parent) : base(new BoxHeader(8, "tref"), parent)
	{
		References = [];
	}

	public static TrefBox CreatEmpty(IBox parent)
	{
		var tref = new TrefBox(parent);
		parent.Children.Add(tref);
		return tref;
	}

	public TrefBox AddReference(string type, HashSet<uint> trackIds)
	{
		References.Add(new TrackReferenceTypeBox(type, trackIds, this));
		return this;
	}

	protected override void Render(Stream file)
	{
		foreach (var box in References)
			box.Save(file);
	}

	[DebuggerDisplay("{Header.Type,nq}, {TrackIds}")]
	private class TrackReferenceTypeBox : Box, ITrackReferenceTypeBox
	{
		public override long RenderSize => base.RenderSize + sizeof(int) * TrackIds.Count;
		public HashSet<uint> TrackIds { get; set; }

		public TrackReferenceTypeBox(Stream file, IBox? parent)
			: base(new BoxHeader(file), parent)
		{
			var numTraks = (int)RemainingBoxLength(file) / sizeof(int);
			TrackIds = new(numTraks);
			for (int i = 0; i < numTraks; i++)
				TrackIds.Add(file.ReadUInt32BE());
		}

		public TrackReferenceTypeBox(string type, HashSet<uint> trackIds, IBox parent)
			: base(new BoxHeader(12, type), parent)
		{
			TrackIds = trackIds;
		}

		protected override void Render(Stream file)
		{
			foreach (var id in TrackIds)
				file.WriteUInt32BE(id);
		}
	}
}
