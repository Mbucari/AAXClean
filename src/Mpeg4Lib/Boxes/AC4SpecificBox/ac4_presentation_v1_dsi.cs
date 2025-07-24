using Mpeg4Lib.Util;
using System;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

/// <summary>
/// ETSI TS 103 190-2 E.10 ac4_presentation_v1_dsi
/// </summary>
public class ac4_presentation_v1_dsi
{
	public uint presentation_version;
	public byte presentation_config_v1;
	public bool b_add_emdf_substreams;
	public byte? mdcompat;
	public bool? b_presentation_id;
	public byte? presentation_id;
	public byte? dsi_frame_rate_multiply_info;
	public byte? dsi_frame_rate_fraction_info;
	public byte? presentation_emdf_version;
	public ushort? presentation_key_id;
	public bool? b_presentation_channel_coded;
	public byte? dsi_presentation_ch_mode;
	public byte? pres_b_4_back_channels_present;
	public byte? pres_top_channel_pairs;
	public ChannelGroups? presentation_channel_mask_v1;
	public bool? b_presentation_core_differs;
	public bool? b_presentation_core_channel_coded;
	public byte? dsi_presentation_channel_mode_core;
	public bool? b_presentation_filter;
	public bool? b_enable_presentation;
	public byte? n_filter_bytes;
	public byte[]? filter_data;

	public ac4_substream_group_dsi? substream;
	public bool? b_pre_virtualized;
	public byte? n_add_emdf_substreams;
	public (byte substream_emdf_version, ushort substream_key_id)[]? substreams_emdfs;
	public bool b_presentation_bitrate_info;
	public ac4_bitrate_dsi? ac4_Bitrate_Dsi;
	public bool b_alternative;
	public alternative_info? alternative_Info;
	public byte? de_indicator;
	public bool? b_extended_presentation_id;
	public ushort? extended_presentation_id;

	public bool? dolby_atmos_indicator;

	public ac4_presentation_v1_dsi(uint presentation_version, uint pres_bytes, BitReader reader)
	{
		this.presentation_version = presentation_version;
		var start = reader.Position;

		presentation_config_v1 = (byte)reader.Read(5);
		if (presentation_config_v1 is 6)
		{
			b_add_emdf_substreams = true;
		}
		else
		{
			mdcompat = (byte)reader.Read(3);
			b_presentation_id = reader.ReadBool();
			if (b_presentation_id.Value)
			{
				presentation_id = (byte)reader.Read(5);
			}
			dsi_frame_rate_multiply_info = (byte)reader.Read(2);
			dsi_frame_rate_fraction_info = (byte)reader.Read(2);
			presentation_emdf_version = (byte)reader.Read(5);
			presentation_key_id = (ushort)reader.Read(10);
			b_presentation_channel_coded = reader.ReadBool();

			if (b_presentation_channel_coded.Value)
			{
				dsi_presentation_ch_mode = (byte)reader.Read(5);
				if (dsi_presentation_ch_mode is 11 or 12 or 13 or 14)
				{
					pres_b_4_back_channels_present = (byte)reader.Read(1);
					pres_top_channel_pairs = (byte)reader.Read(2);
				}
				presentation_channel_mask_v1 = (ChannelGroups)reader.Read(24);
			}
			b_presentation_core_differs = reader.ReadBool();
			if (b_presentation_core_differs.Value)
			{
				b_presentation_core_channel_coded = reader.ReadBool();
				if (b_presentation_core_channel_coded.Value)
				{
					dsi_presentation_channel_mode_core = (byte)reader.Read(2);
				}
			}
			b_presentation_filter = reader.ReadBool();
			if (b_presentation_filter.Value)
			{
				b_enable_presentation = reader.ReadBool();
				n_filter_bytes = (byte)reader.Read(8);
				filter_data = new byte[n_filter_bytes.Value];
				for (int i = 0; i < n_filter_bytes.Value; i++)
					filter_data[i] = (byte)reader.Read(8);
			}
			if (presentation_config_v1 == 0x1f)
			{
				substream = new ac4_substream_group_dsi(reader);
			}
			else
			{
				throw new NotSupportedException();
			}
			b_pre_virtualized = reader.ReadBool();
			b_add_emdf_substreams = reader.ReadBool();
		}
		if (b_add_emdf_substreams)
		{
			n_add_emdf_substreams = (byte)reader.Read(7);
			substreams_emdfs = new (byte substream_emdf_version, ushort substream_key_id)[n_add_emdf_substreams.Value];
			for (int j = 0; j < n_add_emdf_substreams; j++)
				substreams_emdfs[j] = ((byte)reader.Read(5), (ushort)reader.Read(10));
		}
		b_presentation_bitrate_info = reader.ReadBool();
		if (b_presentation_bitrate_info)
			ac4_Bitrate_Dsi = new ac4_bitrate_dsi(reader);
		b_alternative = reader.ReadBool();
		if (b_alternative)
		{
			reader.ByteAlign();
			alternative_Info = new alternative_info(reader);
		}

		reader.ByteAlign();

		var read = reader.Position - start;
		if (read <= (pres_bytes - 1) * 8)
		{
			de_indicator = (byte)reader.Read(1);
			//Extension to AC-4 DSI 
			dolby_atmos_indicator = reader.ReadBool();
			_ = reader.Read(4);
			b_extended_presentation_id = reader.ReadBool();
			if (b_extended_presentation_id.Value)
				extended_presentation_id = (ushort)reader.Read(9);
			else
				_ = reader.Read(1);
		}
	}
}
