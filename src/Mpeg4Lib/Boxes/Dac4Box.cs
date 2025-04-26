using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

/// <summary>
/// AC4SpecificBox. ETSI TS 103 190-2 E.5
/// </summary>
public class Dac4Box : Box
{
	public override long RenderSize => base.RenderSize + Ac4Data.Length;
	private readonly byte[] Ac4Data;
	private readonly ac4_dsi_v1? Ac4DsiV1;
	public Dac4Box(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		Ac4Data = file.ReadBlock((int)(header.TotalBoxSize - header.HeaderSize));
		var reader = new BitReader(Ac4Data);
		//Ac4DsiV1 = new ac4_dsi_v1(reader);
	}

	protected override void Render(Stream file)
	{
		file.Write(Ac4Data);
	}

	private class ac4_dsi_v1
	{
		public byte ac4_dsi_version;
		public byte bitstream_version;
		public byte fs_index;
		public byte frame_rate_index;
		public ushort n_presentations;
		public bool? b_program_id;
		public ushort? short_program_id;
		public bool? b_uuid;
		public Guid? program_uuid;
		public ac4_bitrate_dsi ac4_Bitrate_Dsi;
		public object?[] presentations;

		public ac4_dsi_v1(BitReader reader)
		{
			ac4_dsi_version = (byte)reader.Read(3);
			bitstream_version = (byte)reader.Read(7);
			fs_index = (byte)reader.Read(1);
			frame_rate_index = (byte)reader.Read(4);
			n_presentations = (ushort)reader.Read(9);
			if (bitstream_version > 1)
			{
				b_program_id = reader.ReadBool();
				if (b_program_id.Value)
				{
					short_program_id = (ushort)reader.Read(16);
					b_uuid = reader.ReadBool();
					if (b_uuid.Value)
					{
						program_uuid = new Guid(
							reader.Read(32),
							(ushort)reader.Read(16),
							(ushort)reader.Read(16),
							(byte)reader.Read(8),
							(byte)reader.Read(8),
							(byte)reader.Read(8),
							(byte)reader.Read(8),
							(byte)reader.Read(8),
							(byte)reader.Read(8),
							(byte)reader.Read(8),
							(byte)reader.Read(8));
					}
				}
			}

			ac4_Bitrate_Dsi = new ac4_bitrate_dsi(reader);
			reader.ByteAlign();
			presentations = new object[n_presentations];
			for (int i = 0; i < n_presentations; i++)
			{
				uint presentation_bytes;
				var presentation_version = reader.Read(8);
				var pres_bytes = reader.Read(8);
				if (pres_bytes == 255)
				{
					var add_pres_bytes = reader.Read(16);
					pres_bytes += add_pres_bytes;
				}
				if (presentation_version == 0)
				{
					//ac4_presentation_v0_dsi();
					throw new NotSupportedException();
				}
				else
				{
					if (presentation_version == 1)
					{
						var start = reader.Position;
						presentations[i] = new ac4_presentation_v1_dsi(pres_bytes, reader);
						presentation_bytes = (uint)(reader.Position - start) / 8;
					}
					else
						presentation_bytes = 0;
				}
				var skip_bytes = pres_bytes - presentation_bytes;
				reader.Position += 8 * (int)skip_bytes;
			}
		}
	}
	private class ac4_presentation_v1_dsi
	{
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
		public uint? presentation_channel_mask_v1;
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

		public ac4_presentation_v1_dsi(uint pres_bytes, BitReader reader)
		{
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
					presentation_channel_mask_v1 = reader.Read(24);
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
				_ = reader.Read(5);
				b_extended_presentation_id = reader.ReadBool();
				if (b_extended_presentation_id.Value)
					extended_presentation_id = (ushort)reader.Read(9);
				else
					_ = reader.Read(1);
			}


		}
	}
	private class ac4_substream_group_dsi
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
	private class ac4_substream
	{
		public byte dsi_sf_multiplier;
		public bool b_substream_bitrate_indicator;
		public byte? substream_bitrate_indicator;
		public uint? dsi_substream_channel_mask;
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
				dsi_substream_channel_mask = reader.Read(24);
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
	private class ac4_bitrate_dsi
	{
		public byte bit_rate_mode;
		public uint bit_rate;
		public uint bit_rate_precision;
		public ac4_bitrate_dsi(BitReader reader)
		{
			bit_rate_mode = (byte)reader.Read(2);
			bit_rate = reader.Read(32);
			bit_rate_precision = reader.Read(32);
		}
	}
	private class alternative_info
	{
		public ushort name_len;
		public string presentation_name;
		public byte n_targets;
		public (byte target_md_compat, byte target_device_category)[] target_ids;
		public alternative_info(BitReader reader)
		{
			name_len = (ushort)reader.Read(16);
			char[] nameBts = new char[name_len];
			for (int i = 0; i < name_len; i++)
			{
				nameBts[i] = (char)reader.Read(8);
			}
			presentation_name = new string(nameBts);
			n_targets = (byte)reader.Read(5);
			target_ids = new (byte target_md_compat, byte target_device_category)[n_targets];

			for (int i = 0; i < name_len; i++)
			{
				target_ids[i] = ((byte)reader.Read(3), (byte)reader.Read(8));
			}
		}
	}
}
