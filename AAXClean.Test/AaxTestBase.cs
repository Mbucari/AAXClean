using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AAXClean.Test
{
	public abstract class AaxTestBase
	{
		private AaxFile _aax;
		private AaxFile _aaxNoFix;
		public AaxFile Aax
		{
			get
			{
				if (_aax is null)
				{
					_aax = new AaxFile(File.Open(AaxFile, FileMode.Open, FileAccess.Read, FileShare.Read));
					_aax.SetDecryptionKey(new byte[16], new byte[16]);
				}
				return _aax;
			}
		}
		public AaxFile AaxNoFixup
		{
			get
			{
				if (_aaxNoFix is null)
				{
					var fs = File.Open(AaxFile, FileMode.Open, FileAccess.Read, FileShare.Read);
					_aaxNoFix = new AaxFile(fs, fs.Length, false);
					_aaxNoFix.SetDecryptionKey(new byte[16], new byte[16]);
				}
				return _aaxNoFix;
			}
		}
		public abstract string AaxFile { get; }
		public abstract int AudioChannels { get; }
		public abstract int AudioSampleSize { get; }
		public abstract uint TimeScale { get; }
		public abstract uint AverageBitrate { get; }
		public abstract uint MaxBitrate { get; }
		public abstract long RenderSize { get; }
		public abstract TimeSpan Duration { get; }
		public abstract ChapterInfo Chapters { get; }
		public abstract TestBookTags Tags { get; }
		public abstract string SingleM4bHash { get; }
		public abstract string PassthroughM4bHash { get; }
		public abstract List<string> MultiM4bHashes { get; }

		[TestMethod]
		public void _0_OpenAAX()
		{
			try
			{
				Assert.AreEqual(AudioChannels, Aax.AudioChannels);
				Assert.AreEqual(AudioSampleSize, Aax.AudioSampleSize);
				Assert.AreEqual(TimeScale, Aax.TimeScale);
				Assert.AreEqual(AverageBitrate, Aax.AverageBitrate);
				Assert.AreEqual(MaxBitrate, Aax.MaxBitrate);
				Assert.AreEqual(RenderSize, Aax.RenderSize);
				Assert.AreEqual(Duration, Aax.Duration);
			}
			finally
			{
				Aax.Close();
			}
		}

		[TestMethod]
		public void _1_ReadTags()
		{
			try
			{
				Assert.AreEqual(Tags.Album, Aax.AppleTags.Album);
				Assert.AreEqual(Tags.AlbumArtists, Aax.AppleTags.AlbumArtists);
				Assert.AreEqual(Tags.Asin, Aax.AppleTags.Asin);
				Assert.AreEqual(Tags.Comment, Aax.AppleTags.Comment);
				Assert.AreEqual(Tags.Copyright, Aax.AppleTags.Copyright);
				Assert.AreEqual(Tags.Generes, Aax.AppleTags.Generes);
				Assert.AreEqual(Tags.LongDescription, Aax.AppleTags.LongDescription);
				Assert.AreEqual(Tags.Narrator, Aax.AppleTags.Narrator);
				Assert.AreEqual(Tags.Performers, Aax.AppleTags.Performers);
				Assert.AreEqual(Tags.ProductID, Aax.AppleTags.ProductID);
				Assert.AreEqual(Tags.Publisher, Aax.AppleTags.Publisher);
				Assert.AreEqual(Tags.ReleaseDate, Aax.AppleTags.ReleaseDate);
				Assert.AreEqual(Tags.Title, Aax.AppleTags.Title);
				Assert.AreEqual(Tags.Year, Aax.AppleTags.Year);

				using SHA1 sha = SHA1.Create();
				sha.ComputeHash(Aax.AppleTags.Cover);
				string hash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
				Assert.AreEqual(Tags.CoverHash, hash);
			}
			finally
			{
				Aax.Close();
			}
		}

		[TestMethod]
		public void _2_ReadChapters()
		{
			try
			{
				Assert.IsNull(Aax.Chapters);
				ChapterInfo chapters = Aax.GetChapterInfo();

#if DEBUG
				var chs = new System.Text.StringBuilder();

				foreach (var ch in chapters.Chapters)
				{
					chs.AppendLine($"_chapters.AddChapter(\"{ch.Title}\", TimeSpan.FromTicks({ch.Duration.Ticks}));");
				}
#endif
				VerifyChapters(chapters);
			}
			finally
			{
				Aax.Close();
			}
		}

		[TestMethod]
		public void _3_ReadChaptersFromMetadata()
		{
			try
			{
				Assert.IsNull(Aax.Chapters);
				ChapterInfo chapters = Aax.GetChaptersFromMetadata();
				VerifyChapters(chapters);
			}
			finally
			{
				Aax.Close();
			}
		}

		private void VerifyChapters(ChapterInfo chapters)
		{
			Assert.IsNotNull(Aax.Chapters);
			Assert.AreEqual(Chapters.Count, chapters.Count);
			List<Chapter> ch_1 = Chapters.Chapters.ToList();
			List<Chapter> ch_2 = chapters.Chapters.ToList();

			for (int i = 0; i < chapters.Count; i++)
			{
				Assert.AreEqual(ch_1[i].Duration, ch_2[i].Duration);
				Assert.AreEqual(ch_1[i].StartOffset, ch_2[i].StartOffset);
				Assert.AreEqual(ch_1[i].EndOffset, ch_2[i].EndOffset);
				Assert.AreEqual(ch_1[i].Title, ch_2[i].Title);
			}
		}

		[TestMethod]
		public void _4_ConvertSingle()
		{
			try
			{
				FileStream tempfile = TestFiles.NewTempFile();
				ConversionResult result = Aax.ConvertToMp4a(tempfile);

				Assert.AreEqual(ConversionResult.NoErrorsDetected, result);

				using SHA1 sha = SHA1.Create();

				FileStream mp4file = File.OpenRead(tempfile.Name);
				int read;
				byte[] buff = new byte[4 * 1024 * 1024];

				while ((read = mp4file.Read(buff)) == buff.Length)
				{
					sha.TransformBlock(buff, 0, read, null, 0);
				}
				mp4file.Close();
				sha.TransformFinalBlock(buff, 0, read);
				string fileHash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));

				Assert.AreEqual(SingleM4bHash, fileHash);
			}
			finally
			{
				TestFiles.CloseAllFiles();
				Aax.Close();
			}
		}

		[TestMethod]
		public void _5_ConvertMultiple()
		{
			try
			{
				List<string> tempFiles = new();
				void NewSplit(NewSplitCallback callback)
				{
					callback.OutputFile = TestFiles.NewTempFile();
					tempFiles.Add(((FileStream)callback.OutputFile).Name);
				}

				Aax.ConvertToMultiMp4a(Chapters, NewSplit);
#if !DEBUG
				Assert.AreEqual(MultiM4bHashes.Count, tempFiles.Count);
#endif
				using SHA1 sha = SHA1.Create();
				List<string> hashes = new();

				foreach (string tmp in tempFiles)
				{
					sha.ComputeHash(File.ReadAllBytes(tmp));
					hashes.Add(string.Join("", sha.Hash.Select(b => b.ToString("x2"))));
				}
#if DEBUG
				var hs = new System.Text.StringBuilder();

				foreach (var h in hashes)
				{
					hs.AppendLine($"\"{h}\",");
				}
#endif

				for (int i = 0; i < tempFiles.Count; i++)
				{
					Assert.AreEqual(MultiM4bHashes[i], hashes[i]);
				}
			}
			finally
			{
				TestFiles.CloseAllFiles();
				Aax.Close();
			}
		}
		[TestMethod]
		public void _6_ConvertPassthrough()
		{
			try
			{
				FileStream tempfile = TestFiles.NewTempFile();

				//Use Sha1Managed if you really want sha1
				using var shaForStream = SHA1.Create();
				using Stream sourceStream = new CryptoStream(tempfile, shaForStream, CryptoStreamMode.Write);

				ConversionResult result = AaxNoFixup.ConvertPassThrough(sourceStream);

				Assert.AreEqual(ConversionResult.NoErrorsDetected, result);

				string fileHash1 = string.Join("", shaForStream.Hash.Select(b => b.ToString("x2")));

				Assert.AreEqual(AaxNoFixup.InputStream.Length, new FileInfo(tempfile.Name).Length);
				Assert.AreEqual(PassthroughM4bHash, fileHash1);
			}
			finally
			{
				TestFiles.CloseAllFiles();
				Aax.Close();
			}
		}

	}
}
