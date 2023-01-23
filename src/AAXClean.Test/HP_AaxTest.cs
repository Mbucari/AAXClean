using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

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
		public override uint AverageBitrate => 64004u;
		public override uint MaxBitrate => 64004u;
		public override long RenderSize => 2876633L;
		public override TimeSpan Duration => TimeSpan.FromTicks(299221159183);
		public override string SingleM4bHash => "536c14ce3f4c0c51692ef1714b44aa8faa3b6474";
		public override List<string> MultiM4bHashes => new()
		{
"86d6782e23f067d575435f6bb4b433fe7a25537a",
"b0885e1ba5182cf7a35812cb711728f612ff045d",
"fbff7e6416df9445e175d959ea347ea9228cf8c0",
"51fc4a4fe64254d2c3f1647dd9a76ea54edcff9f",
"be8c4b9e18fbd46625df1a17e389b03ad2487756",
"1761f2229286f07c4375012d592aad778294a280",
"5952a574eaf6c84f7d44b8a0e7509c80dc9cbd52",
"b19ec672fbc9cb37ddece675ebba232482b41bfd",
"4954ca01f49374caf0d35fcdb2afb4d5cc7bb976",
"6b46fa4b131bd07cfa0124ad603a45b3d03d671c",
"95b0b5bebe3a3b21d48c2b4814891d7ee50ca6af",
"36f7df34601e82f572e7447e4cf8bf7f34cbfdd2",
"0b1156f92643f9a5159328f8185d3e0e6ca71b73",
"5aa7934f10e17be48a5c5d7fbc68f98f5f8a8dca",
"fc200048e5e74cab177d6193d3b3d6e67c0ec509",
"ae8f3ec0a4f32381a3adfc560ed801f80ca134f0",
"bde3d6b49f9e9fcec88deca5491abf25a18a9a1f",
"941207331565fe3ef03513003e5c9deda7e2fea9",
"013d778a4b12ae25cc08ef9d961e4d44484608d4",
"673774ee66b2412806413649578ffc99d1459315",
"7f54bcd021e41e92144184e661e3b1eefc3b744d",
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
		public async Task _7_WriteTags()
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

				byte[] newPic = new byte[500];
				using SHA1 sha = SHA1.Create();
				sha.ComputeHash(newPic);
				string newPicHash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
				Aax.AppleTags.Cover = newPic;

				FileStream tempfile = TestFiles.NewTempFile();
				await Aax.ConvertToMp4aAsync(tempfile);
				Aax.Close();

				mp4File = new Mp4File(tempfile.Name);

				Assert.AreEqual(tagstring, mp4File.AppleTags.Album);
				Assert.AreEqual(tagstring, mp4File.AppleTags.AlbumArtists);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Asin);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Comment);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Copyright);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Generes);
				Assert.AreEqual(tagstring, mp4File.AppleTags.LongDescription);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Narrator);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Performers);
				Assert.AreEqual(tagstring, mp4File.AppleTags.ProductID);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Publisher);
				Assert.AreEqual(tagstring, mp4File.AppleTags.ReleaseDate);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Title);
				Assert.AreEqual(tagstring, mp4File.AppleTags.Year);

				sha.ComputeHash(mp4File.AppleTags.Cover);

				string coverHash = string.Join("", sha.Hash.Select(b => b.ToString("x2")));
				Assert.AreEqual(newPicHash, coverHash);
			}
			finally
			{
				mp4File?.Close();
				TestFiles.CloseAllFiles();
			}
		}		
	}
}
