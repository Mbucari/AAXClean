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
		public override uint AverageBitrate => 128008;
		public override uint MaxBitrate => 128008;
		public override long RenderSize => 64416191;
		public override TimeSpan Duration => TimeSpan.FromTicks(3421559002267);
		public override string SingleM4bHash => "643282a9e62a1d19614f6d1983059ba6b0f0d19b";
		public override List<string> MultiM4bHashes => new()
		{
			"84b4b13be2d8e42f64283dab3717f066690dc946",
			"b5b6d1972bc26eeda37af9d9c75b432049d01b15",
			"6d845f6c0a7f1ee126cac5fc6063a4c8a7d63f2f",
			"8a92f09e8bd40b3658989482405cf5e13b1b0c52",
			"32ee5efb0770d1bb37b4bc1940284a39af2aa89e",
			"c2a1406bce0e43fce39f74971793fa54d0e0283b",
			"9cd57316be5d553ed19e2f4f84060f701d55c6bf",
			"e1c14de2738309bc2954420b6116b2d2e85c3311",
			"b3ac5d39659cedd54877bfab4c1ac31f54ebe330",
			"cf67d9a25eca4770e59d166f1a6bdbc3747d3ed6",
			"6e64653bc370fcf4ce1246713702bcb49e23f619",
			"5b2f381a7c548701e66e6ee50a0502555351284e",
			"90206b6773c6489bbef028d36db1c233677bcf7e",
			"3e81801f2c5f2317083df1cdc5af50333eee5bd5",
			"d0d7195b82b11d81dc275673de20164a9eb8e8ae",
			"e4ef37c6f0738e03cd0f65fe561b20bde3d8f9f6",
			"c03867ac2f5e19143f69f448bfe38dfb66372ecc",
			"14fd4e70cdac0ff597c4e6e02117e164a184e0cf",
			"4741373814d08afe07cd4d71a15ec956fb24c1f2",
			"2afb363338a650eec842e1a71b53103adb1ae3ee",
			"7a024726db4fc31fae42918903e85e68bcf89a7a",
			"0612f2cf36fbde61b619090055bbe53623cded65",
			"15ae13df7cc8ae3954907818650371969a77be49",
			"a668083bda740175151e8411cad83f6ab1753b83",
			"ceae5110f1804091125c50e0329d9ecb47edae3b",
			"b574a84c0c8fd458abfabbb0be2d395627c70d06",
			"6691175bdb963a63d46e4175c147b2b8dacb0260",
			"3cf3235311d27914a8572a17c7dc99faaee0edef",
			"ee5beaac9fdb175f75364992d5d2fbe2d5ce4a46",
			"09453f02b7fa5c9cb8170fc86c0005a2f429b5e5",
			"251b7932e70fd9208fd695f1c36d0dc91e8d7dcd",
			"c541735b4fb0ab5da9e5fe9d99c19a2cce59f4d4",
			"57dfc40b306cd4b7be382933f19f5a586eaab5f2",
			"af706b80c1b5087d84678bc2edf72d93d5339dd8",
			"c7b9ae0291a2fd1fb9ca86e2f9d14944db7eb7be",
			"84612aa872377a51a3e542484e81b929f42452e4",
			"5688d27ba3a51f68c778a1cc135dfd858acbe69d",
			"d8f3dac9f640db88e12982fedd7749d016545c3b",
			"8e32b55cd1c99a5edf07ab0e28581893d343fcf3",
			"df80f52fe2c4fffbb5f03d36b6dbbd9b833df71c",
			"dea83942ad774aff07949b0388c5e6044c18f850",
			"f720cf826ca207424e248e99eb434744efb8ba08",
			"fd2c98cd4e05f55b986ae2d99127781d4694d98b",
			"1c9893d972ee92a0dc131363a0db75dee56ce67a",
			"5c47d50a2ac032d1766b0a1595fd6a4482e2d9f2",
			"0ba9fae10c5134119576ece7db535bafbc751d90",
			"9c15f6ae8988e79def39ad7e4dbf11d078c5900f",
			"8f3934ac6d3844efa7c2148bca1e97ecfda17b9f",
			"4d8c063cc0832888651278e4de98625acb23e111",
			"702a1fe99b9803b4ca67e39c93bf67720411c23d",
			"34a0a7d2ddf121e83079efc24ad03f28d2e919c9",
			"6b5e8b6f5f7e86b1e8f04179ef5d6583edd17167",
			"143ded943e360bfc881eb5be9b4742723850f898",
			"753dd7d36c2f1adc700f88f2325631dd92499c1f",
			"a580311df051c4c2cac474a65db5ee14f09ab2c7",
			"93838db7f0c784fc39f874d3ee11a5d41a66af1f",
			"a4dd07e62763ded3e000fc963a365b38b6b1d7c8",
			"c70ee08b4b26fed4932ac56a050a4ce962fb2e1c",
			"3965069661c5ddcfec69a9a94ae9c7b9f5a135d3",
			"3dd841fe55c4228cd8f13f1b9c6f65808529e264",
			"1b0ef943a37346352afa316525e5d20985553485",
			"ca212dc1b42f36f5d7b4f106e26c6b88d5c773b1",
			"c6775940a54da4b16c2856546b049d4991b4e4ab",
			"633c0ae18326dc2c4056a58b863074c339b1eda0",
			"793039c4fde7e4c45a0913d9744a9a64c3eaea9d",
			"16b9aa44ec0c50c3b409500d48627d7e5868330b",
			"14935578fa6b82b4099e16d22b1f8add23db469e",
			"77abfc0aca4383242d797b59f1b071dd64d5791e",
			"efe48df71fdac2f2bd9bb0acd36f6859f16560ca",
			"624da4e51154957ed565ce453fb8d2cf9b4544a2",
			"054e632bc884b79955eb5fdaf7796eb8e456188b",
			"0651ba8c3283fbf125407329bfb2b5fc208c82fa",
			"d3d80a7331e507130d8b55bfcc84b9941b567e9e",
			"ff0f8304afb719b5f80eb2ad05b8b150ec1a5a9e",
			"284057bbca860de6db0a4011c003d234515f729d",
			"d4f74cde13d7b163c97f3b155f7fe789b217e1a2",
			"eecd5353d54cb4467919d949e52871450ed51374",
			"79e6c7bd28a4a312431354d9f888b5202b42273f",
			"823e27e7f71812a5d52cde2daa92ff6eef526e58",
			"9537434cf10d0a3d54081fbfa9fb134d68c1a424",
			"0ecb1ec23366885a82ddff100ceeec6baefa5d82",
			"fd332ff33e269b682a8ae2f23d6dc69deb6dc712",
			"bd17c218f6e6a1fd590afb8eb3780db179e6a985",
			"15e3264a788c62c76cc2cdcba2351315e3d92354",
			"4292318286cc80843e7fcd5f61fed7e230f135ef",
			"b54715248e87fdd0dfdc7579c37d17a7d10fd55c",
			"0a33a477a2ab25092ec420dc3a64214c078c1928",
			"52b2b9f962d75b3e6cb05d769980333ebcf11da7",
			"65c34ac8cba9dd0ee8758f25765438bd1e46a32d",
			"8f9eaf9951ddb6b3113fa66937547a31959362e2",
			"fb11a66de20fce1a93854f216634c6f345363cd4",
			"8413b6bbe37f3b9fa423ce0ae60825a06f650dee",
			"e7b44947dc2165b783061366d01b9a97f6b861e8",
			"a65719cb7416e8e3d750fa64491b0d714c8c2ee2",
			"900e6d7256c48c65525646ef5692d72d67161cc1",
			"155984f2f318321a04fdd3be8d1d7e7fea8ed1c3",
			"cd4325eec0b15270b02f44177948745b96857866",
			"3a3fdc402f10c667269af49e8397eda8b9ab0ac5",
			"a8f4d49b14746ae4e8facaefc0e2e45d4c9ff3aa",
			"b1bc6f437668570338640bd4302cc66ace453d9e",
			"4c99facf2b09df5bdbe6c4ae4c04c95737a2067f",
			"977b78d1edba2e55addd8cdb5fe8d34f8fdccae8",
			"5a164a56a72db1db8d01f7a41c53a9411a676965",
			"c9427e12bcd5b0e05ba60d0d9c0e7bb817700dfe",
			"22e93d6d75e0235ab1e9e1240ecf46f4fecff57d",
			"3bf02a37f9f3cabef9a2f4864f5e553f7fdc4996",
			"dcd1703aaf6c8a828f90e38fdb2feddcbb14f8d8",
			"6f8c104ada565196ae152aeeebdda18cf4879c8c",
			"42a7f47f3e82f044017d60600cbf8933357ede96",
			"03c89b7739727f443862c4482f2c68032b499d0e",
			"e7275d776a9e4501640f6f9d7e586fa00189a655",
			"514ecfb8d0d81a911bce86a33148b5ec0baeec7c",
			"c09be2d4b053e3b860932a9c661edb26743857db",
			"65ce9c04d657f32648e1015824181ef71965506f",
			"483efb2cad93119b19df248c6f9772673e221616",
			"34bf65fc8aa5bb36dd4b08ad860309738ef55673",
			"738348bd8381c4acf3feadefd280f963b657ee2a",
			"f9a82e70cc9cc2715737338c60d8c2b1c05bef3d",
			"bb15cf2adcab074c0e72d1a2c40f442a5f66c3bc",
			"8ce8018b6688ba1ad69bf015c7302dd98c346968",
			"a39b7529a4224a64d395935d796142a59c8ec98f",
			"008c643f274d8c44294bba390aeccc1c7fe121ad",
			"7f2b507523eaf7e96f509fa3afa8bd0532a1e6fb",
			"0d30cac3207b37e19cd5f769d2b58247c1d4e9e3",
			"ed6f8777a43d3dc59118f4e0cdeeeb5ff03100ad",
			"c71ebe66b746bb55f7975894270a3ad742fb6240",
			"536a2cff10716113b22c7e7439a77eb0341cf339",
			"4dead128fcd9328550a1567817200e0e3c7d8468",
			"431cb4eab605572b2350b33c24fea1349487e43c",
			"5dd9792cd1134a13e929ba05a5037bca364c0157",
			"7911195fec8efe9b92f1ad7a15bf5c2c1c98745a",
			"96af24c9a273e50f40023dde3211d547223d0813",
			"30abaf20850c0a05890911555d18fcbed474d593",
			"730d62b669d687affee489cc07108e28e434a91c",
			"a92fc310632f4544036c55fc44484730b23e4365",
			"9ee1b4a7aa93c650b00393e431e49a962724d697",
			"9c82a248fc6d61d322c22d90329036ca981cac18",
			"69c2b49e28c1b4b4da1d3a94b829783c42b52b21",
			"1cf79700a7b2b6fa83deea6b2c8088a19f25dc80",
			"a197e3a750194ecc4b906b5f912bff28194a475d",
			"484fcd2f980ce37aac389636db4dc04b3d122d8d",
			"616b7cb3f1171a067b690d75e9c233387c7147e7",
			"5fa9d50cd87cd725d53b12bb429877b0888dd5ff",
			"4289399006af854fbbc884e307e6bf7815b1d691",
			"533d5bf459fbef6eb7a13ab69fc68d0ff4c28bb4",
			"ed319abde36808f2677c43300f33845465458b39",
			"ac1c2be26768c57a73a2bdd867aa1ed559f31e23",
			"de5418633f867ebe0e31ce6924b55d13f884529e",
			"ac706fea6195627a5ab3b4b97417b52112002098",
			"10e53c204212b536b3421ecdb0b2d4753185ca34",
			"9bc7e5de76dbb705ae6240170c4fdb541df4f68b",
			"f0f105748bae1169bdffd1f65c722a8dd23db275",
			"ef250f2c641300bfd83c11ee0f6e399c27178ae3",
			"a73b08f817e432c3dbba05bb64ad33c684e543fd",
			"7f5b9be693a1cd82f9cde855ecf310dd6bbf3221",
			"a109ea15542bf14ba0c9e3a77db96274556c2beb",
			"5814fbfcbc72c78ba57b9fe120b232740e1d9c87",
			"e5a71cca12ff74945cda34c76e90974026b569d8",
			"004d8fcb1b7fe4aba709ba29e10f205e30528cd8",
			"424f527e21ee73e0a9319c7afd4ec351562e2a83",
			"2bf2bd20680db0e99e7e86332fd948efc36fa210",
			"3e92b2f5b84107f155539eb3a5c13aec38bf1534",
			"d2bfea46172583aa4511666a705bd8b59b439357",
			"914bd6c845a9f90ec2c805d5e037e844fd5d3399",
			"3ecc46504a6c12bf6fa836943091e3a259d815ef",
			"d24fd5de4f70a1e244fb6d59e1fd98391bc1629b",
			"38db708ac8ae4bd0c5aded31a04794c716e38cdc",
			"f603eb5ee33e29a2666ef598e99703da3fb9044e",
			"7c4bf99ac72ab7451f6a0d24eb0213164eeed002",
			"730c57117b0cb82fff66638f1d674142759b9a5c",
			"5c66ba0532b67bae1e0d1d4c586a819dffda2178",
			"7ffa5001872913311e2b4d1e9ad57149c5733d65",
			"2931e3517f7e98643d33b85e1695618a33779ee8",
			"407f2bf24190e19658fb63d2e1b6aa047376693e",
			"4e692d24a404e1e2fa1c698c15ed8f0fd66ebd36",
			"1097abac8d992be32e8a90eeb9d0fa2f8cade986",
			"2154c1aa0ff6e3e1ce7f82632cbd6bed7d515168",
			"31cfa7a7ad8e43f7c0cd3fac1156442a028cdaa3",
			"e267ec1d61bc3a067c19865d6542cd58081a3e2e",
			"80da0df0418974cc92099c36e5927269b10b876d",
			"1b80c8f362b68b6a6ee46c85e7e7ec7b96f4611a",
			"9f168d36eee1002ce36dd1c603a4c56bf77dd456",
			"e1da14f43d4bee50b8655a695aa852f7ab9f2753",
			"9c711183c40b7f47161411bd36ddf3e3eaa48fb8",
			"a15d118403650589686a81199f812eb1bd8f4b67",
			"0d329ec1f98c9f69e013569869cc989a37942eeb",
			"acbfa23ecee3306ce589cb2fc5996a9ebf5679b8",
			"f4360e3aee7338a3965b0423bbebc09cbc8c8a8b",
			"85df80dedd5dd86faa363ddd5be6f37f848ac463",
			"3eb850ef062f091c9d0b8a01ca81b7f4c15644a5",
			"947685aa6981e0914da42faaa2b7e69e095ed1c8",
			"f45ea5f2c3c4b595e66fedc414b0f648a22ccfc2",
			"5f345cd7d39c336c3e52e86db17cf3f9f77a3921",
			"425a3788487cecbadb591e7d35124ab58dcda63e",
			"274c62f61ec462326e049629872a92c87b1caf00",
			"a337bc64134f90692a6974202f510b82c63d644b",
			"449ac8e4e0cf908325fe7ad2b9f8454b78d18d36",
			"7e3fc1daa387a8e04c4a1779a81def7e8e927236",
			"144dd125537b3bb42c5fc8325fec1ac9b2c5758f",
			"6effe471dd20af34ced06787e62a634237a1f9e1",
			"d2b902b12bd863fd1897b7cd0f8ce4f327acdd62",
			"a8c1a5d92357c8adf7f842f96d2e6cc2e4fc1e45",
			"75c0919ed997a944b8b9c5b12390b1688e3d4554",
			"75f611d15e4704f7b0c881d5fe3d8856777fba2b",
			"84d23d9c0516ac33998cdf0d787c171ceaa1b5a5",
			"f538033d48e67566a33c59431ebec927a60f5db0",
			"3d06429f5bcc206ed6e0e724a40ba403a68485c4",
			"7f870b737c957c7550a9048a1d6b18d65585fbfd",
			"81b657d54276d3d15da6eb73e3164f030a08f1da",
			"3e7c4166130f33c554c37d92bad4cd417ba1cf87",
			"46d961cd849787c7fd5cadd4e43cfb3886d15b25",
			"a5446045093485f22811efe9d03d393f75b45f51",
			"562be7040e3d47198a0d41ffaf0708b5fcdf0c5d",
			"524013156ea41f0bd9a98916a3fc518f45b739b2",
			"9487af4288d1b82afcf77068ceaec40b67de7ca1",
			"b312b44df16884832eeca92a1c65c963d66c7879",
			"f2e13e38ad1639138454197954c9b7d08bb37095",
			"982455fc8c859f6797339e1fbeb54191662dfa92",
			"f7f9fce1597aa510cec48df4685cac6f3e6cd7a2",
			"9663e52822ff043a5cb2fa496aa3dfba1c911591",
			"f4b00a013d3f0196aefe8569b0f1ce45b67d1198",
			"3d02b01778c4c5b350ca0c96cc8e7d6b4fded71d",
			"42c37a2b314f336ef471df485f81209410d5c51d",
			"2fa4c29bd2b9cc1c0c652a269a9f200f59cc64b5",
			"617cbb8134f51de2f4a2db665ef0d8ccaa83a028",
			"ac5f000055b9f96539b581945a59ed8b570e2da3",
			"c478e7da2c429745b04696a64d1b3983b1e4a1e0",
			"2689bd3a10d9bb2872c2c0f3fa758f93fc2592f9",
			"f7c912ed454daf4ba69546f0f2fceae3bbf77d00",
			"3210fb99046e7780da289d7028a7d7cf4e0a2449",
			"cfe657c43478fda2cc4fa1a6b5720ae0c860171c",
			"7fb7b1de2dca83d817ffce548ac2cae651e83d73",
			"9db047ac54270fa003d972c9ae23d6a6ca28c7dd",
			"8e575510869e4b9e66a623b8bde40f6296a8fc6c",
			"0669759b4106029101e6c465cd5b167ae8f2a097",
			"6c4c035b34f27bb41c8c5fc664b5864874c98a13",
			"84537e0539abb3e558e8aafe1adf5c45cc22e940",
			"379eacd90ce1b0c7066b34b7d2053a36f2076f74",
			"95fef3e6e8b9834b425f63b36db7db4317003e15",
			"bd201bb34441eb0bba7567b67993ef012a694d69",
			"dd4ff269bd3e425a15e2eddbea40cd1e97255131",
			"4f9d82d3fa4e2c3ddce7282ef823628e9e080401",
			"2572a619ad5f8e3339e654516533c0f02e418504",
			"173a78cf38cce874c3869dc5e60bcb147e4f1c10",
			"49ee997638c3db84dbc471224a033af896a0f7fb",
			"a4ef453c28e7c2b3187edab224c51bdf1ef6602f",
			"e314e6b8a647b010939ce7aeeeecd3afa7bb1118",
			"733c5dbd8bd53cdbedc7e156ee646c74958e56fc",
			"34874c1f7699309e5ed931f807f1d83fe700f604",
			"68644f090a24d74134e61b7ee2ba0f515a475c49",
			"631b5cea6a6152c0c663bc3fcda05e7d9be53afc",
			"2f8a6cea10e0d84e233b1426017bb17b01dde880",
			"366dec8b619626966d2788102c7ebd72d3889cd2",
			"a18bc8146b6330b4e01f57723974699293b8ac7d",
			"28b05128c755bf0645b6bf0a4b617c5922e1edb7",
			"08016c2fd831600631517ffc125bf69283c74aad",
			"6e87ea5f5d215d4a91437c91cc04de49c5531de1",
			"7fb613c28139a4244c4daaa0fbf359a4641c080e",
			"32d83f2c7109d30884e68e0b663fb8de2cfe4b19",
			"d961367654d80397624fb7a92cfe20a35c577103",
			"9831b3235b8f032a289d8cf8101e27528af6c71e",
			"c026bf5f20103891c3a42e12e944f9b79fb8de20",
			"dabd64a958719f71878310f581ca5dfffceb0750",
			"dac20c0f5b35b728f6b4318b71db3a95ae62078c",
			"089ce78638435340f82cd7eeac1b785b3060fbc1",
			"705945c7ddae15a5723cde0999926043849ace10",
			"215748d00183cee875dbbf7e238fd187f6713468",
			"6e91ec0b77a79bd1a62d568a9e38320a70416d28",
			"5ff9f48efda9c10e8e2a003e202610f233b732ea",
			"0a6f0d3710c17364a7f8637f61ad53115449622c",
			"1453e7ff691b750c10040fe2be7b7fa48f71bd60",
			"cee391c04def4a63068ed25e1d69643e95d86e80",
			"d6df374bdc9591d2f28bbe9b062f72b63bd8cac7",
			"dd2682e22eeedfdcfbea52c4955e912f358695fa",
			"bb825c7d6242082aa96d231a0c9f1f0b5a1e6f88",
			"1e1c4ae6ab72db6ee20569e7a6cd9590e80d7b85",
			"c29c51593340650143cc1efb0f182869bfd09373",
			"dc9908175a646a6a962f16756ac0443dff2363c6",
			"a0b61ec93eb317cf3538d08192ae92f246094719",
			"6c0fe0085024d2e1170fffc3ffc5e85cda8b2836",
			"aaf4ea83a8149792fe69844c07c3f89a79d6311f",
			"f5efc7536ba8a2a20783bdc67ede36b2d51af4d3",
			"81ead31a16320dfd5d02cae3f627fe9f8354fca3",
			"9175b6d0ed021422842c8617524187f45326d9dc",
			"5ad6f0062ee6253ac5070ecd7be30827af87ce9e",
			"d88a4a9ad6b5d03a1b4a4fadff2b90b30e56d775",
			"5ad6f0062ee6253ac5070ecd7be30827af87ce9e",
			"d88a4a9ad6b5d03a1b4a4fadff2b90b30e56d775",
			"5ad6f0062ee6253ac5070ecd7be30827af87ce9e",
			"d88a4a9ad6b5d03a1b4a4fadff2b90b30e56d775",
			"5ad6f0062ee6253ac5070ecd7be30827af87ce9e",
			"d88a4a9ad6b5d03a1b4a4fadff2b90b30e56d775",
			"5ad6f0062ee6253ac5070ecd7be30827af87ce9e",
			"9dcccf213d84a453382f0bf436293ab230df2d8c",
			"356a73ec2bd1acb1a74ab2a376ae61f0d00c5629",
			"a025b0f3a0f72752cb1dd8cdc08e68a97248df08",
			"da8ee40d9c2e5f255c133bf749210caa1c304151",
			"428296f734740471c5fd9866cbab2b6c4c1cc7f7",
			"9998b2a609cf08d00f48f912de5d151f539499f2",
			"65524449a8d143e3744b40a465083ca1b7b5e290",
			"0232f91af6ce47206f455c04da2425c12ad2ae8d",
			"516620aeb8695490415af908d4f43e9eebdd3da2",
			"6ae0997b0f52e40e5845b7b8eeff2894c2f3f1ba",
			"938efc71cfdb22cfd87f706f658f04eb611d6a4d",
			"2cc58fb65222a53a5c60dca4b99c2b3f100edd24",
			"ef85c89043d71767b58d8454533b7e2263e57fc1",
			"8eef2be3d2546eb125b6e062b06ca430ecb78327",
			"55ac037cdc6a2364da8e7a1669e76873b873d43e",
			"c9393ba6cdab408d28f77e3709d8ae52cc3c8d89",
			"3c3269ecd873318c9f7a5ac078fe754b693042f9",
			"ac98a03cd631647daa0cb76be77d7aceef8de8f5",
			"5b51c74de7b13376b86f22fa403b14036241c368",
			"f8d5a09b479f56777e819a3055a28da9d405039d",
			"ce43a097ea65e9db6428c4daee8cfa774a62de2e",
			"bd17c8e279adc7978db0ccd15c23491b96bd3658",
			"6215296d1be0146be4f1bfcf611e3be8736472d6",
			"1f7d796e0b82dee74e99d93998ae4e3ff407a3ae",
			"f8931143330fb2ffb02602cf780e48d8db0296ab",
			"3f35c950dba54dc651f1daffa1dfa5043d22fcb1",
			"a88b93d1ffd6d95ea2cd743dd5a950ddded3463a",
			"3f6fe52b4a7ad458cd89630c774a06c1dc16d7f4",
			"412ab0679d5d901f69d25ed316a683222174b2cd",
			"f36ec787f5a9b7f93bc3844e75cb20b6da54c8d2",
			"ec24f75bd6c122239ab42038ff5ec327d80401da",
			"730691b1982e177a880f964fa4e8e992a87b1421",
			"d202051c1f15acb1bbf6b92fd9aa3077b9b3c850",
			"39b3f417134b5cda0be9b7382964db65667ae809",
			"fc79fb295af379f10361b0d555f1633fc1e5ec8c",
			"8630f6249a1b838ac7b409452f5122f4b3f36a6a",
			"cc2948b14bb544f16d8178c4391cbfb0bbbbd2e9",
			"f3f5cc02dbba0cc8fdaf2997c84fe2a6918b10cb",
			"34ba15f5283a11d8d940abddb1e156d68efebe78",
			"d965aacbc4bb774152f42ecd9a2aadf722c0a138",
			"51f804589d54662bcd2cf5ce095adf7017f782ca",
			"15c35c1cb609692cf5d0762dd4aa14eb52c58315",
			"edd553aa1fb3f2d3c36ddbaccf52e25ae80b6e16",
			"7426624442cb4777e8a5c176cfc426120fdf8c4e",
			"e09daf9a8e8a4f9db4c01f329f05751a258f2670",
			"c56104bfd19a4d97a429b4a62a594d4b330ca3e3",
			"aef685e124db170cb1ec15c893138995e4b34d9c",
			"4a11cad16c5f4773c11bd8525cbecf4e86d59062",
			"0b03ed8da19206e491cfdd231d0d77b695abe745",
			"ebaf7cb0dbe942a00c8e2bc0e1b2635f289f555b",
			"73646aa834b5d332d03e1cd77e5d515f0cedeaa6",
			"ea0ae1ddddd62cfa15b56d5dbaee107fdd1beb06",
			"536456eb62700f5a604dcb17c3deff2d7a38a482",
			"c60fef19fb06def0f4825c80ad321997cd6f0fd3",
			"adadc42cb8048c6dcc2816d2b9a83ea0edc09a9e",
			"b15bfe41564aa123bee3fb5e36eb5197ee097a81",
			"4bd5c6aeb7143184e2891cf28d0a4b3c5dabed0d",
			"82268210fd6b0da1ad81614110460d8c40c9363d",
			"dcf5b319dd6e2894d0f3499cc2e4abb7710c665e",
			"69e03ab2fe16fa7bcba4d13a45fbdcaf129e0566",
			"b0e8811120c39f080574c6f597d0662ba536348b",
			"7915935e3168748a99c0a8c4c8b884cd2373a4a1",
			"64c6a01dcb7239f64baf3fdc3b89ae6b295b0919",
			"6a4ce88964590169504778f7b82c835251f85ab1",
			"431d3ebd0732e4a3e8e5d7b218018485f686e5dc",
			"f2f24f89b284eb9a5bcf10dbb1d0985f9b26d235",
			"c9191482538e47eceed46e52c72728dbd7e38608",
			"38f589c1c8f610fb714a6366ad3507a941b118c0",
			"747e4ddc00590f96faec10252a579db467f58f5e",
			"6f079df5352a896eec9a39d17a544e71ca7ade13",
			"733ccc00b32e9ec89040aaddf269261b8e8f6eea",
			"3ed3be0fd81f4c21bcf7470955d7b34085d2c7c4",
			"04a793fc22c0275f5ae3bb1659880507700224f0",
			"d6fed9c01867806742749139abcc89cb140abfd7",
			"c397fe870f201df9305f879a53bbd565236b3498",
			"ef2c059a4b03ed99e520e7483f6bcbe5f8e8a93c",
			"6593969a47d28b4088904d53bddfa88df66b6548",
			"de5a6a37d8912c414ed6394c737b9667c1763061",
			"fd34340131abf85310c265fbbb8c643c4553ca83",
			"2d23c70d574bbaa3f56705dea1a7343d03d907ce",
			"5e5bf2dcab2e6c496c7e775f72c2b212c9439981",
			"c0c2150245aa3263bb786ee423d4f82a03f86035",
			"711f20f52a549534802cf462a2c99d7364bdbaab",
			"59391a27c4cd4d7e287da595f30ef23c18c91055",
			"d11b8b380d2f39154bd76cca3273841ca8bd5f33",
			"b3c7998398109b527a1843abc4a29a344ad9e32f",
			"19d0fb904d7ad9a8b5ac295363aa3a3b849fab8c",
			"c44f90415a43694ed9ad34c6d75e4fe9c4ce119d",
			"ee7d0b071d56e3060e32621fc544d7d0dfa4ace1",
			"4d7e3a81ac824ec2f6fb19b1f51ba43f447dc3e2",
			"b016577b2a47be0c5a9663b60c0465cf81d2f604",
			"6d01444f8c957323b6f304b0ad6272f914d1451c",
			"c52e2b2e396dbbf32d7b47468069e36b403e85cc",
			"83e60f12a8268a821bc80712fe5e06c40741cfe1",
			"060f15aff05ca2ec2b8bade5db03efe9f4071062",
			"d97a4ea552bf4aec57e29a022597d18bc9a260aa",
			"c26cdbe82e7bde82850c0d74055f954a35d757d1",
			"ae06e6519fef483d680b792160d26bd5c1de4ad6",
			"4be56b60ea371769a72a04c6baa1180af9fde390",
			"532e98b2f424df9fb6381121f95ba3b4eec9a0a3",
			"33cdac4fc22d12d845a8823aba2887b134de71bc",
			"30e8deca712b60b1950e75e7e8e9d4543c154c48",
			"0da5ec87ddb61ccee2ace4878d7f2644a0a93e1c",
			"fe419cb4687516a171edb3ae9e30ad4334e9d8b5",
			"123bf7c6fff41570f8136e11c062194649c95cfb",
			"6b9edd8e73c4a5a60a46150f6073fa0e6f6cbcf2",
			"8a7b37d09059b48bfe9b5b094223ec4a63a39f77",
			"36def09b0d326a9eef7870ce52ff926364d532d9",
			"328938c833a928501b2946cb6a0bd1a56d6816ae",
			"3429d41202253464c860a709cd3ff95d44ff5131",
			"185bdb8ba2445cc6fab13ade14086e4c8b888a8e",
			"abb6062be45cb525b61da257de6619f0f6491ec9",
			"95d2620081399ec72e049da2a14cdf44032cad0d",
			"e4e5db9e5697c1bc66d7c19f5772cd89b894d19d",
			"c163530e94c3c416bdfa044033351ea87c746004",
			"7ddc9df686bac066b250037f02198c94899e65e4",
			"5bd34b28bf5dc46d543038d0ab77dcf008f7b92b",
			"c9e11d0763baa06784e822beb3220970eddaa508",
			"816d5fb44cd1bd050a8fce2480d2c679955bb122",
			"fb60647505cf29521943ae15d33e34ba94ffd681",
			"8c475b337f213e9a4fd8730233291fbc0dbecfe7",
			"26c659b8df8d42cc6792c935fee74989c9d84fd6",
			"0c4d97044e179de161f3bd9a3a752d08416b209b",
			"719a5e5aaf0440916ba1815ad0c25563602d6694",
			"1765f99c927a9209c1cc6a8bac40386dc10db4db",
			"dd5a52c01d87ef0fb4d7d83c5d9048b63d00dcc2",
			"05ab9bfe413d0a609a1c86fcfaebced7f792ee9e",
			"dd670455d55bb00634f13b2b21031b058cc2c4d1",
			"03648336a101d9eae67d6f59b6bfd91531db3257",
			"d3b95f1cfd612dd577cb8891825715ba22023226",
			"22825d97f97141e62260e3d39518d565617ca39c",
			"cd3b8303ad720b503762ef5f79027b2a84fd8e58",
			"307a12ac2c5eddbbbd46c17a437ef17b025b226b",
			"de08d002b88910cf762b8bd0d04c7c3097ae3567",
			"0a80dfe709e7eaf9e16fa57c5621e57f4d4914d1",
			"8124bc345c0fb38bfb79b3f146c40f89cd0490f5",
			"2f2e14db5b307d52e9e9d347b544939a7caea2d0",
			"dff24895c69e8d0f7218331fddfa1576e565b184",
			"2695d2a4a86463cbf86df2fc946fa87cd8938720",
			"ddb0ba57328dab1e6bcebd3aff86a07f636e69bd",
			"04f9fd6422f101d334cd5bf406a1ac4cfee1e97a",
			"c1586aa3a1241ae5614d43b8944fa797dff46454",
			"6cffa55f1f52c213b27461062bfa3bb659306485",
			"722dc32a482d1d15a0ba629a86ce5fc0555af9c8",
			"cbb496242a3630306e5fea9f75360af8c190761f",
			"478f021dfac2b69c29cfa018f21d904fea186a20",
			"ccb36a30a3e3b3a8994585f214bdd452fce5066d",
			"003d496ed7028bd8e14f6534a9d3fc9e6f5b3ac9",
			"6e8ea8af463ae63aeda123d2bfab719e1375d0bc",
			"f3e2a0b11fd1854e7bed8ee73d01cca1255ea539",
			"9ddb6c42bf177b5ebffe30377316ec1198821680",
			"696187c0079e3618e068770375a8cf5af770564c",
			"6ac6270a12060ddc2f5e712a8417a6915f640dc0",
			"efa205dc8bcaf1648faf5b8666842661c8668d95",
			"2e45312e6557a77e748dba193e1455aebb532ca6",
			"015d1c0fa9dd3f9445abc875b9493565b559fa0f",
			"6a1ee1df64ca63ebf9d410660768b12d2d591bf7",
			"4d7f84e5f0349e23b13020388ea207f7bd819a79",
			"06a639d9ba213274aa8b461becc8aa336cc1167d",
			"b76c492318906f553358ce1c99c46ae8b59c0696",
			"be0058451c1db6086ebfb6939dd5803aedde0c64",
			"5abd483aa8e2b1df21a4d2210675106d1bd2406c",
			"1586199d38f1f5151c874a45d0479e1c294d5a36",
			"003ca0b73ddae083338128b69f577d3951490642",
			"c9e4bfe9c2f25d8a7362a24afaff86827a2267b6",
			"0b29bf3927b0d60c168457fab790a29f72ad3719",
			"dae1550f3b940dcbd4f0674bd02624a4d2a838e6",
			"f5817dce677d179ad257d1f005e68778b0ef29f1",
			"40e93708d269beffa58b8ddb95b612cc8453b77b",
			"735393625f6c0ce30346727ec1f945025107af62",
			"57fbf51518ae1b7c026cb5605c4b20eda4c0205b",
			"d51b9f450b966f23767a1706bca749a08a01f8a2",
			"2e14b23652d51a2d2c286620424b24ee8a354d4f",
			"516dc19ad2038d353c088a6c21286528bb7ca11e",
			"2688556e0234fbb9c597c607c4f3860ef42a7c8b",
			"b35f94c408eb5b3c27f7319cb1796c033241eeeb",
			"e1508b9d8aad278222cfc61ad3ebfa3783764b61",
			"8dd016b6550f8754349b6ecdb24c07f5c8ef49e7",
			"9ce73aa4c5e8c1129b8460cebe2321e9501005d4",
			"db3966504a09e49407e52a6e980ac7ebc8459a06",
			"c698eeecb48261095a48a6fbb7e1be7c67a6b665",
			"0866d0ca7b843ec62a2a195d233f5131af62f7ec",
			"2c9b3a0835d80bf12a63052d704ced457c072485",
			"a6d70d8fd15ba9b0b00d8202054ce60109bb9fc8",
			"ba02d426b770099ecb76f16936a6d7442d83a4f7",
			"9dd2f04a51db7f82e00e0e379589080d85534b85",
			"7dc8e241554d1be7dde1a05b51e58ffe0377b018",
			"6357123d85489155768ab46e95a4b98c3d1fc524",
			"1a028cde0b28e20743c0c021553357329c354406",
			"b2d7baef2cc783f7797a819457cbe822757efd1a",
			"5363b1d08c1141a1a59d67cd55d5021358f085fb",
			"e3a6aa47f03ce3f9fa0ad2641fb66f3cd3a83618",
			"b223d825e54314d38f754823a82829079214e9d6",
			"f522e706d8060b9be0bffdeceb6614ebb45c6094",
			"3ce154bfd34aaf804c15c1c8bb75bb241111a1ca",
			"af490c69e368bc288984ec8e58045bea68e8d260",
			"38f84ae9c00724a0f85841206c4aea8204ffa750",
			"047fe6f7d55d65a27d7b256ed264ac601012279f",
			"96f3908c49b82bc91aa61a266840ded25e9b106c",
			"2162fc8a8733f3de5f1c7b99e4bd325f05ae4777",
			"4d0c9fd1eb512f20c242f5e88ef9b2b7f9df20ae",
			"fa6ce06d9e39e4d271d7c0660650d610688f6a24",
			"3c13eada1a11e014eb46f09e51ba985f763b218d",
			"df92ae09c63afed7909295b9a87185834f111029",
			"fe083cc68e03fac37ddbf0e828379291446a0626",
			"1096506b6b2c6d5e91cc1bba01248d2f4efb7b04",
			"34ee816409d04c3230a84078f3f3e84c0f248e17",
			"23bde14c7964ffc072912eab9b08b162a83cc809",
			"1037daa04742e31a47ab6d39cacb5ba073866e7c",
			"40b23d943bacfa2ed7ab10806b09f31b4d5cfae4",
			"271575f75d6a62ec6cc3aad099d45dc7a0f94a7a",
			"56af9ed18e2e0ad512e7cfb1ae2b6f2471cd9ac4",
			"fdb5a5b2fcbd9dea094cf3f67912f4aa4747159c",
			"1ec926dc98c5faa001ab1a6d9b617db2441c0b8d",
			"be763951112e106425037331fca8eccf0cdd201f",
			"9440cbe7f97e62a827e41219c5896d860f6b037c",
			"293caf8f3740f303c0d712704aa4bec22b883e51",
			"e5953a4a39c450624e4d445e361393d959a6d3f8",
			"8639f899c2e8324da851c4591cf6d1f3a1611e6f",
			"397a752e2435c23232d7c2f7508da2d83b375736",
			"cdacb0c1cdb452d65d777ebbe40e71faa073303c",
			"1fedf2cba59029f0d5370b36ef01ec360979064b",
			"61b5390ac78f2bfdce830d4d41109d19bda88e93",
			"938bef69d54bf25921d13c7a42e1c15180640670",
			"73937c172d109313eae2a0d923dd5668ca7abee5",
			"ca9fb59f4b36a9cf271a2cdbb3d1b6082ee72889",
			"6cabe954694aa9fa02ae4353f28efa280ad55d2f",
			"26bcf4f91b80556d7e2d0eca1412d774aa9f074f",
			"57c85c6a52d3b4d4ba9a4b4ff36268e77208c644",
			"1cff6c4f9db8dcd14e76e84df8f18ec9bf94b0fe",
			"6fbae7099bea36353f1afeb5aa7ab643fb9f231a",
			"c3e374a63c1364bbad2df36d57b1cd33a5e01f1b",
			"48dc153c7bda7d5bdd147c7a8797ed47e0e7c140",
			"a9db6aa0232700a77a63ec76caf2904df6abca01",
			"99122e222291acdfdc939b28f1941c6c71e53936",
			"6eddbf78b06d82d9a497c08331ba433f52f354a4",
			"c5469ca21ea9d62e1336cb80d95c3fe7ffa2c4f5",
			"7cb2a683520b8be4cfd11cd38d9c11ede7a8770f",
			"86fddfa7dbf13d415be10864ecc65275ff2fa677",
			"ee8187dd51f15b7c7832cd08e5f0b3f647ec926b",
			"481346daad3b9bd3506d47303fc3bd2e1469db60",
			"da82217582f822218d807af075b6e0513f73c233",
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
