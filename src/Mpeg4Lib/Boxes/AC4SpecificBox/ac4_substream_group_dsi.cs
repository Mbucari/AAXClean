using Mpeg4Lib.Util;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

/// <summary>
/// ETSI TS 103 190-2 E.11 ac4_substream_group_dsi
/// </summary>
public class ac4_substream_group_dsi
{
	public bool b_substreams_present;
	public bool b_hsf_ext;
	public bool b_channel_coded;
	public byte n_substreams;
	public ac4_substream[] substreams;
	public bool b_content_type;

	public byte? content_classifier;
	public bool? b_language_indicator;
	public int? n_language_tag_bytes;
	public byte[]? language_tag_bytes;
	public ac4_substream_group_dsi(BitReader reader)
	{
		b_substreams_present = reader.ReadBool();
		b_hsf_ext = reader.ReadBool();
		b_channel_coded = reader.ReadBool();
		n_substreams = (byte)reader.Read(8);
		substreams = new ac4_substream[n_substreams];
		for (int i = 0; i < n_substreams; i++)
		{
			substreams[i] = new ac4_substream(this, reader);
		}
		b_content_type = reader.ReadBool();
		if (b_content_type)
		{
			content_classifier = (byte)reader.Read(3);
			b_language_indicator = reader.ReadBool();
			if (b_language_indicator.Value)
			{
				n_language_tag_bytes = (byte)reader.Read(6);
				language_tag_bytes = new byte[n_language_tag_bytes.Value];
				for (int i = 0; i < language_tag_bytes.Length; i++)
					language_tag_bytes[i] = (byte)reader.Read(8);
			}
		}
	}
}
