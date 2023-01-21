using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AAXClean.Test
{
	[TestClass]
	public class BA_AaxTest : AaxTestBase
	{
		private ChapterInfo _chapters;
		private TestBookTags _tags;

		public override string AaxFile => TestFiles.BA_BookPath;
		public override int AudioChannels => 2;
		public override int AudioSampleSize => 16;
		public override uint TimeScale => 22050;
		public override uint AverageBitrate => 62794u;
		public override uint MaxBitrate => 64004u;
		public override long RenderSize => 5529703L;
		public override TimeSpan Duration => TimeSpan.FromTicks(581463887528);
		public override string SingleM4bHash => "db753ea85644459ffc1088c5bc52acf5af9f0f83";
		public override List<string> MultiM4bHashes => new()
		{
			"945a867b633ff1189c6d23951214588e5b49c3bd",
			"972e3d7e0f66632d54154a761a65a4d703bec47e",
			"e112ac60f476f2aa1dd66a6a583dd8612be7c283",
			"42d943b875d25b22ac059491c4569f5188ff8d4d",
			"8c27c2c2f2ed933f0e65a2ec4e4118c5561d653d",
			"81d330d42ee5ab93855fefdf5b21c07edf42e37b",
			"00c4b634754c435b59a083c3cb2e198327b19486",
			"f3b28e1293aba127c34cfa353774221dd2948791",
			"a9559d995c879dd5aea782f447def1d188f04cea",
			"5f9bdb7bb63254c41b18c658495673803356a627",
			"74dac82a751679d86577b47ccda40958f9fd5773",
			"80fc65d09b9045e9a26980d8dc3a85ac65a38717",
			"8a1c76ba72e87c269610891e13a573fbcb0472ff",
			"1059e77e78fe75c0693af9e9b4d9736529e25b74",
			"0d998201b2d66fe1adf785b991fa6df9eb3fc35a",
			"11e77dcce925027e6a5331502f7f9127c52dc661",
			"8867397664cd5fc66769d009ec0df691f58d4ed9",
			"f3bc97cb54950edbd2ddc4c3916d8109897fe930",
			"20d146ae6ca9b8cc528063b267c593f561c7a9bf",
			"7c44ed17dbec2ea7cb310b87e9e15f8a6c7a5bbc",
			"01e5a69eb1765ff063995e045f270e8e7c4d8d77",
			"61e87d20e339aeabe9942709c92c94daa9560de6",
			"b0f5bcc20fc4c003a272b1e4e8f4b3dd05379f24",
			"c6e10e09302713689732403a1cf85104a5bcbdb2",
			"72c608d923f4b5b74a6d4e2637848ac411a36817",
			"f0057637f27e2137fb3763efa539bf5facb899ee",
			"16e6801fb27ffbdc71d80654b9ae8bba6199ed0b",
			"c429483ce56d42199b1b222a5089b90e053cfe3e",
			"d9c4588edb28cc6c2152e727338b282dc9658954",
			"c3c71f5ab6ad9379641f1728f2277d37f3bc3716",
			"1b66366042a5e553846cc9fa429f68b7f4384b4c",
			"bbd4ea310a662edc50a35c8ebc6e7950dbb8b378",
			"c9a8f9277af59b95f14455b89092e6855ed694bd",
			"107d3520c7bc2700de5d2805939a5d7a5b77f1c5",
			"fcb415d3dae7373e989aaaefd6665faee1a943c2",
			"7e3366f567e38a4f34166c3e98da33cce7a9bb0d",
			"928c53056b6e9d8cdfc2a107848fee6db58ba283",
			"8602506cb3660a62c6b05079a9efebb99c9e141a",
			"d575ee8d1b37a65c1504d86890f43f76f092326c",
			"b4cd0b4ae71d778c6bea84c874d08619792df54d",
			"44a9b460dcc8eea71190e8452e470c97b8d910aa",
			"8b03c07e714f04bae8b15050d314c72de48bc2c7",
			"ff4d4ec24f9bee581ffbdbddc16efd9446f87f5c",
			"9144cab603d787147509a603026276a8d1ecf311",
			"636f05c07c35089e43dcca9f399597ab7996cf64",
			"7648b3b6d1ff686e69e0d0d96126c052662ff5c0",
			"599a6ccf519ff982b773c34f2d3dbc195e177429",
			"99ebfc6cd3296d5c1612443c4e4666ab4587f4b9",
			"f4de2aa65c9a7e838a06d268ebde0c7167bec607",
		};
		public override ChapterInfo Chapters
		{
			get
			{
				if (_chapters is null)
				{
					_chapters = new ChapterInfo();
					_chapters.AddChapter("Opening Credits", TimeSpan.FromTicks(1789039909));
					_chapters.AddChapter("Part One - Injured Parties", TimeSpan.FromTicks(1519000000));
					_chapters.AddChapter("Chapter Four", TimeSpan.FromTicks(453));
					_chapters.AddChapter("Chapter One", TimeSpan.FromTicks(7438999546));
					_chapters.AddChapter("Chapter Two", TimeSpan.FromTicks(13649000000));
					_chapters.AddChapter("Chapter Three", TimeSpan.FromTicks(21807489795));
					_chapters.AddChapter("Chapter Five", TimeSpan.FromTicks(14563000000));
					_chapters.AddChapter("Chapter Six", TimeSpan.FromTicks(20003150113));
					_chapters.AddChapter("Chapter Seven", TimeSpan.FromTicks(14130000000));
					_chapters.AddChapter("Chapter Eight", TimeSpan.FromTicks(16952229931));
					_chapters.AddChapter("Part Two - Commercial Considerations", TimeSpan.FromTicks(350400000));
					_chapters.AddChapter("Chapter Nine", TimeSpan.FromTicks(8978600000));
					_chapters.AddChapter("Chapter Ten", TimeSpan.FromTicks(16626000000));
					_chapters.AddChapter("Chapter Eleven", TimeSpan.FromTicks(8229879818));
					_chapters.AddChapter("Chapter Twelve", TimeSpan.FromTicks(13901000000));
					_chapters.AddChapter("Chapter Thirteen", TimeSpan.FromTicks(10049000000));
					_chapters.AddChapter("Chapter Fourteen", TimeSpan.FromTicks(12620040362));
					_chapters.AddChapter("Chapter Fifteen", TimeSpan.FromTicks(17980000000));
					_chapters.AddChapter("Chapter Sixteen", TimeSpan.FromTicks(10099000000));
					_chapters.AddChapter("Chapter Seventeen", TimeSpan.FromTicks(7695519727));
					_chapters.AddChapter("Part Three - Disruptive Elements", TimeSpan.FromTicks(264070294));
					_chapters.AddChapter("Chapter Eighteen", TimeSpan.FromTicks(8715929705));
					_chapters.AddChapter("Chapter Nineteen", TimeSpan.FromTicks(10213000000));
					_chapters.AddChapter("Chapter Twenty", TimeSpan.FromTicks(16245760090));
					_chapters.AddChapter("Chapter Twenty-One", TimeSpan.FromTicks(13961000000));
					_chapters.AddChapter("Chapter Twenty-Two", TimeSpan.FromTicks(10833000000));
					_chapters.AddChapter("Chapter Twenty-Three", TimeSpan.FromTicks(13009479818));
					_chapters.AddChapter("Chapter Twenty-Four", TimeSpan.FromTicks(7869000000));
					_chapters.AddChapter("Chapter Twenty-Five", TimeSpan.FromTicks(25449310204));
					_chapters.AddChapter("Chapter Twenty-Six", TimeSpan.FromTicks(11805000000));
					_chapters.AddChapter("Chapter Twenty-Seven", TimeSpan.FromTicks(7354000000));
					_chapters.AddChapter("Chapter Twenty-Eight", TimeSpan.FromTicks(8849000000));
					_chapters.AddChapter("Chapter Twenty-Nine", TimeSpan.FromTicks(9322259863));
					_chapters.AddChapter("Part Four - Unexplained Phenomena", TimeSpan.FromTicks(236090249));
					_chapters.AddChapter("Chapter Thirty", TimeSpan.FromTicks(17082909750));
					_chapters.AddChapter("Chapter Thirty-One", TimeSpan.FromTicks(15647000000));
					_chapters.AddChapter("Chapter Thirty-Two", TimeSpan.FromTicks(15369120181));
					_chapters.AddChapter("Chapter Thirty-Three", TimeSpan.FromTicks(16369000000));
					_chapters.AddChapter("Chapter Thirty-Four", TimeSpan.FromTicks(18188000000));
					_chapters.AddChapter("Chapter Thirty-Five", TimeSpan.FromTicks(4922029931));
					_chapters.AddChapter("Part Five - Divided Loyalties", TimeSpan.FromTicks(30000000));
					_chapters.AddChapter("Chapter Thirty-Six", TimeSpan.FromTicks(11143000000));
					_chapters.AddChapter("Chapter Thirty-Seven", TimeSpan.FromTicks(14985000000));
					_chapters.AddChapter("Chapter Thirty-Eight", TimeSpan.FromTicks(24184720181));
					_chapters.AddChapter("Chapter Thirty-Nine", TimeSpan.FromTicks(10161000000));
					_chapters.AddChapter("Chapter Forty", TimeSpan.FromTicks(11759000000));
					_chapters.AddChapter("Chapter Forty-One", TimeSpan.FromTicks(23966349659));
					_chapters.AddChapter("Chapter Forty-Two", TimeSpan.FromTicks(28971000000));
					_chapters.AddChapter("Epilogue", TimeSpan.FromTicks(6177507936));
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
					Album = "Broken Angels (Unabridged)",
					AlbumArtists = "Richard K. Morgan",
					Asin = "B002V8H59I",
					Comment = "Cynical, quick-on-the-trigger Takeshi Kovacs, the ex-U.N. envoy turned private eye, has changed careers, and bodies, once more....",
					Copyright = "&#169;2003  Richard K. Morgan;(P)2005  Tantor Media, Inc.",
					Generes = "Audiobook",
					LongDescription = "Cynical, quick-on-the-trigger Takeshi Kovacs, the ex-U.N. envoy turned private eye, has changed careers, and bodies, once more, trading sleuthing for soldiering as a warrior-for-hire and helping a far-flung planet's government put down a bloody revolution. \n But when it comes to taking sides, the only one Kovacs is ever really on is his own. So when a rogue pilot and a sleazy corporate fat cat offer him a lucrative role in a treacherous treasure hunt, he's only too happy to go AWOL with a band of resurrected soldiers of fortune. All that stands between them and the ancient alien spacecraft they mean to salvage are a massacred city bathed in deadly radiation, unleashed nanotechnolgy with a million ways to kill, and whatever surprises the highly advanced Martian race may have in store. But armed with his genetically engineered instincts, and his trusty twin Kalashnikovs, Takeshi is ready to take on anything...and let the devil take whoever's left behind. ",
					Narrator = "Todd McLaren",
					Performers = "Richard K. Morgan",
					ProductID = "BK_TANT_000116",
					Publisher = "Tantor Audio",
					ReleaseDate = "17-Jun-2005",
					Title = "Broken Angels (Unabridged)",
					Year = "2005",
					CoverHash = "712719a5a29fb8a3531ead6a280ce8e2c35f0f65"
				};

				return _tags;
			}
		}
	}
}
