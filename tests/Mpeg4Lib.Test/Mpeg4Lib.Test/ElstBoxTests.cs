using Mpeg4Lib.Boxes;

namespace Mpeg4Lib.Test;

[TestClass]
public class ElstBoxTests
{
	private static byte[] MakeBox(string type, params byte[][] payloads)
	{
		int payloadSize = payloads.Sum(p => p.Length);
		using var ms = new MemoryStream();
		WriteUInt32BE(ms, (uint)(8 + payloadSize));
		ms.Write(System.Text.Encoding.ASCII.GetBytes(type));
		foreach (var p in payloads)
			ms.Write(p);
		return ms.ToArray();
	}

	private static byte[] UInt32sBE(params uint[] values)
	{
		using var ms = new MemoryStream();
		foreach (var v in values)
			WriteUInt32BE(ms, v);
		return ms.ToArray();
	}

	private static void WriteUInt32BE(Stream s, uint value)
	{
		Span<byte> b = [(byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value];
		s.Write(b);
	}

	[TestMethod]
	public void Parse_Version0_RoundTripsByteForByte()
	{
		//version 0, one entry: segment_duration=1000, media_time=37, rate 1.0
		byte[] source = MakeBox("elst",
			UInt32sBE(0, 1),
			UInt32sBE(1000, 37),
			[0x00, 0x01, 0x00, 0x00]);

		var elst = BoxFactory.CreateBox<ElstBox>(new MemoryStream(source), parent: null);

		Assert.HasCount(1, elst.Entries);
		Assert.AreEqual(1000ul, elst.Entries[0].SegmentDuration);
		Assert.AreEqual(37L, elst.Entries[0].MediaTime);
		Assert.AreEqual((short)1, elst.Entries[0].MediaRateInteger);
		Assert.AreEqual(source.Length, elst.RenderSize);

		using var rendered = new MemoryStream();
		elst.Save(rendered);
		CollectionAssert.AreEqual(source, rendered.ToArray());
	}

	[TestMethod]
	public void CreateBlank_LargeValues_UseVersion1AndRoundTrip()
	{
		var trakBytes = MakeBox("trak", MakeBox("tkhd",
			UInt32sBE(0, 0, 0),      //version+flags, creation, modification
			UInt32sBE(1, 0),         //track ID, reserved
			UInt32sBE(0),            //duration
			UInt32sBE(0, 0),         //reserved2
			UInt32sBE(0, 0),         //layer+altGroup, volume+reserved3
			new byte[4 * 9],         //matrix
			UInt32sBE(0, 0)));       //width, height
		var trak = BoxFactory.CreateBox<TrakBox>(new MemoryStream(trakBytes), parent: null);

		var edts = EdtsBox.CreateBlank(trak);
		var elst = ElstBox.CreateBlank(edts);
		ulong bigDuration = 7_400_000_000; //46.9 h at 44.1 kHz: exceeds uint.MaxValue
		elst.Entries.Add(new ElstBox.EditEntry(bigDuration, 12345));
		elst.UpdateVersion();

		Assert.AreEqual(1, elst.Version);
		Assert.AreSame(edts, trak.Edts);
		Assert.AreSame(elst, trak.Edts!.Elst);

		using var rendered = new MemoryStream();
		trak.Save(rendered);
		rendered.Position = 0;

		var reparsed = BoxFactory.CreateBox<TrakBox>(rendered, parent: null);
		var entry = reparsed.Edts!.Elst!.Entries.Single();
		Assert.AreEqual(bigDuration, entry.SegmentDuration);
		Assert.AreEqual(12345L, entry.MediaTime);
	}

	private static ElstBox CreateElst(params ElstBox.EditEntry[] entries)
	{
		var elst = BoxFactory.CreateBox<ElstBox>(new MemoryStream(MakeBox("elst", UInt32sBE(0, 0))), parent: null);
		foreach (var e in entries)
			elst.Entries.Add(e);
		return elst;
	}

	[TestMethod]
	public void SingleEdit_ReturnsEntry_ForSingleNonEmptyRate1Edit()
	{
		var elst = CreateElst(new ElstBox.EditEntry(SegmentDuration: 1000, MediaTime: 448));
		Assert.IsNotNull(elst.SingleEdit);
		Assert.AreEqual(448L, elst.SingleEdit.Value.MediaTime);
		Assert.AreEqual(1000ul, elst.SingleEdit.Value.SegmentDuration);
	}

	[TestMethod]
	public void SingleEdit_IsNull_ForEmptyEditOrMultipleEntriesOrRate()
	{
		Assert.IsNull(CreateElst().SingleEdit);                                //no entries
		Assert.IsNull(CreateElst(new ElstBox.EditEntry(1000, -1)).SingleEdit); //empty edit
		Assert.IsNull(CreateElst(new ElstBox.EditEntry(1000, 0, MediaRateInteger: 0)).SingleEdit);
		Assert.IsNull(CreateElst(new ElstBox.EditEntry(1000, 0, MediaRateFraction: 1)).SingleEdit);
		Assert.IsNull(CreateElst(
			new ElstBox.EditEntry(1000, 0), new ElstBox.EditEntry(1000, 5000)).SingleEdit); //two entries
	}

	[TestMethod]
	public void UpdateVersion_SmallValues_StaysVersion0()
	{
		byte[] source = MakeBox("elst", UInt32sBE(0, 0));
		var elst = BoxFactory.CreateBox<ElstBox>(new MemoryStream(source), parent: null);
		elst.Entries.Add(new ElstBox.EditEntry(uint.MaxValue, int.MaxValue));
		elst.UpdateVersion();
		Assert.AreEqual(0, elst.Version);
	}
}
