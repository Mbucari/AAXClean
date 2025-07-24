using Mpeg4Lib.Util;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

/// <summary>
/// ETSI TS 103 190-2 E.11 ac4_substream_group_dsi
/// </summary>
public class ac4_substream
{
	public byte dsi_sf_multiplier;
	public bool b_substream_bitrate_indicator;
	public byte? substream_bitrate_indicator;
	public ChannelGroups? dsi_substream_channel_mask;
	public bool? b_ajoc;
	public bool? b_static_dmx;
	public byte? n_dmx_objects_minus1;
	public byte? n_umx_objects_minus1;
	public bool? b_substream_contains_bed_objects;
	public bool? b_substream_contains_dynamic_objects;
	public bool? b_substream_contains_ISF_objects;

	public ac4_substream(ac4_substream_group_dsi info, BitReader reader)
	{
		dsi_sf_multiplier = (byte)reader.Read(2);
		b_substream_bitrate_indicator = reader.ReadBool();
		if (b_substream_bitrate_indicator)
			substream_bitrate_indicator = (byte)reader.Read(5);
		if (info.b_channel_coded)
			dsi_substream_channel_mask = (ChannelGroups)reader.Read(24);
		else
		{
			b_ajoc = reader.ReadBool();
			if (b_ajoc.Value)
			{
				b_static_dmx = reader.ReadBool();
				if (b_static_dmx.Value)
				{
					n_dmx_objects_minus1 = (byte)reader.Read(4);
				}
				n_umx_objects_minus1 = (byte)reader.Read(6);
			}
			b_substream_contains_bed_objects = reader.ReadBool();
			b_substream_contains_dynamic_objects = reader.ReadBool();
			b_substream_contains_ISF_objects = reader.ReadBool();
			_ = reader.Read(1);
		}
	}
}
