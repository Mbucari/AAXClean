using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace AAXClean.Test
{
    [TestClass]
    public class HP_AaxTest : AaxTestBase
    {
        private ChapterInfo _chapters;
        private TestBookTags _tags;

        public override string AaxFile => TestFiles.HP_BookPath;
        public override int AudioChannels => 2;

        public override int AudioSampleSize => 16;

        public override uint TimeScale => 22050;

        public override uint AverageBitrate => 62794u;

        public override uint MaxBitrate => 64004u;

        public override long RenderSize => 2876635L;

        public override TimeSpan Duration => TimeSpan.FromTicks(299221159183);

        public override string SingleM4bHash => "5645220c03cceb40315bb089e35eda0ba68548dd";
        public override List<string> MultiM4bHashes => new()
        {
            "c902a186cba4f48489e4a7b86a92c1f30117ee0d",
            "3aed773601a3616e26fa3d5b9b335244dda3b064",
            "a6996dfa0a82e15f41c1294a4df9b6264c71fd3f",
            "57f2715991555ec915b18dcc8fd64c99f00eddde",
            "cb593a52d9f0f9dea2964deec6bea160d9e2a877",
            "04295c7021592ab09bed73800f3474d74e77a672",
            "fb85d39dbbb69a92e565c897af6d54f7e48dac2b",
            "b5317fa568d2194be97230b23b97a69cd997d236",
            "6e42daae059355abeef5219c70b852abf16f6f78",
            "ac9d8eba914d02b19050ec12fd9d7c5d82dd9468",
            "8bd4408329a73a0f374c34f03abca65b32856f71",
            "4c59ba2cfa3c49516ece3c8efcf620d12b3b4ffb",
            "378fc09c2f98565b6545ceb15c3492d8179acfb7",
            "783f78f2431983b8ec22b20c04233d0c9444ed71",
            "be78f33011b96d283a9773940e4b78ec37472bd6",
            "c660207b791ddec7455af543e9cabb9730499206",
            "530ca392378822398a72d42dc6567619e9aad78e",
            "d238452f3d0d669c64e094be5a1a6d1dd93eb884",
            "0e70481506b6b1b5299900a2854bf3c179b01f11",
            "01cbc54db5c5ffe2e1f2fcd590de73c168232d78",
            "85244dcf7e66d61220c42759807de652d8061a47"
        };

        public override ChapterInfo Chapters
        {
            get
            {
                if (_chapters is null)
                {
                    _chapters = new ChapterInfo();
                    _chapters.AddChapter("Opening Credits", TimeSpan.FromTicks(236039909));
                    _chapters.AddChapter("Chapter 1: The Boy Who Lived", TimeSpan.FromTicks(17323809977));
                    _chapters.AddChapter("Chapter 2: The Vanishing Glass", TimeSpan.FromTicks(13085829931));
                    _chapters.AddChapter("Chapter 3: The Letters from No One", TimeSpan.FromTicks(14571909750));
                    _chapters.AddChapter("Chapter 4: The Keeper of the Keys", TimeSpan.FromTicks(14658750113));
                    _chapters.AddChapter("Chapter 5: Diagon Alley", TimeSpan.FromTicks(26371829931));
                    _chapters.AddChapter("Chapter 6: The Journey from Platform Nine and Three-Quarters", TimeSpan.FromTicks(22966390022));
                    _chapters.AddChapter("Chapter 7: The Sorting Hat", TimeSpan.FromTicks(17312790022));
                    _chapters.AddChapter("Chapter 8: The Potions Master", TimeSpan.FromTicks(11137209977));
                    _chapters.AddChapter("Chapter 9: The Midnight Duel", TimeSpan.FromTicks(18606140136));
                    _chapters.AddChapter("Chapter 10: Halloween", TimeSpan.FromTicks(15349310204));
                    _chapters.AddChapter("Chapter 11: Quidditch", TimeSpan.FromTicks(12756569614));
                    _chapters.AddChapter("Chapter 12: The Mirror of Erised", TimeSpan.FromTicks(21399510204));
                    _chapters.AddChapter("Chapter 13: Nicolas Flamel", TimeSpan.FromTicks(11891859863));
                    _chapters.AddChapter("Chapter 14: Norbert the Norwegian Ridgeback", TimeSpan.FromTicks(12775609977));
                    _chapters.AddChapter("Chapter 15: The Forbidden Forest", TimeSpan.FromTicks(19429520181));
                    _chapters.AddChapter("Chapter 16: Through the Trapdoor", TimeSpan.FromTicks(24760360090));
                    _chapters.AddChapter("Chapter 17: The Man with Two Faces", TimeSpan.FromTicks(23633000000));
                    _chapters.AddChapter("End Credits", TimeSpan.FromTicks(67000000));
                    _chapters.AddChapter("Preview: Harry Potter and the Chamber of Secrets", TimeSpan.FromTicks(490000000));
                    _chapters.AddChapter("Copyright", TimeSpan.FromTicks(397719274));
                }
                return _chapters;
            }
        }

        public override TestBookTags Tags
        {
            get
            {
                _tags ??= new TestBookTags
                {
                    Album = "Harry Potter and the Sorcerer's Stone, Book 1 (Unabridged)",
                    AlbumArtists = "J.K. Rowling",
                    Asin = "B017V4IM1G",
                    Comment = "Harry Potter is a wizard - and not only a wizard, he’s an incredibly famous wizard. Rubeus Hagrid spirits him away from his less-than-fortunate life to Hogwarts School of Witchcraft and Wizardry, setting into motion an incredible adventure....",
                    Copyright = "&#169;1997 J.K. Rowling;(P)1999 Listening Library, an imprint of Penguin Random House Audio Publishing Group",
                    Generes = "Audiobook",
                    LongDescription = "A global phenomenon and cornerstone of contemporary children’s literature, J.K. Rowling’s Harry Potter  series is both universally adored and critically acclaimed. Now,  experience the magic as you’ve never heard it before. The inimitable Jim  Dale brings to life an entire cast of characters—from the pinched,  nasal whine of Petunia Dursley to the shrill huff of the Sorting Hat to  the earnest, wondrous voice of the boy wizard himself.\nOrphaned  as an infant, young Harry Potter has been living a less-than-fortunate  life. Belittled by his pompous uncle and sniveling aunt (not to mention  his absolute terror of a cousin, Dudley), Harry has resigned himself to a  mediocre existence in the cupboard under the stairs. But then the  letters start dropping on the doormat at Number Four, Privet Drive.  Addressed to “Mr. H. Potter” and stamped shut with a purple wax seal,  the peculiar envelopes are swiftly confiscated by his relentlessly cruel  family. But nothing stops Rubeus Hagrid, a great beetle-eyed giant of a  man, from kicking down the door and bursting in with astonishing news:  Harry Potter is a wizard—and not only a wizard, he’s an incredibly  famous wizard. Hagrid spirits him away to Hogwarts School of Witchcraft  and Wizardry, setting into motion an incredible adventure (Banks run by  goblins! Enchanted train platforms! Invisibility Cloaks!) that listeners  won’t ever forget. ",
                    Narrator = "Jim Dale",
                    Performers = "J.K. Rowling",
                    ProductID = "BK_POTR_000001",
                    Publisher = "Pottermore Publishing",
                    ReleaseDate = "20-Nov-2015",
                    Title = "Harry Potter and the Sorcerer's Stone, Book 1 (Unabridged)",
                    Year = "2015",
                    CoverHash = "944f59e79cf55ffb3741bb213dcc07aba6e49723"
                };

                return _tags;
            }
        }

        [TestMethod]
        public void _2_WriteTags()
        {
            string tagstring = "this is a tag string";
            Mp4File mp4File = null;
            try
            {
                Aax.AppleTags.Album = tagstring;
                Aax.AppleTags.AlbumArtists = tagstring;
                Aax.AppleTags.Asin = tagstring;
                Aax.AppleTags.Comment = tagstring;
                Aax.AppleTags.Copyright = tagstring;
                Aax.AppleTags.Generes = tagstring;
                Aax.AppleTags.LongDescription = tagstring;
                Aax.AppleTags.Narrator = tagstring;
                Aax.AppleTags.Performers = tagstring;
                Aax.AppleTags.ProductID = tagstring;
                Aax.AppleTags.Publisher = tagstring;
                Aax.AppleTags.ReleaseDate = tagstring;
                Aax.AppleTags.Title = tagstring;
                Aax.AppleTags.Year = tagstring;

                var newPic = new byte[500];
                using var sha = SHA1.Create();
                sha.ComputeHash(newPic);
                var newPicHash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
                Aax.AppleTags.Cover = newPic;

                var tempfile = TestFiles.NewTempFile();
                var result = Aax.ConvertToMp4a(tempfile);
                Aax.Close();

                Assert.AreEqual(result, ConversionResult.NoErrorsDetected);

                mp4File = new Mp4File(tempfile.Name);

                Assert.AreEqual(mp4File.AppleTags.Album, tagstring);
                Assert.AreEqual(mp4File.AppleTags.AlbumArtists, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Asin, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Comment, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Copyright, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Generes, tagstring);
                Assert.AreEqual(mp4File.AppleTags.LongDescription, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Narrator, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Performers, tagstring);
                Assert.AreEqual(mp4File.AppleTags.ProductID, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Publisher, tagstring);
                Assert.AreEqual(mp4File.AppleTags.ReleaseDate, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Title, tagstring);
                Assert.AreEqual(mp4File.AppleTags.Year, tagstring);

                sha.ComputeHash(mp4File.AppleTags.Cover);

                var coverHash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
                Assert.AreEqual(coverHash, newPicHash);
            }
            finally
            {
                mp4File?.Close();
                TestFiles.CloseAllFiles();
            }
        }
    }
}
