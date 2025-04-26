using Mpeg4Lib.Util;
using System.Diagnostics;
using System.IO;

namespace Mpeg4Lib.Boxes;

/// <summary>
/// EC3SpecificBox. ETSI TS 102 366 F.6
/// </summary>
public class Dec3Box : Box
{
	public override long RenderSize => base.RenderSize + Ec3Data.Length;

	private readonly byte[] Ec3Data;
	public uint AverageBitrate { get; }
	public byte acmod { get; }
	public bool lfeon { get; }
	public int SampleRate { get; }
	public int NumberOfChannels => ff_ac3_channels_tab[acmod] + (lfeon ? 1 : 0);
	private static readonly byte[] ff_ac3_channels_tab = [2, 1, 2, 3, 3, 4, 4, 5];

	public Dec3Box(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		Ec3Data = file.ReadBlock((int)(header.TotalBoxSize - header.HeaderSize));
		var reader = new BitReader(Ec3Data);

		//data_rate
		AverageBitrate = reader.Read(13) * 1024;
		var num_ind_sub = reader.Read(3);
		Debug.Assert(num_ind_sub == 0);

		var fscod = (int)reader.Read(2);
		SampleRate
			= fscod == 0 ? 48000
			: fscod == 1 ? 44100
			: fscod == 2 ? 32000
			: throw new InvalidDataException($"{nameof(fscod)} value of {fscod} is not valid");

		var bsid = reader.Read(5);
		Debug.Assert(bsid == 16);
		reader.Position += 1;

		var asvc = reader.Read(1);
		var bsmod = reader.Read(3);
		acmod = (byte)reader.Read(3);
		lfeon = reader.Read(1) > 0;
		reader.Position += 3;
		var num_dep_sub = reader.Read(4);
		Debug.Assert(num_dep_sub == 0);
	}

	protected override void Render(Stream file)
	{
		file.Write(Ec3Data);
	}
}
