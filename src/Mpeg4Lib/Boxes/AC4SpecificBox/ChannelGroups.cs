using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

/// <summary>
/// ETSI TS 103 190-2 E.10.14 presentation_channel_mask_v1
/// Values from A.3 Table A.27: Speaker layouts and speaker indices, column 12 (speaker group indices)
/// </summary>
[Flags]
public enum ChannelGroups
{
	Left_Right = 1 << 0,
	Center = 1 << 1,
	LeftSurround_RightSurround = 1 << 2,
	LeftBack_RightBack = 1 << 3,
	TopFrontLeft_TopFrontRight = 1 << 4,
	TopBackLeft_TopBackRight = 1 << 5,
	LFE = 1 << 6,
	TopLeft_TopRight = 1 << 7,
	TopSideLeft_TopSideRight = 1 << 8,
	TopFrontCentre = 1 << 9,
	Tfc = 1 << 10,
	TopCenter = 1 << 11,
	LFE2 = 1 << 12,
	BottomFrontLeft_BottomFrontRight = 1 << 13,
	BottomFrontCentre = 1 << 14,
	BackCenter = 1 << 15,
	LeftScreen_RightScreen = 1 << 16,
	LeftWide_RightWide = 1 << 17,
	VerticalHeightLeft_VerticalHeightRight = 1 << 18,
	NOT_CHANNEL_CODED = 1 << 23,
}
