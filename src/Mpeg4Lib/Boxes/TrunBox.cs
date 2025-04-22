using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class TrunBox : FullBox
{
	public override long RenderSize => base.RenderSize + 4 + (data_offset_present ? 4 : 0) + (first_sample_flags_present ? 4 : 0) + SampleInfoSize * Samples.Length;
	public int DataOffset { get; }
	public uint FirstSampleFlags { get; }
	public SampleInfo[] Samples { get; }
	private bool data_offset_present => (Flags & 1) == 1;
	private bool first_sample_flags_present => (Flags & 4) == 4;
	public bool sample_duration_present => (Flags & 0x100) == 0x100;
	public bool sample_size_present => (Flags & 0x200) == 0x200;
	private bool sample_flags_present => (Flags & 0x400) == 0x400;
	private bool sample_composition_time_offsets_present => (Flags & 0x800) == 0x800;

	private int SampleInfoSize =>
		(sample_duration_present ? 4 : 0) +
		(sample_size_present ? 4 : 0) +
		(sample_flags_present ? 4 : 0) +
		(sample_composition_time_offsets_present ? 4 : 0);

	public TrunBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		uint sampleCount = file.ReadUInt32BE();
		if (data_offset_present)
			DataOffset = file.ReadInt32BE();
		if (first_sample_flags_present)
			FirstSampleFlags = file.ReadUInt32BE();

		Samples = new SampleInfo[sampleCount];

		for (int i = 0; i < sampleCount; i++)
		{
			uint? sampleDuration = sample_duration_present ? file.ReadUInt32BE() : null;
			int? sampleSize = sample_size_present ? file.ReadInt32BE() : null;
			uint? sampleFlags = sample_flags_present ? file.ReadUInt32BE() : null;
			int? sampleCompositionTimeOffset = sample_composition_time_offsets_present ? file.ReadInt32BE() : null;

			Samples[i] = new SampleInfo(sampleDuration, sampleSize, sampleFlags, sampleCompositionTimeOffset);
		}
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		file.WriteInt32BE(Samples.Length);
		if (data_offset_present)
			file.WriteInt32BE(DataOffset);
		if (first_sample_flags_present)
			file.WriteUInt32BE(FirstSampleFlags);

		for (int i = 0; i < Samples.Length; i++)
		{
			if (sample_duration_present)
				file.WriteUInt32BE(Samples[i].SampleDuration ?? 0);
			if (sample_size_present)
				file.WriteInt32BE(Samples[i].SampleSize ?? 0);
			if (sample_flags_present)
				file.WriteUInt32BE(Samples[i].SampleFlags ?? 0);
			if (sample_composition_time_offsets_present)
				file.WriteInt32BE(Samples[i].SampleCompositionTimeOffset ?? 0);
		}
	}

	public class SampleInfo
	{
		public uint? SampleDuration { get; }
		public int? SampleSize { get; }
		public uint? SampleFlags { get; }
		public int? SampleCompositionTimeOffset { get; }

		public SampleInfo(uint? sampleDuration, int? sampleSize, uint? sampleFlags, int? sampleCompositionTimeOffset)
		{
			SampleDuration = sampleDuration;
			SampleSize = sampleSize;
			SampleFlags = sampleFlags;
			SampleCompositionTimeOffset = sampleCompositionTimeOffset;
		}
	}
}
