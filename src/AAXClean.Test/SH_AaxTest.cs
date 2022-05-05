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
		public override long RenderSize => 64416191;
		public override TimeSpan Duration => TimeSpan.FromTicks(3421559002267);
		public override string SingleM4bHash => "e38c08b98750de9d29ce25c5c61a1c0c85b64299";
		public override string PassthroughM4bHash => "958dc38cbb682099da0dad06b55dc37727db499b";
		public override List<string> MultiM4bHashes => new()
		{
			"ebc1098695bd6773689ceaee32ecea0164d36286",
			"12e46928410dd4d7af21c3b9ffacec84f7950775",
			"bf5009dcad382765c69af2cdd56dfac804ef7ca8",
			"a0b05b7a47cf9e7b869583dedeca2cdf021b406c",
			"fc552e6bf4b5c7811c5f4d932afa3899c2ce2be6",
			"affb09cf5c40d0fcfcb64e2d15d6584c2c01f7ab",
			"003c49a7c63c050426d0ea435a6e712854230a82",
			"ba7dea79fbf68f3ec89f8259b826c4c4bd3fd2ec",
			"f4a099627ae7f9aab231436e94e17876fd099c72",
			"45fe5f76e81d841d2c9be7fe969c7bd94f79f0e7",
			"8632496df454221b29918548fc4d59d845173400",
			"39922f8ae51388a61dfe8945ae6f9b6e8b1589d2",
			"362fc5d60088c372252d2d23824a3afaf3937e34",
			"e731976b0436d267befbfda97bf30bc2a2500533",
			"8ca4a3d014cb1305f1c985babcd07adf0ec26466",
			"d00f8a209dead6041ac31aba1a4b8fa77e272e61",
			"9b0ad2f492723eea4dac524c8dcde610decf107f",
			"3bf4684a4e2e52cf0e06c5df42011f3e7c2445a8",
			"175d5829ea8f10876f8df40ac38873d48660a2ab",
			"76b9493f3a2a872b4cd1f23054ac0f21b365aac8",
			"7d9f09c21ba68f39c76fe5afbc4b9f2ddd926011",
			"9b682d407916baa71925ab09d157ee841210321c",
			"293efaf64143c1ac66325ade2ac9b4f95c4eb1ef",
			"dc1f0681d73b31f567a295b66ef9a5e4d5757f8a",
			"f5bc0a8025eb3fd1194f50c1463dbbfece423ee1",
			"fb8e298b1c50bb5fc3f15c5e1bdda1e115f3f053",
			"9167200adcb746ad414f52868627f4158afd9da5",
			"405590aef26da35da1c9c823e5e070d7ba3e293c",
			"b8bafffda34f1ef98d0c6cf912b303365eb4e2da",
			"72cab19725e1fa9e18e9dd11962a567c20865a75",
			"34e8853daf5c85321906e91bed63a3423d8714f3",
			"1a7594a21c5001e8990732edc0438ce93ae9e634",
			"e167177fdf154766aaa56ae8c9250232dd079da6",
			"ef77e33596d49f073346531d6a46863e1cc03b64",
			"be09f1fd7b249c32ff46c5d201139543e7bd9050",
			"f4b769ed63f9b4b5b1773ce6e10206b072c4f5c2",
			"de52603743be928f03374bca432d05cac17478b6",
			"664f89e89840baf8803fd169f4f0a253426ef9eb",
			"b17bb51773309d0568b982178bbd130c550a84c0",
			"15669505a328bcfa0248bc08da42a2ddb2323475",
			"d8a3679b7599c8b27fa1533e31ddf0fdb2762873",
			"0a8360077a7a1b65a47ea392535d0ccde9a89a57",
			"0e9194d1780d235bf1fb70aa16dcc5e676f48cc6",
			"d165ef47043fc5aea60b284e75037d091c3eb9b1",
			"97a6a15c155c79f2ada838fc370e2a7266499daa",
			"4bd529a987df616a2105fc65c837c62649603688",
			"197697c0c3b7552d2f94a47c799d53fcf8bc9c6a",
			"f0bde79f7db0009dbffd55b8613181e250633beb",
			"849b42a828dc01dba6a422c36b946aac6ae29eb5",
			"daaebf8d72e7583b57a943d80d5b4b25f76b4dcb",
			"406980b00f45ae8406393a6e7dc276ff532f20a8",
			"4d5b201c14301e6ddfeb059cf1085a76f30b3935",
			"15fd0525a206425dda3f87767637813fdbf90917",
			"ad3f50b856be8b77eeb72c26bf27e461e8932039",
			"ac8246e8a43c456ba54d50543eeac7c09c0ed3bc",
			"c785a8e09c2cbd7fbbbf12148157d3cf15df2507",
			"8a38d7c7c67fbb953be4ecc5513135e61900b2dc",
			"d3116e6b1bd287e58e655f23b51d1ee2876ceadc",
			"3dcd5f1b0c6c5c838f6ab6e4a7a068f1d249113f",
			"e8a9f4b5b52596cfbfe3fba8b0c74ddbb8805a7e",
			"f64f3e5f4944929dcb73465e7900e430ae49ee17",
			"11aaf800b14ad15951b8db82de9b26518f2fad92",
			"0428251b376b35a5650d0e3059c03dbd77434eb5",
			"c50cfb69bc26bc6729a1ed417da33bb692e1a373",
			"25d3583cd0468d7323da7b1c0cee6cd448bfcb75",
			"02a6b4b93bac7bcba6892d176c342f1ce6422280",
			"ec39f28f4f534d418d251615bde21dee84c2d029",
			"69e798d14647d829d59b89cf3e018db0d44677fa",
			"200f83e50bb3589d3e791fdae9770d51a12cb134",
			"0a4d373237d60e88417432a0feeef1924ad98192",
			"5b9ea88b0669361c03e8e0855506c248d3919dad",
			"654703263a4a4a3e642e4bfb0e3eb17c6d84dfa0",
			"fcda91d1017af3011b6ba0fae45e8678d0ef7052",
			"269ab4d7e75b94f2356448f487d2bfd0c61622ac",
			"4bb60e1a686c43a0e0327418289f1f2c0379f4be",
			"0bfc7311c0d05cd2b1ae55b3d51273b1c3e1295e",
			"ca538d17693020f130ee7311b3f7201ae645bd86",
			"1ab57297ad27579bdf8c6aa6f3d6ae60623e512e",
			"d7cf8a748a2ec3bdebfb9ef37e8d6d12d4d93345",
			"034ae63cc2d745279ed101f30ebd455ef45792fe",
			"705cd5404fc93861733cc999029615b6de97f840",
			"0042162a193fb567a422a90f25eccdf0dab94b47",
			"c740385f8018ebbd73e6924f78f4d7b635e1acd3",
			"d225af24bfda7c3ebe5ed4f0e88372934059aea3",
			"009c95303e4fce10901351efa846e5bdc6accd55",
			"6f49d69c5c28f7729110c22ebf36c83f067b671e",
			"bcb3582f07549c32622090e0a8c4adce22e4cd52",
			"eada9377a2b6658d723675cbcf7f5fc2b0a1e062",
			"a39a92ec4db2faa3cb406758517ded64573566a5",
			"fa0cd06be8430b72213a5eb7288a412ec7d96402",
			"0e73e07764a0e9bc1a156d3dd121c4df193624ae",
			"59b833a8b431fd413dbfce095261f4c346535dad",
			"fe19276525663a50dfc931120b8cded244a7ebdb",
			"705514fa714f1857d16dd40e1ab9cffe2541015e",
			"2b10d30f464b22a9e2b496dcc44b47412696944f",
			"baae2ef443d0dcc858e792292e401a3729267244",
			"aa1cf68b4f435cec37644c7d092c57ea1476cd6e",
			"8192f735d6ac6c468e0e4bc002a83683b3db9fb1",
			"fdb6e241c356970bf53c0c9c9f23b32f8fa22868",
			"7cb9ea02d95fcac1f77c4beff151cfae7d04b589",
			"1b89f08c20eaedceb7a541edc8f11b403f1b85f6",
			"ff34001db399a2244c33b2dc95b7d41499595315",
			"a37efdaed07d9e932ceabef818b6d22209c32c0b",
			"4debac3b7e5ce65c4ac8703e08d588173771ac52",
			"042dade2e0deaff2e25333c31340a66e7bf3f6bd",
			"ab8bd3caa693863e86b66e7a6904e7d1b56a5d41",
			"48b79d82cd8bb36c7cb8fa8dd920440a53732253",
			"5c481157365af4b1ee8b0de9fdacda260f8648e1",
			"fe1cfa9bc0f373ca19cf3d8e5ac32381056c5fa3",
			"2e6e6572089a14fbc1b812ff365716e23e047fcc",
			"055101874923b8749006fb6123b87d63a106b219",
			"65f721c48c50832b1b95a106f76b35ec1a789284",
			"dba453b91d8acf7b9b72e01ad119be116e853caf",
			"bdf34455f6934ff38d0d06479ddb17d175a656ec",
			"23c7ec70c8727b22aa5fd982e4b3602cee3c9210",
			"4e505916b6521e7d99e1af73c8323c152cec1bb1",
			"9d60246b3b58760f42d5d4cca4cdd7d9fdce3827",
			"cce5bc6f7b30dab990684032df4494a3fb8c6278",
			"d0322585a9ec41e5b0b872a9d78770cb37061e07",
			"76fe4814d3b5f817f0e07168630daafea6515266",
			"f69db8128f5e78b7010df3b5203cf5cd4000698e",
			"dc92fd7c9ea28e2988a067bd894665b30047edcd",
			"a5133c3548db2a8a99ee9e86b283110706900a85",
			"85995a0450d4cea45073b5fbd69c150668c22622",
			"c0d0f669b6d4b72d8b4ae59e9dd77f8b743a6c94",
			"f0938b8bd41810267b30ffb058746c7b7ffd548a",
			"30cecba36be90ccb5a27a983fda184bed39c5890",
			"627a89f9e08f2f9e4735107589bb8fc846dd8ef4",
			"57470eb3079dd9e7fa1281d762881f1611cd5390",
			"185c735dd7d5f7b1d45c84eba244ae2331c6daae",
			"2356bfb7059f1fb2486983802237ed87fb59e57f",
			"02596d4102c38fac5a4b5ac20e76feb14f24acd5",
			"d6eb649a0a963453856830fb811d08d0f97f149e",
			"e3363f51f4af0d36e542db7467e64bdefd2fd6eb",
			"bca53eba84aeade44311cb3b492fc1793532e178",
			"86d8e5b8c68e7c2acccd4934854cffa7493d8e95",
			"c4056429bef43635883013d8509eaab9fd7bbd31",
			"0a7c01a8ea568f437ff05227899caeedafad08f7",
			"2f053899888894c9699bfd8082bb7aefbc3e369c",
			"2a0a31f91d1c9aa40bd36ca7e8431d9888da20cd",
			"812d2391732a4808f7516ad2d5614c54480ea2c1",
			"109af3b60ef437c93c7bf956ec08787dd57a793c",
			"c70791ffcbff2a67bda3a4f8167c292d92d3d025",
			"f6e0e5eb69edce61b2d9a744df6b7bc2967291d7",
			"3ad7dac2fac2a7ab54752fc57abbc9941e9a5b3e",
			"c3358477e92f35b1a14d2054021786e870e7f47b",
			"bc25f063d7933c6af07952751913615ec1aa096e",
			"8ad8c636ebcd86ac94cdf7c7d481afbfb17f2556",
			"78a6ccaa2724ad82d39a09f434c5fd157c21f20d",
			"eb2885ffbf5599f4e7a5048d6627e61fc03f7415",
			"e0b288e27d44b63d650144c8b5d2edc1ff3f365c",
			"2fdfd30ae293cd7ddc451bbd9242846e2bfc8cc3",
			"8b2788af4b118fc791a6187e7c2954f251702be0",
			"6349ddccf2fb439b995c8a96e13282e9a38a9a1f",
			"d5bec32eec3b91cf8ae3149484617b71ebd91d2c",
			"19b5120221a3fbb628fda7b27a7913118e061c38",
			"49e7c33c67bac4510cca6cc7b9380544bcb68a51",
			"0761aca4ad57b6f046967895febf6196e983bb48",
			"a8b7ab4100b655df7071f5d4aa758e3131b65e24",
			"034bc20b05f2cae422e4b27a13201d84762f77c8",
			"e0249f03fb9ab7a37ea307ca55de8ef349c621bd",
			"302f2981f99778437c4246bc4f8016517ffa6596",
			"131c8f229b14edd0c8e11317fe93e003ab6dc820",
			"a50336c142231c05cddf444029c46145cb049a29",
			"e351e0ca49f4a01cfd00911c08b63b654b0886fc",
			"b1e13fe7bc69e64c149d656e06f5e7bc9f1379e8",
			"38bcee7234eb3e15ce9982d72114218c7c08e6d2",
			"f6e5771cb2273ee8917fca17b826efe02d45436e",
			"1d1119f050504f8f0aea7c8ba8fefd06c5219e4c",
			"9aa2f0bb5183ccd170cdc33c7fd3cc6d313d843e",
			"1d80ec680f564080173c808068945869c5de18dd",
			"e5ff0629f6712172152cc936791374de9c8c3499",
			"257a8edce95f5c02d92936d42bfa1a0cde40db2f",
			"1e547ccc8d00142e62716b2de59e2466607bc613",
			"16b259d0da62472281de45b3fc41178540e45087",
			"14b83e120acd58ea1736999ef68815414d6c89a9",
			"67e1d283597dea1d2cc2dc3026a9d559ed79df50",
			"3a85c2c888e720353f9b99d221cc4074cd6b1044",
			"8466754f39d0c35df23147e197d7fba27ec408c1",
			"45c0c9c1c92ead499c2df01f1ebfb8508dddc409",
			"9e53daf8ed4985775df453aebfe3879bcae59af4",
			"40da8b420d153f4c934860a79cf603c9ff457560",
			"edb3594b8cf9769190939fc6ebb37a5fcc13a640",
			"e68de824f43e88c5197d87fb1d15ab59224d418f",
			"aa19d94409fccb3321008954edf176340e42e3b2",
			"cebba918b0d65ca69609a014d4424be5d564961b",
			"7a2bdac8a4d5240a8ab548015dda14addaa0508f",
			"3c768cca2a0cbc5f0dcb87090ca590d63fe646a0",
			"f2318700d22cae972016300e2e716c329946fb97",
			"6466a617f1a554870841b9f41c0ff9e93d807990",
			"d95fdb4abfd52deb601dc3c805741b42af47edc8",
			"417134f8be0cf8963be69a51bebbbee7caafcfd5",
			"3c0d9b28d60832f41ee205e94a9cdd65d49a2c2c",
			"15e32c8380bfe120c9fe577d36cf808ccbee8f3b",
			"9bb38844e1c9084a915e35c9954af0db6951cff8",
			"36e9ccb806ed6f6452d0e5734a0846a8d80a1fc1",
			"8bcea901b2f4b176fb034d82e3db9e2584d92888",
			"cb0921cd63ef9951793a4659a91801f9037cbe3b",
			"a24b3f4fdaa0d9eecfd244a6f291d90f8cdefc7c",
			"4336d50fd831b54dbe8e88d821d81e6ed6d493fd",
			"bbc7e03218e457c018ae1098d202f5841575d325",
			"b82e5108c2e3e3e79a0ff2740aa8e70754aa54b5",
			"fb44026270773cfed8bcedbf0478ee31c11b0a26",
			"43d5cd411c688f2ee4047f2172de2a85d0a171ff",
			"714238d97b584eec4b8754f29376b2e885e935b5",
			"c0e8821a18c1ffbf937433d70c7ec6989467d61e",
			"a638dbe7bf1f9ab5cf04342658b206084433e924",
			"5eb0c577d88e8323fa145fc88fb5e53e5023fa22",
			"83e7965e97d9af811b5bc8da24b78e3a92501786",
			"35a461ffc87006e938e4ea64da07b3fb5f3c05b8",
			"e6260fb996098401a4906677cd82957a50294480",
			"e5a6e30fd1c616684a2cb7eebf8fc8117118aaaa",
			"9d2250fd125d80c01186c0b19d801c18c7d6efb4",
			"24d145836beeefe06cbeb2519b5ec7269f48dd05",
			"2a6d34246fd8043ee9293868e4adf579f6a251b1",
			"a0dc1356bab290f69a1ea0c25a4734c8b34de8aa",
			"399b118ae239b3a7d30c4fd40d23a75ff9361284",
			"335cbea00a9e120e2fafee9796a1a4028dd7566a",
			"546a6f774a139c08cd6950a6948b6dfa48f071ed",
			"ff539410fe8abafbfc491a0480861f664f1c8f33",
			"6316a1cb3655e3c673be6e5bc72ce5a79a454b60",
			"08ba63e73f4de72c98ca07b6c6505bd1ae4a217a",
			"99b6331ad211aeea52419ed6ccb26ccd745a962f",
			"9be838a1a80eb78bb1782ff54ab43db3d76f3fc8",
			"6e0dfbf4c83cd3277d71e17e5f87dba676f58612",
			"0bce06660fd6720e4db4f99c41c105e8f46eb906",
			"f17ac5812a0ef996de87983ba68cec76955da4b5",
			"579fe767c7ec9173261bc7cacddad7a2bd915678",
			"2fa5ba52b1400b82f4cf792a5ecbb68ce9be42f3",
			"a1153668bca9485125b9064ae106dca5d640a444",
			"3fce33b50770f494e7213edb4d46a9e0371146ba",
			"5b61a2fa6c8dbdb5e7b033376b681fe32ee9da18",
			"be65ac53ea8f084bbabf5f0dac479de14ca8ed08",
			"adff8188d129a75cb763085d5c956e5bd4103fa6",
			"07d94ff6f2fecfea9238d71368fe1a44a56b7cb8",
			"ab2abeb3c2831a8e930b2d452c9a91b3a3430b74",
			"32290d7b4bb314259aa29948abdf842eeb51a8b4",
			"ed63326ec915d33cf9eff14d4015529a2a6f807c",
			"4816df6564a9f2e07d4eb527f4cf31653c3b45ba",
			"918dfa84a84cbf67730b6285ff344de40fb1c629",
			"cd0b772c7ee99c806879a54eb2dca86430bdc9fa",
			"e97d8a411d45b74be964020ee504724cbe456d5f",
			"696d8db932098e8c500ec1325acd7e9b483b042a",
			"b647e600bebcb75a24a50a75d938b4d62af1d688",
			"7a275dc32a76ed8317991823981383f690ff8aac",
			"c776142ca41a3d497fae84b493121438b80ebe07",
			"00d2befb9f0ca60c215b5249de0eedbfc7c3fa2c",
			"13a6e9eb48dd3f9adfacc3ef6b2bce4f170245bf",
			"377b7c7e1c9ba55c0ac776e35e17aa85d259cfbd",
			"45d3d47970b1826003da972068eeadc67b6b419f",
			"29b41ca6b32ffb92c9ff6d34198c8e730147cb3a",
			"db52cff5f33de453a49ba7e8bb34cba9693a2be6",
			"e866d643a1e63a56b95c6da540005ff958eb4386",
			"9a15078f692dc711a92f66465c3d5774e92ce97d",
			"4c5f5647c3571c9d2250475634b56a772ff007d7",
			"8df9d905f0b27b6c18cd6aa3cb96e015e2876fd5",
			"8a9f408e50d6c8fe903a49ac27a37ca3a252b42f",
			"9d9a53b1d290a7adf9a9ce97931b1707df00b3a1",
			"00257b9bf2fb457a7c24f09fe55bda0f90974922",
			"231539e84b0f76d6afb0180b616fb1065d34755a",
			"1e07ff704f56cd652295bf2ded07792b257332a8",
			"0100ab0bc2c62bbbc11fe68724ece283e4e906bc",
			"5fd07bc575f57c318f2cf39ad3e599ac06b1911a",
			"80ad9d1a0668598813c845a2ed5cde3f3a5902ea",
			"144285b6ef8812fa95dc4d18c8283980836917c4",
			"e8ea1848f60ff20b61ae826b1b840e5c12af9e38",
			"948cbd4cc312b66abe7072ad3f88210a48455fa1",
			"e13994ed45c2ee8e5c551d5da88ea2395f32c849",
			"9b4c667fc7442963bd1e51b831d8dabdb6c7ef18",
			"38cec0529153893bf242663671a01eb27a7af5ba",
			"83eb657b0d4e515207f6b7fc5a7a218c2240de87",
			"4acbeb374148e989f04ab98a535ef8ffcd69b9c3",
			"41a0a1e475e88d62196aff60f662e2e40ea88d74",
			"18f0b4b698258953109e5f84578c8940004a4578",
			"5c8b8409b981d8426817b69b2480b504d1290621",
			"ccb78c9b1a7e10261b73e301ca4e05279e365206",
			"045a43d963ea9d161aa16ff6bc570fa7293319c1",
			"b2e21f902226a9a9399490c396dcb5c60bb00453",
			"8ff1b4dd1f88bce2c1fdffb2dd59f4865f278776",
			"7618814800b66468890c2cea0910a2b2df6092d7",
			"0ba1909a75c74f8e71bae4a44f203361c51ec6d0",
			"7d5d494ed83d6bea38e6782e2755359b078abdb2",
			"15b90cf01f474bfaf0a48f5c8f57e03ff002a7c6",
			"baa40997097ce979c83d106f06df8338a9009ca5",
			"a7af8f19b20a84b232a51eeb6cb4960bba8470ce",
			"a12b5b522c007cc3c323baad7cb8e0c0f6a3842f",
			"d9835ff711af0859fcf0a402350da7f6d307da23",
			"2fd12c050594f4b9f822c6b0349a1202cc05e408",
			"f1d097475d4e364e4e7f2e127a5e2a95c5047098",
			"a56a23fd280a9f40008b9c925f12a7b9aead5410",
			"1311fef9316ff95a8c8e54de725b0097e3cb2a1c",
			"4e3fc0d684d7ff7d23f3dc53cb65cc73c2534281",
			"1c0477f610d39315a44e138e739e8b80dc3f9f96",
			"166cc163db5770b4fe03e2283e7d9e4c4a08acf6",
			"fa2e89b6b1eb54cb1c570730c860f94d7298dbb2",
			"2975750c7b1341c77ca77171637702ef29bcd710",
			"25b7136161ee41f6bb75643798ceb4fcc7cf7e48",
			"07149adc8ced93eaecb328e1abbb67dfda90f84b",
			"b712f22d4ea8491baceedf25ec3ca255c339e07a",
			"e3e675c12a929c6b3034933fa8b2f6b7f6f674f5",
			"52357696b330be0ea13796b79e2c7138ba76599c",
			"96f31b5b391793ac97d12bd6908fac08ae648286",
			"38169a54f7e446da097a0151babce751e225633d",
			"edd422a6a8087a3e8d649ae512e16ecdf37f1042",
			"82e60a4523e51ed0b3eb17e9ad18357b6b42a415",
			"7a90a1d26ec50b3b8bfaeecf8b5bd10f325160cf",
			"1c19c06d85c56c095ca72aff70308a7e1da496b3",
			"b4db15da6ba2452aca11e05037f40e601a0e3e8a",
			"b1c89b23d03212dcc5fbc8192c0f3303e78a49f9",
			"efe7b869f4a11893399efeecfa1f0c3adfab8f14",
			"9216e3670cb7477eac8c365023e0c4dbe30a9470",
			"8125b67613711d4e0c190cee1cee2110a07b3390",
			"324f8b9039fbba51acf2ca094764488cdb0ea824",
			"89418d1d21882a85cf033797be574d5902c9537f",
			"51feb0efd8237237ff000927494739ead7684091",
			"e6f9fd2da18f276eaa9e0339b7159a1fae527dc1",
			"3a8f5b876929ab29103fdd1e0d12d607879a0911",
			"7e77b04b0c0d0c26ca5bd8a670fce599cd665baf",
			"a27cf6cef856da6a1fdf6683d8fba641fae1d79e",
			"907fa855528f7f7635ebd0dab51b85a8ac74e7d1",
			"46e029107bc11db6c073af46a1b300044903a4cb",
			"7579575886f3b2590e63bb7ed03b31b8c0e48286",
			"d8f1b2fecc5c2fd32fcc9ce6b0c359c77ea1e90c",
			"2f82eb1486c96226bd93d964dd0b5aa61b383329",
			"68d98139527998db043cd0fe8e8f026b1adb5da3",
			"77c4b05a9fa0d5b7daee15e2c12495a674f5ea2f",
			"6c81dc44438ca706c930e49f69fd5478a08d69fc",
			"735076b3ffc08802fb97401a0a1d030cb2445952",
			"4db3e791218d3d7c40a3b7a4491ebb2b08f3149d",
			"3653870dc6aa077f807784a174d6a370249c7471",
			"6e6fa4b4f821a66d13d5151635c4db816c457ad6",
			"aaeccd78899dd1e7eaf860d6ef08b6dedce775ba",
			"5221ef4cfced347df7fb41e38e6068229c8a90f3",
			"a2b758f892a8ca6a29bd089e74aea16d100c5672",
			"1487fa4cf571bd56768a108b0a3760f70282ef7c",
			"f048ec315b01a06cbd197bd99ee6b056b0abb373",
			"2e62990ce8029aced87f3af6083179da8759d0b8",
			"c92041778087708e6baed049d4b73717319788fe",
			"4f115d3603fd3f83905ec6f184745460120fafd5",
			"576e5f29dbdfecc019bc02db41220e2994f7a96f",
			"6fb3d3ae7baa13ef288fc236a2337fb3117b8caa",
			"d677f556fb7f1e640df2830d3311b5d4398456ba",
			"fd1256fbe9b4344bdc3b818bd6284bc91a07e530",
			"ffaef52c99bf60957a2f692e5daf90c594defb6c",
			"83e2b698c8033e79b8aef0ae69ba53fe216fe44c",
			"45c1e097dc7e29f261fd97a0ecdf589320d3810b",
			"ee6565fd34037aef58eab03d60135ca37feb9b04",
			"843d6a5f4fc7860360f17db02f9a2cc207c88d5c",
			"fae4792b91ca05705e05ace9c827cf25c387b38e",
			"4fbda471d018d4c5e290cff4d86a9282a2034806",
			"1bf1f85183023f03a8f1c1ff8a9e7e5e50ae496c",
			"e799448f714a0fc87e0a955bcbb587954d3de80e",
			"308ed83c8355fac96f3a9cb18c0231735a18085b",
			"9cbfc1031ee66777fa06874aaa0d305a75e00a40",
			"549783ceb179c0470a89c18a5d8a73d010743593",
			"76873e11dbeebb3cf52c457754daa46547611f53",
			"03e3f32657c2272fba1f855e614b5844303f74ea",
			"a79fe533e4b63614860abe0b0471b3d2b5dca600",
			"105219dc92afe0d29c27fab0dd2c5a5a6fca11d2",
			"12bc36e31ca9c8b2229ee63abe75e0c430e0eec2",
			"60b1c8de28ff1e501539011d34fdcdba5b71921a",
			"c4ef37e2c9640249a6804291b20eddd639de7c58",
			"9a7bcb68726552fff132a183c92d6cc84bda083a",
			"eda03cde4bd02ef0c4b4d9c84fe30f9abae91827",
			"bb6dd577c92bfd10405c9299221078763df8b5b7",
			"536d34efe5983a478e793906ef13819027059c4b",
			"59ac16cf197a81efe9b0b78e68d10a0b688abef7",
			"fbaa61274b68748e41018b8b6b8b6dceefe1406e",
			"3c00d406ee279f501b19f76357bad25d90b76367",
			"0a256c852047053b682a0bdba5be55dd27f59830",
			"e39a113273af1037f4c61dc4c41f4aa67ccd1cf4",
			"77549b010d7891ac4f78e0df73cfb55cab2dc5cf",
			"fadc28ddb8b7ed4bd6fdf5d890369bef5f1d86eb",
			"2164f3c25e5c45cfc8478de5daa59f2085b0a780",
			"6f17714c2f27befae7d3ecb058dd2d9019e7713d",
			"176357e7abc0b78e0f821b9edc620a48cbd7025c",
			"39783569264327ac5ad3a1794b101f9b52d6b2d7",
			"9f68d589ba432370a1c238305d61320d3a783e25",
			"216d859bb5b2d248e38fa320673bc23f4c3932fb",
			"b4976f65b4bdbe38275b515a84b803020155eced",
			"ad01b15498312cd02825c747364e47eff15e6c3b",
			"a12bfb1e73ca28dc06a9d8e67bde0326020a1bad",
			"0d07d1a8236ccf14dc0c7e628ef4487988dee0be",
			"6fb5bbb606d8060c1cced1ae7b7b9beab3b1e799",
			"81b4175de5fd85a5948fcb7a13f7187d612699ab",
			"98f15758f1f5b1c5a271d52008ac4a715b277baf",
			"2a826a5deb1d6700448cd9b2973b07b26afa0fc7",
			"df2f2199b5f610266479348eec11a7fdab939dd3",
			"1180b6acff7cf98dad06ff526452292026d99dbb",
			"cdab811bc38aa51b26332f43b3547204f5dd8be8",
			"2406f7e08273e7a0a70754b11928a2b9c25ba001",
			"d747cd071afb5c61a23cfffad027b094113237db",
			"8ebf2361deab5d89b179d778df428f036c77d3da",
			"4288a5036ab169be8905a45315dd701013b64642",
			"47bc15af63ee2012e1ce43ce0337e35d9765d791",
			"adc69bab165189ab80b1e678ac7edfe4c304248c",
			"59387821277c9756f8403ac13ada0828a4768bd8",
			"4471ab2c75b5c38a4286131b4c64cabed3eac7dc",
			"3db05775e49cc5c760d4fc4abc159b14200ea116",
			"9cb62346f345bac355ff12f3d2f8113e097a9a5e",
			"f3823864ffb5c19635c4d4b7074e1e11b8930bcc",
			"32b86bc6a35a2787b4ef715aaae85a86cb4c8e44",
			"174b44fedb9bcfea35b58ed93d47d8b46c15621f",
			"2c4f2378cab26167f7c9b22c5d51cfcb4fb71940",
			"40061bd9b7b4646f2db77e83218abed70e402861",
			"1b48bd74f588e987e2d98609975ccbedec37ecaf",
			"f28db3637a535c44ec97341bd26627d62e3afecf",
			"c830d632cd41c372ad5bc74d3be4769d9f699553",
			"c393513264bc55a4b9f956b9f94b846f1f2c2bdc",
			"fbd112814e9e49c6b1c6130d2c8ca22ddfd1c3ff",
			"322383a8eae5869320bb44b211f864c44fd46584",
			"caf0ba1f07029772b05c9496ceebe28c2bb057bb",
			"29786c38599c599e9c11da0f7330f08f953ef3d7",
			"8f9cdb550a685665ae806d2bc89c249614bd6039",
			"c9d23b7eb41245418bbda1998848c3d03da9f3c3",
			"959d36d7619e13f9594a729c3ff6a35a010cc53c",
			"0c799b7dbd4efc2cfc1462f170285cceb0f113c5",
			"b63e013394a3296185cdff9c6c51fa4cd53f91d1",
			"09f5e6f3ca50536b5c6230e1adbe46befe41e978",
			"a5fd07293466623b7924d0961c2694d4a9da75eb",
			"e13fbd84aa6c4b541c4bb3a3ae0e5acca7230f51",
			"19e97b0c1cbb84562e47fbe6301044b51393a48b",
			"64ad9ff030b8b79657122938abb27fce9adccb83",
			"dfaa7c6551ff68eb00d12cbb765e3bc64f419852",
			"45c876ae3ff09f7a55217e0e6f4739b9c2a00d76",
			"4e2556a5066a2677a523b544af07da08c3d0f524",
			"e1f3da64ad98ce743465ab8c91655d496a8c9600",
			"151e33887d6fd378f6777994f9fe0a3072a2e5fa",
			"473d6a00efb6b4edc185901bb9fc3a4d8b45794e",
			"fd283435a5938f28a4234e67f93732c3e6b031d5",
			"7924098a45a6729e9d6d9e6d13cbcfbec5ee9739",
			"4092968fb8a230ecad78ef8da7432a4fcbed076a",
			"7f26a0e647331634b2ad0840982c8b0d090a5f81",
			"656d7a53a086a4ce0cc60366c2830588bcce0af4",
			"6c6d142ee0929fa052226c46973cf8ccf2d64914",
			"b2548916d22e3d56236c20ff3edbaff3ec5dd3c0",
			"c769f8460be5d7cb5ad86e38b0e784e5cf8e85f0",
			"fc36db86c061cda4e79ed6be022054679b13f8bd",
			"d7e93a04cb43c01d7152f2c5fd6dc39a17e49ffb",
			"f25322873509ea29d910dbd51fefeb445419cc8a",
			"6fbd6811871bf28f3e8c3d844af532859a795793",
			"85ec71e421ff3f1b607c13f00ca10ef91249bf03",
			"f24eea1ed64cd27443b8909761ab7af6ab7c4ddf",
			"36cdbc761e84f9ca2ab1a34e4d55e683a50054c2",
			"47bb089b84500c676db0698391af5a264efb658c",
			"8cecfd08518324817418d75493ec0e0ec59378b8",
			"3190415d89ec768c4b35ab14184e486497275ad0",
			"63c7efe41488f7ad7690676721c63d51ebae6e17",
			"144296dae8335a83995fdb043595c3204fa08894",
			"a33093d8ac588c18985480c6005bb54f86a04741",
			"5ebd12a3f78d16e393ee80a6ccedf76c575bb005",
			"233c9057f05f3a2cb4c546f4452dc523037b6dbb",
			"a7c2700e9d31ce9f2a89861e7d41bd7483ed0a85",
			"2b8d67a6e0a7bfcfbf8dc731280888073f131111",
			"b8ef2527c0a083c5e16b807a2d141c77855738e7",
			"4478593e3ac08c0ea9a361033842f2f70e159340",
			"2f556a968c5f20fc235d362b97c94cf8e9535c44",
			"0066a2b7e89193334e63fa33fdafa3eb9fc4cf71",
			"e6cde27eb50bd463cc91daee8b1bfd1a2ff8f4f8",
			"fe29625686229270ffa34e7c5b1a79cadf83772d",
			"2874cf478b0e73c9409658ad9e40c110614e21fd",
			"f32864b74562f05cd8cfa10c11da4305ff873c28",
			"4d9c730f5880fb43be701a9797de17e7dad4d91a",
			"d48a9b62041fdde770ae71c5df8c2453b1a67fe0",
			"84bdc0ff103a6fef90db881c8f73279d7810c8d4",
			"0abbcf1c71c21d3fff38fa6fbd25688e0d23c485",
			"4eca845a04cf27b82db3b8967ff4b10334c1a3d6",
			"792ce1174f3a15a3272e197b0793ed5c184a0ff2",
			"ef8411e4f0d78e6fbb533e8da891368b283f6234",
			"f1674986145a0362803fef86391fbc7457d13bf5",
			"877989bb95d64fef6787eabba8d96912dad2aa13",
			"8861513e41ba7927f34586a2171555d236c7c0e2",
			"5276805381b9a2d5ad32e387809efb5bdb1ca658",
			"f5ec2f0e0ca78f8f12493028c649de0767ed7f78",
			"2acd24103c9ff3c330acce6694f9f9d2135931a5",
			"64f2b72157283e0e8d7fd6ea9fbc3fde7e400479",
			"a3af92ea1d172d8189c691b1db14f26dd490bb0d",
			"b451eac96f885f3edffa9bb174c2083a57013d10",
			"88908966c4b4ea4b61a4ab3b0e522478f020f898",
			"a750bd5cb59fbea2d51a5dabe344ae551238744d",
			"e2c98c0343219fb49d1d5c094355465fd3490690",
			"4c95b92454f98b0aaf58779cd733102e3934b615",
			"599596386877f5e4ce8c7b5b28071c89096f81ce",
			"36c76641f786d916c8fe4637925105cf3c2ee91a",
			"de845c17ac03de7bd95ee98be78f5bbeffc4f56d",
			"ae274b30016455617ca33aff2158c716b5ec1b2d",
			"663fc71f95852fe9c84ee693d40803fdaf24e5ff",
			"4868bfc5df09303397169e4a3a34a3a1e8498b68",
			"dfb65be077205bf340dcac5b304a6f6c17af4491",
			"ec0fa85e42b2ee4c20514ea2b242858633d8626a",
			"5891ae3b7c631ee1e10d1ac9ac197036f1d628d7",
			"c7319ea4a1402e80ea9ff44075f7ffb85c0e35a8",
			"a2d8d65b5dc696a8f110cd7252492a2bc737e05a",
			"f149cb094d854bd1d87f07f852fe1ab9fddf188d",
			"49a8f26716f31cc3bb089f9f378eabd5460962b2",
			"acc3b37353a593b4e2248c2843238996f5640dc3",
			"bdff577dbf5b50895d334ead880bbafeb251fcc9",
			"d01b82fbc79ef102a5f19946afcc3a791937c24c",
			"a5225ec36fcee584606468c410620ebe0d50a8a5",
			"e772776a1c08989d849cb9888beeaee8a89dcf5a",
			"e38eb1d522e4e9b79cf1c03d2469faad6eeaf236",
			"df028e874a05bfd79e308adea166dcc93a19100c",
			"6abf045377a0b46be9f850ec55d0f1cb161c6130",
			"b3e74a307d1eb967d151e468eccf25090097af1d",
			"e36f2301bcfe7e0b7d974b5578f79fea9bf54016",
			"fcc3d528451b301eeab9de9280b6ad885b7d356f",
			"fd0254116621596c28a3a15f400bd3ecf348d015",
			"11c38fec69095f462fefffb48d38de29f96b3a7f",
			"914a2644227061950a3a6e80bd3910440336049a",
			"1d42b8308535fd7254f4e0e59aa482f144d4d503",
			"c00ffd00a2b7348ab6fd493f1b59bb3115f73bd1",
			"319d353b4aa9eef0a87d298fee6b77d4ecfd72f5",
			"b5e2390fdfa5f5dc3f41f93d028aa64ec7729848",
			"2f915b6c306483dcec1ab630a300edb7c0038acc",
			"0064dbf92715d8f6ccdc3ad9beecaa119764e6b3",
			"81ee51ca6fd5e1d61c5832d6f01e5b05644e7dc8",
			"2efdc5c639d3a83d1771701224ee80a1a104c47a",
			"d6a66b1de4a2569da33324cd6f9a3d183bd6596f",
			"526305a4231b56883145198f8e0a23b3721fdac9",
			"d50bb9a7fc77c00f527a1f178cbdb986a7690f92",
			"ffa6642259d60ce7113e3bb53d5aef3b9019c69d",
			"994eb5307fc2fb0c2365baa4bc1b0e5031a63163",
			"31af7f91c01606b3b5f485ae4847d133c8065a9d",
			"59b16e434f2d0c56c179c071a8e630f85d7cd5c6",
			"4783b7357398853b9ebc3270c5840c7c73b64466",
			"315def66edf760350a2d9c7ab0ed3f8b44ec7ed0",
			"505162f0317412421530c2f392b61580c07d8abf",
			"24daec5aa13273b4c2eb354c6cca98b6a18097d4",
			"98644f48b10b58f290d8ac14488485e25820178c",
			"3d8dc4d755c0969f0596901dbf4670a6c03c7867",
			"d123829114e6bb1833fc855a417cd24809d25fd5",
			"9d061a5ec79c095fb1d7cf209b77def35e338b6f",
			"d7be0c740045d2852bb764d7ea08536ef9234a70",
			"1461977f2dfb27bda40941e38bde4840c72a49aa",
			"18587ed132c92cedd79c61b309a1da2b5c7479ee",
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
