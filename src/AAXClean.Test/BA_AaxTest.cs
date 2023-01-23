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
		public override uint AverageBitrate => 64004u;
		public override uint MaxBitrate => 64004u;
		public override long RenderSize => 5529703L;
		public override TimeSpan Duration => TimeSpan.FromTicks(581463887528);
		public override string SingleM4bHash => "18a555721a0b80ed5f54ec1444101d1626fdb600";
		public override List<string> MultiM4bHashes => new()
		{
"302f2836ca32878e59dab6fbc2764a94bfdf2660",
"3fbf8619bc10dca4535e593ae5f55563dbb2cf68",
"96367e8925dcddbd6c4ed035e57b983c20f2906d",
"8a8948320048f8a4707e89ec30616f2e9e89a4ef",
"f2f491440dfcbaaf80efcefc3b9139844b58798c",
"7a535eb08cb9dca6030b10ebfff1995972a3d833",
"08b7c56026e7775ea27ea048e3b833526863b66f",
"28ec617a7c37311323990b5856b54ed153d3a113",
"a14b7ff349b86f4a617e9e94d135a33cadc87dc4",
"2d9ef9e1ae8d4d8be57d27e815bd0541cd3a43fd",
"8bae970079d2ed38ddd6176610a0115035ab108c",
"bc02e4d6090bce44da2c39b14b7194520f61c898",
"126e447f32aa798900bfa4ee04cd8ccf3bf234f4",
"a8821dc868a71830f88b2e8bec99bd12e41acd78",
"cdf02553c4ed4d8435eb67ef53f3cd10a33f86a8",
"4db8b33280a2e0b975c2628276f23b7545312877",
"ac2ba94cced66969ace54047360e204c59a92745",
"f43205d07abd85c5ca4df1be04d7d3f268694323",
"2b1c7a7af475e015ba0fd31f23254f29e4ac82fb",
"7bd753b6bfdaaf9f64bc243f2c10bd462b48fb37",
"dc61c24c2846d9fa1d14d0b5bb17fe9dd16d399c",
"caf27b6561cf042b4b69f5faa813a216ab2c3398",
"bdf9ad750c035f1dab4a830132450a4e103d6227",
"20697b54eddda30f3ccdab463a924a787dbee1ed",
"d9d756898d5c5749edda8d594d44739444e31e9f",
"c710ee06d371fcae177f7c9e468639316ece8087",
"b20c28e7e48de31e10e6db3cd18ebc00165c0b99",
"448065cac80c43cc4ffa81ec89a28bfe22134142",
"eb101735c9bafe0b6eb27e3f86778d2cf11a1725",
"710973f73ecddd5bfa51648845ab6f096985b9c0",
"90489c602ed723356a6fd952074e99aabb3c99e8",
"6eac4340943894d773e72fba435f4c67e89f708f",
"c0f8a405c80b4b693b2ef021e015f6cef14d4316",
"93c2ab5a6df4793c5fb3aaf6bc0b0d52aa3c3952",
"01bd73f33ce695b328e317dd53c30d80c44beb13",
"770982c5c5974d2042279976fe32771764739c22",
"1bad511974458e9861132f7c6b276c1547ad1151",
"3c9f3110a98b829b46639b197c716dfab27d34f3",
"f0f2e4145f015fbb809e877a6081bffa5b07c18f",
"4264a3ce22d5e8585b49f31c95ae8554bc96c059",
"01729f9862006a7fe8f82376a17b40a627558b95",
"cf28d31f373c7f760f12c259d16f71e13fefc33f",
"1a3fbf5ced644781049232a9f437548f7fc4a041",
"bfe13fb0a9baa1779819bf7601d074da865fee28",
"b052e217f4de5122a2a568b8b98b1abb0fdfad7f",
"7a41065e288395ab7c0a9a703fc36ab159f573c8",
"ecabcf97fd649ec9c040db50c08298b9b8ecb2ba",
"608337bfb0b2c291f8599486d94b437586326420",
"4c982735fe335300aec67f1e9c89589feeba3d8a",
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
