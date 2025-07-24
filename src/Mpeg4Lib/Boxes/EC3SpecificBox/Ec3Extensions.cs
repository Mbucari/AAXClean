using System.IO;

namespace Mpeg4Lib.Boxes.EC3SpecificBox;

public static class Ec3Extensions
{
	public static int GetSampleRate(this Ec3IndependentSubstream ind_sub)
		=> ind_sub.fscod == 0 ? 48000
		: ind_sub.fscod == 1 ? 44100
		: ind_sub.fscod == 2 ? 32000
		: throw new InvalidDataException($"{nameof(ind_sub.fscod)} value of {ind_sub.fscod} is not valid");

	public static int ChannelCount(this Ec3IndependentSubstream ind_sub)
		 => ff_ac3_channels_tab[(byte)ind_sub.acmod] + (ind_sub.lfeon ? 1 : 0);

	/// <summary>
	/// ETSI TS 102 366 4.4.2.3 Table 4.3: Audio coding mode column 4 (Nfchans)
	/// </summary>
	private static readonly byte[] ff_ac3_channels_tab = [2, 1, 2, 3, 3, 4, 4, 5];
}
