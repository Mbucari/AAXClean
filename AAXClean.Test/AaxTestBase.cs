using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AAXClean.Test
{
    public abstract class AaxTestBase
    {

        private AaxFile _aax; 
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

                using var sha = SHA1.Create();
                sha.ComputeHash(Aax.AppleTags.Cover);
                var hash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
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
                var chapters = Aax.GetChapterInfo();
#if DEBUG
                var chs = new StringBuilder();

                foreach (var ch in chapters.Chapters)
                {
                    chs.AppendLine($"_chapters.AddChapter(\"{ch.Title}\", TimeSpan.FromTicks({ch.Duration.Ticks}));");
                }
#endif
                Assert.IsNotNull(Aax.Chapters);
                Assert.AreEqual(chapters.Count, Chapters.Count);
                var ch_1 = chapters.Chapters.ToList();
                var ch_2 = Chapters.Chapters.ToList();

                for (int i = 0; i < chapters.Count; i++)
                {
                    Assert.AreEqual(ch_1[i].Duration, ch_2[i].Duration);
                    Assert.AreEqual(ch_1[i].Title, ch_2[i].Title);
                }
            }
            finally
            {
                Aax.Close();
            }
        }

        [TestMethod]
        public void _3_ConvertSingle()
        {
            try
            {
                var tempfile = TestFiles.NewTempFile();
                var result = Aax.ConvertToMp4a(tempfile);

                Assert.AreEqual(result, ConversionResult.NoErrorsDetected);

                using var sha = SHA1.Create();

                var mp4file = File.OpenRead(tempfile.Name);
                int read;
                byte[] buff = new byte[4 * 1024 * 1024];

                while ((read = mp4file.Read(buff)) == buff.Length)
                {
                    sha.TransformBlock(buff, 0, read, null, 0);
                }
                mp4file.Close();
                sha.TransformFinalBlock(buff, 0, read);
                var fileHash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));

                Assert.AreEqual(SingleM4bHash, fileHash);
            }
            finally
            {
                TestFiles.CloseAllFiles();
                Aax.Close();
            }
        }

        [TestMethod]
        public void _4_ConvertMultiple()
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
                using var sha = SHA1.Create();
                List<string> hashes = new();

                foreach (var tmp in tempFiles)
                {
                    sha.ComputeHash(File.ReadAllBytes(tmp));
                    hashes.Add(string.Join("", sha.Hash.Select(b => b.ToString("x2"))));
                }
#if DEBUG
                var hs = new StringBuilder();

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
    }
}
