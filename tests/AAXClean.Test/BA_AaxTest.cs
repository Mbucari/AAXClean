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

        public override long RenderSize => 5529705L;

        public override TimeSpan Duration => TimeSpan.FromTicks(581463887528);

        public override string SingleM4bHash => "51df7afca5241a8cfcb094c34fa0efd19af50a47";
        public override List<string> MultiM4bHashes => new()
        {
            "515abd930c5a58ce43394867823b122594cfee8a",
            "e1041581c4f242668bb9480bc17a33784cedf9fb",
            "80c94c370959fc74fb439dc87416aa26cc632fb6",
            "31c28088e2c87f69105f5eea1238882f446cbd9f",
            "d4083492a5ef0f5a05bb64d9487065a16625187b",
            "4423f649af2fb4896689bf40f042f98a1092a839",
            "6ee8d06b99759e474ac104dc91028c4c65458edd",
            "5165cfc000fa0c3ad267019d3f640a8d36fd7b72",
            "bc4f438923d96a2b0341e7ab0d27cbdb754e4279",
            "c1782a391e2871c63b5addc6a324725ee9a604da",
            "3c342949843cb8f7def73c62f609615b6d7ed7bd",
            "472cb433b9e8c76e9b6d3d8f2dac2779edf628ff",
            "fc5f6aa0a81f4b11cec31d3c9094cb7c08d8acc7",
            "a302ea1427382bb2b0f40ebab14ca3417f016d57",
            "c7acfa6e78d68658931c71bb104c61dec36d2d63",
            "69670ee1b568ff78891c73be17e8cdf643777b24",
            "3194776982fe2a10df09d485fc7def9f3969c5cb",
            "6b2dec61374e0e07e2450dcd4d93e0a400c148d1",
            "9c991b0ff444e9b83e308c9b384bb477f6399429",
            "5ea254cbe388d5a41f4d2970c5d1a17ff8a4ac42",
            "95ced2a6a48fd00af5ec6defe92655ebd60972ae",
            "7f60c89ebdac9d281e9f5cee55941a25cfc7f8f8",
            "5d91b8d54062ba5081d30ed69fc3853a0dfb388c",
            "39f6e05b457287c16a39fc01cb7bbcd99d78e60e",
            "7a866d18fd060399d243a50ae6cf72ba70cf28ef",
            "29f23dd24d0029a8fc9207793917363c06f181ea",
            "7f305f516aa2178caa19646d0acfb674b285c7cc",
            "b29c7288baa32c5c7249ba8225efd8aa9e2a4ca0",
            "fb6a68a74326843d9d5dac50adcf09a690329bfe",
            "df04556b84dcd017acbaaa6d8b779f417b1a0f5f",
            "52131914abeb97df0dbf7a09427fa89406ba6c1c",
            "4207b10791007a93414aa2d63fb12683f31aa9c6",
            "0a6f93e795684618321b478e27f1ea7ebe7545a9",
            "a194caf65a0df36b08464e32a86e30ffe78985a6",
            "96ab7953ac98dcef545831624b8bca77fdd13e5c",
            "9094a04f3fbe016b223028f4342d7ff4e0441487",
            "bd0284fd8796ff2f2aca26b461b1abba52709e9e",
            "16d28e7ee0e9074ac38ad8320272aab967f6b41b",
            "f156161e6233253de1c6eee6f037ed9894b2edb6",
            "4f98a207f79edefcf880678d877eb5c403e9481f",
            "d25ddda3acefbff9784a5d0ba95d534950e862db",
            "58a957debc5664895b8bf87141dda58e7649073e",
            "5d2afff7892b0c95bb49caa1cdbb28cb5fe14cff",
            "5d579c4425094c4212edc2b73fc24d136a9f668c",
            "c6f6635d751baeba31e8e7fc3f0121cd46eca1cc",
            "4b4a86720495b6030fd8b48470bc11c63228879e",
            "481600d77bb23dc7acb1eb257928e7a3ba3000df",
            "001e7b7e429bc064d06883ccf8a4a9e41cfcc316",
            "2441d3cf5d6f0c2e369222d0bb37bfc585ed0aa5",
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
