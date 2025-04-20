using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class SidxBox : FullBox
{
	public override long RenderSize => base.RenderSize + 8 + (Version == 0 ? 8 : 16) + 4 + Segments.Length * 12;
	public uint ReferenceId { get; }
	public int Timescale { get; }

	public long EarliestPresentationTime { get; }
	public long FirstOffset { get; }

	public Segment[] Segments { get; }

	public SidxBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		ReferenceId = file.ReadUInt32BE();
		Timescale = file.ReadInt32BE();

		if (Version == 0)
		{
			EarliestPresentationTime = file.ReadUInt32BE();
			FirstOffset = file.ReadUInt32BE();
		}
		else
		{
			EarliestPresentationTime = file.ReadInt64BE();
			FirstOffset = file.ReadInt64BE();
		}
		_ = file.ReadInt16BE();
		int referenceCount = file.ReadUInt16BE();

		Segments = new Segment[referenceCount];
		for (int i = 0; i < Segments.Length; i++)
		{
			Segments[i] = new Segment(file);
		}
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteUInt32BE(ReferenceId);
		file.WriteInt32BE(Timescale);

		if (Version == 0)
		{
			file.WriteUInt32BE((uint)EarliestPresentationTime);
			file.WriteUInt32BE((uint)FirstOffset);
		}
		else
		{
			file.WriteInt64BE(EarliestPresentationTime);
			file.WriteInt64BE(FirstOffset);
		}
		file.WriteInt16BE(0);
		file.WriteInt16BE((short)Segments.Length);
		foreach (var segment in Segments)
			segment.Save(file);
	}

	public class Segment
	{
		private uint type_and_size;
		private uint subsegment_duration;
		private uint SAP;

		internal Segment(Stream file)
		{
			type_and_size = file.ReadUInt32BE();
			subsegment_duration = file.ReadUInt32BE();
			SAP = file.ReadUInt32BE();
		}

		public void Save(Stream file)
		{
			file.WriteUInt32BE(type_and_size);
			file.WriteUInt32BE(subsegment_duration);
			file.WriteUInt32BE(SAP);
		}

		public bool ReferenceType
		{
			get => (type_and_size & 0x80000000) == 0x80000000;
			set => type_and_size = value ? type_and_size | 0x80000000 : type_and_size & 0x7FFFFFFF;
		}

		public int ReferenceSize
		{
			get => (int)(type_and_size & 0x7FFFFFFF);
			set
			{
				ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(ReferenceSize));

				type_and_size = (type_and_size & 0x80000000) | (uint)value;
			}
		}

		public uint SubsegmentDuration
		{
			get => subsegment_duration;
			set => subsegment_duration = value;
		}

		public bool StartsWithSAP
		{
			get => (SAP & 0x80000000) == 0x80000000;
			set => SAP = value ? SAP | 0x80000000 : SAP & 0x7FFFFFFF;
		}

		public int SapType
		{
			get => (int)(SAP >> 28) & 7;
			set
			{
				ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(SapType));
				ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 7, nameof(SapType));

				SAP = (SAP & 0x8FFFFFFF) | (uint)(value << 28);
			}
		}

		public int SapDeltaTime
		{
			get => (int)SAP & 0xFFFFFFF;
			set
			{
				ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(SapType));
				ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 0xFFFFFFF, nameof(SapType));
				SAP = (SAP & 0xF0000000) | (uint)value;
			}
		}
	}
}
