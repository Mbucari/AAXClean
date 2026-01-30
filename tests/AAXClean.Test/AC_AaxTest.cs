using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AAXClean.Test
{
	[TestClass]
	public class AC_AaxTest : AaxTestBase
	{
		private Mpeg4Lib.ChapterInfo _chapters;
		private TestBookTags _tags;

		public override string AaxFile => TestFiles.AC_BookPath;
		public override int AudioChannels => 2;
		public override int AudioSampleSize => 16;
		public override int TimeScale => 22050;
		public override int AverageBitrate => 64004;
		public override int MaxBitrate => 64004;
		public override long RenderSize => 5872527L;
		public override TimeSpan Duration => TimeSpan.FromTicks(618328816326);

		public override Mpeg4Lib.ChapterInfo Chapters
		{
			get
			{
				if (_chapters is null)
				{
					_chapters = new Mpeg4Lib.ChapterInfo
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
						{ "CHAPTER FORTY-TWO", TimeSpan.FromTicks(0) },
						{ "EPILOGUE", TimeSpan.FromTicks(23459116553) },
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
