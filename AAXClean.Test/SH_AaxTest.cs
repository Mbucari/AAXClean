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
		public override uint TimeScale => 44100;
		public override uint AverageBitrate => 125588;
		public override uint MaxBitrate => 128008;
		public override long RenderSize => 64416193;
		public override TimeSpan Duration => TimeSpan.FromTicks(3421559002267);
		public override string SingleM4bHash => "3a79ff278f2c1c20766bed5ce4d8017fb2c98d54";
		public override List<string> MultiM4bHashes => new()
		{
			"c07e71a4d63f1384affae721920a995a51bce720",
			"35b3ab7394732154b6d0a2d85992bc56feaf3267",
			"cce9929d3c1e0ba1cd66bff59be59350861c8aaa",
			"2276a4c66d4c8d47aa9606cc3a3dc9c4921cda9f",
			"f4777ac7e29b1bada1fe3d25091b0199599d5d27",
			"b1271aa946b8abbda7b97f0f05089930a4613226",
			"9d1170ada007023e166cadefe10ad05287ab2cc6",
			"1fd74721ec852d4dba29ceba7d3ccdf00612e313",
			"d15dcc9ce2439c51910bb4fd904662628b467716",
			"4750adce9b86582f41ea25ebe45404c5f1f009e9",
			"f563359a582bd763d41971eda137e838dafb78bc",
			"fa215803801a42e585a191ecddc37113effc3735",
			"9b6055584df203e0e5a14eedcbd760b1834e54eb",
			"a50ca490a9e082e1fd440249c61d1ee7158ccb91",
			"33c8b2759e256a4e126ca47484b57e83d1e070ff",
			"9350bee188b50b642cdb2eb3d054d71a6a2457ee",
			"1a7b8b9547e4418dbf16e705ca8391e58e79c294",
			"5f5715b443a612c144b2a6b1e666e86625358706",
			"fc92d74a9bc1cb304686519a8275d8448b333b96",
			"2da962f56d765c857fbc8412a9f9f44a342f797a",
			"6cc4143a38268191170dad955ee00603041971a9",
			"1509720d5a2556ab5f06dc07c722bb60aaf3e20c",
			"37936b3aa8b1d6c55a4506190ea03152ffde096f",
			"5f55fd62690368b39deab431f15db0337f6663b0",
			"5de540bf3612329f6a0e10e738e13cc23935d95d",
			"58e5e1c1c2964f529baf5b9296f8ddb827e5bd13",
			"436684c3f81d6fb4f920b29ee9a7530d3e3cd101",
			"379e83305a6313058eff308f21637e8322244dcc",
			"a8ce930e2639c6ecb95b5f163e8d7ed0c2b2d09b",
			"4157235419bcfe77924de296d38e4913cba3a12d",
			"aadca7d09e68cbea1ae4e40fb26945386cbae774",
			"73456ff909cafbaad909d42669ef1306efbae17c",
			"fc63ae18c23111177d8d7ad6d4478af9fa92a47e",
			"4ec0592e1fc63c773ced0216c0e4fb05a866dd5f",
			"2d5e3346b7be8b8b60a5debabe37bb85a0a01af8",
			"005f3b78826c884585f7bb01b58e916e01cfec6f",
			"fc7c8396e6a4d7d1d7a6f8b759ec5253d71ff71b",
			"bbfd69f1af523b496642e84cf6ba818cc6b0ec0b",
			"2be364125b2e96938738af3d2418d54fadb954a6",
			"c4cc905ba08c1d54f90b17d3a9159327344c4319",
			"f3eeefae3f3b3e76801f68412ec8fa274cc02ded",
			"d9bac809acaa5402b591261770ea18234e70bc17",
			"53493e23e87e45f4a0d8590595d6161cb3578dd6",
			"6a1dfa7086b79602d4b081e7cfe089dcb7f1b1e5",
			"2001d27378f81a1bdb3fd947baffa87dd6479ab9",
			"a328869e3d8126a44afbfaa826a1871561c6faa4",
			"bb45db6373f297787bae4aa3e9bd71156d4745db",
			"43e49f8313799099f06fefff540e187e8d373ce4",
			"68a7d683a23ee346fc1f7b9240cb69fc8f6266db",
			"d36d9949799246f5fa76e3479dcc9e987b5123b5",
			"eadc72a1130a307a53e3a6b282d715f44d12c882",
			"a4473a8c218dce3da9372bdd4363a45e14bf5dec",
			"0ced92eb1e57898f2340ba4c798706cf5f94fbc1",
			"ee21fba86176ba5f2f2b71cc23ba3732312d572e",
			"c2d09674ca8af5765bef53a2b10675f9fe0e609a",
			"e1be75e2ca49ff6dd5ced5a5e9b9fdfc115619d6",
			"8e795e736f1d17582b24963648d6f77a74b3673e",
			"d14842fa9ea7bf897a3a34efb1a981c0af177f26",
			"928c8b3e7f1b3c9aed9980cbbf608548616d4169",
			"6d95f13a5f7eb84538bdb642ff7e1bbf59ed9faa",
			"9afd5ed21627715083a2e6e4a21f2a010d47878d",
			"e75e685bd9caaf6c9a88b2fba223715442649831",
			"beced35aa45f3fa56a4c9b4ad18a51ed8fbf232c",
			"0b0b4e560adbf45a2dd9337f23a543710033c0c0",
			"8385a5fd5d24e3be1ff33e74d53326a493d77958",
			"b2bdc527efb3db4ce0c3e2ee91e34866a5b9ebef",
			"7870a33a188bda0116bca9abb3c6ada860a10ab1",
			"6bab044532282ef298c1bc798408c613cd205c85",
			"0eaa9d4a6d40846271034174e5646ac774af08dd",
			"71495921e4a580481b5719a63f5d76ecefd49226",
			"0c6ffa38f73d9fe5bfcc67572b547453982943b1",
			"73c6f81a4a24b5695e8ef1a8d7cb9636d064d169",
			"24b96eb7a059b6b0ee45fc2cd5978d654c6daaf8",
			"b7ffd9fb593ffe272d253a0bec6dc8162c460577",
			"99129f8639572e32a554852d5de15eb5c62278ff",
			"29659d47222ccf06da51c72a709ceb470364d1b8",
			"928ff444e48a05befbdc3a0902faacccee34cf04",
			"61a106ac36d7966c1a72492d44c5da9c208d9243",
			"a19b978468ab7a93d012a7d96de1321ac9a7c1f8",
			"164704dfcc0a624eaee8c553f17993a20052c244",
			"c625195ec65d078e8f2b3ff3db27505c71ca9a81",
			"759217a8b0de39bbdb0f76d4e9730512fb8260e6",
			"47695d83ee5d75a5d0c289162ef81e5cfd0e872d",
			"5a70bbe98ca48a514850dfe7b7ef38cc1028c5bb",
			"0f35207970d9824dcb8c869bce38ef028687689d",
			"06f442b8c39bcea51244bd894ed1868f869dc308",
			"0ed9d5e41911aecd45915f9bd0886b4e3aebdf3b",
			"d5f8924f0fdc6de10d98c70b47bdc7b25ba9bbb1",
			"8bc5b48c9680d794143442692152cfebeb34bbcc",
			"596730f2fafeb12dd7d060e34288fbe828f43f8c",
			"520de04a9cb32e921ab3c5cda841464234c0862e",
			"be65470f7aca041f90b361bf779a3cd944f68af4",
			"195425bd33db520bb7f24c46a1ac3cf16811ecc4",
			"ec1d40e85334ab9a7c70571bb93ba9adc5d1b232",
			"3c7d4abd8a532dfed489307782d628a528cbd540",
			"b9eb67335e16b55a379588eb2dc2c5bc53275ce2",
			"abbcb6de1ef2b16b6c8160e5dbc21f94444808e9",
			"f25a64fb17fcdd8d7799e9022f3a95ae4a4e489a",
			"102d9fabf2141db6f6f32459b89786b7168505e6",
			"05aa33cde2cd93c549b53c34676e6282a7193b58",
			"2fc7a36183a0b7f059fda321f471e8b4127701cb",
			"c8c5286049bd0a904dee4f9ee3e7cd24fb547468",
			"b0cef69d1b54f3bdb0c8e9442129cbdc88a7b070",
			"eb8174d1a63a148a1023ece93734783ae0f666d4",
			"316407be65c80aa33515bbb00b02df1f0ac0d352",
			"7e36b3e3debdf5dc464d29f1cd64c91c7595591d",
			"71c4a62bf6982421872bcd264f1e0e2affe82e8f",
			"c4bc31f72728e4b940534b83bae18aa5736ae98e",
			"26bbe139c2db7f8c9302a8c6e31ec49bbfe80440",
			"1aefec590c5fbcd57efe2396854108f94e3bbe17",
			"5f99f9038c36a71b8b854f2a48898cb76c55b8f8",
			"e397175a6c1ade64f592f96d27a1f49d32b18f8c",
			"6e1b805043f213d69324a17205672323e0f55bd4",
			"4a54f5d2092a4923df6add20ccc75777199112f8",
			"f355bb74c7fb25518729f7ae233877e6562935ee",
			"f322d21f665fdfe70c0d9ed3d1ff8da9c6e08970",
			"4224cc677502710b508017dd19f913125790667a",
			"7acb9df2394dd7869f65b977b0fefc38f043a66a",
			"1ba3de4b9834f393df39926c11e2932afd46041b",
			"3d514fac0113d5a30bf8585985198a3b640513e3",
			"13172c6dd26250eb7573c3857b42c7429c81ba8b",
			"e2b00fb9c3d56badddf400f7e7db43e8d70ba076",
			"884136464ed809d978906d0d4dfe1bb409ba0af1",
			"7b59dbf5a4ad792d0d854cf3538c152de2547c3d",
			"cc056ff9fbd7eb49047e0b3f4be56c0187a55cde",
			"c987ef31966c9e034494f5573b80465b3fef553b",
			"5ed49ed349e246244d06b9f0d56ab1b445edb6e3",
			"cca57e42276ff15a4239aa345bd08bd6b6be4bf8",
			"50796b17f844a4f86d978ecb39eff591615cdd6e",
			"c47a20325e573977d45d790571fa52da41771fb9",
			"b0247cb07fe51707b61b19a6f1b2f27ce7abc687",
			"f4cf2e950f30bca64bfa86461de913038ac3e328",
			"ff32322f19d8c276120a693d028d5ec90d0e0e63",
			"9a9e733c38e0a4f4eae701ef9202855e420100a0",
			"ac1e1ee87fa9b37a8cc2eaf2002d9de81a4a70c9",
			"5f9aad0afd3deb520908cc4c350240b8bb8f344f",
			"26e93353ed1d2759f14f9730b03af3b54c2d6294",
			"ff37184299603d18bf0b7129ad43056f9ecfa8af",
			"b356876ca8834abc417e5d2f4b5d0c25f328725a",
			"841b91a4c71d09df77de00dc45780165d41049a2",
			"799248af60a62b593cfe993eac320da19135736e",
			"c4225a488843f74b76399e4800dd8f5cd63e0cc3",
			"be7bbbbdbe060303a55405e059e3741b23605447",
			"5edbdfac1e3e102a236a1fd87b02ca88166476de",
			"7e886819417124bb5719e2656b7156e23ae3a206",
			"34872153a3c4015af92c08be40e7f8502ad1343a",
			"46fa4a547414ce494d79dcd8ae7a8e3f65a57775",
			"4566ab3613df4242377ff445cd6cac3c4dae85c2",
			"27c4ffa20d4bf60e1882cf440339a30eff993a50",
			"06ae4450b173b88001e4700efa56ebdf0d64bc71",
			"32d8b10251463e5740c541c5f67e5ebdbd191f42",
			"e385fc828e8394a4f7b498586ec2d7498b7f1290",
			"49cc2646cfd0127103d90bde915702cadf6ce4ef",
			"4bc089266545d57850d795e9980af4d2b2704c6d",
			"2d8aa7790fcfa3ea491c4d807c1116586fd6ecf9",
			"330fb440d247178b9991e62784599e0627a1f0d9",
			"42311376eb55dd352028b2aa7cd70c324f08c4af",
			"fcd423e84b9b33b5eaa03019c51eb518bca5e9fb",
			"9954491053384c2157cad49d428da13cecb6ee10",
			"de3b9073f920220f6808de47d737dcb9dddc3a25",
			"19f54b8b2411d4deb9b399c59111459901b1588a",
			"d4ab0aae29afce329e9b74120c0a413231faac75",
			"04f0bac2316af0012f5520009cbe9484dc046484",
			"3bec1e9dc6e2fbbdd6bf7f3361b23a5b8721a6fe",
			"e6e8de209958b8d5cfdc87a82b005c57a57c557d",
			"00118ad53f57a96aee6a065a3bcab515f652653c",
			"c59329c3958cc9f65635c427f420cea82008a12c",
			"0693c2d1d4b5ab8b2562f0f70abf47a9923738cd",
			"f7c92f07a810341cb6ef507ea880ec1b440fed78",
			"635971205434938b4fed41947b6ab6b7865d7c75",
			"aa423467e8c8e733d9b71db8ed2ade75b3178f0d",
			"99007624f6d90cebaddce6592d0ab1709480ed07",
			"4c8ab6197c976fae8124e1295643f257138ae8ea",
			"c0a4ac53c24d879b5213d1496fb3c3c1e9b79677",
			"f466859fba8d1aebb775831f3f3a7857ef153c51",
			"fb40daafcef88ca52c5d3e7516aa8ef2f67c570d",
			"7110afc4be27ffc8978f8aec907d36582940c933",
			"884502418b1199b9b9acb573a858097dc2fb9ccd",
			"0828e27c13012a4b067946d8bff5dcefb63ecb9d",
			"afe81d836c5c5aa0b16430c4e8261a0683a21d1b",
			"83d5d75f0d7caf37881e6885600f87d53637e924",
			"31d4c81f02a96dd89e971ddaa297c99c34751726",
			"5fe2739ee2d5b30ed668df0eb0d6259aa3985668",
			"8fd062175902409d5f63134af954a04630ffd615",
			"c3399f7900617bd96115d4b9b29245676ba8356c",
			"96ea3a9db6a55e36bc1d6bb9adba58749978bfb6",
			"ec12d48a77ce9b57f2fac2bc990ba0c26f3c6b41",
			"b085506d6db4045ed00277ffaa9d52448b3a4200",
			"15a51e3968c347e52e93771816dfcc13d559c1c6",
			"2df8ae15ee9cf8eddd6ce485ffbbde7c2fc7584b",
			"607a3050efcb44dd9008bd8cfe0c4793b0ef9062",
			"d91a9ceb73de1f1ca977227076bf2a48ceaa44bc",
			"2e798cd213b7af8a5e69766bcdc78519e1ebe649",
			"3ce5c01a648ec53386a3700d9a3eb4890a0ca176",
			"fd60bd1edbeb3a9ead918c722d7372268f16b658",
			"408e98a2e83075ff9ee173010528a56a6a166dac",
			"79b8d7966f059c32c6d1dd28d50f0dac818d1183",
			"dcb0449eb8cbd190666a50186f204d98568d7786",
			"4fc6b371cc85292b870475b7da3ca292418f543b",
			"b41dfad999d6bc8b7e9f5266a6e17beffd3b1fe7",
			"e7d4f4e72525fe86656a9e3b06fd5c80edb976e8",
			"51ea00387a80b0ca75cbe4389971a0aa5a66015d",
			"193c31ccf9bc7a45abf16f096391c58efe6c88eb",
			"12950b11626f32c2d247dd896108d0c7f4a09774",
			"2da299417d92d2da26672ded0f254ea2d10c0c0a",
			"52e0bfd9f50b0e6725cbc490b0fb1854fbc3ef2a",
			"4821423df0d4c21af11148ab824eb9cbe0800530",
			"c125d48a270862052796adf35dab3f3bd0cddb69",
			"3788e7e674bbf85f937e79e8df2eaff22cc0ff58",
			"aa21918fadbbed24e596b671b68fe304256e6e0c",
			"94447dc5ce4e464976117a20d9931c296f131f9a",
			"84c3d26b7546f1b0bb9fcfc4c5c0eb831e1b0bc7",
			"2c41745140a5de9f6ad74f19feb1466e900d0062",
			"2b77511f5f4f43d12c1c6739878629c348068ccf",
			"cfba5def558ee387aacdf3234ee79ba6de6619de",
			"fd0bc1684e7dfd94769567a3dc092fc6d052815d",
			"032b7264fc9b50b3b6334bf955d5ba3fba1ae865",
			"dac07e205678f93e5f8ddc9673650e6f48c3a471",
			"8ff5efc089a606aa64380af97faaf7cbc8b86705",
			"82a95f24f18df721dfb47bd3b5b1b768af53d636",
			"df608354f7c65f76d338e2c86de95768ad4cd890",
			"bb9ca736a8763b0c5a03886114a59e5ad21f971e",
			"3572d392ebaee7786566baf2b8ca0781edd9cf64",
			"bf2a27244be6b063fff71574cb706a2e4ebafd3c",
			"ed1fe788ff51bafdad27a43d63397597400c187e",
			"e8e07f9a1ef607a2035d0bce1132f3d702de9433",
			"2bda2b44690121e9072c2e819db26b8e9a0508c8",
			"a777372bd1d50d6343f6312e3ffc6388ea38f2ca",
			"374d7951254d8ed998668f8201de727d5edd4110",
			"ec8c5fb514e53fbaf1c1cca9108330251bc6c84d",
			"58eff6144780e6410a6308c080229b6f6e50cc31",
			"a1900ef715841ae2afd0ad7b9b097e613277b94a",
			"00e3213a855589623eaf328d973216a79eb5a664",
			"f63e147f05944ad86806cb134b1208df707cd8e9",
			"88bb61ffe608d70188132e866b237f02299d9623",
			"a94f5cf916a8d3bc1e50eb1f000ff122b0645617",
			"2420a7dba34178ccce5228b557168a2be362d306",
			"f7cf82c0a1a3864f524cc1a3cdb9529379a8d131",
			"2a714f177d6de9699bcf4f8e5252050d9b19825e",
			"04ef6fbfce01c7563b815c8f39515f7817585636",
			"d9e072f18c92e7ba9bd043d6c2850af3a20af3af",
			"d0fc54a6b1d976bceda47f977d194154bafbaa62",
			"850eb12655e9d8f598fb685be86de6d68d5e584e",
			"6a78d60af50ab14f55e6b132da678d46b91e7d7a",
			"2e16200189a7c48c5d5ee9b4b663269c27217e49",
			"6402e133171116979a4d48f660e18b926bd655b3",
			"2bb65f48102a29304c05c4557873af1ceda47769",
			"0c9712b92161780545bf49c88b325d6149d8d18b",
			"12ddd6c1c35fde22484b5ff4509e212604983c2f",
			"7e4aca219e06adc40c540ec4e25f8c72b5f1258d",
			"a22f90585115f44d8d4915e01708ef7ac2aaff5b",
			"95b0e02aa7d15537eada43acda61f0584ee60f5c",
			"56ffacca3e7ac48f31141392141df291f02bea5c",
			"2a5d6f747d7f6380bd6a9e6558cd47c41030336f",
			"464356ed656bd410a31bf1401fa7050bf34e58dd",
			"1d95893bd2e67e422abd820e7c45b3a3b39a4c79",
			"0b53f46e084638ea755bef2bde5493d41ec0e32d",
			"e4a4d9fff4da5fc0d946af2a2b4dafcc39689d0d",
			"5a0a8e25440f7daa844c070dece9250ae25ee521",
			"1119c4239cc754c348ab516ae6c0c0710091bb05",
			"f83e79d19afa62725734a42946757793d844e4c2",
			"97836b3b00021456fc19c1444592214752e94d01",
			"0213c18439d3eaaec95224a8cf89193c274352c7",
			"50886a1414a43596723a40ac0739a4851f2f684a",
			"2b48636bf5cae03019d948e67a6f388286b69b38",
			"bff272c9f316c12f928d2318e59d319fe4c9b7a8",
			"08c5ac605a4b4dc735cffd94b21c01a2cc7c3d32",
			"8dfd28a6a137b9128824d2755310395166eec34f",
			"8db41afe2b5e4e95b354e741dbe72ff636c670a4",
			"311cce0153968a515072c7372d736429f20c6568",
			"f279c356ab808a11b0758d9fc825329ad68adadd",
			"f85a5a2d490aff158a14514b4e2d0bf2fc7c1f4c",
			"6360d03f45b8f94ed43da4a8eb0a56b0f30dd271",
			"038d233742a4e93aa74cb9cb3d2f5f1d5ba84a30",
			"8b917ca0aea20abe2502b74fa413ba1ae5903447",
			"ccc06683e1a548ccad5090872ea5d0337a0ef681",
			"f3f760130c542a704fff8854982e2a966632ee6d",
			"3dcec43bbca6d9ba30d9de66b88c257147725f7f",
			"e3878bfd389c517fb22138a97287277327abf73f",
			"1d1c4da09bb957c790df1292a9b9b8cdbdbfb1f3",
			"140d08bc8edcac1d34e0bebe2a688e262c8fe0d9",
			"bad673cce57a4ad33b08c555569ba52dac5c2eb7",
			"3485b250cc5e4ed8bb6d97b4e77032398944825d",
			"90de5ae2959c810c16f00538629b6b2549abc094",
			"a017a37c2c4d6b7120d4526e851a383f5fdc73e6",
			"d4947867b0a13cbb36fca2458905f8b53e1c3796",
			"31226b39025d8e865816eb6138e182bc2685c4c5",
			"9a6b17d276892d1a0b08c5124b856b62f2e08e34",
			"5a658134dc0bba1f138dc9ed7025475fbf4a334e",
			"fb98a689e5ad967b44fb162edf1130ef68cafaad",
			"e65efa3ced571420ef5954278026dfb29d279427",
			"b83ffba136e841e5a0c4c3f96bdd74874da99a90",
			"b0ebb9844b185eedeece961aa6b6d0efb556d74b",
			"e81a8c79b2c3b70c9cab36a7cefc939be3c91770",
			"dae6c07f7e89572d7777f01ca949935024c04d72",
			"3521fbdcde9dc8ab281b97b9b125b1c37e4edf77",
			"e530e9e9be57658121eb635c909840e40588d8d0",
			"f4ce15a94691f0ca62c514293865a82fec1cb4cb",
			"58fa7cca357bdb06d19a82adc1f782c85033cc8c",
			"268de354420c92e477129d52f4bd929f356287c0",
			"d759d769a15d513f94284272c64f8f9d8995dac6",
			"1409044fb2e28b9818cf49163620eea7ca3eb5a8",
			"e39ccbe7b6abea9d5c39a645caf20cc018c25cdb",
			"bafee780bf7fe66c21c546f8ca9af647c67256e6",
			"ad549e4b98c3c2c4ef986ea137bc51b41b1a29ea",
			"737dd820ca84eab7fcbbf7ab27fe455a49d2624f",
			"c9d095f4193ae1125164093e36ed39fe65df5c94",
			"4a485d741e6862566a335bae11469a6080592250",
			"88f125ff1cdd40fdda0e8f9e2074c9490173e5dd",
			"0cadf601346e139c6817ba6e94643417ecda794d",
			"ec22af0d62f811fa4250c92eefeafff39fcf7d02",
			"6c497470891f0002c395ae59deeed502cd5cf5c1",
			"587d3174996dc1044b9007880ef88f11110b4256",
			"ad6e10055e91e47a68819a3c4f630bbd0c88f5a4",
			"cd81de1dcf69cf583c1a55ad889802e4ed2abcb2",
			"0bd727dd9522f7be80ad91a06e3c5d29fdbd193f",
			"34444cf982d04da298df2b81e7f3f44953228ad4",
			"a3ec79296e8983ed76f6ed3056e499cbe1573114",
			"133bcfff3f274c83dff7b46b425ede110147856f",
			"dae9f788a21e2c2d49e4057fe55a621140ba7a79",
			"a6074c7e0402e7c4bed6f2e41526dc240a84d4ba",
			"6d251a7f8285543e39c7d20c9fdc95eb15029a31",
			"b1b03c709d7878b7854a1233385532324efc542e",
			"89a51aa0d06a9e06addbc18c2147e7bc3a91d598",
			"3369a8d47cc80915ce57326b11263dfcd00ede39",
			"a9ce89ddf0212d588e320866d06af0670860d1f8",
			"31638965a85bc9169cffacef397ccc8b69f875e6",
			"daeb12cc1e7c9b90a73fa7a8df464e01a792f10e",
			"3026d51c7e40133d250cc893db27ee0b526273b8",
			"f14933a502b924cb01646ffc7555f4815bf8e5a4",
			"c5bca655d5963c5d168e307fc7267cde0261e084",
			"dc2e00162fff46299564033b5b6d0fe803da5f5f",
			"aff6b494439184d041aa822134c2b69ecb4cbbb6",
			"b344392e132799fc2d0e66488baa51538aef9e39",
			"ba44650e70281acc2b22ab4318c6c06faa4503de",
			"2d2f25f30d952db325b6a012c8b9247f7ac312d8",
			"1e6f0f2cc38d88568cd2e8d57a992dc43e8516b6",
			"b8a4e6f492ab7ad7ca2d3041a196df00330a2db5",
			"2d1f9a7613bb4f16cd23a28933cfda9a51ca655e",
			"1a7817fafc116aae7088791d8fe306aea1c25bc6",
			"7bd6b912f7247d533d3472b8de0c7c2282089d5f",
			"78a9d5f99146214be8d8a2f3c39d2287b28f1536",
			"e26e085f372b37687f85328e9a8886ce0058ba35",
			"8b320982ddf84172e07f72c5ac1b2cdc567d82c8",
			"6975d8c48dc919d920071b8caa66c78d0a97fa82",
			"87b3f3475706072c58686ab439e1c9a7d94526da",
			"1acfa24f57f18cc14ec3451b05990a4de26012cd",
			"87651fe91d93e0a2d5beed1437702054e18daa93",
			"1408e85fae06834387b74794815139b6e0e97f08",
			"620180fdd71c10250f6112886708954fd79eb6d9",
			"f82df03fe384a60483e372609931ccea678e0cb9",
			"0b969ffb8cb4a9c01941d017ee341134e20a0426",
			"1c5062dbf61db6022f68bf102d1dcf5b976d14f4",
			"7a5590c7fcd0a69b7d4dc840ba323bd7b35a1fea",
			"2955d2ab1c867065daf0e219102bd89955e8bb31",
			"b02dd56b21f73c41a1c8a294b257796ecc08f0ab",
			"7778ab10ec71b8d8d78a34ebdcfe3c7414943995",
			"20daaf749560c15f80f0c01682f1060675cefc9f",
			"ac0beb1d97e399ec4164f8f857f9577a27cd40b7",
			"0262be742f21296bd61aa2dc5756ca06bddc89cf",
			"3b4c4d75f62f4727f5232db96aa9b136ec65be5b",
			"f801e56c7554a17ce890f6a01700ade6bb839016",
			"6a8bd9352b408b3a4cb7e52a4991cf51fe04c153",
			"6024594b15a296e61110fcff61d79f7476d3e73a",
			"377a7896ef7e63b026e3acb1e8cda725c901c02f",
			"fa493c235549e457bb07df08fbcd95024ec348c5",
			"c01a5afcf59df6c6fd06cb610ed61f974c68633c",
			"a9089b69e87a845ec9bcc6acc0293826a59a674e",
			"ccd8ef13c068f93c2f5fd820fe56257beb01bc01",
			"adaa896f50f9b72d0f7bbe698d8cf2bc2e3b275e",
			"6f3985767e7931c9f41805e3bed1c799610f64d9",
			"119faaac3210bc94b846a3650f872cad92cb984a",
			"5558e2f7d3b783b9529b12e7d90dedb5fe2400cd",
			"6395610d745999043656b52ae3879edb50d7e8b7",
			"942475e8a1a51cb71d358e8a85361184eb120883",
			"bfd3b1afcffe9de3d2eb5c3ceef1675e1ae965aa",
			"a0c6b7db6ef7b8a6add180e38f8e978baa99814b",
			"e097eb3e6054598bc3d849d6c3fd1d485bb6bf9f",
			"feee288747cafbc7d562ec658e2653e310e7a664",
			"b35addfc416191d487e4f35b534c03e111e73f2c",
			"91e3e9d785dc8cf2f83a7e32a01c63bbd4e0738b",
			"3bb36b8e3b4bc84e4cecc8e61c3f3cb753ef18b4",
			"828f63faaa98d637d6372b7e93a58ac049dd43f2",
			"80a1dc0003a82fc0763d96cdc07c3b6c4b5a0162",
			"a0a75c5997874f0c0e359acde205c69d51b3a2ba",
			"ffbfeb2900ca46bdab58c99c8db28b36722c09fc",
			"cc5c3a1c9b58e0ca0bcd634499f9758f4244ea22",
			"116c8d90a562ccd890c4de9484f913382e3fa9f7",
			"39686a7150e77dbb55a6885bfa4ef9f47f6b193e",
			"4e6b19b9bfb97b8c442914309b7358a00c24f1c5",
			"aa353aa80b0790380d22a4c7c9173c829a729a56",
			"fe3df2e40e2a8715e7a9e444b598422a27168656",
			"486137181f2cf59885b56ae829ab80028e352134",
			"fbf8c7b15793bde387cfc021344f113da8c53516",
			"49367f6242fd17f4867a1b15a2e58ac17056f655",
			"60a78daae01096a6a3316b23232c63117ce5d3cf",
			"2bbd4bd12c9b692d4c08a68550d409f930e8bce4",
			"985cf53e58b17cea85cfd3377abf2a673edcaf29",
			"ae805f2882ccd563647ffdd0a99cf488b8a8b6b9",
			"8daeffb9783a6ec93fba137d2b25812cd2fc69cc",
			"35fe9a766d35bf25e5e778756ef68ef4acd0a3e8",
			"e70a4b5779b0338a6696f7c2d736e491235d9ed8",
			"6c4c088dcfa4fefec9ad6d8d609dd1433114c2ae",
			"c5f52290291683be957a636d2f7b824c8e180119",
			"806ee6521a8a8ec6447d3ce5ea76a2161577d577",
			"fa44d16779511af37ad934af4d606da7ddba13b8",
			"461d1d1f53b4287a1aeea9376d90a81e2b3c4a3e",
			"181d6d12d13bfb4e08f13c534f9a12d9038d3aea",
			"fc1a961a6183fd02fee1aa6aeaa8f195b1fbe918",
			"12b0f06effc04cadfdbcc88e1e0303f92547b62f",
			"3985b5560bc53945c820d209add8b749b4ebdf4c",
			"891e0375d3e794631fd2d65ce813e203de5125d5",
			"1956187f9af53c9565635d4232eefb6ad31ca2ce",
			"212d4d8cb59c126e48e1b114d780548796b2c3f0",
			"8a75099d118a1eda2195679cef769f2e58bbaf8f",
			"c4e7a4943bb14fdc914cc3920b718fa0bed70f8c",
			"a3410ada6c81645a985da7f8ccd1b20fc5e280d8",
			"42ab8ee91e332619d21dba1660cbbd5335d0916d",
			"b1ae6e523acc55ad02f97ff1197cb86d0c6b11f4",
			"85be5ced5dcb6b3d706ce2c9c5d44dfbc6d0449d",
			"c834565ca6379ed4e5962e059b070bd9efae7d05",
			"402b2170a5efd1d7aa1e301bcbb454f739238da5",
			"96869108105cfd46a717dc5e7528f0ab6361da50",
			"51250a2c435152ec77343b77a8a57e27fea32483",
			"d87bdafa16d57ca4ec6a6992bc308523da7f28cc",
			"2366043da791f6d3c4bb3a21079d1f70e627d2a3",
			"475a4058a9d0a92b6c69c7677a65ea56132cfe32",
			"56524008cd367a2557f515f001ebf94c5c3c2592",
			"063099324bc01ac52ee9aba9e183507aa561a8c0",
			"0ccc9870809e6f5b37f977915b81275008f76693",
			"49eb7dc040d8fb166820643538f58d5e2d77d451",
			"41b77186f1e2a9c9110f7ede4b556b9141f87cbf",
			"95f5378de7513f796e1c298a9fae27810507ec40",
			"c4911cdbdafaa10b4d37e36eb068de41c3adc550",
			"0a6996591aac42e932f234e19fb6fe9bed2b5806",
			"4d5e2689829a8d41e96b24ddd479446dfc772c59",
			"a09204f1b69546616101692a2ddfaaa373eb22c5",
			"3490ebf569e01d213dac1350dd91ed7fb8ca39dc",
			"d8cacf5c4d6f0c3af4313a9355b983f5ecf44a76",
			"18e34d5db28d297d72f0cdb01b9e5ca1b45fbc71",
			"25f9097e52a415b39cc3405af0881d0f1935579f",
			"5551e679ed4a952245dc4bdae3a1b0d3b09ce85c",
			"92eefff3ba1c7b042a16eb5cff136ada90437b86",
			"b12218918c26becd3dafaec103631338838f5991",
			"aa12e8e8931ceb79f85e545897cc514b6cc45940",
			"92c125c66881cc2f856059f72059bf4c796b3478",
			"eea6c3f69b71726627667babdc2c8c5916c6d256",
			"992d078d1f08fcbece0236a4451eaf67444f61a0",
			"ac58dd328775aec36fff83c231d58f857ab6c4bd",
			"0013051b9854ec29697a79ab830f8a7288ffcd6d",
			"14b4def2e4c01039f10fbde2f761736d98278b7b",
			"fdb980d12760e5997c1d64923c26b5b8dbbd271c",
			"cf249076ab3a2e7b469cd8793ac530a2fe5aea95",
			"dc63e04ce10fa2936c78c9fee34674c134403683",
			"5618eebea33790ee8759f772e254cbce10587f1e",
			"3e1b5d3a48ea46f29145a2cc953a0a151f76b948",
			"9d36e58442cc428cfb3c489064b4edbe4fc2c938",
			"0a2be6f0554fdde33e282dc4969e1f3fd87783c2",
			"9e2cbceceef1deb71c472b9391b883aa77ce4a1e",
			"01b05b9a7c20f1c2ac50ffc6825d7a24bee063ce",
			"f19f128c8c621809534934dbdef522f18da06e9b",
			"f95e5fa188d6de0892b353c1f29f830d73685a6c",
			"1ddb78724837cb0fd36be184ffe4acda98ddcd8e",
			"5cadbf51a282e8543c4879c9897be735e44663d1",
			"690f82af05bbebaf4e09870078cc312b36191fcc",
			"c20c993c0de61fd3a026810edf420d144d8e79df",
			"104861daf2204c0002cdbabefa723b9dc1fba360",
			"0764af14d8a5373e345c84d16f441e0d7231a623",
			"7551b7c0b0fe203a6df3574376c8ab174e87b34a",
			"7d8b107a9fd62eb3094f9169c0eb2be6503d5827",
			"a9481b0ecfee51f39ba18256fb4b8f17a1404190",
			"41523ef87727aaba12e53500fdf6ad6848190179",
			"57fa200c97027f73d31cbfae926f8a186538e9ae",
			"e7264c8dbc93f97ddba92022d6fca25b4c5878fd",
			"4b2df4d12b4563cca4a31613c7055faab23eb88c",
			"c76a2a3b9c221c9fd094bce0c27631271c844a9a",
			"a55c2239e7a725e8d1a43349f1694fe5fbf058f4",
			"0f593dea7e74886d139e13751bfa4e3c36cba730",
			"9f4b1c2d9f54bca28152836fafa2513b25bcd284",
			"dddb28fd07316930f5b4d700ae645f2b286e592d",
			"9964b1e0ccbcb7c2273884f75cbc05e657701941",
			"2578fc6f686bd3ca8c557afc104437739212d6d7",
			"ba2b3d2a61dde193d4cc1964f829a61a50cc677c",
			"dc625366e701078627c387bb2ebbd48ddf393062",
			"a23d89764cc56dde1b34e033ef9d821d91d05746",
			"9463426156e27d2dc601b302caed24cee498cae7",
			"d632dc186421c96064835bbce26e5e118068173a",
			"a88496c4d5e1b2c09cc73da2d3a2ddf4269c9dc2",
			"0b862dbbc690a84e2c9a833078d1b63a885513bb",
			"6a586834006b35addf0e3eeff93d2d0251faef21",
			"b8f40ee832c2ea96aa2e26db7c9fd1f494676caa",
			"b1435f70242525c5883244f1c61a3bb51e00f91f",
			"c99a9eb25f4aa6b792db2a7a9b87d7974604c7e3",
			"a1a272182328aa224b5e228c215ad72a6bb2ee86",
			"1b58bbe7439559910d49b52c47dcf6b81f1b3c39",
			"36699fbd0083a83cec9490bc36a25ec7c0b031a2",
			"e57ffc488d649411434245fa41d87a978cabda2e",
			"e3c93e0867d61db4d8d5eafd754d8fbab54ba6b3",
			"b84cf16a39892be92e09a8329ce4fed5390816bd",
			"c6a02aaf5939d92d3086dd0dd116e44d47d78025",
			"3bcf14f664346d94536b31daa988a7b53b1d3acc",
			"d52dbfbabca2402ba79662fb0c3e476bddd22ea4",
			"e6fb2068213f525d14eac2dbe6268195a05a6422",
			"35fa266e2660b49f1a20f59286e0b7f1ffc85604",
			"392f74067d3c629305be0fa91126025360782a30",
			"326e90e63f75f401cd2662ce99883eddac286805",
			"0ee06406f0539f20cbc8ede327dbcf3aa26a1d1d",
			"24bf6e9ba2bfd3871ec069a8412b3310574fa3a6",
			"e1855c73bd3762b16db81432ce4400efbb47bc1e",
			"9908e690494fc6fc67b795b0e42d09648b034300",
			"b97a381712d3ac4e25431e428d24ac889f2592f7",
			"52d0e067b8b407587f42e0b485ad7c7f02056325",
			"03579a396d47801c808deb4136d0f9eb4aa0ac2c",
			"0cf05890ba223fa1f93258ede9e4a0b0177edb98",
			"e73e434db40bda83ccf1b2007fc83cb419389818",
			"c352963a123f82c02ecf239f25639e00e91a210f",
			"d7bdb28c8c50429e7c18e159aba11f21a9695d12",
			"2dcc59852f2a7ee157deafaf59aeda6334979a40",
			"6cf044aafa8e95293aa40d9257a7c5bf41b1e0c7",
			"c38fb1c1f60ded25cc15d1f377445adcdbd1cb5c",
			"503c1ddf8653aaa77963cadaf61bb265c5751ef0",
			"a6cd94c99b13d01464779a890698a0d3e452cab1",
			"2a30841cfc652be6ff5bac5984d53c27d6891f96",
			"721bfc9b71968bf0cfbae073982b758def99af9f",
			"277530707acc95b4c1eb5ba7e20837b4c5d28ba7",
			"7a506688c1385d8c5b97355f7ed8c5d63380a6ad",
			"a26f7709d4f19b05059e5998f5eae0cfcd75688a",
			"044ac8cd7995ac65987e59b120de464d1d6545e4",
			"0dd30d53876df59c7fa75b6dc75616ef3b6cddc8",
			"f9a77e239facca94ffbf5a1b1066f5655fbabd0c",
			"7318996525b7e0f45c37bb3e847631bda09cc793",
			"1d441e16de5165077a42ab4784cbe630a816a3d9",
			"589f05949c2c5a2331d9eb67224badf2efb6788b",
			"c22cfc9fd683fbdffbd1cdad5ec8c0f5a40e906f",
			"542c51093e2cc0a33d427bd41ee36395d683abdf"
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
