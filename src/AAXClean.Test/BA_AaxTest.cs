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
		public override string SingleM4bHash => "3d268d0d272491f39b5a2f555ad81e7bce090da0";
		public override string PassthroughM4bHash => "8b937c043c1251beadc089fcb89581eea9aafcb1";
		public override List<string> MultiM4bHashes => new()
		{
			"df1c7692bbb749cd3b54bdc1c8fa87084f236fe6",
			"0ebf9e620e4b07ce90a568e03de31238dc53f948",
			"6aa7a044602349e65e4630e22c414e3e6bd59b84",
			"d7a4e41e4bb95e2131face5a774521332e7873ae",
			"f6c3c670f2265c1c5878ed4c00267e4c2900275b",
			"9903d2d8a2970f8dbf0623c75a0303166b170532",
			"5983ff73c873f608af12068b9d108684ce1da920",
			"1a52cefc4c0ef0abc875d23aa671eeab28a982fd",
			"802e6a208eba08deba159e10a99d3341fdb74e7a",
			"65d5b78b8eca57b186d75bbe7125c514f927dfe7",
			"babb0d5ab2324e5d9a1ad57c8ac0a242a2df7774",
			"08262ccc7319d7533a791072eb3aef5f4073a2eb",
			"a084b5d3eacfd746a6ee79908db8cc2b7dd6a42d",
			"9dc8c9e0b0804656beacac46acec7183983ee217",
			"4eafb4cfffeb8ad733618ccc589ff69d0f9f2196",
			"c8cccb9cf8a6fad422ac1256686584ff75e0786a",
			"77df7c6ed8e05fe34fdffa5c4a877868e6f660bd",
			"99fb818875fa9571bc070e2e6e2fc15991205ea0",
			"a10d4373f356254a54291d45f7b62b32e0e1c818",
			"2f9cee389066899bc26542768b6c1ae1b18868f9",
			"561382492ecf3c8a17ab26ddfebab4d4505fc04a",
			"fdf367c19dca8787ed9c5a0a236075915bb3f10f",
			"fabb9b97e708859a799aaafbe5c430dfcc004dbb",
			"709f046f22b0f4d9fe5318b28f6f5868a8f6e0cf",
			"efac0e31bf0b308b232e80258644add0812b7d35",
			"53586fd9bc950e07c03db3a01e524c62abdc0ecf",
			"ea58f64f5d4a7772c31dda6f11f4eb148b9a4857",
			"681cea3a313ccdfb053192395d65c4fdd214617e",
			"05f58cd8fee275268c85617d0e30d5701545b59e",
			"3529509d88596c789235869576d76ca8807692da",
			"cc0be003f14ce00c5eb904f4633b07837c1f16b1",
			"e53d2bdc2dc1d2f59f8ee3362346585d73546ff2",
			"82426dd84ed9263249cb02307e079c9b99705bb7",
			"db78944a47cf4083b795d16f10ad373d68ff601b",
			"895232923e20791ffed52e7b47a01bf21026e2a7",
			"0740c692a9d8abe25e66523f7bbcfaaf4a892027",
			"167b897e5783267acf9ebc4961f4d998bbf64fcd",
			"0c7091e919c151e51b1f6c0a48b8ef4efec9004f",
			"6bc2444eacba92eff98004092b958e6196b18411",
			"7a1ca4132d8a271ace898630805a644be323e730",
			"efce4b9186123d3d801e2ab7de083f7c545a0d34",
			"b393339787273a6e80cf9898241a2cc507bc706c",
			"b75b6790c8e14c255d1856839eee9033a544f021",
			"c49b3fdc1a6b741160a6d138c84718c917eef86d",
			"e0030a22d1a0860d452285e564fb78eaaf872d03",
			"adbf4aea048391c3957f33879369bf431dbffec8",
			"f1c9ab0839a6e4f1f32015b0326c4ae481159fdb",
			"7aec69a8c3f134767d1b1fd165f2217a19c18cc3",
			"34ea75aa2c87afcd70dfbe4d450184c21b1f90dc",
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
