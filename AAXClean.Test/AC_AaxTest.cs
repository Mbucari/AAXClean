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

        public override long RenderSize => 5872529L;

        public override TimeSpan Duration => TimeSpan.FromTicks(618328816326);

        public override string SingleM4bHash => "df6767d2c9fc9b5eeb541a2ef9f4fc4766f1b5b9";
        public override List<string> MultiM4bHashes => new()
        {
            "7455032bea3a1961a7b6c4a715d824f643b06f87",
            "dff8ff7d0c2a92238ff35b9c7a5a3b83c270cab0",
            "d97f504bb4570e62cd526034bcb36f66401de617",
            "c99dfa67273d17d663e00b3830cfcfced1c75004",
            "fc3afa38c0965b9c1a12cf4c5656e597ef501afa",
            "c7b27a4290a37612aba66bca98754ada02788f53",
            "00a953722c5466b5ce692eaab05793496c8572db",
            "ee7323c4b2d518c9b90617ebb3fb760c51692b3f",
            "ed6dafe341bca5efdf1b7babf9b33ee58e87e084",
            "9cdc4c4e11c4fd2f262d000d0eeca12b5dbd8f8d",
            "a00ed2b6139de25ba6789b1c98236ee1adaaf672",
            "c0a5c1706899e859df5196773474c6715ad9b87b",
            "63733367b63161d7913ddf7c943bed91d8c36199",
            "491bff078fdde7fcdbbb6fa99b242ed4d80e52c7",
            "8247350b3a6773b0e9f61106d0989856d930b1cd",
            "a795689d085fcabeff47f7bd9ab999634a96e7c2",
            "30bcf46e4ebd7aef21f9efe406c805956c3ea89f",
            "9aa10f706d3d9c7afa42aa72d5a5d8a973de432d",
            "00d26d20808ab09e39b551dcc9e0f6d2bc836e57",
            "7410fb6cce4709dcfa730abfadc030889c913889",
            "9ff0f607c5a190daf78bcbdad7688ea9250b19b8",
            "c18d9b365227d31d5363648d3a1d99db5338a404",
            "72492ca521a3392cc50585ae121318e00d5bcc63",
            "69078a2afc47888ab77ea77dee5cb44d456fe0b2",
            "68423fc0a0bbc74fd285372fd934b41d74fff3ab",
            "f6ab20c908718b2e2cdd41b1864eebb8fb2edf86",
            "7b60aa9f8a7a40cd6b8776c10a2eb2e389415ddd",
            "d0a9fbff2fbc5b63371e308c000ce7f81fec057a",
            "5f20f81627ec2358fe0b5590fd6ba274a2dbbd0c",
            "6dd198dd21c8c2aadca58166585d0110d012045c",
            "80b74a857502bea397ae7ac74751c91549ad20b7",
            "2a4085e13e325f0223c408f7ba6bdbb3a6816557",
            "536068b0e8b691fbd7f0cd44afab10c96a0229ac",
            "1dc8350138e9710c1010427c1df532cbdabc0ce8",
            "1ba4c2d10f401bc0c48b84d43eeceb7f117a7d1b",
            "a259d9cf32f48c991e3c09f33e976fec956ac3ee",
            "1723e161077d03a9fd9d981cbf1cecf9d6453bf9",
            "4a3afe0c6d60a880846adbad2bbc947c8d6b9fa9",
            "5c8af08a212e454fd378848f4c36579fc7beee0f",
            "fa14b4f63f603e2ebd24ee626d11b6810988bdbd",
            "8a2606b2d7024f3b5c6872fca5a3684dff39fa0b",
            "a187d4a2ac603ff38b3cac2f574859d71cb298e0",
            "2e33c881f07f4bdd789e75fe688a93a44a54f5a1",
            "b59045f1cb60676767a24dc60db5bbbe0d634a83",
            "373062129a8a92ccdb18ef911b1f902598987b44",
            "0381dee3b52571da751284d93becdf7e9f9a39a4",
            "a61961264d3307ec64847759b59f058451a01946",
            "a0ea39884968f750dcc65933e4c6879ff48c37e7",
        };

        public override ChapterInfo Chapters
        {
            get
            {
                if (_chapters is null)
                {
                    _chapters = new ChapterInfo();
                    {
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
