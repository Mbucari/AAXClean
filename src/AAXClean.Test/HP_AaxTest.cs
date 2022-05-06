using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
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
		public override long RenderSize => 2876633L;
		public override TimeSpan Duration => TimeSpan.FromTicks(299221159183);
		public override string SingleM4bHash => "d934641bbfe88db424c71ebef1eb4f62d55042be";
		public override string PassthroughM4bHash => "9b3d155fd909521b6f7306b5c7c2a2bd1590158a";
		public override List<string> MultiM4bHashes => new()
		{
			"922603def2bb7dc73a2d51d3f4db85d41fdf4611",
			"b688ac7720b9471a7bf1f1b5e8395c43e6c623b7",
			"ed994c6bfcd315171ff67fe68d913f1b99827a49",
			"61fcc07e0ee4dae6bfea0b7e7e885065e1e89855",
			"94129c14d0087f87d6662ff6d503e9a78101364b",
			"afd4cab923a4e5f5b558feac8d8bb950df818797",
			"1b9ed4337470e70b4af1a48fb0e20f27b304de3e",
			"817802dfc22aa5e0ed2016e5e7999d689bed11f1",
			"695237516b7f14706171782b3c90216e5d093407",
			"dea663df21bacac296f5378e17ff4f1930678f1c",
			"f1061a9025bf8b0e8761e4d6e4e71e404eba7665",
			"ffc6429c136542d7e26c15ab5a5d2f758b71e02e",
			"d9dafe3f211e38d60c5c57d343cf13b62ceab837",
			"f44f20b03799abe4836f392e931f47a54f82ad17",
			"c45650f19cb6ef79cfb3e3a5c2ae40f4bed916a5",
			"d68b70537dc4d46fbd76e375e1aa891a98480919",
			"7209caae2666664e9bbc0cf3557622c716abed47",
			"a470890ec65f2d990e87cec93954c685aabc27da",
			"b1e508be9884cdbdb6b38c37a9e936446ec05a03",
			"55bc1db600e2ed4731134b15226bd4ff2ccd5444",
			"563a863f0d3cf72ef65d66fec482a5225549ced1",
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
		public void _7_WriteTags()
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
				ConversionResult result = Aax.ConvertToMp4a(tempfile);
				Aax.Close();

				Assert.AreEqual(ConversionResult.NoErrorsDetected, result);

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
