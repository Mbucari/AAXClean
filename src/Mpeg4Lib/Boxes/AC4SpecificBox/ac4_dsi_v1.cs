using Mpeg4Lib.Util;
using System;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

/// <summary>
/// ETSI TS 103 190-2 E.6 ac4_dsi_v1
/// </summary>
public class ac4_dsi_v1
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
				throw new NotSupportedException("ac4_presentation_v0_dsi not yet supported");
			}
			else
			{
				if (presentation_version is 1 or 2)
				{
					//2 is an Extension to AC-4 DSI
					var start = reader.Position;
					presentations[i] = new ac4_presentation_v1_dsi(presentation_version, pres_bytes, reader);
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
