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
		public override int TimeScale => 22050;
		public override int AverageBitrate => 64004;
		public override int MaxBitrate => 64004;
		public override long RenderSize => 5872527L;
		public override TimeSpan Duration => TimeSpan.FromTicks(618328816326);
		public override string SingleM4bHash => "3ee052f276ff281b6ed11e6c7f84a26ea4f3a5f2";
		public override List<string> MultiM4bHashes => new()
{
"0e19d1581682d4c4c47298d438af88d445ead893",
"5612712c34c27c503fc3b2f5764a89e16daccbed",
"c7823b8f701b960768b0231e64131853337b9a55",
"7d2bd8d0a013d9b859e023fb9ffaaf3233426c94",
"505dfdefbdbcf5340babb62d0a087cfac83590a2",
"2af51c5a2fa8ca0f41a4794777379e7029d7417c",
"4d1b5e883eacc98030bfee0770d7de3add1c1e70",
"a6e0c52551f317abba5ecf7de719fac4a8849ea5",
"3c7f35daaa5874bbfa76bdfbef7608aa721b506c",
"207decda88c733f80d21fa7fda70d2796761359e",
"5d12b308a458f1111b48ec5acc4f901f6fa88844",
"32b68f11a9506e68308eb2f88c054cf5061de1a5",
"ef735a46c0e6cd7272a03333bb2b47123a91a223",
"65ef266561d01c59f60ddda01440bf077e7e930b",
"f365192283d3e33699c624c2eb37264629ed8e0b",
"54372e29bf23fcfc095088c82d8bfe1c2f630b0c",
"8ac965e0e49c343ee1cea98064c6c77ac19f3686",
"e11d547ade0443d87d4294d68f738919156f6641",
"2df1e4638253e1903be2bc60b01103f96cfb977a",
"6ec876238141ac4bec063cc1795fb180f4560cbb",
"7e8aaeeb4221e5720b2b18fa1b9838e773c71624",
"580370c5951fce7600c00d1823d295d658b5fa82",
"47f69570dde76c32d286ff77ab98ba7f09dbe1c3",
"cf1808f89b599944ec6624c6403d8f591dc5cd9e",
"7c454084ee4f250f07ef321525806a8fd0519b66",
"a134fafa346b5db9cb07e9701bdf3edb8dc22081",
"c66188b49d01379e8a82fcae01914a807a0714d3",
"f5862c4b581ca45aaba70c070fe440acf12f5bca",
"2b63217736ef4a07e54174cd8b4244975df2a1d8",
"9b4f65e69fa76b4b84efc17ab78d4706c853965b",
"a5e0b08f9ae6f413dbd83d6039a1e11c5b5d229f",
"13dead88766756e6f41dcce6252973e92bf724ea",
"6a56968908d640cf6f3b274245143e8dfb8620c2",
"8ba7fcc58ff68ad26a1d8639248b4c1d841ef278",
"97982dde1d766d1259d22bd86e6a7c98946bea0b",
"50d4255ef43f1cd20c522ced6aaffc139449b0c9",
"43058c2b50b208ea42a19cbde7c657af4ab07b6a",
"a96423a020afa85278cfb80ee0be826072750d4b",
"f430acc5b6a9b51515027781a32a0797670a2200",
"5301579443b93288a57523b8c467fc5c3652f453",
"bb161a84e4e6ad40d6a6d6c7aa5159ebc48b2ac4",
"c0bee7d84adecefe402c829f7598bb226fb062e2",
"eca8ea6b5fe1dcf65ed00009acd06ae283f1e366",
"783f5ffc52429c41873fed5efd38525e4591db0c",
"374a6cd914f762b8d9e1218be060cc3d7a6b7c45",
"6e4bb8fb48f3cac69f2d59acf30cafd3a84adb1d",
"39ac84af498a0bb1c3d619682181ab14df7c3f4a",
"648cd427bdbf5bd45a52e2adf23a840dbc20fc7a",
"659a6167b341e3c7c8b098fa361f8053719cd30d",
};

		public override ChapterInfo Chapters
		{
			get
			{
				if (_chapters is null)
				{
					_chapters = new ChapterInfo
					{
						{ "PROLOGUE", TimeSpan.FromTicks(7327059863) },
						{ "PART ONE: ARRIVAL - (Needlecast Download)", TimeSpan.FromTicks(32380045) },
						{ "CHAPTER ONE", TimeSpan.FromTicks(10467600000) },
						{ "CHAPTER TWO", TimeSpan.FromTicks(11490939682) },
						{ "CHAPTER THREE", TimeSpan.FromTicks(25143000000) },
						{ "CHAPTER FOUR", TimeSpan.FromTicks(8356000000) },
						{ "CHAPTER FIVE", TimeSpan.FromTicks(10298940136) },
						{ "CHAPTER SIX", TimeSpan.FromTicks(2717000000) },
						{ "CHAPTER EIGHT", TimeSpan.FromTicks(13314000000) },
						{ "CHAPTER SEVEN", TimeSpan.FromTicks(35002750113) },
						{ "PART TWO: REACTION - (Intrusion Conflict)", TimeSpan.FromTicks(86259863) },
						{ "CHAPTER NINE", TimeSpan.FromTicks(14781740136) },
						{ "CHAPTER TEN", TimeSpan.FromTicks(17825229931) },
						{ "CHAPTER ELEVEN", TimeSpan.FromTicks(15079000000) },
						{ "CHAPTER TWELVE", TimeSpan.FromTicks(19101700226) },
						{ "CHAPTER THIRTEEN", TimeSpan.FromTicks(15345000000) },
						{ "CHAPTER FOURTEEN", TimeSpan.FromTicks(10000000000) },
						{ "CHAPTER FIFTEEN", TimeSpan.FromTicks(17892879818) },
						{ "PART THREE: ALLIANCE - (Application Upgrade)", TimeSpan.FromTicks(105340136) },
						{ "CHAPTER SIXTEEN", TimeSpan.FromTicks(18666659863) },
						{ "CHAPTER SEVENTEEN", TimeSpan.FromTicks(21703170068) },
						{ "CHAPTER EIGHTEEN", TimeSpan.FromTicks(14506000000) },
						{ "CHAPTER NINETEEN", TimeSpan.FromTicks(12753299773) },
						{ "CHAPTER TWENTY", TimeSpan.FromTicks(19688000000) },
						{ "CHAPTER TWENTY-ONE", TimeSpan.FromTicks(15225000000) },
						{ "CHAPTER TWENTY-TWO", TimeSpan.FromTicks(8819000000) },
						{ "CHAPTER TWENTY-THREE", TimeSpan.FromTicks(4922000000) },
						{ "CHAPTER TWENTY-SIX", TimeSpan.FromTicks(5305000000) },
						{ "CHAPTER TWENTY-FOUR", TimeSpan.FromTicks(12026000000) },
						{ "CHAPTER TWENTY-FIVE", TimeSpan.FromTicks(28400400000) },
						{ "PART FOUR: PERSUASION - (Viral Corrupt)", TimeSpan.FromTicks(78330158) },
						{ "CHAPTER TWENTY-SEVEN", TimeSpan.FromTicks(15110669841) },
						{ "CHAPTER TWENTY-EIGHT", TimeSpan.FromTicks(8480000000) },
						{ "CHAPTER TWENTY-NINE", TimeSpan.FromTicks(18785900226) },
						{ "CHAPTER THIRTY", TimeSpan.FromTicks(9363000000) },
						{ "CHAPTER THIRTY-FOUR", TimeSpan.FromTicks(11214000000) },
						{ "CHAPTER THIRTY-ONE", TimeSpan.FromTicks(1649000000) },
						{ "CHAPTER THIRTY-TWO", TimeSpan.FromTicks(11734000000) },
						{ "CHAPTER THIRTY-THREE", TimeSpan.FromTicks(17617560090) },
						{ "PART FIVE: NEMESIS - (Systems Crash)", TimeSpan.FromTicks(82759637) },
						{ "CHAPTER THIRTY-FIVE", TimeSpan.FromTicks(6285240362) },
						{ "CHAPTER THIRTY-SIX", TimeSpan.FromTicks(14428000000) },
						{ "CHAPTER THIRTY-SEVEN", TimeSpan.FromTicks(9285000000) },
						{ "CHAPTER THIRTY-EIGHT", TimeSpan.FromTicks(9938590022) },
						{ "CHAPTER THIRTY-NINE", TimeSpan.FromTicks(18333000000) },
						{ "CHAPTER FORTY", TimeSpan.FromTicks(19029299773) },
						{ "CHAPTER FORTY-ONE", TimeSpan.FromTicks(27074000000) },
						{ "EPILOGUE", TimeSpan.FromTicks(0) },
						{ "CHAPTER FORTY-TWO", TimeSpan.FromTicks(23459116553) }
					};
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
