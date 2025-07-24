using System.Linq;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

public static class Ac4Extensions
{
	public static int? SampleRate(this ac4_dsi_v1? ac4_Dsi_V1) =>
		ac4_Dsi_V1 is null ? null
		: ac4_Dsi_V1.fs_index == 0 ? 44100
		: 48000;

	public static uint? AverageBitrate(this ac4_dsi_v1? ac4_Dsi_V1)
	{
		if (ac4_Dsi_V1 is null)
			return null;
		if (ac4_Dsi_V1.ac4_Bitrate_Dsi.bit_rate != 0)
			return ac4_Dsi_V1.ac4_Bitrate_Dsi.bit_rate;

		foreach (var presentation in ac4_Dsi_V1.presentations.OfType<ac4_presentation_v1_dsi>().Where(p => p.b_presentation_bitrate_info))
		{
			if (presentation.ac4_Bitrate_Dsi is ac4_bitrate_dsi btrt && btrt.bit_rate != 0)
			return btrt.bit_rate;
		}
		return null;
	}

	public static ChannelGroups? Channels(this ac4_dsi_v1? ac4_Dsi_V1)
	{
		if (ac4_Dsi_V1 is null)
			return null;
		foreach (var presentation in ac4_Dsi_V1
				.presentations
				.OfType<ac4_presentation_v1_dsi>()
				.OrderByDescending(p => p.presentation_version))
		{
			if (presentation.b_presentation_channel_coded is true)
			{
				return presentation.presentation_channel_mask_v1;
			}
			else if (presentation.substream?.b_channel_coded is true)
			{
				return presentation.substream.substreams[0].dsi_substream_channel_mask;
			}
		}
		return null;
	}

	public static int ChannelCount(this ChannelGroups channels)
	{
		int channelCount = 0;
		for (int g = 0; g <= 18; g++)
		{
			var group = (ChannelGroups)(1 << g);
			if (channels.HasFlag(group))
				channelCount += num_channels_per_group[g];
		}
		return channelCount;
	}

	static readonly byte[] num_channels_per_group = [2, 1, 2, 2, 2, 2, 1, 2, 2, 1, 1, 1, 1, 2, 1, 1, 2, 2, 2];
}
