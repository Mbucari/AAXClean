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
		public override int TimeScale => 22050;
		public override int AverageBitrate => 64004;
		public override int MaxBitrate => 64004;
		public override long RenderSize => 5529703L;
		public override TimeSpan Duration => TimeSpan.FromTicks(581463887528);
		public override ChapterInfo Chapters
		{
			get
			{
				if (_chapters is null)
				{
					_chapters = new ChapterInfo()
					{
						{ "Opening Credits", TimeSpan.FromTicks(1789039909) },
						{ "Chapter Four", TimeSpan.FromTicks(0) },
						{ "Part One - Injured Parties", TimeSpan.FromTicks(30000000) },
						{ "Chapter One", TimeSpan.FromTicks(8928000000) },
						{ "Chapter Two", TimeSpan.FromTicks(13649000000) },
						{ "Chapter Three", TimeSpan.FromTicks(21807489795) },
						{ "Chapter Five", TimeSpan.FromTicks(14563000000) },
						{ "Chapter Six", TimeSpan.FromTicks(20003150113) },
						{ "Chapter Seven", TimeSpan.FromTicks(14130000000) },
						{ "Chapter Eight", TimeSpan.FromTicks(16952229931) },
						{ "Part Two - Commercial Considerations", TimeSpan.FromTicks(350400000) },
						{ "Chapter Nine", TimeSpan.FromTicks(8978600000) },
						{ "Chapter Ten", TimeSpan.FromTicks(16626000000) },
						{ "Chapter Eleven", TimeSpan.FromTicks(8229879818) },
						{ "Chapter Twelve", TimeSpan.FromTicks(13901000000) },
						{ "Chapter Thirteen", TimeSpan.FromTicks(10049000000) },
						{ "Chapter Fourteen", TimeSpan.FromTicks(12620040362) },
						{ "Chapter Fifteen", TimeSpan.FromTicks(17980000000) },
						{ "Chapter Sixteen", TimeSpan.FromTicks(10099000000) },
						{ "Chapter Seventeen", TimeSpan.FromTicks(7695519727) },
						{ "Part Three - Disruptive Elements", TimeSpan.FromTicks(264070294) },
						{ "Chapter Eighteen", TimeSpan.FromTicks(8715929705) },
						{ "Chapter Nineteen", TimeSpan.FromTicks(10213000000) },
						{ "Chapter Twenty", TimeSpan.FromTicks(16245760090) },
						{ "Chapter Twenty-One", TimeSpan.FromTicks(13961000000) },
						{ "Chapter Twenty-Two", TimeSpan.FromTicks(10833000000) },
						{ "Chapter Twenty-Three", TimeSpan.FromTicks(13009479818) },
						{ "Chapter Twenty-Four", TimeSpan.FromTicks(7869000000) },
						{ "Chapter Twenty-Five", TimeSpan.FromTicks(25449310204) },
						{ "Chapter Twenty-Six", TimeSpan.FromTicks(11805000000) },
						{ "Chapter Twenty-Seven", TimeSpan.FromTicks(7354000000) },
						{ "Chapter Twenty-Eight", TimeSpan.FromTicks(8849000000) },
						{ "Chapter Twenty-Nine", TimeSpan.FromTicks(9322259863) },
						{ "Part Four - Unexplained Phenomena", TimeSpan.FromTicks(236090249) },
						{ "Chapter Thirty", TimeSpan.FromTicks(17082909750) },
						{ "Chapter Thirty-One", TimeSpan.FromTicks(15647000000) },
						{ "Chapter Thirty-Two", TimeSpan.FromTicks(15369120181) },
						{ "Chapter Thirty-Three", TimeSpan.FromTicks(16369000000) },
						{ "Chapter Thirty-Four", TimeSpan.FromTicks(18188000000) },
						{ "Chapter Thirty-Five", TimeSpan.FromTicks(4922029931) },
						{ "Part Five - Divided Loyalties", TimeSpan.FromTicks(30000000) },
						{ "Chapter Thirty-Six", TimeSpan.FromTicks(11143000000) },
						{ "Chapter Thirty-Seven", TimeSpan.FromTicks(14985000000) },
						{ "Chapter Thirty-Eight", TimeSpan.FromTicks(24184720181) },
						{ "Chapter Thirty-Nine", TimeSpan.FromTicks(10161000000) },
						{ "Chapter Forty", TimeSpan.FromTicks(11759000000) },
						{ "Chapter Forty-One", TimeSpan.FromTicks(23966349659) },
						{ "Chapter Forty-Two", TimeSpan.FromTicks(28971000000) },
						{ "Epilogue", TimeSpan.FromTicks(6177507936) },
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
