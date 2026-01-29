using Mpeg4Lib.Util;

namespace Mpeg4Lib.Test;

[TestClass]
public class TestSaveFile
{
	private Mpeg4File OpenFile(string filename)
	{
		var baseDir = Path.GetDirectoryName(Environment.ProcessPath)
			?? throw new InvalidOperationException("Cannot determine base directory for test files.");

		var testFile = Path.Combine("TestFiles", filename);
		string fullPath;
		do
		{
			baseDir = Path.GetFullPath(Path.Combine(baseDir, ".."));
			fullPath = Path.Combine(baseDir, testFile);
		}
		while (!File.Exists(fullPath));

		var tempFile = nameof(TestSaveFile) + ".m4a";
		File.Copy(fullPath, tempFile, true);
		return new Mpeg4File(new FileStream(tempFile, FileMode.Open, FileAccess.ReadWrite));
	}

	const string SAMPLE_MOOV_IN_BACK = "sampleMoovInBack.m4a";
	const string SAMPLE_MOOV_IN_FRONT = "sampleMoovInFront.m4a";

	[TestMethod]
	[DataRow(true, SAMPLE_MOOV_IN_BACK)]
	[DataRow(true, SAMPLE_MOOV_IN_FRONT)]
	[DataRow(false, SAMPLE_MOOV_IN_BACK)]
	[DataRow(false, SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_FtypGrows(bool keepMoovInFront, string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.Ftyp.CompatibleBrands.Add("test");
			await mp4.SaveAsync(keepMoovInFront);
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreNotEqual(originalFtypHash, newFtypHash, "Ftyp hash should change after modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after Ftyp modification.");
			Assert.AreEqual(originalMoovHash, newMoovHash, "Moov hash should not change after Ftyp modification.");
		}
	}

	[TestMethod]
	[DataRow(true, SAMPLE_MOOV_IN_BACK)]
	[DataRow(true, SAMPLE_MOOV_IN_FRONT)]
	[DataRow(false, SAMPLE_MOOV_IN_BACK)]
	[DataRow(false, SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_MoovGrows(bool keepMoovInFront, string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.MetadataItems.Comment += "1";
			await mp4.SaveAsync(keepMoovInFront);
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreEqual(originalFtypHash, newFtypHash, "Ftyp hash should not change after moov modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after moov modification.");
			Assert.AreNotEqual(originalMoovHash, newMoovHash, "Moov hash should change after modification.");
		}
	}


	[TestMethod]
	[DataRow(true, SAMPLE_MOOV_IN_BACK)]
	[DataRow(true, SAMPLE_MOOV_IN_FRONT)]
	[DataRow(false, SAMPLE_MOOV_IN_BACK)]
	[DataRow(false, SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_MoovShrinks_4(bool keepMoovInFront, string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.MetadataItems.Comment = mp4.MetadataItems.Comment?[..^4];
			await mp4.SaveAsync(keepMoovInFront);
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreEqual(originalFtypHash, newFtypHash, "Ftyp hash should not change after moov modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after moov modification.");
			Assert.AreNotEqual(originalMoovHash, newMoovHash, "Moov hash should change after modification.");
		}
	}


	[TestMethod]
	[DataRow(true, SAMPLE_MOOV_IN_BACK)]
	[DataRow(true, SAMPLE_MOOV_IN_FRONT)]
	[DataRow(false, SAMPLE_MOOV_IN_BACK)]
	[DataRow(false, SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_FtypShrinks_4(bool keepMoovInFront, string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.Ftyp.CompatibleBrands.RemoveAt(0);
			await mp4.SaveAsync(keepMoovInFront);
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreNotEqual(originalFtypHash, newFtypHash, "Ftyp hash should change after modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after Ftyp modification.");
			Assert.AreEqual(originalMoovHash, newMoovHash, "Moov hash should not change after Ftyp modification.");
		}
	}

	[TestMethod]
	[DataRow(SAMPLE_MOOV_IN_BACK)]
	[DataRow(SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_MoovShrinks_8(string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.MetadataItems.Comment = mp4.MetadataItems.Comment?[..^4];
			await mp4.SaveAsync();
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreEqual(originalFtypHash, newFtypHash, "Ftyp hash should not change after moov modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after moov modification.");
			Assert.AreNotEqual(originalMoovHash, newMoovHash, "Moov hash should change after modification.");
		}
	}


	[TestMethod]
	[DataRow(SAMPLE_MOOV_IN_BACK)]
	[DataRow(SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_FtypShrinks_8(string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.Ftyp.CompatibleBrands.RemoveAt(0);
			mp4.Ftyp.CompatibleBrands.RemoveAt(0);
			await mp4.SaveAsync();
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreNotEqual(originalFtypHash, newFtypHash, "Ftyp hash should change after modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after Ftyp modification.");
			Assert.AreEqual(originalMoovHash, newMoovHash, "Moov hash should not change after Ftyp modification.");
		}
	}

	[TestMethod]
	[DataRow(SAMPLE_MOOV_IN_BACK)]
	[DataRow(SAMPLE_MOOV_IN_FRONT)]
	public async Task MoovInFront_FtypShrinks_MoovGrows(string moovFile)
	{
		string originalFtypHash, originalMdatHash, originalMoovHash;
		string originalFile;
		using (var mp4 = OpenFile(moovFile))
		{
			originalFile = ((FileStream)mp4.InputStream).Name;
			originalFtypHash = await HashFtyp(mp4);
			originalMdatHash = await HashMdat(mp4);
			originalMoovHash = await HashMoov(mp4);

			mp4.Ftyp.CompatibleBrands.RemoveAt(mp4.Ftyp.CompatibleBrands.Count - 1);
			mp4.MetadataItems.Comment += "1111";
			await mp4.SaveAsync();
		}
		using (var mp4 = new Mpeg4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreNotEqual(originalFtypHash, newFtypHash, "Ftyp hash should change after ftyp modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after moov modification.");
			Assert.AreNotEqual(originalMoovHash, newMoovHash, "Moov hash should change after modification.");
		}
	}

	static async Task<string> HashFtyp(Mpeg4File mp4)
	{
		var mdat = await mp4.Ftyp.HashBoxAsync();
		return Convert.ToHexString(mdat);
	}

	static async Task<string> HashMdat(Mpeg4File mp4)
	{
		var mdat = await mp4.Mdat.HashBoxAsync(mp4.InputStream);
		return Convert.ToHexString(mdat);
	}

	static async Task<string> HashMoov(Mpeg4File mp4)
	{
		//Chunk offsets change, so remove cobox for hashing
		var cobox = mp4.Moov.AudioTrack.Mdia.Minf.Stbl.COBox;
		var coIndex = mp4.Moov.AudioTrack.Mdia.Minf.Stbl.Children.IndexOf(cobox);
		mp4.Moov.AudioTrack.Mdia.Minf.Stbl.Children.RemoveAt(coIndex);
		var moov = await mp4.Moov.HashBoxAsync();
		mp4.Moov.AudioTrack.Mdia.Minf.Stbl.Children.Insert(coIndex, cobox);
		return Convert.ToHexString(moov);
	}
}
