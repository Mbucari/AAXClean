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
"abacc646f39a97430f9716e6e763a25a261bb08a",
"19e44b9c31e5341cc300c5d5cb66bf11ebd90959",
"0b42fd1baa0367f847572a17bf5a0180a1ee24fd",
"358cc2fdb374500927a40fc2dc0959e335f9e380",
"ef77ff8c387ebc24d51b2e6643c83d3b86fa3268",
"bf0601324863ee2e8c97bbf6e42bb49d816fe596",
"617c16e140e44326814c16515cdce78a996d687d",
"7ecef1e4710ede3957e395f7fb158edf92d05d5b",
"a4404e21e957d593d4ab082483c2f73a43194cc1",
"5845340eca28e1778b1f535ff7b0beb4a93d0e21",
"cfc8927a36e4e5388c02ed4fe19530b637b6f301",
"c57a666170063be25798c65657ac2629de8c33a8",
"5cd5e12767375043d28cfcd73a0813d1cd4bc55a",
"87dc47e738bf34657bdb122f5c2133bda39c44ad",
"4aa2805de54233c5da3295dba4cc91ad605ad184",
"1fb3f70ea49cb0acc1fe6ad99fceae31531986a6",
"e951ba14e2669628014bcdc680b497c1181c23eb",
"406708c0d74e188fdb0a071f7e1a6beb5525c85a",
"214cc39ae7ef3d936ebe6ee96de97d9183b42676",
"ca20e0d206226931c8d03ede95b858967dad996b",
"4585fe8766b967a4577c2f1de8f6e0bcb9a332a6",
"98dbe53c9d66c8d7e3dca14136b07daef1ddbd55",
"becdfe5da9428d6334ebdcfbf330fd641e3bbc60",
"c3c5bf974e44af93a053062771c3a93ce6a8a87d",
"bd6aaa6b82ab6b152631477d226ef803cbf88bb1",
"84d12b38569b079e403ff06c03941f2c3c760217",
"a90ad3ca49173eb12b560cb860ebd763222c21d1",
"4513e5315b1ae3b43854c64d9091fa771b9d8bd9",
"13fa936b6a7c95a7bab78aa1b21610ecdfdac5e0",
"85c8dc8e669566d1da68c12311114091abc2233c",
"4705ffa665eed949df0aa6289b5c9fac2c66cdb3",
"cc7128ab9bde7e92741a8c40ed915220568d9aa6",
"540f7ee2a030fb99d56aca2885662d5c75a11155",
"a1d82bfa282d6f5e3b93a3899620ece14f354c82",
"95c8de5b18c3096f3242a72321e063ad96960da8",
"895dc9addbf5775e9ef2b0a6e4b94c35721638bf",
"6c8b13fdb9631e27089dc39b830491d13d0f9316",
"85dec9d43c83fdd3cce336a9f94c68191139f5cf",
"c787f45b5a3fdb64f7c0888706a1bd8d36419ffc",
"309cf5a54b3850c7ab50318511caaf453d13b520",
"d72acb30359ed5bd511d531fd9e3ab71dd66eb1d",
"00286521fcdb3ae66e1dfb1265691f3fa7398bd6",
"0eb75c5dde2ecca56e52bdf3585803f7d1ae7eae",
"b91356e173904ebe75bb38b30531b26c9d776463",
"d813adc21c72410922c7a2ccb383233dde773e0a",
"a8b97447d1ea8b05ff1b39f73056799ff3feabec",
"058293fc3e4d87853d7050109505ccde324ca94d",
"fd2ebe9bfa62a8c4998611aceceb60b11764be20",
"5a865540e3d3b1dd4cbf414804f42eab060e4ba1",
"7370a620869ece902599ae25d029074d1bba9211",
"87948e36153c3887c02db53d95f8afa894d078ec",
"efef661b7caf362b78601130c7fabdad48e9386f",
"28ddd9d50a96ff5c89b35f9b6d84aeeffd9df608",
"071fff09ac44fadf3d01d49f0b31e5d983fa6cbf",
"5ea4b7d018d0992f48711950e6187647a933633d",
"b2dcbecdcdaaf0752a7a1e11fb9995d31443682c",
"176b86bf9cc97c668efd5817df397042ac83e91a",
"af343d7529e10c0d1b57adc17a51894090e73fef",
"4e851d13699ede2f75b8b58028707fd51823410c",
"974ef04310bc0fa5d952ba0ef1f5622337986cfa",
"68ca27ae22369f2d94ff6f870a014214b6685008",
"2058ebfe4aa94d5162face7862782e422dbfd81e",
"8bf4461def34f4ff4701a295d5532bd76a2b7c18",
"a288322f24066c5abc107bc779ca4539b924db62",
"84a142e7f49fc3ded500ea8c815c6b853ad3b0cb",
"d6504982e48bda10243a267c6a28b8a8fe5d5e68",
"8ce5293ebc8460fa93416563ef1794dbf9939943",
"c10eec92237a48180cc9c0556541363a45c33ea8",
"4bcbec9e7c81696f245c00ca6db3e8492616a948",
"637e01eb28a8fa1a827ddbbf6df7417eef304df8",
"a936b64310e977e6d71f66cd6c10758480d6f6a0",
"b1778425c46357443f22975d0a50e339583aa92c",
"00470a8e0d059423cc23efb09764fa6ad90a2d3d",
"9043fc255cbab654aafe6a64f701264efe36bd21",
"167fb1e6c85121013525475901ce7395b99f3dcf",
"e8974f1ed728af3591bf7145ad2694a949b992cb",
"0ef0495d53b64c1b334283d291966214ec0d3712",
"69a0672ee46a705240ac45b729dba3bcc45d354c",
"5792f2eb2f2bbd73309ef288f6eeb7c139475881",
"5a1e080b450f30fb60a5eb66b29f0e7a2d233cb4",
"6c6c23e7e900bf4ed2cd4b34261ccdf9e0433ff1",
"5d1a7eebb5f63e47dc5ba5c8f3cd93cae852ec3e",
"fe670afd235b092167f69c3d99d52845076f3d3a",
"218e3ef513313e6ab6e27f93d50ac5cc09337b32",
"161f142c47cb7eb67c19db4777503eab54516e99",
"c2ac77ad446c9384b2dfb1999026c872065b30e3",
"2f822e65bcb7dbe46e25bfe2203d0f5c7a36eb5b",
"5fed0db5b35a1f7c34e9330d46c496c31429a58c",
"813659a5a270a428a60ce440dd1ac91eb6e4c2de",
"70eb67afbfd504a69dde512c9e5a1feb8cd82d58",
"4aa0b775beadd3ffb2ffaed81d3c8b78c973451c",
"2345db1be112ced88aecb89edd0c13b470823d06",
"b444a1acfec322313890e1539ceb5fbb2812cc0f",
"2c7a7178c5c0951e81c73eddbc77658d807bc4eb",
"0d6fdab1b3225a9863fb5adaed55f91a25ca2bb6",
"0d9233401e3036ace08e5e3f6b026240f47321d7",
"c66507c121a3b040f9702d05b212db57540fe7d5",
"35dbb52a9aad1ae5b790097f8f57023bccbb3135",
"702cb248f899d69e73090d8b95da98740c68614e",
"e7f4d3b5e4be9fb8acb8aad663c81a2fa23a4469",
"bbb975b83eb5e8b310d806830b14352bda689cca",
"1d309c18b7bfe6c799057cdd3e4eae2ae699b7d7",
"075af68007eff267663a76cc7f3f5751ffda4a6e",
"d2a51933a8204031b09d55cf603cb42ad6a56320",
"de74275322a3c9b77eb8474f4007a7977a796195",
"d2bc67ced0058ff2b2544e83fcfaf99714da709a",
"6ab8b05af63f45740e649febf4d4fee931a916da",
"37e7b8857a16e83a6781d13f7f8e5023d02754f8",
"94a05341ee3357fa2004ff4a6818830be71df72d",
"249fddcbece6a1717af6e8a259501167b3af0d6b",
"55a5048aebb27493b9aeb25f72344a7906c280aa",
"0626d388838b027a70033d3aab302610275ca61d",
"0a20dd3f27fc248c84a0cbcf9b41ba56792fcf69",
"87683586553f5e8101ea1ef9982dcefb937c0360",
"2b3cf128680b35a96f63419951ea8b31e4953abd",
"07af43ac6b52722176c7b5886dfe014d76e670bd",
"8cf88ee502cf26f22e6cf476a61d20fd13795f59",
"337170e038d54b0c13a7beeb13e8572c0d7bc69b",
"c0939b38355b10b56960a6b1716ae3fd470ff964",
"740671f119292e9e74b6e1c9700f4fa30edf92d2",
"7f7a60c0da9e07cc7458a29502e72cdddf6d7078",
"442b0169a5851dab9ef53532a9617658f1e005cb",
"94cedafebf415e6b6aeef800d5e6b45cd14d500e",
"49b7edc7f99ee2ee04bbb6ce41e8a0e26df44ed7",
"cba020a4f39a55473d55eabde24442989159caa3",
"dd4990983a94bd4cba1a0e2fd80673ce6e2247e5",
"86ece2fe21e84a716929a2850037f632bd0480da",
"298c43fc58f717b5bbaf697010e6f5e45a7e9e45",
"9d23086e3865795785225c81bad5c28128b0fd1b",
"60794f2a2229e0fe80cf4b950eb02200a7fcc166",
"2e74ca4aa8cbbe6763369658d0ea185908a3eded",
"b1108a815ec0e675ec941c1662efb7f2d2f3e8d3",
"8ae46c94cf7c8becba81c85e146daa9f449ae6e6",
"308024942a2f06a4b05946b45cb1f6d004143911",
"947e12a61892cf535c5c385a12e24f6d616cca79",
"962e8d2bd4105dabbcf58072f7b7d650309e63a1",
"1e0f3f8089367566f85b097043c451f861055bb9",
"1f66961e620819cb4b70df617e13072b1cecb5f9",
"10137694519d7624939c1bc1a1493070e1c0e55f",
"615ec957773c24f402c22171fa284acfaf690b56",
"13a5053e4d6d31e7886939f35a6c5979cbd7297b",
"f698066b49be05f2e838a792c54de8e644ded319",
"15d21adaaa6eed03aed226b72759a5981719c998",
"9956783044546e461e12f9d51231c2966cf5fe1a",
"08e002cb3431bcab0c084d2b37498baecf6a1234",
"52f98d0b67d08c7ba3d9438a062fa7e29b4b74fa",
"8b0ec78eacab4f113ec65f413fccb4147ea6b2e6",
"167a67a6ee90b11a79d5ee9a565c144aab97dbc4",
"e86c81f3c7f4552c05ce3655d5385a9b022362dd",
"7acb079275a28d21b4139e13ca23d9bce6f700e6",
"f89789bbf250a4d2f8c51e511dffc7cb2656fcf6",
"9e7117730c141a2311fe2b545c18d435ab189afc",
"eaf406afc7fe38775df05a5f4b6fdd05fa6f99a8",
"e72c624746ec7fb17648d81d3dfe5ca1c933bf77",
"528e9e841af6583c4e131112e7b302d6a3787908",
"eee1a5c6c5c0621f339d3fc5c230d4a695044d57",
"4c683ed47b499e9e69fbd92460a2ba07b7aa8170",
"bc378d6032ed13f6a28614631a5b8cdfa224fe32",
"e9778ddaf03d4729dceeb5c4c4e6467ab653fcfc",
"be5df05230969e05267816965d71bd9cfb42e610",
"0668e2dae759265c6dbf359c5bc4c8f4dbf3f64c",
"ab687cd9ef09edd893afd813fb9ff1af89d0815e",
"1fd63dbfc4eabf3b58384ac69f744f7ead6b40f0",
"8fa32d36889b026827102dd31082e509da528b54",
"1c1c5b55caf7fd848525e92c4d9e80902f6bf024",
"ca5b385f268e9cebeae81a55fd777796e65c23a7",
"4fbc612dc6073eda198c4dbd1974b7ca6cdb5128",
"4cc6c3b234de7a8eb303cda4130d0db8d8cb702a",
"b58a3a29f9921091af98068ee81b226775c6aecb",
"45d3832ffa1ad428a27f29b739c02d564581cb62",
"3a57e07880aeb24f32382b5b1b6163b7cd420f12",
"ffa11d986c3a647982ac6f3dcd541d2789d94368",
"d23f646ce65d8128d9e5f865ab0512f2240a86de",
"17c8b9504dca4d5edc7a4337406ee57862099aa8",
"ed14db778b02c21eb7cfcc517c0820cabb601c4a",
"0dffeab9f485dab58ea8cd2ee8082a6d1f22a4a7",
"44195f5ed2a3ee4616ce290781317e6235785d83",
"16dd2388c6bf8b2eb2d1d36391702efb77c8f80b",
"7a935a5a355c8c0e8355641060ffa0e995f70da1",
"f1aae47f2143fade609515615f7de8020cf6eb7f",
"d8ce208dac543d196f7ccb22a08451894c466b66",
"9b8f0f03e40c9eb23565316b1945a4836f6c7e92",
"d76ba382925e432da254ee89fbe02bf8414ad62d",
"0e34f330d180a83f091861c5c32450bca8b14978",
"e2668c05ba5844161a3509d1090be8d4d7920a1f",
"d2911fe0331202b8ff4427ae8b17fa79033bb070",
"36c2787fbff19ab962006910a9b100154ee5c591",
"537829a53c9409021cf63ae9c1bbac4d85dc2d1d",
"5e4cf4115e1b38d6097c7c81bbe176e8c48585f3",
"2f028493fdaa2e937d6e1e2a0530c51bd4c14817",
"272e519cdf81aaf66856cccf83edf34519cc0f6c",
"6b830036dc3ba3835802c8a02a17a385313df383",
"a5ebb6178bd2c1b28530d1d64a37990294fa7888",
"56e35b7b6834648d1e1e52c94dcf21e5c61079b6",
"24a10aaf78918b3c0b1b0649ff1fcd137544209c",
"066647c206188e877780c36f297ba4448037a022",
"bb5746029af103ab28f6d44657b1f1d44a46ad73",
"3d806374c51efa30cb3c992cb58080e14a1d9efd",
"b016d3e149ea6f469f8b5831c01db8d682562b19",
"13ebda787027d6fa4e89eca0c88e0bd4ca0a1900",
"77319c6c395763df57204593c3f035aad09b9aec",
"520b6cf9766bba7e86c08d611279a3f1f094e2f8",
"6573f4088c3e040bd100c72f60da83eb8c1d4582",
"543f0133c614135b4f814aa063f90c3d0d4358ed",
"2e6633c47c2e5ff568776b976f5337c10ddad648",
"4651e18600b202551ec0b9615282153912be7671",
"c52e1a08991842d862ef87b4b10fce295d8ed34f",
"3f4843c11dd68d4b905ae8d7af7851e66413819a",
"f051ff69c729a4ac9c3c71c592b6d8657e2ec104",
"143bc5e173dfdce2fdf2a1abeb87008b4a5c72e2",
"febadfcc7d0f9cdc77dd36b093966d5f7151a99a",
"875b2cc418a001287eeba7e8f373f57d0436612f",
"ebd686cfd6d99cb62a8d347de17aad8ce8173b23",
"60ddbe28f0a33a2eb513812a55936629fff6e073",
"2f2dc390eb2656f009733b68bc1e32b8c45ee407",
"0b9b3125984c8340ce185b2f696e67feced237f9",
"649eca99040421e16f93a0e3e3eb1949c5a444b9",
"1e7d92709903d4ac98ce64f7027516e16d4860d7",
"eaf1643cf4a760dfd6f9d143761ac10c644a98c7",
"636b7cef14470255ae13cf7fa17cfecc11798885",
"40df0724dc11f7ff38bf955033c88d6f7403ab8e",
"8278618720fa9221d93a50fb65fb20712b775604",
"17993622783b44e2f3dd46405362db95023823f4",
"6b1f04204066df4575a9c65935e68822be7c8760",
"f7fd7bb554449f875cd1a6fcbc00b04cefa680c7",
"e85c70181e14b2126f1a34c8669b6e3243ede738",
"316d8bdfd231bc57fe23b5ad538b0eff564f189b",
"dfb9e8ed0b074b9ce27346ee8ad3b9c8f61e4925",
"4e951d8c62bd05cc783b6d3ef23f6995f8a48bff",
"1177122afe6813043586d5d3ed8b5d58257f1aee",
"390f4ce5bb27055fa29f5b1efaffa3b2fa09be6e",
"b27e0a7311776767866c06982754b1b0faa2f3f6",
"33b667f338eb68abbe9a4683d8ee8f62b2fd8f33",
"d5fa583cda52fea4948b574dcca7670cd61acb30",
"386342292e242587eb3abc3c05c079fba123701d",
"19aebc17f9e8f771cb78af5623f8c53e53e05fa9",
"6fc2d093e63749df62c9a5387d81478193b67465",
"50893b3d2e71a0cf51381cca12e01bdb2f9abc6d",
"9da0daa587b343c64d31972b57845154ade65459",
"c05596b3ce0fde927d645edf8ba98bff8bd3ea7d",
"52e9d277d0de22c30cba41bbf71b1118df1cf7fa",
"eafc04f80e84b5b96427fe3dd79949788cc4a435",
"a8c4e64acfff0c686076d08fcc0aade8efd872a7",
"6375bc1aad62a94e61f72e897bf405e48874e992",
"5dcc2c8730a47b6373fa647459c84f9062d6afef",
"a8c0b098a0adfedbeb8ac573e411cec65856242d",
"52dfd0cf53f66346002a7990d9f2c7bcc58bd190",
"fbc558b6f1ce6ad6d786dc3094d5091330203111",
"d41fd883c3f665b855ca783c795e6f892e4c6ccc",
"e07e5407d3abb184d12581110b636ea28f1b8f19",
"d214e5f4e16d9fbc80415d1e067b8691ae3e2259",
"3b2fb288bd92548fb7e69078d48b5e29d8d24fc7",
"bda61ffa2a81c7fafd7b2e224786241c67f5fbca",
"6da8c4ef1d4897ba9c7c5e2263588630cc067611",
"e4194c8f9e4c1b42b89ef467dc9daa6d8de535fb",
"9607a735f80279abacdcefa5a58a2653ebce9bf8",
"4b162a600accbc09966328d75cc05bfe22d1d8c6",
"06ed95771c5a6d184a79d447ba3912683f9432d5",
"3521d71d721d001d40908327de8b35934d8e2679",
"722435b2e65b683e9c02493e098fb9b20d80fe8f",
"6d80e39491d5a731b37e1ab632d2ef275ca2b178",
"6d8971c4700c732ba7014aef61d23327d52fd42a",
"88d55b1bfdac1226caf55e6f4990a01025321b9d",
"059a53dcda70c17580083a578ae5feb28b945d73",
"4fb641613347238bc6e99514457331a116f56c97",
"58e6c836201cd133bbd0ba5f9741eab745926628",
"77925fdbe03084b8b81b7ff80c878b3fdc81edf7",
"1ded09d26c59327be2070267d0ffde02178f3e1d",
"3d69d2273518cde0bb2114026573f655e247d9cc",
"e1b060e1b6a992e8f8f71a2130ff69507df34c32",
"8d1c4593ddc39aabcc0f91bbb02fb87f636f5fb1",
"193690ebc09b0ec468c8640b0020ecf6bf36ab11",
"7ff12ab1444b058cf131d427f9bf2d4d474b7f7d",
"77a3f065db99b831b08ce03b16fb7ffee5cd1009",
"bc9244fc1e17fbc11b9bf1412be9f7bfdfca2f22",
"b7f029e8628924d39c0e3bfc7acc72624d394d34",
"f8a98902f2324607156b0fb15a4aa57a1f481f2f",
"c24038a5c85faa910f2afcf53deea11faf19b286",
"974b80c0c94b8cbd56c69489cd64ff11d0deed51",
"0cfc3e14efcf74271027450d0baa618194711947",
"8a4a38d4c3a12c5c6b8d6aa580822fc0e35d8449",
"b438cfad1a4613c8f0f7856d260f2d58c02a77cc",
"242d1b14db889828a735870e04bfd6a29c0f4017",
"a2a07f0d94cea3f3961f67592f13d8fe7bb657ac",
"4858ff64e32d3984a412df39011bc9d58d97ee3a",
"5047cdcf5deb0a45b6c49928c888f3e2614c9752",
"4858ff64e32d3984a412df39011bc9d58d97ee3a",
"5047cdcf5deb0a45b6c49928c888f3e2614c9752",
"4858ff64e32d3984a412df39011bc9d58d97ee3a",
"5047cdcf5deb0a45b6c49928c888f3e2614c9752",
"4858ff64e32d3984a412df39011bc9d58d97ee3a",
"5047cdcf5deb0a45b6c49928c888f3e2614c9752",
"4858ff64e32d3984a412df39011bc9d58d97ee3a",
"5d567d64a7ccff9e3cc61bdf4c2c544953495489",
"22fbe3e9db63f2d51f13cc3aaa4b9e963c0b221a",
"4dbca5ca80b4e526f6b8a79af93c8e92ee4f1b62",
"8e8af081f24a31efb607ed9838fef69ba2b378f3",
"98dd6e793eb47482ed16f35b151271855f113944",
"e258ccdfb9b78a7f3e80be106d05719fdafbd93f",
"18160d5222eaa6a60286eed3a7bb7757c4dd7f05",
"ea4989b96d428a84fae6b32000d8431f5c394ea7",
"7e6b3a5a66f22a44f5d51ba287920d8da6076d39",
"bdeafc9bfd69a25ccc1ae4bc2df0b43a26d1b7f4",
"bcb5a7c27bb78795da75c0d365c8dbb2319ad55b",
"a1218ad5109dc861b6d2e22c1d3a80437b45bfa9",
"b10acb5c99cecfa305cd757595b5b3a3dd1b5093",
"171fef52ecdc761f2680fefd1270228e0d480d99",
"fc8fcd942607782ecd7c07f02ca6a69ceb53b414",
"fc793d9cc21fb05663362180bd528378d697b473",
"284b4b0f9219e850d1c52d6bd140b6e335da0e2b",
"4b62e1ee460548250d32f4c7e31e3737fd21963d",
"0bdf9efb8e88ef6c90a735acd1224e0e2686ccb4",
"13d837742b819603729198fee6417a3352ad5230",
"5ff0739a3ef2dbf1c17990164cfb8b2362c216b9",
"ee0f05063dd76542eaf51b20de75a6d085003cfd",
"587b584e239419f9e96b582d38968bfd87186199",
"cf983f6cd047bb9febffcb6b88af92f5bcac21bc",
"53588952694a2c19d03ac6f9dbedfb8fce6c3ba2",
"56870297de9be5b2f49c9a0ea20e70d3811a5b01",
"4f2774a0b8b6edd0d1379a016136364400840a59",
"144c42c9abb37b6e7615f72cc3c8a4180714656e",
"db296b301158a6c2a61bfbecfd9c9781f897f79e",
"3d6e488cb8743c1a0ce185563fa6f8a9f8168d92",
"aedb309419c622c0f78d9f96f994cc0a21314119",
"256a237cf3d526fa982494802b16604dbb6ca183",
"37be349f0760c0b583017663a6adb887b74849d3",
"d886a4b119fd49ca2dcfc571a7a80e5146fc9504",
"7da8c45641878b12ed52833d34a8966166a92e34",
"ca138ea05b0f7ee067435818479698708240be5e",
"ff1adbd9694acea73e141ee706ed11bb32c42786",
"49cd19423a6bd387b85cae0aab80336eef4b06ea",
"ea0bb1a870657c099cbfcd9825f243dcc34f3a41",
"806e0c16b4c673091142c97103a5d9c90f011d86",
"179b031d0ad8901e94c412c7c4e773884c3feae7",
"2806cd160a628027e64cdbdb028cfb6b85e67835",
"584d5fdaee2ec5637652c4496aa00c0d44980988",
"4cf0a0bd341911b9bee466c0172aaaf01266dc9b",
"f70164d701b12a21fa093740e34177dcbcd57f81",
"9c8d1a1a3636f01b1f9e8d611b4637683b2bc802",
"6523f1ca2c2b308d39e3eb8dcfc6859abbe8aead",
"266830dd82e75e96eca98ae9e90207fb6d7fcb89",
"bc6f47d0591982caaf5719d4b9c778a187d931f4",
"6ad107e28b4174165787ea70a778040083e031e1",
"5928f1396036a067b2ed2a5a17da58130361ed47",
"3394b971b05e8c873afa591185d168f93469ab29",
"b65ec441dbb6926e0cc56fc9ea05eb395e16d46b",
"584ce6de0f30f4c652b06193c2e944d30674f222",
"f1ef87d9ee6200bac89ef4ce0c64fc7480491604",
"28dd0c25acda06a1ddfab7ecd83b769d91cffc92",
"07ab7259655ba1ec4f28e5572fc575e3250b0aeb",
"a219b5b39fdd46837a4b13a1159e15cb15a43a8b",
"c7c5649abe99ffe168c33c6fb999f1bd1c716305",
"28b3b71b2a4ccaa6b2647387e4a4415a57e7eda9",
"6c89f907e001cef926ce7602c2c9ed939043cd33",
"bb9663757b6da7829af510dd40ac1b43a16c9f9b",
"56bbd9b26e24fbbba97e358aa55d0c09f8d39581",
"9b32b9f040329315d70859e4c65a0513a0afd74e",
"22d812338e20bc94ae3569b1921c966d73989718",
"bb3128100f82957056a08c1199e65307f13c1cd5",
"c6584ca98d90ce32a48682ecf1853bf7d32a953c",
"8ac7552e1796e927c7db3a0dc977d865d7c3b88b",
"b831fd27131c9069dc4cb27fdf42b4e35d244e25",
"719540544521d4d1eb7e782b726385cb67bf7cbb",
"51bce00d718c1651c3a9b57ca397092f9605e366",
"d4e56a639d6ccd236b11fd56618f6bc2689041ce",
"62f4b77ea26a9a2febd84e4f2a09cf4db6bbc5b6",
"b7dd8918fed3ebacc6f45c7d11d581b2484ed76d",
"8bf7471099ff1d998ac90ae69bd599fe91acfaea",
"0eebebcae17b46bd3a0ca6b5e26744c852f2c8eb",
"ee81deda7b68dc09da63ef40224ef4b159ad9cd2",
"c11ba4ca3d873e141eabc14fecc4c34bc6393538",
"571454723faceec29bd44c7e29bcbacfc28e4444",
"3f03d985c9a514f323941bbe990cbb5efcc5613a",
"9bc7bbacaf495acce7b4753198fd4c9985377933",
"1a2532a1e2b0ad71648fcca96002ac84a386038d",
"0cf23ebf172d7af34ceefc2c0a781946bab9ea76",
"6c526dfb39ef332d0b466151a991ecc3077dae7c",
"30349a3279cf64658a4d8637623df39bdbf2d463",
"1d9752923a9b02e25ef40a72d73fe9f4c02033f7",
"bafced27f3e8cd091d7d14c3018479a8759b3be5",
"20efdf7e76880b2fda3a68f7ff5df6acda204ac1",
"e0dcea3e7a3897f1e35e5a5955c725eb25563e64",
"42dd3a9b519d9ce72095f54e946f5ef54395a5ad",
"14a28ea64f6e95a954bff0a46378b3a9b92319aa",
"a711a717ecd40989bfdb534eb38a61d2d95f860e",
"0b9d71d739f53390d2e1eee2b3b1751df4533123",
"1cb29ca516cfa442dfceb28ce3ecbe2a48365789",
"a2be234f1c59aa0429052a30676bc4aad6a6ee38",
"c3abd64fd584b9552caee556701cc443d4899e33",
"cbcc0c300d2ef780ab9b784faa9e1c7f37dd65c1",
"23b388a35016825096fb2e3b3eac5927a4714402",
"2ea3118baa0c4a2b50d99ed62ade3467d86e5e9a",
"2186757cd8888b78ab6a2aca57b2df9b9230c5c0",
"f9ccb55f32e13ec97d5d333a3e589a82d8962cfa",
"9c065d9e4bce449fbc41a35c60a7c15749d2a4a0",
"a179d772202b91e9cb40107b59759e931830d522",
"a8423ab01572f0a1e6fb36bc56835210bcb162d6",
"dd689bc2ada5ad116ced5de860791f4a9130e8be",
"bed1eb5c8d5223f03d36ba213da8b54c2efca3ff",
"5b510d76399f1547a3103332c3908c494904b19f",
"d2b91b567f01469d7e70c16100828253932a5270",
"9171c0011e718e01b57f386a53be60ec1b43c8a2",
"4e114754b4a5f53ce981cccc3e0afbaccdfbf889",
"b224543289b33330f1ff1e25852bbdf01cfa71ba",
"854e9bb510cf89bc60db487fbacd097f9047bfce",
"5eebb0f46f0787c0571effdb5e0a4f2a1dc6dcdf",
"81a64468f11b4617f49dc66aabb5d58cf448ada5",
"f46d4931214df1c6029e1dcce771c4b032be9671",
"cb59b311cf58c07df696c7f0be01bed610235c72",
"055977e2e209d890952c4ea30efe1b51008b2bfe",
"5efa017cc33b079696ef31bf59d19a29f35b7dde",
"74e084e2136c334ea35170b6fa9cbf750d62aaf3",
"a15138d123bb80b2e87e8f6504d668ea2ad810cb",
"b2e3fe866d4323828d50e6b949a1e40c6b029704",
"28e98878c12f63c9a4d4e9e819b40facd22cdb31",
"387359a8117b2c112e0053f615e58342ab289a69",
"50ade037fe711270115434e69579ed314ffa41e8",
"8ac5bb843ab0f941a94c08e99858a8888036ed0b",
"886b84ae65a80e0a548c603cb4e827d585dd7360",
"c30b052ab953080998fbe1ade0b5511eef03bfac",
"a403ad8e9157359c55ba696a8da07c11cc1aff3b",
"ab110a1d87a0c8c63b04528bd1f3102186d666b2",
"8b25888fc26dc8160801880aea434f94e1e75def",
"cc4a5e86b37950d4029975dd44ade776fda17f12",
"db032a61c30b10a6bc12904ccd7847b8fd87bae4",
"d5773c7dfffc23dcc3ef7f81d45629beaebff98c",
"a85ee09ece501ff18805e60dc9b3cfb979cde69a",
"a11d72264cd2391978dcf8a7761ab35b18d433f7",
"6877f4d0143fbaa4ba29b2c10afd850f3dca1c2b",
"fffa17d8dcf92892b274ac84aa9b93e08e913ab7",
"ee1faeec7113f7df880600efa0e8ce0b4c6c41d8",
"c1ecdeeb119590ea6a887bcc626da7f2b45e1cde",
"e168a6cf9005c6168182a72b994de8371ee1aaa9",
"b4906f2634b726fa39ac6a8370e4aeb5469bf022",
"54fa76ac505e5911d9199facb70167ba1ab6cef4",
"66c8ff54fafb74af4e624597471a19ab40d701bc",
"32d4aba620deaf88b5a651b865faa08c5c22bc5f",
"56bd8a0cc3e74d0e4f43580ed6ca00ea63196cd3",
"462a023e17369aa1c0c860baddf907b1805b3b1b",
"9c84a9f5e370a7cee5eb74a3f47416850f6f83d3",
"fefd23259f107623ab59ccea54e809f183e5d69e",
"6833325f50c90efab83fa79b171f05778ee11229",
"056e308d295043e416b256ce7ad80acdd7a323e0",
"93f7966b35e3968095eb85f452b3b9fe928a80c9",
"eed77254ed2b919148308a895974f0d9051208eb",
"f4e9a715fb8b81d8f93bf894052bbbca315fbdd5",
"abe46d925e0282a52a132685a33c1538e9c20ca2",
"2f987a857ef97780dc747a02703ea4d459d4f4dc",
"f6b592829c90972ab00650e4a9744dfd5c8d90b7",
"099f881e8dd015b564cc0a38473c63277b1fe951",
"78c917f00312f1289badffc3187eac527ceab229",
"d204492e932406a3547d603129e25cd642e5a489",
"931b42793d0dc4d8176cb0ff44521a15e9e3e027",
"5aac870c4a4327908727599bd7b58aa84903aab3",
"d1af63027aec09517fab8898c5eb54cf4f12e5ac",
"8ecc16bc325f90af5f4a212f1d7cd965897eccd8",
"d33d6be1fd4890568d6b31ef3892926234b2924c",
"ae5bddbc04873c16e494f0508426f2ad60b4ea76",
"63eba34084462bfebf69e15127ef81fa9227e179",
"773c70107557ca34586a19137a8b47385081a61f",
"a698301111a241456e0e58595e932ce6ce42ddbf",
"d44ca598ceec492e0a5b4c7d1aebe578f114db4f",
"8746a3ffbd5f24b9eafd9e68bbace874d83ab011",
"aa30339fc7ef22145ed0b38ea9d23d10958f1388",
"4298ead76c715b770925a98dec3b37726f4299ce",
"d17e41501120a74814370e8c68a7a551c7bb3d01",
"d2fb281f28cf5759f8d4ea31d57c95afe6eea929",
"627afe5416282509e526abcaa5446b7031100a54",
"16c901f34db0695b72d23156d2f4fc99e4bc0407",
"788db900eb1bd8e94a56afce731b04ae48678d5e",
"be84f525a95fb18045505670e60c13be63ee9160",
"9981374b295db3111dcac288b56c046b1c87e56e",
"914dc72ac3fb60af275c3867106087b4ee292cc1",
"bf6bd838fb26fc628a5c6873f41694c8ff9f1655",
"50387e65004e00461b25c2d5f35a5cf8d84351c8",
"9d4f927c77de0334f179017a77b33ed711bfc55b",
"d26237e12978ed3d20d44be733513bfb8ac6cea9",
"37146b8f1c5cc58661be134da80ac25f1b8188e0",
"51e4ddb98e279979fb4a8512be77ebf0165c4db0",
"0d85b660731f103c2f73545566cc90a4dd0646f4",
"8ed47f51576cb578b427e7c64dc00dffd77ba9dd",
"20331dfdaf776baa58f6ecedd5594dd1fab3719f",
"43a100214d387c7b5f38647979b1a3a044654f30",
"aaa0ed7ba7850449447511f23c32c0750a419d1d",
"ace9505fe1fce2257ede1fb5c2c826eda9a18c89",
"e8db5f7ff7fc438e1cef2b058cafc9d74bcc9f30",
"78a1813070ad6561195599ae97ae84bc1d1cb2f8",
"f8eeb426d2284f2e77429015394c226ba14c48e1",
"8b6038dc7d56ee1b3f960449aa67bfe345631215",
"00bc692ce87a48980561d06b126038a7a9447a4b",
"69a464b72a326fd776351c315f898f29c2bb6e04",
"62263f6b50ea8318e8ee8f4daa0e65c070194daa",
"f8e0d357c968b34e51e0483ee4dc3b80263f08a2",
"7cd66874045a1f2a6dffaab4f2878e53e1c47c60",
"263f4f6637b54a1076af1115cbeb79c75285994a",
"6c8d3279b8c25dfceaa9cfcb3f4417222c7f55e9",
"866f409add01de990a84e9af27792cb30b9464be",
"137ec93f740bb666ca88432d22fb4f675f513626",
"01d8c7866fe2575615a362d523fd2ec6caeadc0e",
"e7d8b71f7c282e863e4512b3f7af157832b0b2f2",
"204627841e4938029f1963ae5c8503bd480b7a86",
"40ae368a89be7d892c4ffd53d2ec064d447d5c92",
"32a611aff271c72505707513699b65648674e0b8",
"4275ca56bbbd8163bec27ed939b5e5c0e58557a8",
"f5a91acfdeab94429b906998475807db03458b56",
"2a82a5143354001444693b174f5f645322baaae8",
"600c7564f23947faf2b299367b7b93c3120c6542",
"cd94e873c1d07c3575c03471017828a69782179a",
"b5c77480e65387eec18e83e39941c424139d7644",
"5505347b447969a9f49a9c50f5138ef8ad1df322",
"9d55a1a9880e38701750477a0cbfe1aafa68f10b",
"f18265cac7dd828e8ddbadad2aaffa4391a7239d",
"71c801fa830abadd74d309540810b81027ca745d",
"04c24c2629c91b9ca74b4e8ca7c6e6e118f9af35",
"2b636a35d7a684ec2e7777290ce1bac40a0b45a4",
"5511abcbd94816065dd63df1ac63f93bbb6673da",
"f65658276583e8e7faa8046acfe665dfb72dd798",
"9fb17993fd026365464161d5c17c89d2ee3d2883",
"292ace104b0ce89b53cdd20381db73ec683d0914",
"3755d16c9691f90c8c46637b489c10c2ec5570c2",
"2678a140f1247190af428969cc45dee06e6c676b",
"59b7684c8226597077f6d09353a6f5a83b674395",
"7b511c64086e62a7c40ed798d2473d0f16d4771c",
"fa63a88d48d383ab6d60f8cf3545b6bd80828750",
"b2b65f4c9420998955213bdf24d692de0809e75c",
"dd4cda94306374caeb7c8ed9816c1ced429dcd07",
"611f3adc326a38cb44c0ab07df70d5379668d6e8",
"102ad52c017a255c91e85ab3bba4f82464c6812c",
"a27dce424cc9f3e2194aed5842d2bc848f586219",
"9648ede9b9f5b3de769f759c7d60d19975040c5b",
"0ade92515cd8375ab7a5f93281d65f1ceb618f0f",
"6cc47abcea66efe7a58ceda64e327fa926b041e3",
"275804ede5a7a496dd75dde041053d5962f2ddfa",
"6fe27a9dd3e37193e886c4535b816ccaba0d3c8a",
"d7dfec0d13203eb1078beb880fd826bc04d9b3b3",
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
