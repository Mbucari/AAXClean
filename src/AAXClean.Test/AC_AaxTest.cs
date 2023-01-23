using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AAXClean.Test
{
	[TestClass]
	public class AC_AaxTest : AaxTestBase
	{
		private ChapterInfo _chapters;
		private TestBookTags _tags;

		public override string AaxFile => TestFiles.AC_BookPath;
		public override int AudioChannels => 2;
		public override int AudioSampleSize => 16;
		public override uint TimeScale => 22050;
		public override uint AverageBitrate => 64004u;
		public override uint MaxBitrate => 64004u;
		public override long RenderSize => 5872527L;
		public override TimeSpan Duration => TimeSpan.FromTicks(618328816326);
		public override string SingleM4bHash => "18c4ee2ad09c3d22a4507482180f1cc8983b4c16";
		public override List<string> MultiM4bHashes => new()
{
"3d45029aac5fba8420df9e3536b521a4022d0703",
"eea16bcae3dd5dc0e8e3b5de1799f06304877673",
"095dce41c795f446ab0b01404ba7eaf18c620414",
"04c8f85d67163abf329ed1c69d7e017e40080713",
"d9fb6748f9e39fd68955e511a7e96a3f6ff55fd1",
"5fee02a25d3b4549ad18cd0f0cba14aff7abe737",
"272c92bff5400cf75c3cdedf48544cae8d2c709b",
"b2d1c2235544736c0f53f787dd0affa89bf47f80",
"5d3b1a87175af5168a3d452e485601ac18c92301",
"026ba3be0cb0fdb0016b1d82699527af7b6c0d6a",
"ef40d022222e258fe26e0551a61719ea6e55605b",
"e4605d60fa0faf872e02955a37df4f0f1a1d0eff",
"fd8efe3cb583f6daca0cb90e5c16fcf79b091ecf",
"e9a06f7184839f9a888d9080bf7b3f1f89b1af84",
"56c9aacbbb911344f6989893bcb72c90585aa85a",
"585a9af9c1c9d5a4e544f104f8b00136016e52e1",
"2932fee455c423a1a7d52bbd8bb42974b509009e",
"7ba009ac97d661ce3c595a97a58bf51537a0bd27",
"54aa3718abadbe3cbcd0242f199cf8d30c6b8954",
"dafd93ea103183ea1668403f49c78b9adbbef898",
"a258f82f5b603c98d8b36777e8292a8ca785ee59",
"38064f58091336afd48fa83695c102d1c53e6452",
"e80fc2157191f864814b28ba5d0570711229d4c6",
"9cf8d65807463fc1e034bfc770bd78c81bb16d62",
"e48333c424ea7e78dd75d3c1a32b4ff3d1dd61e1",
"b033af388c183a2e15adaa8e552086f9404d6a71",
"5e748c17cee80a25b0561d7d3960f39ace4c04c0",
"7c71cb1ae1e54e7b5d36aa7edc71c78e58789a69",
"e62978a470ffab241579e68af559d5ff20505604",
"9ed6bba87f3e68436e8167652933f7db2f7855ea",
"f6ecdbf1a73f677acc846ec60bfa101bd6e57833",
"db2cf42ff327555b9a0fcf596d750fe62fa4fd20",
"ff3292360e92fd20eb92a474137277eff71ecc0d",
"2334a5260d4f9a4fb6b86268b04cb3a31a8e8519",
"83a1d587432115d9091b4a907871ce9999d97423",
"b160650a8e8d583300abbc6f8e8de73d215dc421",
"3272075599ae903d007591539794e383d7ecf9f7",
"3e57d723d167e48e5c76195739b64b224efe7219",
"a8c03138d8c89a81bb8168f1781b022bfe517ec0",
"eb7f977c794cd3631af110bc427b06d9da78fad2",
"d1eab0e54191899f662e30f5fa40d6421ecb6048",
"e528c9114be548bb634d57636f09d9d6546e27c9",
"133f24a5b535ce50eb1694342e24e4b112cd593a",
"527d447dfbc8a00f7f94627b5677e7efcbfff400",
"30355b4ee63da00a4ce7afe1009348e2a8f208fa",
"772f35de8bc7bb7e4dbdd57628c454cf744f2808",
"853998952dd1eea4fc30d589402ec3fbf6b25d0c",
"271b5a74172901992c9b00503bffd5b1c2886c54",
};

		public override ChapterInfo Chapters
		{
			get
			{
				if (_chapters is null)
				{
					_chapters = new ChapterInfo();
					_chapters.AddChapter("PROLOGUE", TimeSpan.FromTicks(7327059863));
					_chapters.AddChapter("PART ONE: ARRIVAL - (Needlecast Download)", TimeSpan.FromTicks(32380045));
					_chapters.AddChapter("CHAPTER ONE", TimeSpan.FromTicks(10467600000));
					_chapters.AddChapter("CHAPTER TWO", TimeSpan.FromTicks(11490939682));
					_chapters.AddChapter("CHAPTER THREE", TimeSpan.FromTicks(25143000000));
					_chapters.AddChapter("CHAPTER FOUR", TimeSpan.FromTicks(8356000000));
					_chapters.AddChapter("CHAPTER FIVE", TimeSpan.FromTicks(10298940136));
					_chapters.AddChapter("CHAPTER SIX", TimeSpan.FromTicks(2717000000));
					_chapters.AddChapter("CHAPTER EIGHT", TimeSpan.FromTicks(13314000000));
					_chapters.AddChapter("CHAPTER SEVEN", TimeSpan.FromTicks(35002750113));
					_chapters.AddChapter("PART TWO: REACTION - (Intrusion Conflict)", TimeSpan.FromTicks(86259863));
					_chapters.AddChapter("CHAPTER NINE", TimeSpan.FromTicks(14781740136));
					_chapters.AddChapter("CHAPTER TEN", TimeSpan.FromTicks(17825229931));
					_chapters.AddChapter("CHAPTER ELEVEN", TimeSpan.FromTicks(15079000000));
					_chapters.AddChapter("CHAPTER TWELVE", TimeSpan.FromTicks(19101700226));
					_chapters.AddChapter("CHAPTER THIRTEEN", TimeSpan.FromTicks(15345000000));
					_chapters.AddChapter("CHAPTER FOURTEEN", TimeSpan.FromTicks(10000000000));
					_chapters.AddChapter("CHAPTER FIFTEEN", TimeSpan.FromTicks(17892879818));
					_chapters.AddChapter("PART THREE: ALLIANCE - (Application Upgrade)", TimeSpan.FromTicks(105340136));
					_chapters.AddChapter("CHAPTER SIXTEEN", TimeSpan.FromTicks(18666659863));
					_chapters.AddChapter("CHAPTER SEVENTEEN", TimeSpan.FromTicks(21703170068));
					_chapters.AddChapter("CHAPTER EIGHTEEN", TimeSpan.FromTicks(14506000000));
					_chapters.AddChapter("CHAPTER NINETEEN", TimeSpan.FromTicks(12753299773));
					_chapters.AddChapter("CHAPTER TWENTY", TimeSpan.FromTicks(19688000000));
					_chapters.AddChapter("CHAPTER TWENTY-ONE", TimeSpan.FromTicks(15225000000));
					_chapters.AddChapter("CHAPTER TWENTY-TWO", TimeSpan.FromTicks(8819000000));
					_chapters.AddChapter("CHAPTER TWENTY-THREE", TimeSpan.FromTicks(4922000000));
					_chapters.AddChapter("CHAPTER TWENTY-SIX", TimeSpan.FromTicks(5305000000));
					_chapters.AddChapter("CHAPTER TWENTY-FOUR", TimeSpan.FromTicks(12026000000));
					_chapters.AddChapter("CHAPTER TWENTY-FIVE", TimeSpan.FromTicks(28400400000));
					_chapters.AddChapter("PART FOUR: PERSUASION - (Viral Corrupt)", TimeSpan.FromTicks(78330158));
					_chapters.AddChapter("CHAPTER TWENTY-SEVEN", TimeSpan.FromTicks(15110669841));
					_chapters.AddChapter("CHAPTER TWENTY-EIGHT", TimeSpan.FromTicks(8480000000));
					_chapters.AddChapter("CHAPTER TWENTY-NINE", TimeSpan.FromTicks(18785900226));
					_chapters.AddChapter("CHAPTER THIRTY", TimeSpan.FromTicks(9363000000));
					_chapters.AddChapter("CHAPTER THIRTY-FOUR", TimeSpan.FromTicks(11214000000));
					_chapters.AddChapter("CHAPTER THIRTY-ONE", TimeSpan.FromTicks(1649000000));
					_chapters.AddChapter("CHAPTER THIRTY-TWO", TimeSpan.FromTicks(11734000000));
					_chapters.AddChapter("CHAPTER THIRTY-THREE", TimeSpan.FromTicks(17617560090));
					_chapters.AddChapter("PART FIVE: NEMESIS - (Systems Crash)", TimeSpan.FromTicks(82759637));
					_chapters.AddChapter("CHAPTER THIRTY-FIVE", TimeSpan.FromTicks(6285240362));
					_chapters.AddChapter("CHAPTER THIRTY-SIX", TimeSpan.FromTicks(14428000000));
					_chapters.AddChapter("CHAPTER THIRTY-SEVEN", TimeSpan.FromTicks(9285000000));
					_chapters.AddChapter("CHAPTER THIRTY-EIGHT", TimeSpan.FromTicks(9938590022));
					_chapters.AddChapter("CHAPTER THIRTY-NINE", TimeSpan.FromTicks(18333000000));
					_chapters.AddChapter("CHAPTER FORTY", TimeSpan.FromTicks(19029299773));
					_chapters.AddChapter("CHAPTER FORTY-ONE", TimeSpan.FromTicks(27074000000));
					_chapters.AddChapter("CHAPTER FORTY-TWO", TimeSpan.FromTicks(44681116553));
					_chapters.AddChapter("EPILOGUE", TimeSpan.FromTicks(453));
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
					Album = "Altered Carbon (Unabridged)",
					AlbumArtists = "Richard K. Morgan",
					Asin = "B002V1O6X8",
					Comment = "In the 25th century, humankind has spread throughout the galaxy, monitored by the watchful eye of the U.N....",
					Copyright = "&#169;2003  Richard K. Morgan;(P)2005  Tantor Media, Inc.",
					Generes = "Audiobook",
					LongDescription = "In the 25th century, humankind has spread throughout the galaxy, monitored by the watchful eye of the U.N. While divisions in race, religion, and class still exist, advances in technology have redefined life itself. Now, assuming one can afford the expensive procedure, a person's consciousness can be stored in a cortical stack at the base of the brain and easily downloaded into a new body (or \"sleeve\") making death nothing more than a minor blip on a screen. \n Ex-U.N. envoy Takeshi Kovacs has been killed before, but his last death was particularly painful. Dispatched 180 light-years from home, re-sleeved into a body in Bay City (formerly San Francisco, now with a rusted, dilapidated Golden Gate Bridge), Kovacs is thrown into the dark heart of a shady, far-reaching conspiracy that is vicious even by the standards of a society that treats \"existence\" as something that can be bought and sold. For Kovacs, the shell that blew a hole in his chest was only the beginning.",
					Narrator = "Todd McLaren",
					Performers = "Richard K. Morgan",
					ProductID = "BK_TANT_000103",
					Publisher = "Tantor Audio",
					ReleaseDate = "22-Apr-2005",
					Title = "Altered Carbon (Unabridged)",
					Year = "2005",
					CoverHash = "55513db89e506c82f14bb23c261924a5cce22378"
				};

				return _tags;
			}
		}
	}
}
