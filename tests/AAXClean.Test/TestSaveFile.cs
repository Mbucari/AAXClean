using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mpeg4Lib.Util;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AAXClean.Test;

[TestClass]
public class TestSaveFile
{
	private Mp4File OpenFile(string filename)
	{
		var filePath = TestFiles.GetTestFilePath(filename);
		var tempFile = nameof(TestSaveFile) + ".m4a";
		File.Copy(filePath, tempFile, true);
		return new Mp4File(new FileStream(tempFile, FileMode.Open, FileAccess.ReadWrite));
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
		using (var mp4 = new Mp4File(originalFile))
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

			mp4.AppleTags.Comment += "1";
			await mp4.SaveAsync(keepMoovInFront);
		}
		using (var mp4 = new Mp4File(originalFile))
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

			mp4.AppleTags.Comment = mp4.AppleTags.Comment[..^4];
			await mp4.SaveAsync(keepMoovInFront);
		}
		using (var mp4 = new Mp4File(originalFile))
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
		using (var mp4 = new Mp4File(originalFile))
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

			mp4.AppleTags.Comment = mp4.AppleTags.Comment[..^4];
			await mp4.SaveAsync();
		}
		using (var mp4 = new Mp4File(originalFile))
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
		using (var mp4 = new Mp4File(originalFile))
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
			mp4.AppleTags.Comment += "1111";
			await mp4.SaveAsync();
		}
		using (var mp4 = new Mp4File(originalFile))
		{
			var newFtypHash = await HashFtyp(mp4);
			var newMdatHash = await HashMdat(mp4);
			var newMoovHash = await HashMoov(mp4);
			Assert.AreNotEqual(originalFtypHash, newFtypHash, "Ftyp hash should change after ftyp modification.");
			Assert.AreEqual(originalMdatHash, newMdatHash, "Mdat hash should not change after moov modification.");
			Assert.AreNotEqual(originalMoovHash, newMoovHash, "Moov hash should change after modification.");
		}
	}


	static async Task<string> HashFtyp(Mp4File mp4)
	{
		var mdat = await mp4.Ftyp.HashBoxAsync();
		return Convert.ToHexString(mdat);
	}

	static async Task<string> HashMdat(Mp4File mp4)
	{
		var mdat = await mp4.Mdat.HashBoxAsync(mp4.InputStream);
		return Convert.ToHexString(mdat);
	}

	static async Task<string> HashMoov(Mp4File mp4)
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
