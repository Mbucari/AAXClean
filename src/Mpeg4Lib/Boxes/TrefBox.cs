using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

public interface ITrackReferenceTypeBox : IBox
{
	public uint TrackId { get; set; }
}

public class TrefBox : Box
{
	public override long RenderSize => base.RenderSize + References.Sum(r => r.RenderSize);
	public List<ITrackReferenceTypeBox> References { get; }
	public TrefBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		int referenceTypeBoxes = (int)(Header.TotalBoxSize - Header.HeaderSize) / 12;
		References = new(referenceTypeBoxes);
		for (int i = 0; i < referenceTypeBoxes; i++)
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

	public TrefBox AddReference(string type, uint trackId)
	{
		References.Add(new TrackReferenceTypeBox(type, trackId, this));
		return this;
	}

	protected override void Render(Stream file)
	{
		foreach (var box in References)
			box.Save(file);
	}

	[DebuggerDisplay("{Header.Type,nq}, TrackId = {TrackId}")]
	private class TrackReferenceTypeBox : Box, ITrackReferenceTypeBox
	{
		public override long RenderSize => base.RenderSize + sizeof(int);
		public uint TrackId { get; set; }

		public TrackReferenceTypeBox(Stream file, IBox? parent)
			: base(new BoxHeader(file), parent)
		{
			TrackId = file.ReadUInt32BE();
		}

		public TrackReferenceTypeBox(string type, uint trackId, IBox parent)
			: base(new BoxHeader(12, type), parent)
		{
			TrackId = trackId;
		}

		protected override void Render(Stream file)
		{
			file.WriteUInt32BE(TrackId);
		}
	}
}
