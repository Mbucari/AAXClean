using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class TfhdBox : FullBox
{
	public override long RenderSize => base.RenderSize + 4 + OptionalFieldsSize;

    public uint TrackID { get; }
    public long? BaseDataOffset { get; }
    public uint? SampleDescriptionIndex { get; }
    public uint? DefaultSampleDuration { get; }
    public uint? DefaultSampleSize { get; }
    public uint? DefaultSampleFlags { get; }

    public bool DurationIsEmpty => (Flags & 0x010000) == 0x010000;
    public bool DefaultBaseIsMoof => (Flags & 0x020000) == 0x020000;

    private int OptionalFieldsSize =>
        (base_data_offset_present ? 8 : 0) +
        (sample_description_index_present ? 4 : 0) +
        (default_sample_duration_present ? 4 : 0) +
        (default_sample_size_present ? 4 : 0) +
        (default_sample_flags_present ? 4 : 0);

    private bool base_data_offset_present => (Flags & 1) == 1;
    private bool sample_description_index_present => (Flags & 2) == 2;
    private bool default_sample_duration_present => (Flags & 8) == 8;
    private bool default_sample_size_present => (Flags & 16) == 16;
    private bool default_sample_flags_present  => (Flags & 32) == 32;

    public TfhdBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
	{
        TrackID = file.ReadUInt32BE();

        if (base_data_offset_present)
            BaseDataOffset = file.ReadInt64BE();
        if (sample_description_index_present)
            SampleDescriptionIndex = file.ReadUInt32BE();
        if (default_sample_duration_present)
            DefaultSampleDuration = file.ReadUInt32BE();
        if (default_sample_size_present)
            DefaultSampleSize = file.ReadUInt32BE();
        if (default_sample_flags_present)
            DefaultSampleFlags = file.ReadUInt32BE();
    }
	protected override void Render(Stream file)
	{
		base.Render(file);
        file.WriteUInt32BE(TrackID);
        if (BaseDataOffset.HasValue)
            file.WriteInt64BE(BaseDataOffset.Value);
        if (SampleDescriptionIndex.HasValue)
            file.WriteUInt32BE(SampleDescriptionIndex.Value);
        if (DefaultSampleDuration.HasValue)
            file.WriteUInt32BE(DefaultSampleDuration.Value);
        if (DefaultSampleSize.HasValue)
            file.WriteUInt32BE(DefaultSampleSize.Value);
        if (DefaultSampleFlags.HasValue)
            file.WriteUInt32BE(DefaultSampleFlags.Value);
	}
}
