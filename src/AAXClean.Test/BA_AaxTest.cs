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
		public override string SingleM4bHash => "aa44bed068d0bd12752a5d8b5ed6dc0889fc8e19";
		public override List<string> MultiM4bHashes => new()
		{
			"5720775372c8623f3f3d8ca83b77c1e3b39720fc",
			"04fb30c349b3bc633d7df4729581e6020af46312",
			"2ba680e1a0c79e05108d8916525a60f64b26bcec",
			"564b9384f42326a96e06c64cb9840c1dbdb3fe24",
			"7fa4654685aa819c425ad380ec400712805192ce",
			"233d06656876ec26dbc9016486f2b3f738662efd",
			"7d8b2bb825b0bccbb0b9dc743ddb456dcccc8bc0",
			"38df511418ccf6ee41c408d8e3f5690271388e87",
			"d8a92a03e0c20eb17a35a6937a247ab09f690218",
			"8bcd5d24a45fc73f08f3dc4027f9b73a852d6e7c",
			"a41de7c3776b73d1dca20b33ff62a4b0e3d5b3dd",
			"284fd625853ee67280e62468f1aafd0a2e166bd4",
			"542346ce496c23b40d34d2ea73abf041c10f36da",
			"f8ce73e534408f6892ed95e34431879a52262729",
			"23c7678fdf259d64c13746d2336193599c89325d",
			"3d268047f57cfb925de3581ead682ef4c30b7421",
			"feed730c7772e9f390e089d5dd2011b80631bff8",
			"43e83648c1b0ff8b8ae2da25f9448c7a599dee28",
			"c5e412a3fa091b00c28354ee1ca8a560c9d5b444",
			"75b6e1a8cdb6790de80ccde7f8b513c55d168702",
			"caadd988d921b7b7e56a497d0b4fc7cebf3f3501",
			"eb0e653a084a59e1939f0ebec43a951d0940ef09",
			"3ffe89a0b26624f40d1e9dfeed40c60c5f31dbc2",
			"17c82b38006737bb937ac6fa732966571677cc26",
			"77d36da7d218316a796a4e649f3fe4bad4a3b8c5",
			"c4e0b6d019f549f6d229a0e71c0371b08e292c25",
			"cf28e5abee36e5267ed8b2531b97ffcb388386c8",
			"74be33e6c985f656f1f70b3a1a973cd767ca1f9b",
			"1b5683d1ba77b957f1028679a6815ba85f5a5108",
			"e4868b02a6453e015c82ceb69466d2feead1aced",
			"95f5b268c0b6e6153cdfaaf95fd2ca7ef7a75e05",
			"eeb50c8beb7d2c4c33b8400cccec67761632e749",
			"1aedc170d68d6edfba095a7b63b6745e7b08f6b6",
			"540c28a57edabd328ecab4c29cb6179e1423fc44",
			"a8ae947ad2f178f906eecd95d4a6f61ad8dc8a25",
			"0149602920914b90a983fe45f8b599d8aa2b49bb",
			"cc593e240fda23b163f1c6ddba1bd4ba8c9c7352",
			"3f57602aca3ad80597061b995a9f64044f569b5a",
			"04d62234822e59b20192194652390fdc18095b29",
			"8261fde042492db6650b37b0bcc40035d1a155e3",
			"49928015dcedbd5f190350622664e604099f5093",
			"d1bf679dac520afbcc2f9de098960c7ab4ef90b9",
			"abba6340b6f79328307054bb7274607083282d05",
			"862c0e09c4f2e33ffc8c5f577f12f5b6c14050b9",
			"0d0d5c3d71206059a5b99f8f4ea855593c19cd98",
			"77fc9c05bf3b6f2eea157ffe3cbd8aa25c76009c",
			"7236c8d85f817839a48f4377603f61688e7176e5",
			"ce113891963b2e9002859d9151e9d99c1e6504da",
			"da625b45b4a1aa515bc5a9c94553e3eb9ebd78ec",
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
