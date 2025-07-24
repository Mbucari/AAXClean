namespace Mpeg4Lib.Boxes.EC3SpecificBox;

/// <summary>
/// ETSI TS 102 366 4.4.2.3 Table 4.3: Audio coding mode
/// </summary>
public enum AudioCodingMode : byte
{
	Ch1_Ch2,
	C,
	L_R,
	L_C_R,
	L_R_S,
	L_C_R_S,
	L_R_Ls_Rs,
	L_C_R_Ls_Rs,
}