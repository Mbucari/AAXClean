using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes.EC3SpecificBox;

/// <summary>
/// Part of the EC3SpecificBox. Represents an independent substream in E-AC-3.
/// ETSI TS 102 366 F.6
/// </summary>
public class Ec3IndependentSubstream
{
	/// <summary>
	/// ETSI TS 102 366 4.4.1.3 fscod - Sample rate code - 2 bits
	/// Table 4.1: Sample rate codes
	/// </summary>
	public byte fscod;
	/// <summary>
	/// ETSI TS 102 366 4.4.2.1 bsid - Bit stream identification 
	/// </summary>
	public byte bsid;
	/// <summary>
	/// ETSI TS 102 366 F.6.2.7 asvc - is a main audio service
	/// Value must be 16 for E-AC-3 (E.1.1 Indication of Enhanced AC-3 bit stream syntax)
	/// </summary>
	public bool asvc;
	/// <summary>
	/// ETSI TS 102 366 4.4.2.2 bsmod - Bit stream mode - 3 bits
	/// Table 4.2: Bit stream mode
	/// </summary>
	public byte bsmod;
	/// <summary>
	/// ETSI TS 102 366 4.4.2.3 acmod - Audio coding mode
	/// </summary>
	public AudioCodingMode acmod;
	/// <summary>
	/// ETSI TS 102 366 4.4.2.7 lfeon - Low frequency effects channel on
	/// </summary>
	public bool lfeon;
	/// <summary>
	/// ETSI TS 102 366 F.6.2.12 num_dep_sub
	/// </summary>
	public byte num_dep_sub;
	/// <summary>
	/// ETSI TS 102 366 F.6.2.13 chan_loc - channel locations
	/// </summary>
	public ChannelLocation chan_loc;

	public Ec3IndependentSubstream(BitReader reader)
	{
		fscod = (byte)reader.Read(2);
		bsid = (byte)reader.Read(5);
		if (bsid != 16)
			throw new InvalidDataException($"Invalid bsid value: {bsid}. Expected 16 for E-AC-3.");

		reader.Position += 1;

		asvc = reader.ReadBool();
		bsmod = (byte)reader.Read(3);
		acmod = (AudioCodingMode)reader.Read(3);
		lfeon = reader.Read(1) > 0;
		reader.Position += 3;
		var num_dep_sub = reader.Read(4);

		if (num_dep_sub > 0)
			chan_loc = (ChannelLocation)reader.Read(9);
		else
			reader.Position++;
	}
}
