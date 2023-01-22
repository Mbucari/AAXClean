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
		public override string SingleM4bHash => "889c4ef77388a1e2d7e50d01434f3c1763034819";
		public override List<string> MultiM4bHashes => new()
		{
			"9a46ad8b9b32d2ac00ed9e0901b8bebd2f6fe530",
			"5b5f09e593b63ddd4a9342983c213565689840ea",
			"fee44deb689d40d72790f74c690c29787b097012",
			"e57e6121820518586700c6f17525b3dd48bdccd8",
			"40220c90974f64adfafcf703df282d7561fc74d9",
			"13049262442922c63cb4b1a0e9e87e8e7070df5d",
			"eb717b3eb1952d333ca3eb7d98b4f1fe8fb87e13",
			"261ea605ebed33e260ca532309e438e0282d58cc",
			"85c871088462cc9971c5299ddf56ecee6033a0f5",
			"79538472acdd22ab04af908cc79832e79f8fbd06",
			"0510f5723c6e55b741b5ea0d246df0e23b58ae84",
			"b1886f26a3ba657bbde7bad0dc8ece0252174fc4",
			"d1f5a335170b5fd1106dc05fc3891b3f8e985e0b",
			"daa34f5a49de0ffbec906cd03ed2927f39747170",
			"393b2e52ac66e9e440427522d25c4e40632aca0d",
			"36f018d4dc8ae13db97c1f7029f99c172f09eee3",
			"08ee7caa8bccaae5e2a399120333fe84a540b953",
			"7f8d0e7c346057112d89de7a92659639c19c6fd8",
			"2b5defb088a02f831ab25de6de5b62ef17b325f0",
			"392039a831b2a08d958e92bc77767e9c5b672e9e",
			"16c7ed7739220425dc32c3e9439c9d460d6833b4",
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
