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
		public override string SingleM4bHash => "da093bba71aa18b3392393146fc77a4c08cb8fe5";
		public override string PassthroughM4bHash => "e86a92a00b360f56fafcdf2f8d0e847032c3043f";
		public override List<string> MultiM4bHashes => new()
		{
			"730e0943a2de0b85d730ef3a1dbc5eb4c45e32c9",
			"986ab033293a9ab1a49c72f219b26fc20aea4f55",
			"6df9cbc0bc7d5fea2e87ba71f4d83f8141220b3b",
			"386e41e35ec45d49fa5bf37a8b51dfb815ebf7c1",
			"9b38c7d51b2df880b5662861a06715c9fd19381c",
			"46359400d8d6e7311302efc84fc030875923ae8e",
			"09fcf3f1f6f1cb8fbb987a9a05a6fe71b37a96e8",
			"69582b530fc8985901b47407755496c3721ed831",
			"6deb8dc92627e3918af11733f994b364abb7cb91",
			"79c799e9992ff5988c64f945abe7bffb2872d7e7",
			"d58bebbd6c016fd5fa2e90eb20aba2423a4edd3b",
			"c0c82b0fbd7a231538238f3fdb47fd1dcd7b05cf",
			"60ccbfdacfeb529561d4beb215d1bbaf0e95acef",
			"0051024719a14a7358dfdab0e88fc9d96e62083d",
			"cb33a1c02d9c809ae152dd7b1fb8aaa760b6faae",
			"cd47a3d314ce711e74f049af602756f3d9c69035",
			"04e08a32c39d93e9856753bf135836d55a7f01ab",
			"cbecea7831020a4efe0a0f5e2e070bd6bed62592",
			"d1da19af62bd3b02ddda1e5bc5a8a7030198fcf4",
			"2b49be60a5d11fd3c3b61c53175ec9e0b1fb85df",
			"7d33aa20121115a818c24cedf21a52588617c723",
			"d7ed2838976ad388ae868a3df7f6186f36730252",
			"d610f43f1d934acda2f7a87ba10e2f5998cc8cf8",
			"456cd12ff04026d41157209048097436ef2e9478",
			"0ad5be0a4d2ab8c1a45c99313e998e4624c00120",
			"ff6f6173702ec38b681f1efa4112850aed605bd7",
			"157e7bf72858ce8914fce989a2be0148941f7c13",
			"3dde231abebceda4c1a635df21e517b1368038e1",
			"fd81bcf35f9b66129a9835efcc5ee1bad1e72756",
			"9e318f82e98e8648869c9e92e6d8c39ae3cf21d0",
			"79e38c72365e75a75d45d74396d1de8334504a7c",
			"dbadfc3ff93ff55e696be9e8688e40507475416b",
			"3c4b2084bf98992724112a952eb6b5c768db97b4",
			"d5f285dc323e0056be252a23e829697a6cebdaf5",
			"7047ce2785452fc3b680ef212595dd7803af80e4",
			"8d78a2623b65dd600316c80df3ea76eb0e7fad6e",
			"1568e602c2780a7162d0731ed4bb09b1dc7f34c4",
			"3e1836ffb2ac6af939583532ba436927dc54ad80",
			"76f90d3ba3b978b477fe716f5bffcfea7c11a6e2",
			"6f34511b88c2d8e2ea71f71ff77c1ebae0081367",
			"aba74df232638a110cc04261e9f2d6ad618f09bf",
			"a6da7a3f130c7f14c548cb787e25003ec1bb7d45",
			"be5748deab7806cde997807bd04eaf9b7aa93b88",
			"1cb8adef4788bd0dd904b223ab9ae75ae3aa33ee",
			"1e23ddd365c78affd2bf05f95f62b6b1adfca6c1",
			"b2e2841b1ed363feff30047b242f1dc2981b8a8f",
			"def9bd26ef3690b51a1ba1fa1d46c82856913d9b",
			"b16552bea95f041015ef89b1abb412f82a474124",
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
