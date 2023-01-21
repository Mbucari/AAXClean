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
		public override uint AverageBitrate => 62794u;
		public override uint MaxBitrate => 64004u;
		public override long RenderSize => 5872527L;
		public override TimeSpan Duration => TimeSpan.FromTicks(618328816326);
		public override string SingleM4bHash => "f8e4d456230b4af03aee859bddc42f01aec1dca1";
		public override List<string> MultiM4bHashes => new()
		{
			"e40b3d9233072fe48c3adb7d2f50effe42314947",
			"908456c6d924034a413609d154a2e5376b530782",
			"ea1b9abb378ca489e90a2b59da70d818b0a572f3",
			"5ae8334fc7ea6e333ec5b2ca8b4fc5d7a2a63fd0",
			"81c48f3a185c37615ab41f6660547cb2741d5213",
			"11d76af47bf358ea6dbd86400feb34f5581eb237",
			"3bbe853ef53b598882b85b65694bc0f2e8c73af4",
			"701092274034aa2160f23a66459c36f681a077d6",
			"9fb7ecfed6cee3e30761b1af407e5332ed9b08be",
			"ff22507a47899954f16ceec1cd0602434f9baaab",
			"4cca7aca344ad7f27537704a908d9cc6f226e465",
			"2fcd170d1d03c093ae8771f5c732d24efc916787",
			"d8db5ac71760b3ad2e094069578c2bb7f8971c3c",
			"e8263d83e262f69414db4e08438e83eb46c3c439",
			"34dbf8b6895f8d9c66b26fc20c5376d60e5ae6bf",
			"65369fa4e9de31387c8ca283ea227ee46dfb4dca",
			"385cc1e65907edca4c443cd5053be657ddf830a6",
			"1fb80927f6544eb6b9b4eff1c8448ea272f058c5",
			"53e5692ba30022f7ade3abafa8addd3a8cd4d03b",
			"22003deaee1d251cfeab4938797cb6336f8fba37",
			"ff9b1da8ffbda9c27730396504d37649a2ccf171",
			"15f6c342ac2a90e857b78f089fca6e420d6393bf",
			"bdaed48a1f2f3c88701a8b8e58893c34417577c4",
			"3ccdffd8c3aa718af83cf2013a2839502a567f6c",
			"57615d2c8ea65f95205cc39add80e214d24b06f3",
			"7fc2c6e757266a284426a38b8db07fdd0755bd95",
			"af9b8ec7df1d0d53b4887595c43d31801b99b275",
			"a1c3726d650585cfcdd76dd33b6fe27e066d966f",
			"13fc4758d18fe57434e98dec7f2a18b8f5315e5d",
			"dd27d1e0e394cf1a02331dde7fa136f488782998",
			"086e816866008c0fd0cb1af76b0554ef09756f7f",
			"057dc430fdaefc2265b2278b858ac4ca4afa41aa",
			"4b7d69779d0194ee8e6069400c6a6c02cd7b0fdd",
			"66239fecc5a9be266c2a2ab2b7dcf962e8390958",
			"81c939f6d3d03093c713c355bcc6138152762ac7",
			"14dd3507bdbde2781a94eeaac35dac8ae50789fa",
			"734e0a8e6b216564ef3b485ba2acb1529bf20014",
			"89996eb9a2dd532251c4e833f76f32d49cdef73a",
			"0b13c5242006061198f218bb5dc11c74fb757a59",
			"f2374dd4fb3edbf18e4d28cd20ae3711b3e18856",
			"0b59237cee8da6d89884c961042cf2820c5bc19f",
			"b7e6dac55648108e25e4ef47c6884b490cba9dc8",
			"14185eb3215f0ecde964b3f889ad62c517bd9408",
			"a1ad60443506ce097236e1908649ac518207fcea",
			"8fa14ec7c642e8c120a77960fbaff71bb11ba78f",
			"bb5390ed219b08aaf137e0cc2d22279755aabb7b",
			"441e48f724b2ed974d0ea4c5689f661cf9396a31",
			"a49ff74f0ec70029c3b7ce019e17264b3863025b",
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
