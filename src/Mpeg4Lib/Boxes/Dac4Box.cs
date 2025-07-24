using Mpeg4Lib.Boxes.AC4SpecificBox;
using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes;

/// <summary>
/// AC4SpecificBox. ETSI TS 103 190-2 E.5
/// </summary>
public class Dac4Box : Box
{
	public override long RenderSize => base.RenderSize + Ac4Data.Length;
	private readonly byte[] Ac4Data;

	public ac4_dsi_v1? Ac4DsiV1;
	public uint? AverageBitrate { get; }
	public int? SampleRate { get; }
	public int? NumberOfChannels { get; }

	public Dac4Box(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		Ac4Data = file.ReadBlock((int)(header.TotalBoxSize - header.HeaderSize));
		try
		{
			var reader = new BitReader(Ac4Data);
			Ac4DsiV1 = new ac4_dsi_v1(reader);
		}
		catch
		{
			return;
		}

		SampleRate = Ac4DsiV1.SampleRate();
		AverageBitrate = Ac4DsiV1.AverageBitrate();
		NumberOfChannels = Ac4DsiV1.Channels()?.ChannelCount();
	}

	protected override void Render(Stream file)
	{
		file.Write(Ac4Data);
	}
}