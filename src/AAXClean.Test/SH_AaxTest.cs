using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AAXClean.Test
{
	[TestClass]
	public class SH_AaxTest : AaxTestBase
	{
		private ChapterInfo _chapters;
		private TestBookTags _tags;

		public override string AaxFile => TestFiles.SH_BookPath;
		public override int AudioChannels => 2;
		public override int AudioSampleSize => 16;
		public override int TimeScale => 44100;
		public override int AverageBitrate => 128008;
		public override int MaxBitrate => 128008;
		public override long RenderSize => 64416191;
		public override TimeSpan Duration => TimeSpan.FromTicks(3421559002267);
		public override string SingleM4bHash => "9d71f8940bc420ada97c044f68c2fe8d426eee46";
		public override List<string> MultiM4bHashes => new()
{
"45ae0882db76e7eb95dee47bee968a24dba9411b",
"06681214a2a78310c11fd994e63189574663b837",
"290a82979060cd132ddf8581af6e15c0ea9b136a",
"7153c095284d28971c6cd34f86c0521d5fd09535",
"cbbe2676da71a856042f26859726bb3d8a58450d",
"e576c0478cf4cee3d107704c9910bc3f69e3ba8b",
"cb09f42f2e512a20328894cccdea9cbb4c032613",
"bb39a8170a78b55b38a30aff9b3ee111afd2699c",
"d0543dff35bc80483e62e698295d41abd12c1927",
"78c708dcc62d65c6bf9449dfd29f1e78f1484c1d",
"35ee10b9fc7853e2e5e262d2618290228dc75725",
"a236956f4c116d35805c230ad946f646c43910ac",
"7586560c9c2647c1416132979850451babca0b6a",
"b001d4b0892f33ac1c0e42feba83b640ba387ade",
"20b949dc3c1894feeaca20d7b5121d9e1bcebf25",
"0bd7fe48149f899b39b3616026b01e05590859fa",
"52d03e16b3a095e8e6a7f9a6717b31467d3794a8",
"45cc26cd1d22b878846612dd07f0cd13eaecde4c",
"304fc52e86a7bae9accc9c8b101bdd278c6728ce",
"9fcb53b27ef6e0cd9480a18a254bdf40bfca187c",
"8d82a61b558a8708a789e58bf54ef5590e784fe9",
"ff2d8daf3e13639f2a16310b2bdc14f1adabecc8",
"07cc0eb830a9cc1f949f1106be0acaf29fb6a941",
"93b2ece2aa8576e200bc89b6bfecc9c12e0c5480",
"5bf26fb3fef1ce6ab8876df267e340ab31ada1d5",
"462b75d9f22c3c6d21d1b66577a4907954eb806a",
"87a8089d991c55bddae150ff83f3e2ec353fbb6d",
"3574f2fd43ee1be221f272562f81b058e71dddfc",
"3bcc9ec097783bbf2220aa936ae667dbefae36ff",
"268393ff9965ef3cf1255943aa239b0077f1f700",
"da3023827bcc732b62d1cbb710e6f52c04305394",
"361e8fc240884ca880d2433b92d9053b4d489e36",
"513b2034e317cb39cc98975d3fac530f8ba9d05c",
"d0e1a7171677493c2adea4c358c5404b8f361083",
"183b47e6073d8fb1f57dd7b2b527931b362cf0ba",
"11c54ee0f3aa3ef96c19ab91d7b6d3421ab3cdff",
"a6e84731c1ee857bfcb6e22fa9dd1309d663b9a1",
"83bf117e9b5501ba476b8395df0f5cd9c47e6595",
"2e7f30575f84082687715c68afb6d83e5594c456",
"9455f37947530cbd8c0c0ef3350def1655a7fcc4",
"825231abee8b3f97eb3efdbdf5b3a2cda45d3442",
"bd24f824f6ce4a6a8afc6d416c3247adeeb5faf1",
"207f62d882ca080b42a300ee7ab4a7024c613c4c",
"76aa064ff4ee806e6d4f3e4385ad6ede9aea8370",
"3f3245c72864430a1574d407df75cb28131107b0",
"d8bb7768ea47117f1e818b31828a540053a6d4ae",
"9ee4f881129aea1a98c9726dbc44f76a1eda5e66",
"ed3ff391f5c3242ed76c73629b37c587bcac5aec",
"bba81b6f3606f0eed95f5356c03508caad55cbb6",
"573176cd3e96789a3d6aa0bde16dbec9149be81d",
"e3778b82b941690dbffe8703db2ac6ea990f63d3",
"c543b924c1291dcbebac58f460a3945a38776c9d",
"c20c5bd331b51c67c0ec8ca604873d39f5bbebb4",
"6296e24d3fadc57e0c528abce0f3821e19d17cc7",
"3c4663a5634f88f67a4b72f9c94dd395651a2554",
"b5c47c2dcccc2991bab7b4b444718e8c06f1f29e",
"fd9824d385cafc6e4671aed5e62560a7579f60a5",
"68ac1f69220aecdc7f03c3bd5a2b063fb8902513",
"346edb773a83256325406f6a6de350bee21597f1",
"710a25a3f8de464c9869a5c10e3fec465a71e816",
"756b346549955b55c6e66447b73bb49ff5996798",
"ba67dad647c71effb9f415fd8a15b58e645eec3b",
"0b4dd0e9fba6f26d0e694e980dd85d71db95cf03",
"acd01f8de00b46e32dfe446574c58867c59f466e",
"92af370007465e390a72cf47a2328e64340aa743",
"1911e697c12d9f4a8d992e1175b3bb61ecad4d63",
"264b803bae4496c63b76c45e91fc8b19b6859d9b",
"eb586e1ed72b8528dfd836bfdb70b994f610124e",
"d3152095685fb9c39a6768a3f252522ad0671a82",
"a3012c8ce9fdad876c047e97c6f0ae8f4187f028",
"924e2b58abd44db8345a05ee29c8f1ddc3297567",
"2c7e0e03c7bcd6bdc009e56f44a3930e1d67e16f",
"5dd8774432f9a53562a80ed4c87e8c7efb2200d3",
"02be919f19a965b84d294e6baf1236639456eb8c",
"4423724785328d6caf8ec53476fc09f2ee7fda7b",
"18fbf900af8859082a3f9147301bcaf325e7c9ab",
"dc6b1cc1809f2ee504893ba6039d1b1173136697",
"f2f39d4282da1b40a3146856809edb96f98a7907",
"047989395e30a3e7c3514d8a8b8895e211f265fb",
"6360ac3140acce555a03c62cdff4be41b386c8d7",
"32d544d1a58ee087179040784cfd2b699ae57548",
"97c1ff19ae622177b3ee2f46b84b6aceea98a085",
"95dfff6a0c6cfc512ed90864b1060b4494b5a509",
"d33c194c286c701327c900815ee5a6dc22903ad3",
"decc5a4191760c44f0e76819c9813af20627ce39",
"51096094af125046e226e1c7bdaec147008fdf46",
"5b490d2c3d1cd6f25b3dc9d9658cc52c0a8c8a2f",
"97c485605a7b5cd41a395f3a8ddaae3926ef4854",
"5c2025acade395959923d9df3e4ca67b5a48b83d",
"cc1c3dc5e69a256350ae808990602017c4253161",
"b9980688724d4d86682d00d3d06777afbcacc93a",
"8975c85941886a98ed9599228394a2d4daf34bf0",
"e1fd34303114bd242314eac4e86a31dabce72934",
"8511ec2b6e120802994b50b979a87fb1e59ded9b",
"83e6e2611dfacbf154bb067b12454ed975911810",
"9849aa590af79e94b169a424bffb241c896a0d83",
"8bdfd736e314e052ab3beb737ef4a752687cd7d8",
"76dc2bf456e97af717c49f18faffaa2a0d70b7ec",
"3e78ffbe0f9411482124698de8a9cd2356941587",
"9ab55dabdfd81159049d2c07bb0fad1a5ea33a31",
"0e17e15e866e51b009352d63538b6819a41c79ec",
"16e032823346b0e2caec12b4cbbde7249f0e5cea",
"fdf89335daa294831bf3c3974893c0e07f49e375",
"c4f4e11617765e0862f24b5ff4c6e5771bf8f321",
"3c7c53c5e74cba0bcff43ddcd5ed9fcf7ef604c4",
"bd3286e020f6026efb3c18a871bac6d0a08f9ad5",
"31c3ec00337408bf01c4bc8970b09c1e151167a9",
"2670c6d839c381f03f0942d752487aa85656d035",
"772547e6bfdd1d96396d00caefcaab7e7b8ffcd9",
"eac233fdc2baf18959c8cb051990acbd986bf5bd",
"440bd76a2c34fe36bc344d994bd9bc88d43f479f",
"c18f05debeceb0e32419e8b6b03b0017dc5c39a0",
"19e01a2be7b93f87430da71c1099365889a06b7b",
"a97ee2fa120bc08f52c719474da3edfb9f3d463d",
"66c05b7ce8688253e4455cca1bcd62f0b7012455",
"690aebdf74f298b4f886591cedce783c5937a8d0",
"06b265b29d5a34f0c974e5849702e89429864dde",
"5b8f037a332182bd3a0d8c064c2a9b28d8a13dc4",
"a1beb7439449a8b9c7d995e156743ef15cb9c477",
"68a875c29551d77bbeaa248a8c8bf775d8d63a09",
"21dc2fb00c53c6765728a44dd057a15b9926fc16",
"0f69fc88c42ccab6d00ecbc824c55205c69d6ac5",
"73a556d18801d2f5d20270d4db6ab5185c35e69b",
"a1d6c214f5bc301d4f722e5fe1ec843dbe674d90",
"09d28242b9640b8f0358e9078a570359517f4f3d",
"d4b0f77e535a859f0eaf410aab925927a73e604b",
"b0c0b655632e581fee7da3005f89a20131042f86",
"4f681bf4745d51467cd159698cb0a2e8f9339e96",
"43c13620fcda9ef356189340d9817f5b94f9b538",
"8a3d574b306e3e4faaef4f9284a300458cc82cc7",
"efa770940b817add0c47a7aaee68a1ee3e8ad3c2",
"ce0e864bcc691017582defb39126edcd3d632719",
"cb6f1544036ea1dca021bc928b14e5cad2206ede",
"7d3cc57dc3394cce3c09106c6e2304b99607f191",
"0862edb5d76e77383831323e51c9873382ae0f62",
"e8949af3ba916dc24fac06bdfc305353f61bbebe",
"453503140522549bba9b55dbfced99785111f86d",
"487c1610dc1f504dcd17ab95fc5d158a64b4ab0f",
"9bf3153bb1bc41581fc52066ff87ec650f2aa08b",
"2f022d2d48fcb6635afee5b3ed0c6c4fb3037bbf",
"7fa984fcf6eb3a3d3d26045ea9bd20721f7380be",
"a3fef716feb845067c071ee8b11a032a7ed7f0c0",
"90eb5471347b5fc31b358c13bdc1b343f1b11c8b",
"f6664fc1a6f9fb60c9e5d47440b90bfc93a79b86",
"ae624da456237614846557ed59a37a99d7ff8c3e",
"6319edf29b1513b496fb30d15c6b2bd2ee8f6413",
"b01c724b2eaf50ccf78b383b309c22df47d52c00",
"ee877c69dfb6ce0eae8ddafe60de668f8067f44d",
"acaaffe8281dc49d16a74a08b965f332e5ef4b7d",
"e00e56c42f88cf058e1dc8b4a6442251df47f9b5",
"2e729b5e632a5a1b0262d82e5cc07ec99eae405b",
"915d0e85c00c78bca1cad4fdc069e69d6f80b0a1",
"1234f391973ab21d4808aa9889b996e0315da64c",
"80a789eda8fc5908039baab2dfbd9199a505cb83",
"e31abe84e441ee2dc7f18411dac465bc090a63dd",
"046843b5ce3e50771993b284b5ca3082db06caa2",
"87b76b966f5131339532be4002dfc3b7f5e60da3",
"9f28a8158ca04909a751611584a1cde0e6abe4d5",
"60204e06956058c514a614b470677d9ef57fcc39",
"1c3f568b940f9108098c0e5e0a506c8fb080d16f",
"cfb4339c177ec0862f83955eb5c94408cff81d5b",
"f1be6643bfab419b61947ad21406f07e249eb40d",
"7ac7ff7a989707a32e9a1c6afe0f7fa5fc61dfa7",
"391275145a9315c2bd51709e0bb6f9da9bb7e4e7",
"9994a01a467399472c7fb0297e738350da0a1cc7",
"3a90bd5b380891ea2425ca35e962b3a3c6409a0a",
"8289c9e32cb3754fc905721b97447eef6b0d10a9",
"821b322871f520d9bbcae15bab47a194720bbc9b",
"09c4c7a202ebaa9a2f443ec7c7c34493f2102d07",
"531d4dfd8c72733c7a0c86cb4b0e8a56a4c7247a",
"6ad7773f44921575b358229ced225bf3311024c4",
"ea3c8daa6c85fea3b0124a615dfa258404034358",
"2a22314a7aece827b4734dda4d1016c06ace8d6f",
"6ac1378028e4af20a66271b86465cc2675f5ab39",
"cd4c1ed0cb1fe062eb0698e98e8e6a2d24b8a06b",
"11890c9a4820243529bf727e35088abfbdda8446",
"ec2f834134ed5e1812b59b23adb70272e6568dfc",
"ca3ce238dcb203020e0cb6b09f0c029f98504525",
"eca9e305077dbb54c49868ca250ca0621d63c4e0",
"d21c850b7dce0a4755e5a93b0402619db67b3714",
"1e9c533ddcbe1c37a6ed87eb54ee9abf37e17931",
"f9fbd3d1cbf3b8cb3db4f2521354e07f2910d8a1",
"9ec4654e3f38881bfe1bf962d94c299d65b3e1ba",
"24d8dc4671e8df029ac0eebda8061b51b6f8ed4c",
"b0bdae1b984c0db2233392221e3540e6ca79963f",
"d14396ef8cd9d61d38af5f1f9afb68674e89e957",
"24342835a295f0f9e0c470ec19915d967bcbce80",
"2c5142b339c1abfd15f5c171bfa02d613c7d938a",
"4bf3c1d30579be76e0ae2adee8bfeb4ecefa912d",
"9d8ae43af6c9bc9777934d4112eac0a49327c240",
"cf50f252c0144cf35cc58fb9cbf647c0e91592f8",
"73f626ee01909fc811a123552e828d04a55a825b",
"33d9518a11baf660ef9c161e96219ed9b25f81eb",
"a47e6ae981e5bd951c10b0466361e10f6a8a652d",
"5f5f9c79b00736fb8eea76232000bf48cb8b782d",
"22148a9f399da6d3ca04c4963c7fc51cde3b1046",
"7a581d3810cce3566110d35044a187c7396ac8dc",
"191225cc4d93d26572537c4e2e35a7f6f5e748f4",
"c85af49400d330613922935918c1d5cfdeee12a2",
"af0d0d71b0b9578ac528ca71d5a480e9e129a645",
"dd06f59a5e44ae0f2aecca2338937f81b0e16d1e",
"122851cb9856975fd234099330f445840fa21919",
"f14f3286d78ab13e5714c398e5ede28563ef17a9",
"ee4eadeb126e1075f79ec8b27e858f4565109b1d",
"78cb559c287793a2d9643a6fbf783d479075529a",
"e1d6d2422b39446f1085505d1657a378a7cfdffd",
"9d8b104f9269e6cbd3e907e40c67c8b9eae47d89",
"360cb157ce8ac748cf6ec1be06cfcf19c81395e8",
"76420f761f06984931fd095323e962361f4ab98e",
"e62714d42fc5d77981035e51e2a902050fdac4b3",
"d2699a947ed49c7ac9e71f590c170ee822a892aa",
"a58cf1022d88d494c1d300f080a83f2457186a78",
"a16b661c396e69451a34bd8a49797ecb2d784896",
"0a19d4c69d786e9799afaddfe83630016141978a",
"0992ffda0ea7992b407eaa88d3d2e97315a5e922",
"c124284a6f3714362a42a35539d95b719ae167ef",
"6b09dac8d26d5b7c2f878b8298964e2360faa315",
"e9e684ad747a5f594741635606220d584adf5ff1",
"3fd1abbf86aae32984e06b323bd109a971b10ba1",
"ed4a01f14ab4e64f8fe3cffcabf72ec8f9cb085a",
"6791027aa274177a23804384b1efe1f1436b2f0f",
"2e20a6c46eefb29da32469e2bae2dbf9e034350f",
"1b534428038ddc3ce39d4f9c6c502d057ebd1d0e",
"3032192046ae4587eae875596a03bb549f68a803",
"254024bc63f20a46f1134c369336bbede31c6a38",
"d8932cac1489c42659f900bd338b2ad982dc3051",
"579c23757360b6045773313cb338044fc9600530",
"70d70cb426a0e63aa3fbef16076b66af82f591b7",
"f75bd83526c5882103029a752d20728d23976017",
"e9c6b7e38adad70f4eef765deb0b124d740f04ed",
"f67474437f12168760a954880af936dac37637ac",
"5b9ff1577b97cafd8a8b61ab7b82617ebb0bc4a1",
"63b4bbcf5a6c020cdaf3578d888d9ea143acaaee",
"5cb4cc4915b002d909ffc143357d97f048ce5792",
"d498d4fa4bcf8b2abcdd4a2ecfe69d0ad92f2dce",
"bbec420c788aef62389d6e6f00847b332098846e",
"2c0c2f64edf96542352aeeb7400c3658d1767b97",
"42bdae3c76b147937bc0c43cbfce9d1d901a5737",
"8638253aae82a04e1118a373e7d66e6268299f70",
"c57a16c91f131ac3bfdd6f717199eb3c3cdb4ea2",
"f5dc3cc5a0b80801e9f817f125c514dee3ef8432",
"8ff0d00adc9a02b2c3227d6288793d21916504a3",
"fc1a524767a00210b9831d45fcf53b4a30c52a5a",
"4f8e10403c95b5041f86b70fad6867ed73ff5538",
"0668d1248a988f176e0c328fcbd933cc4fe85763",
"b1e33cbb1f27d213941c75ea0f9a55fba6277983",
"18adf826a8c35829820a7af45630bf7edd87ed76",
"7ad662dd34420dcf7f1d9766a4461e5381919c98",
"202fcab2ed7a9db2822c67ac3eb2451b69e1fc90",
"5be87c10f1e1810f2c5e6f44705fcf4054400cac",
"c889b96406d8054d2c7794e976276bdf38ed0e34",
"223711e20c16a47fc1ea162325a5a98755d0e88a",
"634eaaf870c0c907aff8e360e651ba6fbede4091",
"5fc8d58da0e77e320e45033ca593fcb7590b7e52",
"954c3e8659f65d95517fc8a5ed76f0e3a94c1694",
"c9001690eadc0aab09e3c8039aa2be529984bd04",
"b2adbc3976dd3226fca1febb808a892fd6bf491d",
"bf8f07fb2f0bc267516fea18c45662cee4ee108e",
"38a258d122c12f97c4719b61da8447d36546411b",
"7aaab08c3ffa22c090469d393f89a53cde16354d",
"570882f0e573f0e97f2515485a355aceebe1c307",
"5f50432eea2aa85d59ef12080ab2174ad06a8cf5",
"1b974f6447c9f56c8454dc607735aac42e200576",
"dffa0a983c567cb80edd7e96b6f25b3712aa51c5",
"d81fcffab122452421c35d28c93352fe26bb15cb",
"945f08e72606d1d5c2b9520bd5470562d5cfdd67",
"8e6267bddb0f709db406b49acf852024dce1ebbd",
"941f8ef67eca7e546207e46d9ed6717565b63beb",
"c18dfc0e8db7512a33c623fc49467187e76cf66a",
"11d00ff12b86ff111784c92a26f7f2de358ec8a7",
"1cb0196ee7ed76b9e4905135924e199136edc0fe",
"7bc057d8e52ac3ade942c0bef895f5206065a98f",
"7080e37d77a55a12003b9a68f88249b9e2533673",
"ca4819f38f3da5e5dc68317174c73f0cefbd2e51",
"9dce6cf6ec1527b359d5d5fd95e0fcc4a8393937",
"3fd97158d92661d9fc88eab0bafa4e9bf0ac79f6",
"2f17e0d89444d6ecf347627c5daaf853f9b23e84",
"14ba6a93f40904eec4c6f2c24ec83cb4323613af",
"f0d71dfe42ea8f47a65a9b90f53256f3a7f072c6",
"6f31fbb7589ccef511857aac3cfa339d066f4e05",
"135f99ed7e6c7f52a14237a0b231055f6f300786",
"6d8778ba28d49b47850942b1622bfe68221a8a7d",
"7761b7e9c380eba1b2554f7b0e4710cba58a5fca",
"6c9be58bb5a0d5ced27bf0e7d40c3fe77116ad4a",
"cedd3844acdbf717cee9fbe938026735cd9c8a02",
"f575f596e539be00b30da11201e03c2184486dd3",
"fafbbbc80a2510785a0a50ce3cc55e9c3552060e",
"8a3dec70ea1f885cf16e05880f498bfa8326e1ab",
"dacf1111f6bdfe72d3df7742a3545e14b4be9e00",
"5db92925e38b8b426258769934d496873709ff66",
"3f37c4ee3c0224472dc168a7c01ec5a232b4311f",
"4428355ac92ecf805cd4d99eba41da5acab6176e",
"bfe15992d9887f75cfa3df960d7158721f35d94f",
"ffae11debde293b3ef50eba855cf6a66370f05d7",
"853062918f3f9222989e52e2cea7475b6cc8e689",
"c838c4b491dfaa66612f38777b1f0f6d3b07e055",
"3a23acfc0f633ba92a3349beb50418dbc4aeeef1",
"85e8865e6047dd638f0fd3e4cbaf05ab6031bb56",
"8d53bed2a824df3f277eff5d57ac865df22e614a",
"3f1370e07160dfd41153ac3f66ae64aadb96651c",
"0aa97e9d577c3bccfe3529427803b735e30ecac9",
"a52ea596381d5122e5da613f6e7ed7803322c644",
"168b72fc11fde9830e798522112bc00b5f77fef7",
"416d3d53f4daacdb3604adaa2d1ae56685bf492c",
"7edef31649d4880f04da324d8c0ca9d1b4362025",
"80ea3578354f80198a0d4272e70191e82d7b90f5",
"83b17d4c58ec096147b3f29955e318318a09eb82",
"4ec2f3a5553acf6805a76dfa69e3222829120df8",
"3b709a88193a8b10cd29c53e81cdab20234b9ff8",
"83286c987d53d0d618d8587c8a9b3a0eedf18fc6",
"e8bb458ba162114e4cebd2d648b9380bf33f7d50",
"f2d2cefa13af20365f2cee7514cb04ea9c5856c9",
"098cf5ef4f2056c8cfb79288db5166a7bcb9bfa9",
"bfaaf8680e57d696b62e9b0dda49679c491b7739",
"2b82101aaf234ff8585c5026358a2915b533c43c",
"ab1b6328a88e5d3afb95a890bf3aec3ab1120c6a",
"6a89c4752c186fac0f15b39b6a5d972700aef5fe",
"f24d0061516ad1640ca627c7513735caec2d1e72",
"1905e68cc05628ba992c0806f7339f5a41e04dd6",
"cd6e087a9c9c70c3bf3e1d9485188309cc411224",
"e9e1857bf7f9bd97286402bf5233eba7d81c5fd7",
"ceb8823ab529db5ce95f847fbc7013cea88f1e84",
"3896517c0267a7a9f5319bddd32244253f9c87aa",
"321e221af792528fd816b992d5117aa58586bd6c",
"3eb9d26e8269f486ede56e676a6d8c529fe0310d",
"6d58e3461bc48101346b9952bf6941c028229538",
"33de34e35d9d43c2be8dada2329f4d3e099c73e3",
"a824b897796dacda2bf0d352973acd335bde3b3f",
"123c97b847caeeb314a28679ca0afa03ba3b11fb",
"71c0ba6cf2a3d11afe2b82758c6033e24b3dd952",
"a0e9c466ec4fc9e6d4032d82bb252abb7ffa2ec4",
"6c0c2f9f0ac4868a963a1da0e1db98688e462f61",
"73d0cf8e4cac97ee054586054dadbcfb507d08b6",
"25038d1d4fe50d44f708749661d510b4efa8d15a",
"ea421209ef1bac64bb9007122684aca4448a6f69",
"81d024ee969e09289f4f6e06b420891f7fa715bd",
"ee4be97ba254045272360608fceb0b65b461e506",
"31fdc07a69e9fea779d91f67b74ee0ddcdf65ec8",
"b16b5ba6842626520b57e74e037c43cbf40d5bb0",
"e4c42e6a37d9f725d882bd3430af1975a29300fc",
"8c55c4076dee1b05cdebd42a49603b97b21ad544",
"03e0ed387cc03c41afbe02ad08fccd300a3e2cbc",
"cec49aa5fe6fd02a9ae6d8fba51df6ff842ecfdf",
"e2ff1934f573398b05fca39f615c404d90aab637",
"1ef28abb123ce5855fbd22e2afb08513205c3554",
"6f2fdb5c5f57e95ef0e87b2fffa3a0d7d21e89f5",
"78b83b6ecf2557aba70a3d72bfb3f1a48bd104aa",
"417af1e5485810f81de6036c15e6c17945f9c333",
"70844102a8c5149d6dace1e8016fbf53b8aa7872",
"60338e9beb8cf99146ba61e2f4515ea947bcbad7",
"7412a9c4ba752d3aac5663a6fc50bc8da3d628ea",
"3a6ce02a93144d90d35cf2b3e98701188d042928",
"151fe8b8c51542cb6e623d44a093b71a8959026d",
"e1f0357a7c46d92a1507fcf64de37f8860ecd704",
"92a2782a0f266d8c279fe97a054b0282dca75746",
"adde94ee3ebc94b4db92db43315d97601d2f58f3",
"c7124cf6b69d7e01ca9f77e8afa61c95179ddb59",
"3c63e926bcde2d28eed6dfaca9c1e0e86a69bbdc",
"5a22cdaaef9cc2fa1db95f40f7fa885801416907",
"95967bcb7d7481709f129a104f74f82687d148fa",
"a7f0a8df5e9abd9d45ef89987b5944105251e603",
"3fe48db8f1b626ddacc562061fd5b0fc2369fce5",
"e70ac77a0eb61d16718a02d28783765edbe7d36c",
"8e7a455358f4817aa16fed7070b153b2f6555669",
"a9621cbcfcbc06d14aebdbba72b2754a6ee19c73",
"4f4f17e33577cb2d514934361057a2b93085945c",
"24bcd1670167f020cff6607a263dec6a471fde13",
"2f8396f02296ff8d253c0dca228e5e375cc4bf7a",
"3b69b3a521b905b5d8e102a7f99ef5a2b3856121",
"7b1587043b091bec89a0b1716db4642135cc2cef",
"f42db6f4bf18610e9968494129dea1b883fed6ef",
"6f1616667cee4f57741e972910a49b7eae244d3e",
"a8900f1df2c5f08f1956920e4968b6702aaae303",
"d87ac0fd912f6392a6014e8e98cadc8a9187a691",
"e250a832b8dabf8620538a995a66b82a41a53da3",
"eb002b9e172e059735ebda0fc2b32a91c777e94b",
"2166f1f247c5453b1e22aab21adefb3761795bfa",
"ffc687c574ba7ba4b7620d93d8aa193bfd056667",
"2d109f5917fa148950f34f1c0cb176494824a765",
"bf021b36c73d2a8f2786be5573e5f95003c77a7e",
"b0c543a86babbb2cc61ea52821ee5484ca7b3b2c",
"9c8204053cbd63285d4b41fea7bf1263a3520d56",
"3fbdafe2ecb5b5c228fe844a795ba714020f555e",
"e780859f8ad901f752903d0ad8edb3dd33482dc1",
"0b0370bbf617290be89553c4fdd2cdbc45b3e108",
"0a9befe724c4562a48c171f83f1a6641b46ef915",
"699d93f6465bb8b97a94b08ef7f789577568ee97",
"c4f9957810a27ad919bec43fe78c380cce36df1a",
"3e0193a64cb88f233f3525e7ea42333ac07533db",
"bb33b527a57e28413a8b3099e5db25696cfdba21",
"d449a15d3a4e30ca3d30626b4c257ddc362b6f7d",
"3f108627d86c4cf383a0b6630064ece1ad0cd6f2",
"65ba8ea71eb1e21863b9f5fcb747ab29d7cab776",
"95228928548310dabadd23e0117739b4ef9fd776",
"2fb287ab4c3b9d0ff962656617d75bbdc08808c1",
"ce966d43a2724f47ffa09d78c0fcd419c268b667",
"2c502bdd994ae56bb9135b43bdba0fb0f82df023",
"66f95401a121e64f8147c8b687825feda84c80d7",
"5e896453194131b858e79d5caf7500dd7f6d5a56",
"bf66071b05eb7ec2f674f6c0a573284f53f12655",
"dd0f776374de6f4a7e546067999003799c683ec7",
"f0cd95e0c8f86a762bbad74fe36d4640c7653140",
"74d80fc151f32a3910bcd3c09f85ef1230216206",
"b6e2753877763303cbaf23a1488a22b307730280",
"6e87351232750535d9d20b531ca4ac2d5ffbb2c8",
"2fc41d1ce327e31cd12ca1c2c2705fa224f8a132",
"648a267845e82bd3745f0e061b167abe07f82f87",
"9e5e7ba1ae6c19bd72216b305144e7c1e7df7af1",
"274e40584a5694bce91b65dcfa8943c905f57e47",
"e6cdc250bc1b0faf2340ba6b642a9e0135fbf130",
"648190852c596d573caea639b29b946ea00ae7c4",
"cb7a02d876d7dc362609dcc2643a3770624bf2ba",
"c143faaab3e63b22a54c0b19a6d3418ecca0277b",
"6a874d884fba1ad8e1a8efce2942b6ae01fd6df7",
"2419798aa04af58fca845b180e4eac4e573ad8c4",
"bc149c68b552ab61546bc0f2ee1ae7bfaf47fa9d",
"e95be80b6443c064234fde2b9a9381a8c5c87ff8",
"e16f48bd8ba94e3c57d18c020e30b96d98cdd29a",
"f81a9cea0a17c6eeb0e537c9c51b91ad6ebfff16",
"b8df66015070db5bba7d428f38904c98800acbe2",
"3eb0eb4206349f970c5fb063f3473ceba3d3045e",
"8499ffcafb4a26f401aa10f672e1540e204d833c",
"b2777d10275c2f2c93e956c34073b2497ed0067f",
"3728ec8f7f0d6dc81a1cd7e42df8c188a995eea7",
"556d44bcd788b3d14b2901ad6dbb9ee313df5892",
"59753c9370220b881ab8bd96531f5a04e82a2edd",
"15f9f73c0eb6f10f88f4f28f734d8661780325f0",
"dcd6c599e8919d4adcd07f632c1e00035184540b",
"34998468ed46e634be93d6e48ea2435b907532d8",
"c217637eff2dbf802afa06e73bbd58849333ee11",
"1408d88e5eddd4163b0c5ecf1eee9e5130343165",
"b846c84937461fa6b2acde2a8ddb5fb429a25734",
"ad8e886cafd115262221f55a732898e59a94bed2",
"c99f5a6f3362598dda8e61b5939f480526941dbf",
"993c2048ed7eebc619ba140c90c830ae08495d56",
"66cfe69e05dcd1e609e7a8cd6882031b0436ec31",
"823f42370ffb77548a9eaf9c71a8a38f83c0a0ea",
"c0b9bd8f113b7b416043c55452080482759ecf18",
"fb45765bdf51878c41869217814dfd73e7188406",
"0ba854f1c6fc81cef8055e393a981138f12b4f20",
"13c98efd0df6041ac87bd391189444dfbac7bd5e",
"07d629137ead56d31f0aa74f245ae2d0069fe1ae",
"127ee8bd27b0074b4e77f9b475c06b14b8654ba0",
"4c3bc85ffcb9303eae7bffc0fe0e04c20534c032",
"def1e468d33408d191c17973f0aabdb0f7c93135",
"581b2fd6f4c9c4763a001ef165a378479ab1a455",
"bf91ecee1ac72b67a81577e1e0bb0e5255045ba8",
"afcc27784f6053b948ddf0079508a11243c18ba2",
"f7ec8ec7c556b52cf6c2e5c4eced862a69faee4c",
"4e4442cfe2d97acc435c5fa5371bbf21cda57235",
"c678088b2b8b7b20a2664f0c62fe5b84ab228a11",
"53372578ec3ca3fde045657b8d84eb9fd077ed64",
"ba9bfd66228c3e6c089ce440784c0bfa691c2a86",
"11efe3304f2469a7db79cf735726825b8dbed213",
"71db17749540081affbbebbcc825c4dc79a2149f",
"e4f4c22fd275e1d91cb3c796b40c0aa1f1728d1c",
"8682d4994a42413eb9aad9c5f3b02efbae1afbec",
"3e830410ab19b5af66822dd351b70ce0de99e8c9",
"5550e9ace93aaf405494b6122164618d0cf77544",
"14299e843fbe5c2697f33454f2cfe05472e132cf",
"0db323dddaee01c3632a130103dcb58f84b40cff",
"8b5b8f26e85a7529562986a93e9990e6faa5924c",
"7ea2b8a87bee7ad948a452b11f2bed6be7ef097e",
"ce452e2fba981fa566fd3d3b319f91847b79b5bf",
"a2169c761ede8dddb38f2caebad0296dda02a38b",
"82600e6243ccf83126f09b6236244edbb5cee43a",
"c593705835358b9ed3eaf69994d3b707b1e351e4",
"1b0dfa97c1995634ec9623e3253027e4365ada8f",
"200743cb90f1ceb79b2f3cb357bea7d22cce2dc7",
"b712c5c023d2cddc4eac09e1cd262c1eedb9abf2",
"645dd7ae347ebcb97983cf3d4de1e0a526b8963a",
"0d67548c6a973fcb5765c5d029bdaa26ba5a33ed",
"425761d422460ffce57047dcdf369ba73779688b",
"5077ee313abd8d9dd60d51627cf48d3d373e0f8d",
"d6cd173aed3e8ec97ced9fb886aa3cb522b3201f",
"fd0caf1805d4daee62753a7b4a9828e3cd0f60dc",
"af3d55cfc142964c3a5dfc969728979813e4aff9",
"7a12be5b77584e414b62e6131224f4e46ad94846",
"de454e2fc8197e6358f0ab0e276d1bb3ed92bb6f",
"6559bc79256959e034ca60b0865cc1aabdb32949",
"91517f504428018a22639b27fca5e4449fff3100",
"9b9e1012745326222a95f17ccefb3ba035126f7a",
"be67167d060f83730400dee2ee034cd9bd3d7b7d",
"137e43d23d586f8408a58603ca42c007af91bea8",
"02d357f0b8b88979d7c9c74805d1d836b7d9efd2",
"f50c5f70886b9330c3e0d63a7940b3d3aacd1956",
"454c47e793c021e85e365184c677b83870f530cd",
"561c6ef2beb31bc308ba01374b2866b4c237aedb",
"505756545dcea203eda2500d973491aea3a07c98",
"4cd10f9ba957dd339e309c463d7b4aea6d248da5",
"7ff6d9705d4f8f66abb16fb25212188738ff6082",
"8d1eb61f4a03c2ba1cfc7bddc9be543f16252e0c",
"4142402d0574eddf1387e9e840a2adc794be71b0",
"ab0e75a0750fe7843748debd72f2c7967d979e0f",
"bf56ef4cdef32dbf63d6ac48cc05391aaf6a14c5",
"6c0e3cefa23df8ab870ee477f26746a43944d447",
"97f3950e8de8da77b45167a94b2c4ff4a997f196",
"44edc5d883a100dfcaca3cb64455c153bd3506d0",
"31c5346dfc6f63536d8ed40e634a389d3bc6284b",
"85942c968ef4af646283c42ac011ee82319b662a",
"a6ad9fe24e28d5b9d9d675def8c4b1487648c80d",
"452c5ea65040eae9d6a3ca805cd9cd380c1334c9",
"c14c16ebac4e733e3c6045cf1b4351c79bcdfe59",
"6c3d3bdb3b50f022adb91c0fa3ad36c0b3523b1f",
"7ec8c435fb059f2a2c3f54c24e76f1629d837025",
"15df63c34ab9f683985ee4582fe8ebb4a4e8f53c",
"6c85e4e22409f40eca50d7c7911d9f0440b10b31",
"366cb492e2a03471fd2a930217c9df3818c936de",
"facbb360f6feb1fb1bd343686955478c98d9fe3c",
"74acc8eed6cd02dded030ffb60b992e3d5deb775",
"fb77f7dc86b8b432eb84cef368aefda783790108",
"80df480198ab03ae48f7bb0d49063ddb5e8d3306",
"f04c014cbdb8d4667c96d9dea6900321380ea5ea",
"6804584eb08e23cd3c69b673837e974e3831169c",
"ac897821857343de5eec3472dfd90ba72f6edbad",
"fb23a341ea8a4828b2bf89f2050e86db6407271d",
"63b826a1c7e63f9358e7ca95565dbd7ef2c441ef",
"61103badb1a233de6bf84efb2dae527a191134d2",
"1b3bb9c89fa092d523ac8f1d9ba8cc2d4d28fca7",
"e0990b824ae309a08151197edb48408279790bac",
"e1bf8a7405e98ad4b2b0def54caf5ffa064f526a",
"59934b3bbe12029587a7f7e796f5e035f9394be2",
"1e5edd50725599c1186652eb586fc03a65482df4",
"2a28bc13bad88bd7d2f0e1f9ef75da9bc4d36c12",
"2cede8b5d06a54d05d7fb0334936de0b1b7571a3",
"e3c6394ccaa74305f57d0eb60630bfd374683dfe",
"80de3e46fa345fd6639ccdc58b5b4279e69470d2",
"d239e7133123f33eada9793593ab783acbd557d6",
"fce66b432338a58e46e50655380f1be6a3fc0a00",
"5e10f9978025ab75f30518977996d81077c84b18",
"11c85bcfe057e785e4343dc96742eda5ecf41a32",
"dbeca96f20c71e501706a40ad65dd93db18df858",
"bba57eca981af117debce6669c015b7b9c5d8e29",
"2435154a166da7bbc673305ed7fd3ce99a0c565e",
"39ce0c69252155ae5841da3c34936cf2c7190280",
};

		public override ChapterInfo Chapters
		{
			get
			{
				if (_chapters is null)
				{
					_chapters = new ChapterInfo();
					_chapters.AddChapter("Chapter 1", TimeSpan.FromTicks(8654989795));
					_chapters.AddChapter("Chapter 2", TimeSpan.FromTicks(20531310204));
					_chapters.AddChapter("Chapter 3", TimeSpan.FromTicks(29224859863));
					_chapters.AddChapter("Chapter 4", TimeSpan.FromTicks(27946840136));
					_chapters.AddChapter("Chapter 5", TimeSpan.FromTicks(9697809977));
					_chapters.AddChapter("Chapter 6", TimeSpan.FromTicks(21772419954));
					_chapters.AddChapter("Chapter 7", TimeSpan.FromTicks(20561029931));
					_chapters.AddChapter("Chapter 8", TimeSpan.FromTicks(33593929931));
					_chapters.AddChapter("Chapter 9", TimeSpan.FromTicks(26156110204));
					_chapters.AddChapter("Chapter 10", TimeSpan.FromTicks(16749239909));
					_chapters.AddChapter("Chapter 11", TimeSpan.FromTicks(24120190022));
					_chapters.AddChapter("Chapter 12", TimeSpan.FromTicks(9972729931));
					_chapters.AddChapter("Chapter 13", TimeSpan.FromTicks(31721470068));
					_chapters.AddChapter("Chapter 14", TimeSpan.FromTicks(10418790022));
					_chapters.AddChapter("Chapter 15", TimeSpan.FromTicks(8717000000));
					_chapters.AddChapter("Chapter 16", TimeSpan.FromTicks(14988239909));
					_chapters.AddChapter("Chapter 17", TimeSpan.FromTicks(32672790022));
					_chapters.AddChapter("Chapter 18", TimeSpan.FromTicks(13517029931));
					_chapters.AddChapter("Chapter 19", TimeSpan.FromTicks(12172130158));
					_chapters.AddChapter("Chapter 20", TimeSpan.FromTicks(1516949886));
					_chapters.AddChapter("Chapter 21", TimeSpan.FromTicks(2334300000));
					_chapters.AddChapter("Chapter 22", TimeSpan.FromTicks(2638940136));
					_chapters.AddChapter("Chapter 23", TimeSpan.FromTicks(1829029931));
					_chapters.AddChapter("Chapter 24", TimeSpan.FromTicks(1360919954));
					_chapters.AddChapter("Chapter 25", TimeSpan.FromTicks(2542350113));
					_chapters.AddChapter("Chapter 26", TimeSpan.FromTicks(1197449886));
					_chapters.AddChapter("Chapter 27", TimeSpan.FromTicks(5090970068));
					_chapters.AddChapter("Chapter 28", TimeSpan.FromTicks(231500000));
					_chapters.AddChapter("Chapter 29", TimeSpan.FromTicks(268649886));
					_chapters.AddChapter("Chapter 30", TimeSpan.FromTicks(402400000));
					_chapters.AddChapter("Chapter 31", TimeSpan.FromTicks(2252560090));
					_chapters.AddChapter("Chapter 32", TimeSpan.FromTicks(268649886));
					_chapters.AddChapter("Chapter 33", TimeSpan.FromTicks(1167730158));
					_chapters.AddChapter("Chapter 34", TimeSpan.FromTicks(2319439909));
					_chapters.AddChapter("Chapter 35", TimeSpan.FromTicks(4288490022));
					_chapters.AddChapter("Chapter 36", TimeSpan.FromTicks(1271749886));
					_chapters.AddChapter("Chapter 37", TimeSpan.FromTicks(617880045));
					_chapters.AddChapter("Chapter 38", TimeSpan.FromTicks(394970068));
					_chapters.AddChapter("Chapter 39", TimeSpan.FromTicks(2430890022));
					_chapters.AddChapter("Chapter 40", TimeSpan.FromTicks(290939909));
					_chapters.AddChapter("Chapter 41", TimeSpan.FromTicks(558430158));
					_chapters.AddChapter("Chapter 42", TimeSpan.FromTicks(3084769841));
					_chapters.AddChapter("Chapter 43", TimeSpan.FromTicks(12848290022));
					_chapters.AddChapter("Chapter 44", TimeSpan.FromTicks(2817270068));
					_chapters.AddChapter("Chapter 45", TimeSpan.FromTicks(2980970068));
					_chapters.AddChapter("Chapter 46", TimeSpan.FromTicks(3932059863));
					_chapters.AddChapter("Chapter 47", TimeSpan.FromTicks(4949790022));
					_chapters.AddChapter("Chapter 48", TimeSpan.FromTicks(2386309977));
					_chapters.AddChapter("Chapter 49", TimeSpan.FromTicks(1598690022));
					_chapters.AddChapter("Chapter 50", TimeSpan.FromTicks(3634609977));
					_chapters.AddChapter("Chapter 51", TimeSpan.FromTicks(4147309977));
					_chapters.AddChapter("Chapter 52", TimeSpan.FromTicks(4600800000));
					_chapters.AddChapter("Chapter 53", TimeSpan.FromTicks(6435870068));
					_chapters.AddChapter("Chapter 54", TimeSpan.FromTicks(6525270068));
					_chapters.AddChapter("Chapter 55", TimeSpan.FromTicks(5848869841));
					_chapters.AddChapter("Chapter 56", TimeSpan.FromTicks(4414800000));
					_chapters.AddChapter("Chapter 57", TimeSpan.FromTicks(5120920181));
					_chapters.AddChapter("Chapter 58", TimeSpan.FromTicks(5291589795));
					_chapters.AddChapter("Chapter 59", TimeSpan.FromTicks(6012340136));
					_chapters.AddChapter("Chapter 60", TimeSpan.FromTicks(6072009977));
					_chapters.AddChapter("Chapter 61", TimeSpan.FromTicks(5157839909));
					_chapters.AddChapter("Chapter 62", TimeSpan.FromTicks(4637720181));
					_chapters.AddChapter("Chapter 63", TimeSpan.FromTicks(4667669841));
					_chapters.AddChapter("Chapter 64", TimeSpan.FromTicks(4712019954));
					_chapters.AddChapter("Chapter 65", TimeSpan.FromTicks(5477350113));
					_chapters.AddChapter("Chapter 66", TimeSpan.FromTicks(2126480045));
					_chapters.AddChapter("Chapter 67", TimeSpan.FromTicks(53400000));
					_chapters.AddChapter("Chapter 68", TimeSpan.FromTicks(454639909));
					_chapters.AddChapter("Chapter 69", TimeSpan.FromTicks(350380045));
					_chapters.AddChapter("Chapter 70", TimeSpan.FromTicks(380109977));
					_chapters.AddChapter("Chapter 71", TimeSpan.FromTicks(342950113));
					_chapters.AddChapter("Chapter 72", TimeSpan.FromTicks(402400000));
					_chapters.AddChapter("Chapter 73", TimeSpan.FromTicks(580729931));
					_chapters.AddChapter("Chapter 74", TimeSpan.FromTicks(387770068));
					_chapters.AddChapter("Chapter 75", TimeSpan.FromTicks(328089795));
					_chapters.AddChapter("Chapter 76", TimeSpan.FromTicks(328090022));
					_chapters.AddChapter("Chapter 77", TimeSpan.FromTicks(283509977));
					_chapters.AddChapter("Chapter 78", TimeSpan.FromTicks(461840136));
					_chapters.AddChapter("Chapter 79", TimeSpan.FromTicks(647600000));
					_chapters.AddChapter("Chapter 80", TimeSpan.FromTicks(320890022));
					_chapters.AddChapter("Chapter 81", TimeSpan.FromTicks(394969841));
					_chapters.AddChapter("Chapter 82", TimeSpan.FromTicks(506650113));
					_chapters.AddChapter("Chapter 83", TimeSpan.FromTicks(417260090));
					_chapters.AddChapter("Chapter 84", TimeSpan.FromTicks(491559863));
					_chapters.AddChapter("Chapter 85", TimeSpan.FromTicks(469270068));
					_chapters.AddChapter("Chapter 86", TimeSpan.FromTicks(491559863));
					_chapters.AddChapter("Chapter 87", TimeSpan.FromTicks(469270068));
					_chapters.AddChapter("Chapter 88", TimeSpan.FromTicks(306029931));
					_chapters.AddChapter("Chapter 89", TimeSpan.FromTicks(357810204));
					_chapters.AddChapter("Chapter 90", TimeSpan.FromTicks(365239909));
					_chapters.AddChapter("Chapter 91", TimeSpan.FromTicks(335519954));
					_chapters.AddChapter("Chapter 92", TimeSpan.FromTicks(513850113));
					_chapters.AddChapter("Chapter 93", TimeSpan.FromTicks(454639909));
					_chapters.AddChapter("Chapter 94", TimeSpan.FromTicks(558429931));
					_chapters.AddChapter("Chapter 95", TimeSpan.FromTicks(461840136));
					_chapters.AddChapter("Chapter 96", TimeSpan.FromTicks(469269841));
					_chapters.AddChapter("Chapter 97", TimeSpan.FromTicks(536140136));
					_chapters.AddChapter("Chapter 98", TimeSpan.FromTicks(491559863));
					_chapters.AddChapter("Chapter 99", TimeSpan.FromTicks(498990022));
					_chapters.AddChapter("Chapter 100", TimeSpan.FromTicks(394970068));
					_chapters.AddChapter("Chapter 101", TimeSpan.FromTicks(491560090));
					_chapters.AddChapter("Chapter 102", TimeSpan.FromTicks(513849886));
					_chapters.AddChapter("Chapter 103", TimeSpan.FromTicks(603019954));
					_chapters.AddChapter("Chapter 104", TimeSpan.FromTicks(372909977));
					_chapters.AddChapter("Chapter 105", TimeSpan.FromTicks(417260090));
					_chapters.AddChapter("Chapter 106", TimeSpan.FromTicks(342950113));
					_chapters.AddChapter("Chapter 107", TimeSpan.FromTicks(357809977));
					_chapters.AddChapter("Chapter 108", TimeSpan.FromTicks(328090022));
					_chapters.AddChapter("Chapter 109", TimeSpan.FromTicks(417259863));
					_chapters.AddChapter("Chapter 110", TimeSpan.FromTicks(417260090));
					_chapters.AddChapter("Chapter 111", TimeSpan.FromTicks(402400000));
					_chapters.AddChapter("Chapter 112", TimeSpan.FromTicks(432119954));
					_chapters.AddChapter("Chapter 113", TimeSpan.FromTicks(506649886));
					_chapters.AddChapter("Chapter 114", TimeSpan.FromTicks(424690022));
					_chapters.AddChapter("Chapter 115", TimeSpan.FromTicks(372909977));
					_chapters.AddChapter("Chapter 116", TimeSpan.FromTicks(558430158));
					_chapters.AddChapter("Chapter 117", TimeSpan.FromTicks(446980045));
					_chapters.AddChapter("Chapter 118", TimeSpan.FromTicks(573529931));
					_chapters.AddChapter("Chapter 119", TimeSpan.FromTicks(513849886));
					_chapters.AddChapter("Chapter 120", TimeSpan.FromTicks(424690022));
					_chapters.AddChapter("Chapter 121", TimeSpan.FromTicks(588390022));
					_chapters.AddChapter("Chapter 122", TimeSpan.FromTicks(394970068));
					_chapters.AddChapter("Chapter 123", TimeSpan.FromTicks(595590022));
					_chapters.AddChapter("Chapter 124", TimeSpan.FromTicks(573529931));
					_chapters.AddChapter("Chapter 125", TimeSpan.FromTicks(536139909));
					_chapters.AddChapter("Chapter 126", TimeSpan.FromTicks(461840136));
					_chapters.AddChapter("Chapter 127", TimeSpan.FromTicks(506649886));
					_chapters.AddChapter("Chapter 128", TimeSpan.FromTicks(402400000));
					_chapters.AddChapter("Chapter 129", TimeSpan.FromTicks(417260090));
					_chapters.AddChapter("Chapter 130", TimeSpan.FromTicks(60599999));
					_chapters.AddChapter("Chapter 131", TimeSpan.FromTicks(4689729931));
					_chapters.AddChapter("Chapter 132", TimeSpan.FromTicks(1955350113));
					_chapters.AddChapter("Chapter 133", TimeSpan.FromTicks(825929931));
					_chapters.AddChapter("Chapter 134", TimeSpan.FromTicks(803639909));
					_chapters.AddChapter("Chapter 135", TimeSpan.FromTicks(2111620181));
					_chapters.AddChapter("Chapter 136", TimeSpan.FromTicks(1138000000));
					_chapters.AddChapter("Chapter 137", TimeSpan.FromTicks(2312239909));
					_chapters.AddChapter("Chapter 138", TimeSpan.FromTicks(1011690022));
					_chapters.AddChapter("Chapter 139", TimeSpan.FromTicks(1992729931));
					_chapters.AddChapter("Chapter 140", TimeSpan.FromTicks(692180045));
					_chapters.AddChapter("Chapter 141", TimeSpan.FromTicks(1041639909));
					_chapters.AddChapter("Chapter 142", TimeSpan.FromTicks(2965880045));
					_chapters.AddChapter("Chapter 143", TimeSpan.FromTicks(1457740136));
					_chapters.AddChapter("Chapter 144", TimeSpan.FromTicks(1665559863));
					_chapters.AddChapter("Chapter 145", TimeSpan.FromTicks(1338630158));
					_chapters.AddChapter("Chapter 146", TimeSpan.FromTicks(736759863));
					_chapters.AddChapter("Chapter 147", TimeSpan.FromTicks(863080045));
					_chapters.AddChapter("Chapter 148", TimeSpan.FromTicks(766490022));
					_chapters.AddChapter("Chapter 149", TimeSpan.FromTicks(1167729931));
					_chapters.AddChapter("Chapter 150", TimeSpan.FromTicks(885370068));
					_chapters.AddChapter("Chapter 151", TimeSpan.FromTicks(855880045));
					_chapters.AddChapter("Chapter 152", TimeSpan.FromTicks(1613549886));
					_chapters.AddChapter("Chapter 153", TimeSpan.FromTicks(1420360090));
					_chapters.AddChapter("Chapter 154", TimeSpan.FromTicks(1985069841));
					_chapters.AddChapter("Chapter 155", TimeSpan.FromTicks(1591490022));
					_chapters.AddChapter("Chapter 156", TimeSpan.FromTicks(1368350113));
					_chapters.AddChapter("Chapter 157", TimeSpan.FromTicks(989629931));
					_chapters.AddChapter("Chapter 158", TimeSpan.FromTicks(7973960090));
					_chapters.AddChapter("Chapter 159", TimeSpan.FromTicks(1145439909));
					_chapters.AddChapter("Chapter 160", TimeSpan.FromTicks(892800000));
					_chapters.AddChapter("Chapter 161", TimeSpan.FromTicks(6681070068));
					_chapters.AddChapter("Chapter 162", TimeSpan.FromTicks(9883800000));
					_chapters.AddChapter("Chapter 163", TimeSpan.FromTicks(11770890022));
					_chapters.AddChapter("Chapter 164", TimeSpan.FromTicks(3917200000));
					_chapters.AddChapter("Chapter 165", TimeSpan.FromTicks(1918200000));
					_chapters.AddChapter("Chapter 166", TimeSpan.FromTicks(3196449886));
					_chapters.AddChapter("Chapter 167", TimeSpan.FromTicks(2646609977));
					_chapters.AddChapter("Chapter 168", TimeSpan.FromTicks(2906440136));
					_chapters.AddChapter("Chapter 169", TimeSpan.FromTicks(2282280045));
					_chapters.AddChapter("Chapter 170", TimeSpan.FromTicks(2921300000));
					_chapters.AddChapter("Chapter 171", TimeSpan.FromTicks(4622859863));
					_chapters.AddChapter("Chapter 172", TimeSpan.FromTicks(4073009977));
					_chapters.AddChapter("Chapter 173", TimeSpan.FromTicks(1947919954));
					_chapters.AddChapter("Chapter 174", TimeSpan.FromTicks(4957220181));
					_chapters.AddChapter("Chapter 175", TimeSpan.FromTicks(4355359863));
					_chapters.AddChapter("Chapter 176", TimeSpan.FromTicks(8263980045));
					_chapters.AddChapter("Chapter 177", TimeSpan.FromTicks(2319439909));
					_chapters.AddChapter("Chapter 178", TimeSpan.FromTicks(3263330158));
					_chapters.AddChapter("Chapter 179", TimeSpan.FromTicks(774149886));
					_chapters.AddChapter("Chapter 180", TimeSpan.FromTicks(1138000000));
					_chapters.AddChapter("Chapter 181", TimeSpan.FromTicks(603019954));
					_chapters.AddChapter("Chapter 182", TimeSpan.FromTicks(506650113));
					_chapters.AddChapter("Chapter 183", TimeSpan.FromTicks(491560090));
					_chapters.AddChapter("Chapter 184", TimeSpan.FromTicks(298369841));
					_chapters.AddChapter("Chapter 185", TimeSpan.FromTicks(498990022));
					_chapters.AddChapter("Chapter 186", TimeSpan.FromTicks(632740136));
					_chapters.AddChapter("Chapter 187", TimeSpan.FromTicks(409829931));
					_chapters.AddChapter("Chapter 188", TimeSpan.FromTicks(796209977));
					_chapters.AddChapter("Chapter 189", TimeSpan.FromTicks(446980045));
					_chapters.AddChapter("Chapter 190", TimeSpan.FromTicks(484129931));
					_chapters.AddChapter("Chapter 191", TimeSpan.FromTicks(833360090));
					_chapters.AddChapter("Chapter 192", TimeSpan.FromTicks(825929931));
					_chapters.AddChapter("Chapter 193", TimeSpan.FromTicks(907900000));
					_chapters.AddChapter("Chapter 194", TimeSpan.FromTicks(967109977));
					_chapters.AddChapter("Chapter 195", TimeSpan.FromTicks(491560090));
					_chapters.AddChapter("Chapter 196", TimeSpan.FromTicks(306029931));
					_chapters.AddChapter("Chapter 197", TimeSpan.FromTicks(498990022));
					_chapters.AddChapter("Chapter 198", TimeSpan.FromTicks(1130570068));
					_chapters.AddChapter("Chapter 199", TimeSpan.FromTicks(722139909));
					_chapters.AddChapter("Chapter 200", TimeSpan.FromTicks(766490022));
					_chapters.AddChapter("Chapter 201", TimeSpan.FromTicks(907900000));
					_chapters.AddChapter("Chapter 202", TimeSpan.FromTicks(513849886));
					_chapters.AddChapter("Chapter 203", TimeSpan.FromTicks(855880045));
					_chapters.AddChapter("Chapter 204", TimeSpan.FromTicks(454640136));
					_chapters.AddChapter("Chapter 205", TimeSpan.FromTicks(900229931));
					_chapters.AddChapter("Chapter 206", TimeSpan.FromTicks(885370068));
					_chapters.AddChapter("Chapter 207", TimeSpan.FromTicks(536139909));
					_chapters.AddChapter("Chapter 208", TimeSpan.FromTicks(699609977));
					_chapters.AddChapter("Chapter 209", TimeSpan.FromTicks(1004260090));
					_chapters.AddChapter("Chapter 210", TimeSpan.FromTicks(699609977));
					_chapters.AddChapter("Chapter 211", TimeSpan.FromTicks(417259863));
					_chapters.AddChapter("Chapter 212", TimeSpan.FromTicks(684750113));
					_chapters.AddChapter("Chapter 213", TimeSpan.FromTicks(409829931));
					_chapters.AddChapter("Chapter 214", TimeSpan.FromTicks(484129931));
					_chapters.AddChapter("Chapter 215", TimeSpan.FromTicks(476700000));
					_chapters.AddChapter("Chapter 216", TimeSpan.FromTicks(1383210204));
					_chapters.AddChapter("Chapter 217", TimeSpan.FromTicks(1257119954));
					_chapters.AddChapter("Chapter 218", TimeSpan.FromTicks(268649886));
					_chapters.AddChapter("Chapter 219", TimeSpan.FromTicks(1033980045));
					_chapters.AddChapter("Chapter 220", TimeSpan.FromTicks(722139909));
					_chapters.AddChapter("Chapter 221", TimeSpan.FromTicks(335520181));
					_chapters.AddChapter("Chapter 222", TimeSpan.FromTicks(551000000));
					_chapters.AddChapter("Chapter 223", TimeSpan.FromTicks(454639909));
					_chapters.AddChapter("Chapter 224", TimeSpan.FromTicks(409829931));
					_chapters.AddChapter("Chapter 225", TimeSpan.FromTicks(380109977));
					_chapters.AddChapter("Chapter 226", TimeSpan.FromTicks(521520181));
					_chapters.AddChapter("Chapter 227", TimeSpan.FromTicks(580729931));
					_chapters.AddChapter("Chapter 228", TimeSpan.FromTicks(722139909));
					_chapters.AddChapter("Chapter 229", TimeSpan.FromTicks(825930158));
					_chapters.AddChapter("Chapter 230", TimeSpan.FromTicks(781349886));
					_chapters.AddChapter("Chapter 231", TimeSpan.FromTicks(573529931));
					_chapters.AddChapter("Chapter 232", TimeSpan.FromTicks(796210204));
					_chapters.AddChapter("Chapter 233", TimeSpan.FromTicks(900229931));
					_chapters.AddChapter("Chapter 234", TimeSpan.FromTicks(551000000));
					_chapters.AddChapter("Chapter 235", TimeSpan.FromTicks(952249886));
					_chapters.AddChapter("Chapter 236", TimeSpan.FromTicks(744190022));
					_chapters.AddChapter("Chapter 237", TimeSpan.FromTicks(714470068));
					_chapters.AddChapter("Chapter 238", TimeSpan.FromTicks(439780045));
					_chapters.AddChapter("Chapter 239", TimeSpan.FromTicks(751619954));
					_chapters.AddChapter("Chapter 240", TimeSpan.FromTicks(848219954));
					_chapters.AddChapter("Chapter 241", TimeSpan.FromTicks(841019954));
					_chapters.AddChapter("Chapter 242", TimeSpan.FromTicks(1182590022));
					_chapters.AddChapter("Chapter 243", TimeSpan.FromTicks(558430158));
					_chapters.AddChapter("Chapter 244", TimeSpan.FromTicks(573529931));
					_chapters.AddChapter("Chapter 245", TimeSpan.FromTicks(833359863));
					_chapters.AddChapter("Chapter 246", TimeSpan.FromTicks(446980045));
					_chapters.AddChapter("Chapter 247", TimeSpan.FromTicks(573530158));
					_chapters.AddChapter("Chapter 248", TimeSpan.FromTicks(461839909));
					_chapters.AddChapter("Chapter 249", TimeSpan.FromTicks(306029931));
					_chapters.AddChapter("Chapter 250", TimeSpan.FromTicks(432119954));
					_chapters.AddChapter("Chapter 251", TimeSpan.FromTicks(662460090));
					_chapters.AddChapter("Chapter 252", TimeSpan.FromTicks(417260090));
					_chapters.AddChapter("Chapter 253", TimeSpan.FromTicks(454639909));
					_chapters.AddChapter("Chapter 254", TimeSpan.FromTicks(506650113));
					_chapters.AddChapter("Chapter 255", TimeSpan.FromTicks(595589795));
					_chapters.AddChapter("Chapter 256", TimeSpan.FromTicks(506650113));
					_chapters.AddChapter("Chapter 257", TimeSpan.FromTicks(521519954));
					_chapters.AddChapter("Chapter 258", TimeSpan.FromTicks(707270068));
					_chapters.AddChapter("Chapter 259", TimeSpan.FromTicks(491559863));
					_chapters.AddChapter("Chapter 260", TimeSpan.FromTicks(13858820181));
					_chapters.AddChapter("Chapter 261", TimeSpan.FromTicks(1294039909));
					_chapters.AddChapter("Chapter 262", TimeSpan.FromTicks(7520709977));
					_chapters.AddChapter("Chapter 263", TimeSpan.FromTicks(7379529931));
					_chapters.AddChapter("Chapter 264", TimeSpan.FromTicks(12283580045));
					_chapters.AddChapter("Chapter 265", TimeSpan.FromTicks(3575170068));
					_chapters.AddChapter("Chapter 266", TimeSpan.FromTicks(2334300000));
					_chapters.AddChapter("Chapter 267", TimeSpan.FromTicks(2438319954));
					_chapters.AddChapter("Chapter 268", TimeSpan.FromTicks(2312240136));
					_chapters.AddChapter("Chapter 269", TimeSpan.FromTicks(1710379818));
					_chapters.AddChapter("Chapter 270", TimeSpan.FromTicks(1643500000));
					_chapters.AddChapter("Chapter 271", TimeSpan.FromTicks(2297150113));
					_chapters.AddChapter("Chapter 272", TimeSpan.FromTicks(3107059863));
					_chapters.AddChapter("Chapter 273", TimeSpan.FromTicks(15062550113));
					_chapters.AddChapter("Chapter 274", TimeSpan.FromTicks(13175229931));
					_chapters.AddChapter("Chapter 275", TimeSpan.FromTicks(13910840136));
					_chapters.AddChapter("Chapter 276", TimeSpan.FromTicks(4243909977));
					_chapters.AddChapter("Chapter 277", TimeSpan.FromTicks(11629709977));
					_chapters.AddChapter("Chapter 278", TimeSpan.FromTicks(10515149886));
					_chapters.AddChapter("Chapter 279", TimeSpan.FromTicks(20249190022));
					_chapters.AddChapter("Chapter 280", TimeSpan.FromTicks(15954430158));
					_chapters.AddChapter("Chapter 281", TimeSpan.FromTicks(8501519954));
					_chapters.AddChapter("Chapter 282", TimeSpan.FromTicks(13227469841));
					_chapters.AddChapter("Chapter 283", TimeSpan.FromTicks(21608720181));
					_chapters.AddChapter("Chapter 284", TimeSpan.FromTicks(21170559863));
					_chapters.AddChapter("Chapter 285", TimeSpan.FromTicks(21935650113));
					_chapters.AddChapter("Chapter 286", TimeSpan.FromTicks(22730709977));
					_chapters.AddChapter("Chapter 287", TimeSpan.FromTicks(23347429931));
					_chapters.AddChapter("Chapter 288", TimeSpan.FromTicks(20375270068));
					_chapters.AddChapter("Chapter 289", TimeSpan.FromTicks(22812439909));
					_chapters.AddChapter("Chapter 290", TimeSpan.FromTicks(17009309977));
					_chapters.AddChapter("Chapter 291", TimeSpan.FromTicks(14029960090));
					_chapters.AddChapter("Chapter 292", TimeSpan.FromTicks(21757319954));
					_chapters.AddChapter("Chapter 293", TimeSpan.FromTicks(7676980045));
					_chapters.AddChapter("Chapter 294", TimeSpan.FromTicks(8130229931));
					_chapters.AddChapter("Chapter 295", TimeSpan.FromTicks(7610100000));
					_chapters.AddChapter("Chapter 296", TimeSpan.FromTicks(11473900000));
					_chapters.AddChapter("Chapter 297", TimeSpan.FromTicks(5826580045));
					_chapters.AddChapter("Chapter 298", TimeSpan.FromTicks(6547329931));
					_chapters.AddChapter("Chapter 299", TimeSpan.FromTicks(3322540136));
					_chapters.AddChapter("Chapter 300", TimeSpan.FromTicks(3642039909));
					_chapters.AddChapter("Chapter 301", TimeSpan.FromTicks(4452190022));
					_chapters.AddChapter("Chapter 302", TimeSpan.FromTicks(4459390022));
					_chapters.AddChapter("Chapter 303", TimeSpan.FromTicks(2638939909));
					_chapters.AddChapter("Chapter 304", TimeSpan.FromTicks(31996400000));
					_chapters.AddChapter("Chapter 305", TimeSpan.FromTicks(31996400000));
					_chapters.AddChapter("Chapter 306", TimeSpan.FromTicks(8665220181));
					_chapters.AddChapter("Chapter 307", TimeSpan.FromTicks(33289519954));
					_chapters.AddChapter("Chapter 308", TimeSpan.FromTicks(10470800000));
					_chapters.AddChapter("Chapter 309", TimeSpan.FromTicks(8241449886));
					_chapters.AddChapter("Chapter 310", TimeSpan.FromTicks(6918840136));
					_chapters.AddChapter("Chapter 311", TimeSpan.FromTicks(7223490022));
					_chapters.AddChapter("Chapter 312", TimeSpan.FromTicks(8211959863));
					_chapters.AddChapter("Chapter 313", TimeSpan.FromTicks(8130229931));
					_chapters.AddChapter("Chapter 314", TimeSpan.FromTicks(7364670068));
					_chapters.AddChapter("Chapter 315", TimeSpan.FromTicks(8627829931));
					_chapters.AddChapter("Chapter 316", TimeSpan.FromTicks(8516380045));
					_chapters.AddChapter("Chapter 317", TimeSpan.FromTicks(9021640136));
					_chapters.AddChapter("Chapter 318", TimeSpan.FromTicks(10158490022));
					_chapters.AddChapter("Chapter 319", TimeSpan.FromTicks(9133329931));
					_chapters.AddChapter("Chapter 320", TimeSpan.FromTicks(8352909977));
					_chapters.AddChapter("Chapter 321", TimeSpan.FromTicks(7386960090));
					_chapters.AddChapter("Chapter 322", TimeSpan.FromTicks(7305219954));
					_chapters.AddChapter("Chapter 323", TimeSpan.FromTicks(5938269841));
					_chapters.AddChapter("Chapter 324", TimeSpan.FromTicks(6792760090));
					_chapters.AddChapter("Chapter 325", TimeSpan.FromTicks(7201200000));
					_chapters.AddChapter("Chapter 326", TimeSpan.FromTicks(7327750113));
					_chapters.AddChapter("Chapter 327", TimeSpan.FromTicks(5440200000));
					_chapters.AddChapter("Chapter 328", TimeSpan.FromTicks(5826579818));
					_chapters.AddChapter("Chapter 329", TimeSpan.FromTicks(13769660090));
					_chapters.AddChapter("Chapter 330", TimeSpan.FromTicks(13985139909));
					_chapters.AddChapter("Chapter 331", TimeSpan.FromTicks(17663180045));
					_chapters.AddChapter("Chapter 332", TimeSpan.FromTicks(15969290022));
					_chapters.AddChapter("Chapter 333", TimeSpan.FromTicks(17678039909));
					_chapters.AddChapter("Chapter 334", TimeSpan.FromTicks(16348000000));
					_chapters.AddChapter("Chapter 335", TimeSpan.FromTicks(23815540136));
					_chapters.AddChapter("Chapter 336", TimeSpan.FromTicks(18844849886));
					_chapters.AddChapter("Chapter 337", TimeSpan.FromTicks(21579000000));
					_chapters.AddChapter("Chapter 338", TimeSpan.FromTicks(17603740136));
					_chapters.AddChapter("Chapter 339", TimeSpan.FromTicks(201780045));
					_chapters.AddChapter("Chapter 340", TimeSpan.FromTicks(6963429931));
					_chapters.AddChapter("Chapter 341", TimeSpan.FromTicks(5447629931));
					_chapters.AddChapter("Chapter 342", TimeSpan.FromTicks(9497190022));
					_chapters.AddChapter("Chapter 343", TimeSpan.FromTicks(11815470068));
					_chapters.AddChapter("Chapter 344", TimeSpan.FromTicks(35607559863));
					_chapters.AddChapter("Chapter 345", TimeSpan.FromTicks(35600130158));
					_chapters.AddChapter("Chapter 346", TimeSpan.FromTicks(24647979818));
					_chapters.AddChapter("Chapter 347", TimeSpan.FromTicks(35429470068));
					_chapters.AddChapter("Chapter 348", TimeSpan.FromTicks(15248309977));
					_chapters.AddChapter("Chapter 349", TimeSpan.FromTicks(35347730158));
					_chapters.AddChapter("Chapter 350", TimeSpan.FromTicks(27189169841));
					_chapters.AddChapter("Chapter 351", TimeSpan.FromTicks(35458960090));
					_chapters.AddChapter("Chapter 352", TimeSpan.FromTicks(14743039909));
					_chapters.AddChapter("Chapter 353", TimeSpan.FromTicks(35733880045));
					_chapters.AddChapter("Chapter 354", TimeSpan.FromTicks(20107780045));
					_chapters.AddChapter("Chapter 355", TimeSpan.FromTicks(35399509977));
					_chapters.AddChapter("Chapter 356", TimeSpan.FromTicks(23406870068));
					_chapters.AddChapter("Chapter 357", TimeSpan.FromTicks(35540690022));
					_chapters.AddChapter("Chapter 358", TimeSpan.FromTicks(18658849886));
					_chapters.AddChapter("Chapter 359", TimeSpan.FromTicks(35867629931));
					_chapters.AddChapter("Chapter 360", TimeSpan.FromTicks(21006860090));
					_chapters.AddChapter("Chapter 361", TimeSpan.FromTicks(33995170068));
					_chapters.AddChapter("Chapter 362", TimeSpan.FromTicks(12491869841));
					_chapters.AddChapter("Chapter 363", TimeSpan.FromTicks(35540690022));
					_chapters.AddChapter("Chapter 364", TimeSpan.FromTicks(5209860090));
					_chapters.AddChapter("Chapter 365", TimeSpan.FromTicks(35875059863));
					_chapters.AddChapter("Chapter 366", TimeSpan.FromTicks(23979240136));
					_chapters.AddChapter("Chapter 367", TimeSpan.FromTicks(34507870068));
					_chapters.AddChapter("Chapter 368", TimeSpan.FromTicks(24506569841));
					_chapters.AddChapter("Chapter 369", TimeSpan.FromTicks(14118890022));
					_chapters.AddChapter("Chapter 370", TimeSpan.FromTicks(35912209977));
					_chapters.AddChapter("Chapter 371", TimeSpan.FromTicks(35577840136));
					_chapters.AddChapter("Chapter 372", TimeSpan.FromTicks(29395990022));
					_chapters.AddChapter("Chapter 373", TimeSpan.FromTicks(20271249886));
					_chapters.AddChapter("Chapter 374", TimeSpan.FromTicks(35384650113));
					_chapters.AddChapter("Chapter 375", TimeSpan.FromTicks(870509977));
					_chapters.AddChapter("Chapter 376", TimeSpan.FromTicks(6406379818));
					_chapters.AddChapter("Chapter 377", TimeSpan.FromTicks(6257770068));
					_chapters.AddChapter("Chapter 378", TimeSpan.FromTicks(14646450113));
					_chapters.AddChapter("Chapter 379", TimeSpan.FromTicks(9816929931));
					_chapters.AddChapter("Chapter 380", TimeSpan.FromTicks(13346360090));
					_chapters.AddChapter("Chapter 381", TimeSpan.FromTicks(7453829931));
					_chapters.AddChapter("Chapter 382", TimeSpan.FromTicks(3047849886));
					_chapters.AddChapter("Chapter 383", TimeSpan.FromTicks(10730630158));
					_chapters.AddChapter("Chapter 384", TimeSpan.FromTicks(10730629931));
					_chapters.AddChapter("Chapter 385", TimeSpan.FromTicks(6279829931));
					_chapters.AddChapter("Chapter 386", TimeSpan.FromTicks(12402470068));
					_chapters.AddChapter("Chapter 387", TimeSpan.FromTicks(8999590022));
					_chapters.AddChapter("Chapter 388", TimeSpan.FromTicks(9601449886));
					_chapters.AddChapter("Chapter 389", TimeSpan.FromTicks(5477350113));
					_chapters.AddChapter("Chapter 390", TimeSpan.FromTicks(439780045));
					_chapters.AddChapter("Chapter 391", TimeSpan.FromTicks(2066800000));
					_chapters.AddChapter("Chapter 392", TimeSpan.FromTicks(17893759863));
					_chapters.AddChapter("Chapter 393", TimeSpan.FromTicks(16236780045));
					_chapters.AddChapter("Chapter 394", TimeSpan.FromTicks(9363439909));
					_chapters.AddChapter("Chapter 395", TimeSpan.FromTicks(5447630158));
					_chapters.AddChapter("Chapter 396", TimeSpan.FromTicks(4288490022));
					_chapters.AddChapter("Chapter 397", TimeSpan.FromTicks(4288490022));
					_chapters.AddChapter("Chapter 398", TimeSpan.FromTicks(5202659863));
					_chapters.AddChapter("Chapter 399", TimeSpan.FromTicks(6116360090));
					_chapters.AddChapter("Chapter 400", TimeSpan.FromTicks(2148539909));
					_chapters.AddChapter("Chapter 401", TimeSpan.FromTicks(4206750113));
					_chapters.AddChapter("Chapter 402", TimeSpan.FromTicks(2988169841));
					_chapters.AddChapter("Chapter 403", TimeSpan.FromTicks(6376430158));
					_chapters.AddChapter("Chapter 404", TimeSpan.FromTicks(7431539909));
					_chapters.AddChapter("Chapter 405", TimeSpan.FromTicks(1264319954));
					_chapters.AddChapter("Chapter 406", TimeSpan.FromTicks(8040830158));
					_chapters.AddChapter("Chapter 407", TimeSpan.FromTicks(10128769841));
					_chapters.AddChapter("Chapter 408", TimeSpan.FromTicks(6517600000));
					_chapters.AddChapter("Chapter 409", TimeSpan.FromTicks(9571490022));
					_chapters.AddChapter("Chapter 410", TimeSpan.FromTicks(13472440136));
					_chapters.AddChapter("Chapter 411", TimeSpan.FromTicks(11934359863));
					_chapters.AddChapter("Chapter 412", TimeSpan.FromTicks(7751050113));
					_chapters.AddChapter("Chapter 413", TimeSpan.FromTicks(16593209977));
					_chapters.AddChapter("Chapter 414", TimeSpan.FromTicks(4689729931));
					_chapters.AddChapter("Chapter 415", TimeSpan.FromTicks(7060250113));
					_chapters.AddChapter("Chapter 416", TimeSpan.FromTicks(9222259863));
					_chapters.AddChapter("Chapter 417", TimeSpan.FromTicks(5090970068));
					_chapters.AddChapter("Chapter 418", TimeSpan.FromTicks(10990700000));
					_chapters.AddChapter("Chapter 419", TimeSpan.FromTicks(2341729931));
					_chapters.AddChapter("Chapter 420", TimeSpan.FromTicks(7899660090));
					_chapters.AddChapter("Chapter 421", TimeSpan.FromTicks(8717000000));
					_chapters.AddChapter("Chapter 422", TimeSpan.FromTicks(5209859863));
					_chapters.AddChapter("Chapter 423", TimeSpan.FromTicks(3857530158));
					_chapters.AddChapter("Chapter 424", TimeSpan.FromTicks(3352259863));
					_chapters.AddChapter("Chapter 425", TimeSpan.FromTicks(5826580045));
					_chapters.AddChapter("Chapter 426", TimeSpan.FromTicks(7260870068));
					_chapters.AddChapter("Chapter 427", TimeSpan.FromTicks(6331839909));
					_chapters.AddChapter("Chapter 428", TimeSpan.FromTicks(2557209977));
					_chapters.AddChapter("Chapter 429", TimeSpan.FromTicks(2505200000));
					_chapters.AddChapter("Chapter 430", TimeSpan.FromTicks(781350113));
					_chapters.AddChapter("Chapter 431", TimeSpan.FromTicks(10767780045));
					_chapters.AddChapter("Chapter 432", TimeSpan.FromTicks(13212609977));
					_chapters.AddChapter("Chapter 433", TimeSpan.FromTicks(16608069841));
					_chapters.AddChapter("Chapter 434", TimeSpan.FromTicks(10723200000));
					_chapters.AddChapter("Chapter 435", TimeSpan.FromTicks(11934360090));
					_chapters.AddChapter("Chapter 436", TimeSpan.FromTicks(12736839909));
					_chapters.AddChapter("Chapter 437", TimeSpan.FromTicks(11637140136));
					_chapters.AddChapter("Chapter 438", TimeSpan.FromTicks(12447049886));
					_chapters.AddChapter("Chapter 439", TimeSpan.FromTicks(11273280045));
					_chapters.AddChapter("Chapter 440", TimeSpan.FromTicks(8665219954));
					_chapters.AddChapter("Chapter 441", TimeSpan.FromTicks(1242260090));
					_chapters.AddChapter("Chapter 442", TimeSpan.FromTicks(4295919954));
					_chapters.AddChapter("Chapter 443", TimeSpan.FromTicks(3664570068));
					_chapters.AddChapter("Chapter 444", TimeSpan.FromTicks(3842669841));
					_chapters.AddChapter("Chapter 445", TimeSpan.FromTicks(4095299999));
					_chapters.AddChapter("Chapter 446", TimeSpan.FromTicks(1962780045));
					_chapters.AddChapter("Chapter 447", TimeSpan.FromTicks(3879819954));
					_chapters.AddChapter("Chapter 448", TimeSpan.FromTicks(781350113));
					_chapters.AddChapter("Chapter 449", TimeSpan.FromTicks(2564639909));
					_chapters.AddChapter("Chapter 450", TimeSpan.FromTicks(2869280045));
					_chapters.AddChapter("Chapter 451", TimeSpan.FromTicks(2921300000));
					_chapters.AddChapter("Chapter 452", TimeSpan.FromTicks(5321540136));
					_chapters.AddChapter("Chapter 453", TimeSpan.FromTicks(2683529931));
					_chapters.AddChapter("Chapter 454", TimeSpan.FromTicks(2891580045));
					_chapters.AddChapter("Chapter 455", TimeSpan.FromTicks(4674869841));
					_chapters.AddChapter("Chapter 456", TimeSpan.FromTicks(2274850113));
					_chapters.AddChapter("Chapter 457", TimeSpan.FromTicks(2304580045));
					_chapters.AddChapter("Chapter 458", TimeSpan.FromTicks(2282279818));
					_chapters.AddChapter("Chapter 459", TimeSpan.FromTicks(3047850113));
					_chapters.AddChapter("Chapter 460", TimeSpan.FromTicks(4013560090));
					_chapters.AddChapter("Chapter 461", TimeSpan.FromTicks(4229049886));
					_chapters.AddChapter("Chapter 462", TimeSpan.FromTicks(3842670068));
					_chapters.AddChapter("Chapter 463", TimeSpan.FromTicks(5589039909));
					_chapters.AddChapter("Chapter 464", TimeSpan.FromTicks(3575170068));
					_chapters.AddChapter("Chapter 465", TimeSpan.FromTicks(2891580045));
					_chapters.AddChapter("Chapter 466", TimeSpan.FromTicks(2319439909));
					_chapters.AddChapter("Chapter 467", TimeSpan.FromTicks(5128119954));
					_chapters.AddChapter("Chapter 468", TimeSpan.FromTicks(4273630158));
					_chapters.AddChapter("Chapter 469", TimeSpan.FromTicks(766489795));
					_chapters.AddChapter("Chapter 470", TimeSpan.FromTicks(4050950113));
					_chapters.AddChapter("Chapter 471", TimeSpan.FromTicks(4422239909));
					_chapters.AddChapter("Chapter 472", TimeSpan.FromTicks(4229050113));
					_chapters.AddChapter("Chapter 473", TimeSpan.FromTicks(5142980045));
					_chapters.AddChapter("Chapter 474", TimeSpan.FromTicks(6607000000));
					_chapters.AddChapter("Chapter 475", TimeSpan.FromTicks(3196449886));
					_chapters.AddChapter("Chapter 476", TimeSpan.FromTicks(2327100000));
					_chapters.AddChapter("Chapter 477", TimeSpan.FromTicks(818500000));
					_chapters.AddChapter("Chapter 478", TimeSpan.FromTicks(5105829931));
					_chapters.AddChapter("Chapter 479", TimeSpan.FromTicks(5128120181));
					_chapters.AddChapter("Chapter 480", TimeSpan.FromTicks(5990279818));
					_chapters.AddChapter("Chapter 481", TimeSpan.FromTicks(6049490022));
					_chapters.AddChapter("Chapter 482", TimeSpan.FromTicks(5061250113));
					_chapters.AddChapter("Chapter 483", TimeSpan.FromTicks(1858980045));
					_chapters.AddChapter("Chapter 484", TimeSpan.FromTicks(1769590022));
					_chapters.AddChapter("Chapter 485", TimeSpan.FromTicks(1360919954));
					_chapters.AddChapter("Chapter 486", TimeSpan.FromTicks(2274849886));
					_chapters.AddChapter("Chapter 487", TimeSpan.FromTicks(7476360090));
					_chapters.AddChapter("Chapter 488", TimeSpan.FromTicks(3515959863));
					_chapters.AddChapter("Chapter 489", TimeSpan.FromTicks(4132680045));
					_chapters.AddChapter("Chapter 490", TimeSpan.FromTicks(2988170068));
					_chapters.AddChapter("Chapter 491", TimeSpan.FromTicks(3523160090));
					_chapters.AddChapter("Chapter 492", TimeSpan.FromTicks(2126479818));
					_chapters.AddChapter("Chapter 493", TimeSpan.FromTicks(2862090022));
					_chapters.AddChapter("Chapter 494", TimeSpan.FromTicks(2787550113));
					_chapters.AddChapter("Chapter 495", TimeSpan.FromTicks(1881039909));
					_chapters.AddChapter("Chapter 496", TimeSpan.FromTicks(2297150113));
					_chapters.AddChapter("Chapter 497", TimeSpan.FromTicks(1412929931));
					_chapters.AddChapter("Chapter 498", TimeSpan.FromTicks(3820370068));
					_chapters.AddChapter("Chapter 499", TimeSpan.FromTicks(2371449886));
					_chapters.AddChapter("Chapter 500", TimeSpan.FromTicks(4221609977));
					_chapters.AddChapter("Chapter 501", TimeSpan.FromTicks(7000580045));
					_chapters.AddChapter("Chapter 502", TimeSpan.FromTicks(2638939909));
					_chapters.AddChapter("Chapter 503", TimeSpan.FromTicks(1524620181));
					_chapters.AddChapter("Chapter 504", TimeSpan.FromTicks(2089090022));
					_chapters.AddChapter("Chapter 505", TimeSpan.FromTicks(3708919954));
					_chapters.AddChapter("Chapter 506", TimeSpan.FromTicks(4050949886));
					_chapters.AddChapter("Chapter 507", TimeSpan.FromTicks(3664570068));
					_chapters.AddChapter("Chapter 508", TimeSpan.FromTicks(2742970068));
					_chapters.AddChapter("Chapter 509", TimeSpan.FromTicks(5024100000));
					_chapters.AddChapter("Chapter 510", TimeSpan.FromTicks(7297790022));
					_chapters.AddChapter("Chapter 511", TimeSpan.FromTicks(6703359863));
					_chapters.AddChapter("Chapter 512", TimeSpan.FromTicks(6227819954));
					_chapters.AddChapter("Chapter 513", TimeSpan.FromTicks(6822250113));
					_chapters.AddChapter("Chapter 514", TimeSpan.FromTicks(2379109977));
					_chapters.AddChapter("Chapter 515", TimeSpan.FromTicks(6866829931));
					_chapters.AddChapter("Chapter 516", TimeSpan.FromTicks(5336400000));
					_chapters.AddChapter("Chapter 517", TimeSpan.FromTicks(6740750113));
					_chapters.AddChapter("Chapter 518", TimeSpan.FromTicks(1100849886));
					_chapters.AddChapter("Chapter 519", TimeSpan.FromTicks(6383860090));
					_chapters.AddChapter("Chapter 520", TimeSpan.FromTicks(6168380045));
					_chapters.AddChapter("Chapter 521", TimeSpan.FromTicks(4749169841));
					_chapters.AddChapter("Chapter 522", TimeSpan.FromTicks(4868290022));
					_chapters.AddChapter("Chapter 523", TimeSpan.FromTicks(2401170068));
					_chapters.AddChapter("Chapter 524", TimeSpan.FromTicks(4860629931));
					_chapters.AddChapter("Chapter 525", TimeSpan.FromTicks(3842670068));
					_chapters.AddChapter("Chapter 526", TimeSpan.FromTicks(1450080045));
					_chapters.AddChapter("Chapter 527", TimeSpan.FromTicks(4608000000));
					_chapters.AddChapter("Chapter 528", TimeSpan.FromTicks(3538019954));
					_chapters.AddChapter("Chapter 529", TimeSpan.FromTicks(1494660090));
					_chapters.AddChapter("Chapter 530", TimeSpan.FromTicks(2267419954));
					_chapters.AddChapter("Chapter 531", TimeSpan.FromTicks(2906439909));
					_chapters.AddChapter("Chapter 532", TimeSpan.FromTicks(2943590022));
					_chapters.AddChapter("Chapter 533", TimeSpan.FromTicks(12833429931));
					_chapters.AddChapter("Chapter 534", TimeSpan.FromTicks(3634609977));
					_chapters.AddChapter("Chapter 535", TimeSpan.FromTicks(1918012471));
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
					Album = "50 Self-Help Classics to Guide You to Financial Freedom",
					AlbumArtists = "Napoleon Hill, George Samuel Clason, James Allen, Benjamin Franklin, Henry Harrison Brown, P. T. Barnum",
					Asin = "2291082140",
					Comment = "This Audiobook contains the following works: \"The Art of Money Getting: Golden Rules for Making Money\" [P.T. Barnum] Starts at Chapter 19, \"As...",
					Copyright = "&#169;2020 Napoleon hill;(P)2020 Prosperity Audio",
					Generes = "Audiobook",
					LongDescription = "This Audiobook contains the following works:\n\nThe Art of Money Getting: Golden Rules for Making Money [P.T. Barnum] Starts at Chapter 19\nAs a Man Thinketh [James Allen] Starts at Chapter 40\nThe Science of Getting Rich [Wallace D. Wattles] Starts at Chapter 49\nMorning and Evening Thoughts [James Allen] Starts at Chapter 67\nThe prophet [Khalil Gibran] Starts at Chapter 131\nDollars Want Me: The New Road To Opulence [Henry Harrison Brown] Starts at Chapter 159\nThe Art Of War [Sun Tzu] Starts at Chapter 166\nThe Tao Te Ching [Lao Tzu] Starts at Chapter 179\nThe Way to Wealth [Benjamin Franklin] Starts at Chapter 260\nThe Richest Man in Babylon [George Samuel Clason] - Starts at Chapter 261\nMeditations [Marcus Aurelius] - Starts at Chapter 280\nEvery Man His Own University [Russell H. Conwell] - Starts at Chapter 293\nHow to Get What You Want [Wallace D. Wattles] - Starts at Chapter 299\nSelf Development And Power [L. W. Rogers] - Starts at Chapter 305\nSelf-Reliance [Ralph Waldo Emerson] - Starts at Chapter 307\nThe Game of Life and How to Play it [Florence Scovel Shinn] - Starts at Chapter 309\nThe Life Triumphant [James Allen] - Starts at Chapter 319\nThe Psychology of Salemanship Franklin [William Walker Atkinson] - Starts at Chapter 329\nWhat you can do with your will power [Russell H. Conwell]- Starts at Chapter 339\nThe Law of the Mastermind [Napoleon Hill] Starts at Chapter 344\nA Definite Chief Aim [Napoleon Hill] Starts at Chapter 347\nSelf-Confidence [Napoleon Hill] Starts at Chapter 349\nHabit of Saving [Napoleon Hill] Starts at Chapter 351\nInitiative and Leadership [Napoleon Hill] Starts at Chapter 353\nImagination [Napoleon Hill] Starts at Chapter 355\nEnthusiasm [Napoleon Hill] Starts at Chapter 357\nSelf-Control [Napoleon Hill] Starts at Chapter 359\nDoing More than Paid For Starts [Napoleon Hill] at Chapter 361\nA Pleasing Personality [Napoleon Hill] Starts at Chapter 363\nAccurate Thinking [Napoleon Hill] Starts at Chapter 365\nConcentration [Napoleon Hill] Starts at Chapter 367\nCooperation [Napoleon Hill] Starts at Chapter 370\nProfiting by Failure [Napoleon Hill] Starts at Chapter 371\nTolerance [Napoleon Hill] Starts at Chapter 372\nThe Golden Rule [Napoleon Hill] Starts at Chapter 374\nFrom Poverty to Power [James Allen] Starts at Chapter 375\nThe Way of Peace [James Allen] Starts at Chapter 383\nAll These Things Added [James Allen] Starts at Chapter 390\nByways to Blessedness [James Allen] Starts at Chapter 405\nThe Mastery of Destiny [James Allen] Starts at Chapter 420\n",
					Narrator = "Mark White, James Ellis, Andrew Farell, Jason Cooper, Michael Rowe",
					Performers = "Napoleon Hill, George Samuel Clason, James Allen, Benjamin Franklin, Henry Harrison Brown, P. T. Barnum",
					ProductID = "BK_EDEL_014569AU",
					Publisher = "Prosperity Audio",
					ReleaseDate = "30-Jan-2020",
					Title = "50 Self-Help Classics to Guide You to Financial Freedom",
					Year = "2020",
					CoverHash = "4898562e1b94e5357f14c70ce73082d6b0f49d48"
				};

				return _tags;
			}
		}
	}
}
