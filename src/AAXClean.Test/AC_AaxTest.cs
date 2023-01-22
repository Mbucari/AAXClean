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
		public override string SingleM4bHash => "c8839e38713f01107e7d2ed22bcf149e52a03351";
		public override List<string> MultiM4bHashes => new()
{
			"740c85811f0efd6d8f9b22afbc5b1a28632ee765",
"94afbecf03611239678f399608804e9314c1f7b4",
"2d7cb6e2ae725a8530385b2851ed4a97ed30a2f0",
"80b9e547fcb7d4e352d555baf2fe8a1ab9dc093f",
"ad4bcd4c5098b062a97c65497f86d562ad7dd62f",
"ebe2b59277743a1bf77fa523a13c6d75b3f7c52a",
"f3be132ab938fc8d1f3606a4cdae7a18d2d6eae4",
"dd8a802903c0f290e07fac13a113fa064568bd99",
"53abf9882a2b26adc71f47d6618999b9e42f5960",
"44b5914cae00e8342139bef918f711428bb5fdee",
"33723c9a26a699963c830ef578554fe48742721d",
"6e8319b9d6b7a1b34655628d0a0f85f8b1f71cd3",
"de16e631435bc0eb2628df255139401aae46ba68",
"bc819d88d81e8b6c44d0c104800d4bbc4edadccc",
"99dbd6248d476317fb5ef0ff5f8e19a3c4ffcb46",
"0e8dc9ac06e3a4154aa8bba3087cd2849ec293cf",
"7f6d772613b74cdca3b3c349446ea9be58e6012c",
"0cf93d6cb11da3b5619d579fa6b89f044c311e91",
"38a1d39a0e3fd56c17e60d519b7b8b564b54f5a1",
"d83de7e507a451a82e58204ebde1b131c3ffeb2d",
"85cfb3b1ce84ba2e6c9e0837006e3b5a99d22dfe",
"398c80c6729b62f3039fa214b76ac96ada388eeb",
"ea969e1eb669561611d3b98e3a881fa7e371b96b",
"4e2aef9d918aaae6a576730f59435770983478a5",
"fc8f3c8278c71a85db725a82667421f51b9a4a51",
"b9ea42f015b09341acb88ccf8831a6c44c048d9a",
"86697299a3186a286faa1477cc85bc5e9d591f8d",
"24b38bf1e8aea90cd596832bbddce3f5c6fb82df",
"7cb1e8c2c900ab93cfb14112692fc518ca15f3da",
"f8958e46bcf3d30e611193e1bbdc052950477223",
"17343bf9114563946c34f5b8f893532563743d1a",
"1768529fa1079bcae7ddaf70bbf56f8e14dd7614",
"88005e14b222768dbab9b50049b9eee441d95d07",
"ea5ec3ef2ad27fbe0f8aa300c681aa084f6e58af",
"7db0867d186ef750c1db354b2561766df2557ad4",
"0272782e702f4eaa34ec5a526b5574aab1607190",
"826662ca53b31a579f3548053140c2bacf4f9ace",
"b2c6a04a3edd006ab6e8fe1a1cb8dce8fb8ca971",
"80a26ff743f260479019b5d964481ced6d04c1b3",
"6082348311e5405a99fc2cc479048843436f7e9d",
"b24b7eef51a87c7391e6bde6e32eec1030943dd9",
"0e0fa134d361b8ee987cc95eb38f6ec1b943d488",
"2cf94917fea695fdaeb1d41e41ef03da5c225099",
"9c1765d2fc5e76e842deb9caf15f7f3694c33019",
"11b7425ad76d5e34ce6c412194d0bfccbdcce5c5",
"2a61b06c1d72c349f58eabfe255030c4bccd999b",
"59cb2d08bfaae87bf5eaa84f69da4b7d7e8e1dfe",
"9b3f236df1fd31210bdedad23aaa1c70d6f06122",
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
