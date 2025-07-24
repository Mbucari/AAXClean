using Mpeg4Lib.Boxes.EC3SpecificBox;
using Mpeg4Lib.Util;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes;

/// <summary>
/// EC3SpecificBox. ETSI TS 102 366 F.6
/// </summary>
public class Dec3Box : Box
{
	public override long RenderSize => base.RenderSize + Ec3Data.Length;

	private readonly byte[] Ec3Data;
	/// <summary>
	/// ETSI TS 102 366 F.6.2.2 data_rate * 1024
	/// </summary>
	public uint AverageBitrate { get; }
	public int SampleRate { get; }
	public int NumberOfChannels { get; }
	public Ec3IndependentSubstream[] IndependentSubstream { get; }
	public bool IsAtmos => flag_ec3_extension_type_a.HasValue;
	/// <summary>
	/// Signaling Dolby Digital Plus bitstreams with Dolby Atmos content in an ISO base media format file
	/// Having a value indicates that audio is Dolby Atmos
	/// whether complexity_index_type_a is available in the E-AC-3 descriptor.
	/// </summary>
	public bool? flag_ec3_extension_type_a { get; }
	/// <summary>
	///  Dolby Digital Plus bitstream structure
	///  takes a value of 1 to 16 that indicates the decoding complexity of the Dolby Atmos bitstream
	/// </summary>
	public byte? complexity_index_type_a { get; }

	public Dec3Box(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		Ec3Data = file.ReadBlock((int)(header.TotalBoxSize - header.HeaderSize));
		var reader = new BitReader(Ec3Data);

		AverageBitrate = reader.Read(13) * 1024;
		var num_ind_sub = reader.Read(3);
		Debug.Assert(num_ind_sub == 0);

		IndependentSubstream = new Ec3IndependentSubstream[num_ind_sub + 1];
		for (int i = 0; i <= num_ind_sub; i++)
		{
			IndependentSubstream[i] = new Ec3IndependentSubstream(reader);
		}

		var indSample = IndependentSubstream.First();
		Debug.Assert(indSample.num_dep_sub == 0);

		SampleRate = indSample.GetSampleRate();
		NumberOfChannels = indSample.ChannelCount();

		if (reader.Length - reader.Position < 8)
			return;

		//Dolby Atmos content carried by a Dolby Digital Plus stream.
		reader.Position += 7;
		flag_ec3_extension_type_a = reader.ReadBool();

		if (flag_ec3_extension_type_a.Value)
		{
			complexity_index_type_a = (byte)reader.Read(8);
		}
	}
	protected override void Render(Stream file)
	{
		file.Write(Ec3Data);
	}
}
