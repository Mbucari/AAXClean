namespace Mpeg4Lib.Boxes.EC3SpecificBox;

/// <summary>
/// ETSI TS 102 366 Table F.6.1: chan_loc field bit assignments
/// </summary>
public enum ChannelLocation : short
{
	Lc_Rc_Pair,
	Lrs_Rrs_Pair,
	Cs,
	Ts,
	Lsd_Rsd_Pair,
	Lw_Rw_Pair,
	Lvh_Rvh_Pair,
	Cvh,
	LFE2,
}
