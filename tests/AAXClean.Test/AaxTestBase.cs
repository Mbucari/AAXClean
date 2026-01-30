using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

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
		public abstract int TimeScale { get; }
		public abstract int AverageBitrate { get; }
		public abstract int MaxBitrate { get; }
		public abstract long RenderSize { get; }
		public abstract TimeSpan Duration { get; }
		public abstract ChapterInfo Chapters { get; }
		public abstract TestBookTags Tags { get; }

		[TestMethod]
		public void _0_OpenAAX()
		{
			try
			{
				Assert.AreEqual(AudioChannels, Aax.AudioChannels);
				Assert.AreEqual(TimeScale, Aax.TimeScale);
				Assert.AreEqual(AverageBitrate, Aax.AverageBitrate);
				Assert.AreEqual(MaxBitrate, Aax.MaxBitrate);
				Assert.AreEqual(Duration, Aax.Duration);
			}
			finally
			{
				Aax.InputStream.Close();
			}
		}

		[TestMethod]
		public void _1_ReadTags()
		{
			try
			{
				Assert.AreEqual(Tags.Album, Aax.MetadataItems.Album);
				Assert.AreEqual(Tags.AlbumArtists, Aax.MetadataItems.AlbumArtists);
				Assert.AreEqual(Tags.Asin, Aax.MetadataItems.Asin);
				Assert.AreEqual(Tags.Comment, Aax.MetadataItems.Comment);
				Assert.AreEqual(Tags.Copyright, Aax.MetadataItems.Copyright);
				Assert.AreEqual(Tags.Generes, Aax.MetadataItems.Genres);
				Assert.AreEqual(Tags.LongDescription, Aax.MetadataItems.LongDescription);
				Assert.AreEqual(Tags.Narrator, Aax.MetadataItems.Narrator);
				Assert.AreEqual(Tags.Performers, Aax.MetadataItems.Artist);
				Assert.AreEqual(Tags.ProductID, Aax.MetadataItems.ProductID);
				Assert.AreEqual(Tags.Publisher, Aax.MetadataItems.Publisher);
				Assert.AreEqual(Tags.ReleaseDate, Aax.MetadataItems.ReleaseDate);
				Assert.AreEqual(Tags.Title, Aax.MetadataItems.Title);
				Assert.AreEqual(Tags.Year, Aax.MetadataItems.Year);

				using SHA1 sha = SHA1.Create();
				sha.ComputeHash(Aax.MetadataItems.Cover);
				string hash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
				Assert.AreEqual(Tags.CoverHash, hash);
			}
			finally
			{
				Aax.InputStream.Close();
			}
		}

		[TestMethod]
		public async Task _2_ReadChapters()
		{
			try
			{
				Assert.IsNull(Aax.Chapters);
				ChapterInfo chapters = await Aax.GetChapterInfoAsync();

#if DEBUG
				var chs = new System.Text.StringBuilder();

				foreach (var ch in chapters.Chapters)
				{
					chs.AppendLine($"_chapters.AddChapter(\"{ch.Title}\", TimeSpan.FromTicks({ch.Duration.Ticks}));");
				}
#endif
				//These 2 files have incorrect timed text entries, which is why they're in the test suite.
				//Correct chapter info can be found through GetChaptersFromMetadata()
				if (this is AC_AaxTest or BA_AaxTest)
					return;
				VerifyChapters(chapters);
			}
			finally
			{
				Aax.InputStream.Close();
			}
		}

		[TestMethod]
		public void _3_ReadChaptersFromMetadata()
		{
			try
			{
				Assert.IsNull(Aax.Chapters);
				ChapterInfo chapters = Aax.GetChaptersFromMetadata();
#if DEBUG
				var chs = new System.Text.StringBuilder();

				foreach (var ch in chapters.Chapters)
				{
					chs.AppendLine($"{{ \"{ch.Title}\", TimeSpan.FromTicks({ch.Duration.Ticks}) }},");
				}
#endif
				VerifyChapters(chapters);
			}
			finally
			{
				Aax.InputStream.Close();
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
		public async Task _6_CustomChapters()
		{
			try
			{
				FileStream tempfile = TestFiles.NewTempFile();
				ChapterInfo newChapters = new();
				newChapters.AddChapter("Ch1", TimeSpan.FromTicks(15000000000));
				newChapters.AddChapter("Ch2", TimeSpan.FromTicks(30000000000));
				newChapters.AddChapter("Ch3", TimeSpan.FromTicks(45000000000));
				newChapters.AddChapter("Ch4", TimeSpan.FromTicks(15000000000));
				newChapters.AddChapter("Ch5", TimeSpan.FromTicks(30000000000));
				newChapters.AddChapter("Ch6", TimeSpan.FromTicks(45000000000));
				await Aax.ConvertToMp4aAsync(tempfile, newChapters);

				Mp4File mp4 = new(tempfile.Name);
				var ch_2 = mp4.GetChaptersFromMetadata().ToList();
				var ch_3 = (await mp4.GetChapterInfoAsync()).ToList();
				mp4.InputStream.Close();

				var ch_1 = newChapters.ToList();
				Assert.AreEqual(ch_1.Count, ch_2.Count, 0, "Compare to GetChaptersFromMetadata()");
				Assert.AreEqual(ch_1.Count, ch_3.Count, 0, "Compare to GetChapterInfo()");

				for (int i = 0; i < ch_1.Count; i++)
				{
					Assert.AreEqual(ch_1[i].Duration.Ticks, ch_2[i].Duration.Ticks, 10, "Compare to GetChaptersFromMetadata()");
					Assert.AreEqual(ch_1[i].StartOffset.Ticks, ch_2[i].StartOffset.Ticks, 10, "Compare to GetChaptersFromMetadata()");
					Assert.AreEqual(ch_1[i].EndOffset.Ticks, ch_2[i].EndOffset.Ticks, 10, "Compare to GetChaptersFromMetadata()");
					Assert.AreEqual(ch_1[i].Title, ch_2[i].Title, "Compare to GetChaptersFromMetadata()");
				}
				for (int i = 0; i < ch_1.Count; i++)
				{
					Assert.AreEqual(ch_1[i].Duration.Ticks, ch_3[i].Duration.Ticks, 10, "Compare to GetChapterInfo()");
					Assert.AreEqual(ch_1[i].StartOffset.Ticks, ch_3[i].StartOffset.Ticks, 10, "Compare to GetChapterInfo()");
					Assert.AreEqual(ch_1[i].EndOffset.Ticks, ch_3[i].EndOffset.Ticks, 10, "Compare to GetChapterInfo()");
					Assert.AreEqual(ch_1[i].Title, ch_3[i].Title, "Compare to GetChapterInfo()");
				}
			}
			finally
			{
				TestFiles.CloseAllFiles();
			}
		}

		[TestMethod]
		public async Task _7_TestCancelSingle()
		{
			var aaxFile = Aax;
			try
			{
				FileStream tempfile = TestFiles.NewTempFile();

				var convertTask = aaxFile.ConvertToMp4aAsync(tempfile);
				convertTask.Start();

				await Task.Delay(500);
				await convertTask.CancelAsync();

				await convertTask;
				Assert.IsTrue(convertTask.IsCanceled);

				TestFiles.CloseAllFiles();
				Aax.InputStream.Close();
			}
			finally
			{
				TestFiles.CloseAllFiles();
				aaxFile.InputStream.Close();
			}
		}

		[TestMethod]
		public async Task _8_TestCancelMulti()
		{
			var aaxFile = Aax;
			try
			{
				FileStream tempfile = TestFiles.NewTempFile();

				void NewSplit(NewSplitCallback callback)
				{
					callback.OutputFile = TestFiles.NewTempFile();
				}

				var convertTask = aaxFile.ConvertToMultiMp4aAsync(Chapters, NewSplit);
				convertTask.Start();
				await Task.Delay(500);
				await convertTask.CancelAsync();

				await convertTask;
				Assert.IsTrue(convertTask.IsCanceled);

				TestFiles.CloseAllFiles();
				Aax.InputStream.Close();
			}
			finally
			{
				TestFiles.CloseAllFiles();
				aaxFile.InputStream.Close();
			}
		}
	}
}
