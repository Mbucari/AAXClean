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
		public override string SingleM4bHash => "110bd172e56a7a07cd0d813bc54223c2b7cba2ae";
		public override List<string> MultiM4bHashes => new()
		{
			"34d6acd517be2ef8657bf25715eb39f5ef1043da",
			"ffd2ef009f7854188dc8aa3f9c9e41ae6dfc875c",
			"9c83808a0d72d47a567a984701e21f0482af13d5",
			"0054ecc0cd9013c30dd7154d45bf61872355c555",
			"eab8f16e4000085cdfac89112a313a6629b8e92f",
			"bdd983ec82da7cbb7db25b5edad47dd0404490c2",
			"f667ac4a5035ae54e1aa4c74f84eb70ba04bfd65",
			"4405392ff2bfd43d9fc28d7b03a424ad805664ed",
			"104cea82d9fbc7fa8c4db60bf6d2ca8407609a79",
			"108ca82b6752ccfa2ade3a5237dd59b17446f389",
			"730b3e03a4b6e6df5250c1ee3a1b2b5a18668d9d",
			"840502aa9617005d8ebf79322b4e27b9f09b6f97",
			"d871000552dcf32afe8933cc11223ab570c91d68",
			"2fa1825439b5dd73e94117bf86557344d4d4ba16",
			"3bce8c22dec7c339ac1887d4e7372b74d51f6045",
			"7f2c0f556f13147f871c51c8c21e0253583c0a1a",
			"821e89203a0a87cdad65f88d2f50579f4aa6bcb9",
			"770612926388d3e4c64643d672fe093cf2bc3206",
			"9b8cf4bd1ad31d06e705d08407a35a0f7b397c11",
			"f11bed861774ce974caee053b04bda5d692fa811",
			"e27a2cc4a986c4de9b8acbc8a96f432279da0b1f",
			"ea8c34b7ff485a350005a02349829ab546031094",
			"9fb539c87d76715d63b81d9949faf0c30382b68f",
			"1b84b17a307405586ee2d164932f59621f6eb856",
			"99c8a9833fc72276b5a137c9809d2a13a6519a6b",
			"09b50e498e9b62deca36da8fcdb4917b18e5b5f9",
			"9e86b6c76543afa4e8a1af8823539f9ef5b0fa4f",
			"163f8ddb1c8a7d95529775c45c5880407dafe603",
			"3d2e261b52e30615b195189fa6fa7a3a550fc737",
			"d5489f41efd73e14cdcf2b26c6cc33fccf2b0a26",
			"f47236ac46af388700e1dcf87e008f0915e29f35",
			"44d01e053ed5dbd8dde09c963298eb922a608de8",
			"70a05f828384014b83f3ad16eb76362fa90582bd",
			"2bb29250d4cf60cfbe3fc5477089a4b8b78a0e31",
			"7df3c2f955e2f6dbd7052609c8c1896620116921",
			"14e3dc7cd9143db0907f940375e5409fc0735d69",
			"8aa571363e825b75da31db54943fa8f13ca911c6",
			"3e7c51ff6a9430ba74b20f0f85a5694f19f0c05c",
			"475e87d53c3f9e2f2fb8e04d4d8c7ff98c6aebe7",
			"348d7ca86c286192b6fe2c7147895cca9e0fa6dd",
			"5c144dbc9bcb5d2eeea1bf9fa8a95ac3897b3140",
			"7841a4ebdabf5ebc87f3a188085a4ae7273bbeb0",
			"357fa8f612c6e02d33dcb0f9df3ed0cb29a6bd61",
			"cba4ce57b93eede81dae99a0446665d5a7ef420a",
			"01b77f34ef9f0f6768ad7578a4306eb359a0197b",
			"7240ddb586c1255a85b866042f3150779fb4c179",
			"0c3138240263915e8a20c6b86f37046e07d87357",
			"f840165c09a61e85e29430eccfec568c920f520c",
			"1c968f421f4ab9c341e8da503dd087282bf0364b",
			"2e6ad659ec98e9f213f94345840a0299c04883e4",
			"b3c221a0ec627ab4227e4fb67b5e8f24b6d9b63e",
			"6f0539b61f57021fa8036e227d3807b54a3748cc",
			"e69709d6b8ef9d670b0909fcbc6ee1a93455fddb",
			"44b3f588c29c195045dd88fb5aa874a70181449b",
			"41c8fd84f1a3c8e58e982fef5be38ed34cd953f4",
			"0d8a7702350921c0d36679b9365957776f53bee4",
			"56009638abbbd5d2062e5e634551223402ff2c6d",
			"958a3d7ace10b6cd825172895938ea7045895f01",
			"aa5c7f97d89572d7e39bb281c4ed97cca02c0816",
			"add5e16225bcac2e185b87c7e09709ff2bbdd28a",
			"58d98bf83b2a5aa1a939e41a88d8bdd388928050",
			"b3fc9251162a60ad22f1e1389ef8c22680c7c9ea",
			"864a3e7d03a7177cb695db2752e5ad294c936987",
			"b8206090a56796fd1b1c9b7cb8ef0793c6afcbd6",
			"4d44132d6005e4de527b8e7affe5d916dba3a425",
			"c0ad9d9d62530c4a994d12d4d57c4c0f7fe36649",
			"e9992cc66dca9d034baa0e52d3085bf398c72fbc",
			"6ad1011e09bf5c583606596301bfac2305213b19",
			"715827ee6c020001588fd83a3431206748cb63f0",
			"1224cc18663a3eb831c0b8b66f1156906b51e7a4",
			"04ac757e31ad783ac2ef0ca244b1cecd49f9a36d",
			"e1207292978294f84e8b24d000eb0ad36bfe2b3e",
			"ec49343431cc54c5a830a35704bce44227f84800",
			"2afadba79bd5d62cfee4231b0e98765d2ad01e76",
			"64e2e4ec218e9cd4a69652c125f4d22438a1a67d",
			"c0428dd5a6c68c34d44ec4300287fa0c212062cf",
			"c3447d635e33c44ca449ff8a99a160b138dc160d",
			"b7551a775eca3dcb22b038ab8291e417a67797aa",
			"97127e5c6c1bd31140d002d80d8d82e83a736d90",
			"163b32021e25c17b18cec121edc7a6deadd81b7b",
			"e177098f24ef535f74bb7528a742943e441a5c8a",
			"852acc0eadd614237bc82b7c10eb1cac7edf0d7b",
			"0983990a5aff34f9f36fbaf0127d5c0fae9c48a5",
			"a38e93ace6b7699a8d399d5b38e44f7b0301a074",
			"b8d3581d8304ee9a180f6dc5ee6998eaf29262c2",
			"efa7dcfc307300aeeef61fa9f8f53ea2029c4ddd",
			"b9823fc3a9941bc667a5bf89447cc6be238d27cf",
			"953257262147e5eb9a38d4bcd10e6c8b5073b1f0",
			"b789c2d92bf82b870c26ac66b7f0b4a9b3d58583",
			"8b17137fe5203cb4189e03e8cc8c10101c93ce2f",
			"27c19eb5f28a9e6cc10859df938af4b1ad25e800",
			"e9413f13724cba5d87c5fb2a42ecd1054de5b895",
			"dd137e9cc8e8b70e72ee9f36d4627751aa23a638",
			"4956b373d54d3e027e1ad0b2808fce5fe605a0d1",
			"74ac4eaf19136a6e5a6fcababc620452e403691d",
			"5fad7de286324d1607782692b16c7741ce6b5b95",
			"02e36816304b81f66c8cfbda68944a2831650ff9",
			"54dc8f447aa9774ef3587590d99faa9984bd4774",
			"fcdb31ee8ce5d28dc3f3e49ba0bbdd35ddf82434",
			"52d1a4f3057bad1b6536419e8a7b31df8ea6aa58",
			"ce73ee11b2812726513a0b99e54e89de20a587d6",
			"4775beef696c44e2cdb94a58e04a0e12c8c179fe",
			"e128f56b51c6a8794852ec98c379639e9bb5db49",
			"d1e1176dbe8b0631f804b4e091a0e03871261934",
			"ee9744c3e207665d6801c235c33b7789b252fbc9",
			"11e58d24bd1670747e18f7edebf1c62d58522c89",
			"1c16dcea0e3be508ba41112ea13febd284f737d8",
			"2e12dad3599611d627d0cb4481cc32cd6dc1a253",
			"2b95b94b28b5a18809cce9f6ea887639b8eacca2",
			"f6b95bfea0297ce84b271876f38a7abcfbfe5297",
			"2b03565049b32271354b74c54b5987e6361f127f",
			"2d46dfae46ca5707fac46c1cf20e6deae46a030d",
			"c0b25f14b5883c52602ec243c3975af8c0453110",
			"651a81e1c641d2e0f86aa3932e13113994e3d8ae",
			"79b9dee683276582231d8b51554b844e23035071",
			"2d2b9fc6cba4efa02fb8c77366c808bcd76a2ebb",
			"fbaffb800d4e37415fb876dd21a2baef04c5319e",
			"31cf4d93f185888f9e8a6b574eb367ecb32aa2b5",
			"bb8debc49936e2e91beec1f90dc9372ad0f22c5e",
			"b61d4dcf0064b9bade7adb12664ae864f5df150c",
			"12b23d6564f37f46f4f71c18f2a13238e83ad4af",
			"788a9d3593390dad504c772062f1f4b199f1ecc1",
			"a0aba81dbab74ec96dc60f43fcea3587a50fc1ec",
			"33fffc888dbb39c521fbf713559d539de239999d",
			"e75936815120c27230ba5a73786dc85198d0f927",
			"9d83f01e076550a7911accf33eb9383c1843c528",
			"8b130cffe3183071447dd7c5a75d0cffd44fe7de",
			"313d4adbb7964cdbed88df306aebe63e678c403a",
			"f4d9cc1ff9cef9b20e095f2d4f9295be97a7073b",
			"de2c52a81ce6144c8ca3f5f26228f4015e2188bb",
			"8c3c3408d09d96838db6e249c324921a74d3e40b",
			"41b0c3c32885b4b29c47525cd4327237e02ec0db",
			"c9292f322c271d81b4224b7d34ef3de91d38dd25",
			"50c6a95eacf9ac173f5ceb58ddf7350a4cea80fe",
			"204f15e837d9c8d282525c296e81769aa70d971f",
			"09306e9a22e4423b1bdd80ffd0befc451db668e4",
			"e628d70b4d5651ba703cdad8048bc0f2ca70c040",
			"389d5d2b69e8359f4f3a8f8c702fc5ad41e9b3f3",
			"7e199590aab1ffe7d2fdee5e54405e871a201581",
			"b0037dfaf1714aef70dd7cca90abef2d19fd79ee",
			"b4558ba1efa6b1469e596c473e48817fa157e600",
			"1c80572510bdd872b391b1c84ae264debaa40718",
			"ecefabeb8046fb6d58a1311115cd346a3b404498",
			"89409a386a9e95cd182fef78ddbc20b45b36923f",
			"8d3fddf573098489a463e606b0a6db93f486e108",
			"81cb8438ff907a8f6fcceb6d328f733ca85bd5c8",
			"2e9801b4497abc1e9bbf1127592fa1a6a10c5a6a",
			"a2bc90e569e97fc4d2c68cf12f67604c9f9e018f",
			"f3cd78ffd634c15ea8756d9976cd7f39df9315bb",
			"969840479e3343886eaa9c4a1735aa957b409a1b",
			"25e961b6e76b8445769af055c39daab4d1b013cd",
			"7422ab63c85cf6640b20f13221cde0d27fbc0fff",
			"7e9fca7dcd89931a5f59062bbbb786df336edcbb",
			"37bee5e568b873f98854fd06e16c28763dc7c525",
			"767cd63ce17cdff7f0e740845facfcef527555c5",
			"b7ccb0b4686cd3e48aeadb0a48b979dd77ebe793",
			"7cd8129b31dcf404be11a2de143a9c404bbcf099",
			"d7eb4a112239c258cec6e62070425c18024d4c72",
			"3635059093eb69ae23d1ea96b40638995657f1b9",
			"5be39803254ab29cf73608fb691be9642f70f1d3",
			"a3ccb053c82ad1f347fa69ba513ae6733e2f50c6",
			"d21e1b90749359f7f139a5de69160c5b2770f5d0",
			"d097f2599ec06bb714953c6382256f6cefd8bd97",
			"23e4c100663d6233406f1a57f1c8fecfbc0bab20",
			"bc816877efb2b8b720f02adb1c2e82cbeff26a7f",
			"5c38ccd2dde04f757ced3446147179f0a71e1877",
			"e152e0b8cfc75c6de7b3f00a20415dc75d875ee4",
			"ef5cd407ded26b7bd92172ca5599caed600b8248",
			"5589299c16db2b0079033e67e1fac2e944ff98a3",
			"0f1bdeb2edc6f1aee4eebaff2d8fdc13f9fd6c51",
			"f2a6e7ffa2fae9dffe138a5822114432647fed32",
			"90d9ec4abf732abfaf3d452683116785ac998753",
			"b4bd133d977ff4f5f5e5c548ec4749b164924f92",
			"dde054812147a325075c8902799864559098fc61",
			"5c85bb06ef7a59da66ac22013c829bedebcbb338",
			"67610411e3fdec5e5e1ec67489de2751e44d030a",
			"143f66a00693855967291f406e1b6b90a713bd21",
			"346e10cbf5331ee10429a152b04f4a7f0f329a57",
			"494ddd46a2d3d42a6057a9939bc623602185d902",
			"158e05d105b0c8350d83dfcfdca0e417da7eeecc",
			"a463239a7d9524083765a4c9d3c43256db8472be",
			"f98e9ec0573054e76316a9f352050f8c3167e2cc",
			"0a246c1355893ca00a02c8248a520906d5f72096",
			"ae71942ac7e2c920e97b74da4d85f8aa1ec73239",
			"b10323722cb9545622b7857adf2131c51fc474fa",
			"b1fc327fef4867d2967944bfc6f9def5d47cb966",
			"03633c6537d53412515305e91f6572fe51d3ad87",
			"3e1430761b75da4f71d8ccb9914e2429875f2e11",
			"cc7c856413a88fa2693e1dcfc3ccb0acc98e2caa",
			"8aac8c2343a6d0f5e4fdce0042172c0ec8c4cb3f",
			"ae573db17f4e50ba96b1ff1e0eed25be76fc33ad",
			"b1ef3a011702a61dfaf243327dd155adffbdf657",
			"97b3607b650d31da02382919bcde4ecf0392fcc1",
			"29634f2a39b2f7cf69d2e5594ff24d1f6d2459bc",
			"c0ff0ea6e9d2050867bc64191be8039034684669",
			"4aa00d0d5d3f4ec805889c8e6f176fa00854b698",
			"9246c057c904d8d14a9b2c78b5cc068868d90b86",
			"e6e9974dde9c3323d4dd2a3fa5dd57ab9627c8b1",
			"4047792314b27aa1ddfd0ec46c16385bbfa0db08",
			"243f92222a0464f3cba3ee4486f1ccda83e21262",
			"6964524697ee69faa81ca4d139ff7cc1d197046b",
			"537c58c6b7a7c3435c124d2205a15d88808f30b2",
			"4fa0ecaae72dc838d517e4d7c5f8c4ad2446583c",
			"06755cbae4fd8b3d8ac77a77189f2f32aa869ced",
			"a6298f180cf27e81451c96980651c3d5fbfc1a98",
			"5e08dc4b03589c06d80ea9fead2d2d3b46fefb7b",
			"ef067a52fc7c3c1ec6b5b2500b5b479337c5c4b9",
			"421fd1f414eeb1a5f1c231d564989e9aaadbba30",
			"18ba19cc26f5e6704408c5bc09e6c11f36b52fdc",
			"eba9d04293279c72866bc48f7b12050d037727f5",
			"215ecc64cf5ea2ba14ad3bdef5c91dad1360aa8a",
			"703258ffa983f364f90a234bf9645861cd0b7575",
			"e973ac7df63de079531b59b5e70cd2d802e36936",
			"696e4a27e73cc2aa35e3f7f2e90aea6940bc6259",
			"355c66a00c8af1780a5920481262a4d3a1539687",
			"677c473a5c6e61922a015af5b5e89fdc0d1999ec",
			"5ae52639d07bcead55cfa89d1e9766457ee54b61",
			"03129842d7661bd5de565239e07da113b5d111ed",
			"036c95a80b361aed05301b1ff36deb3a7e39694e",
			"890933d6fa00e52916deda63f28653c2a0d982b7",
			"9782e2f622be29862a28c5304a40b88e8bef25be",
			"744d71ae1f1654a9a892cb3305b39974c5e8923b",
			"1ad11d7ebf0ee998602085cb0120321d352277db",
			"76c98d8ac5786df82d5a594a99b82aeb8657cc0d",
			"84d4a0b4c6ab022c264ba1d8447f381821dbddd8",
			"02239391b8f6fc7050ed498941f2af0d170e379a",
			"7f628cfd7a6be4db0b6878d7a9310b1b8414fd57",
			"eefaf189e722792484d54edf7283248ae510db2f",
			"0a0df889f2c002ec6b01f4e9ec576212a020b79f",
			"c9eb5a7ec02d777d9cdf4f25649be84aa4306d96",
			"6ebc7acc336b4708fff24f963e9d846557fe9474",
			"51f1618f2c847b04f0466b279b985d38a407e5ea",
			"3ab66f2e7314445b9d7c31d58ac96f27c130ad5f",
			"6837f7aeeaefbaa083f26109c11d6de01b5c74a0",
			"d57145186f1d04cea6a8afa92095209f58b2bd6d",
			"3e11822e029c43da2d5881cfbdc0dc3285c6fc96",
			"754c63fb9b0138ea6df7f890e3eac6ee4c13dd19",
			"5245479dad013d7fd6dfb1f9d5ff719866d48b7e",
			"ab03907b6af8d3d1a5e42c4a797067510e7d3af0",
			"d070a89f2c928b77bc03d23845f39b1bb247a5e4",
			"70b0179e333735d1a69a483e755d6b44a6379674",
			"94ace079cf4442c3d3aed8d48f298a3c92b0bf12",
			"73a69729c921883b3b58224c557eafa099cea704",
			"390d109f0e7216360b5530718518085b919dcae3",
			"582125bf4aef5ac60c6ed1fb238d7eec27819bbb",
			"7fb85d8005bcbfe7faaaf97dab53f59dd5b792c0",
			"ce42ecdefe7e63f3a21ff92c2ebe367d47059dcc",
			"e8b0d5faf913bcaa1d5ad80f4040ecf39959b9c6",
			"1990c585dc4d250a23b49ad5e78af5d76280cd4f",
			"250d5241f87bec12c1b7677bbab166bb0835a2d3",
			"9cdf398507293cab4034eb50a242c858e1946588",
			"53c154a7098aa45e864f4464ea710e907ee1f98a",
			"326c95fc19c7165e2e26db9c85679d334138b4ab",
			"167d3c76cb20d309781f1cc076acbf084154203d",
			"83f9cdd1910d77089025b539d7a87b0743c6fadd",
			"bd0a0eecfd268605db82485cb52ac3e54f669e59",
			"ee520b9a59052e5c31eb0be87a17cfc7530f9eb8",
			"75a619987af8699cd288f30af2d93b85ea04c61e",
			"1031350f12228a4961f7c5074a6ac91df37bb489",
			"b048deb4bb6ae0623a2860491acd751862e62948",
			"249bb777b7aae5288194a94e2a8e5fc6e6936a2f",
			"de3602e5e65fc11c49f468aee8fb378971d151da",
			"d04cdd7e07306cc965c646addecbc56104dbaa32",
			"748c88f8f772391bd46555b7dc64592768db7901",
			"169e8d8ec3582b5c247d13460c1e5c9563468547",
			"1438bb287274191a1ab21954c684d4c0360b1206",
			"ecc760ae23fef6fd3aa7753cf8f5fbdcec37db91",
			"1a8893fd1849f0d6ef14ceecb62e339d26f6fbd0",
			"7736fdfb8ec1eb7089dfa1907a9b6397ec25afaa",
			"13e7f2ff067cb0aca91d5ca2c86868f8f80af55e",
			"bff2ca3251af2e3a44723d8d43e79e7c1039e2cf",
			"dc82e35c9bffee9245513c4121d0953977a0f4d9",
			"7b06117292e4e171f254f5720ebad7893abe5b6e",
			"cc3ba87ba74a63762ba2f21688909f73bac54fe2",
			"e38d5de97adccbb62c6ebf6d1b3feeb89c1ac972",
			"9e70f6d16a53a6618427c4ed86853eeb818b505c",
			"87f243d1ae91ef719ec3e721fc32f664e5d02d77",
			"0a3574afa7b1451e92d8c8b3bfd0d2e3d82fcf4b",
			"72d74f7747538bc726dd38b680879f208e73fafa",
			"bcc8b194e8dcd4dbf3a2acd46e2f0e5d044b793f",
			"7610de3e37020ff5b15c432b8d3dea3389a5cb59",
			"5242c047f407608e489195baab52bcdb6f523b95",
			"92ac492633c31ed7f9d36500dc52c0c659afdc6b",
			"57d21d6efb58efdcccad52a3b73b7148ca3f8944",
			"ef7d292a250524d99c2d8a3df7fc2662f717ca72",
			"33cc96652902d968d7706680a3762506b4814698",
			"ef7d292a250524d99c2d8a3df7fc2662f717ca72",
			"33cc96652902d968d7706680a3762506b4814698",
			"ef7d292a250524d99c2d8a3df7fc2662f717ca72",
			"33cc96652902d968d7706680a3762506b4814698",
			"ef7d292a250524d99c2d8a3df7fc2662f717ca72",
			"33cc96652902d968d7706680a3762506b4814698",
			"ef7d292a250524d99c2d8a3df7fc2662f717ca72",
			"dc11b4c07c9fad53d9f1972028cd50082621ba27",
			"09ef4333b4a10e2fbace38b215d4f5a68d738c6c",
			"dc3a49b7a3be0d1e0596fe8fa656a65d1f117a53",
			"d401e5e1face2544ebec92ee55fe83f009fd0f88",
			"567e718176c65bba116e2046e5228728a3a3612d",
			"db2e0c9584be418738341e7643ba2e8074055f45",
			"6c1f6ae650cba0986bdf298f9b6dfe37c4ba90bb",
			"91c2a310d20480f725049280d15d67ae768a4b32",
			"11c8d645449768fa54d6e7ec1c34f575530a44ca",
			"d8daa54bc481efae7380c91bf95c2191fc4ddc6b",
			"7f81ef203f3acd3d78c905f60df1ee2d81840fbb",
			"75c11dbdb681073cfba2227f5014662aae4de858",
			"f920684dd6235be1e44bee5f4ec3d756a825a1b9",
			"98e2626d67e6d55c4d0e4783fe0d613ff1e91206",
			"7e0b744cf5575fc3e78dd05170f51b014eff0226",
			"6eb065b485b2c7e72eb308e7c01253a554b7d796",
			"0efa6ddd08ea450e3a74ff8400e022a5e22a5b0b",
			"f5708f4ead40412822395bdfdc767e3ee3dd548d",
			"a9400f74c6b6e9042daae0d8d3c712d353083c7d",
			"32c6d1fb955765aac422c94a5b4cdf84ac86d455",
			"ede0c83276818f8c05adecabbbb3ca4a4ff175ce",
			"4f7b1bceafc07a9c37576fd7edcbc77c1b890e8d",
			"a6fc2607dddae4bfabc5389cce64cc3db23af877",
			"a59be73ff6b62089888badd7a16b8815bfdbf017",
			"81d724a5dd5a79acc1b33b8bf998981f20e5c30c",
			"9964ef54db2f627453cc72bb3487920bad86c3b4",
			"3d0f2620e01b583a2876468b3df12a907c3133c5",
			"55704e91db3cb5cd41817f2c43861d764bca8fda",
			"76c184fe7658bc287b697fb89a7a84b11299283f",
			"0d77781515b662892160f1ab5e725b0862bba37d",
			"2a11ab044d1d24c2eede4e2cce9c12f28b36a097",
			"28c2e29bb992c6fe8b12275277538066d2d8cf6a",
			"558e6d90bacb19aea6378db7e00ba0b2f58e025b",
			"8c40bbf842be3c88596817f0c9918d7f11833c03",
			"382324c3b7db0996945e6603a9a15eef13d53e4a",
			"1352dd8c214bc11f26e19e28861856266152317f",
			"58bf77e8d8f6d75cccbf92dbe66f06272974e64d",
			"5fffb8b2efc941bf0e83ab8463fa1d1cc06c0fc4",
			"e335f066c4575eeacaae6a8f8dfb2b2d8f463f00",
			"f16fd9e2da20a7b0b7e9afd909a6b2321b89d873",
			"0443b628d2ecfca726eb023ce85549ac0fe51798",
			"3a7b68ab3ac20a7d66255aeddc4d5c1a9ff5d044",
			"6d02d2b739a0e3f3924f6106bc91847ae42eb5fb",
			"6f0d661d2a55b4d8385819c932dfc1ff16b5b983",
			"568b38b76dfc145ef340e0451ad44b69a7f8f69b",
			"dbc880755f78edfc2f2d68c409b90634b9efdf57",
			"ab973f3f97936f14cf985d3243ab6f24515da4ea",
			"f8d01febc100ba12fafab9ce6ce13c16e4978db1",
			"bdd57ff9405b118b7225c8bf91343b8865fd28d8",
			"93399b72eb517a08e776a4dddd5112aa257f9ba2",
			"78a209953959e8534f920bc6c65943e10cd0b118",
			"b13e5bc2d0d4653843f77e715dd390ce03d488ac",
			"ae6f8cb1f12d57021b1ce111cd3b44758a0fb8a5",
			"6b2f19a1e3c195e1890af9d309bdee624e0c6653",
			"d3525829866e8c7609b8c50824317a54f8510fa9",
			"45c23f325570e39585efc8c89a897879751b1e98",
			"238327c8f5bc11de38a5bd604e1861e0f8824d62",
			"4729ad2112a4675ba878d27090f22f6eefa5d38b",
			"dd0a3936d127d7787a42ae39894a411401e81e3c",
			"623002f070d72b14134183066a8e28b7dbd49486",
			"a53a1c1aafdd349c15f2e9b585800cc9d90e825f",
			"87a858b27f3c7330e0759175f30592f47cb10fef",
			"e0f5f24445cda6590ece82b54ed526af6173de58",
			"a3a55653f58851c15ab9472340d7835f857ea6d4",
			"ff4962128da0b7ceb7248a3d0b33863735e4e791",
			"76b7d7bfe546d3576083d5244c976e80066fc3e6",
			"587d89b0485b86b74a177edb4023d2fb6b9e1375",
			"e419939b96182aad4c9eb726fcfb7756272161bb",
			"7ba9f73f1bee0e20c28a7f7964d2ec464e7fb6eb",
			"06df190307fba66376526c28199562b73a3ed5ee",
			"8d70be2b03d70d84947883275cc71712917d9f86",
			"0e6a608a9d960fdec4c9b3ed76839a3137539bbd",
			"749385e725a45824918024e1417199555821aba1",
			"0668409204b40ed239a8b2ab40515ba54cc0fea8",
			"2b669ffeb88a191504e3507336f2dcb9f6fb9e12",
			"54aa599560f9ff46ccb1867c8e9533c4bf7262ad",
			"c61d298fe7893236c64588bad5a3c0acd4498b94",
			"906f003fc42c77a050aa70be5f9bfefa3878093c",
			"bbc97c27231799f79eaa3b43c5642c0dea040678",
			"7b4d01938ffb58653a226eba14c6dc80107223a3",
			"1acba4940751f03c7f1c9044c3ecbe6586ef1fc8",
			"66e49c46109fbaa08581a62814b90f9b0940a2c2",
			"6ce458d75c0e5cbb394d0efe94e9a994b30e411f",
			"fabd9a22a31f7e465c7d0221aa525ce56cd30220",
			"1c4be8d141fc87182024044c311559da8ccc46e6",
			"e801eb3268f26171c1cda2d52b94cdd63d6ff908",
			"0e2728f29c6d94ac5f649a6f9250033f2e4be999",
			"169daacbdf09fc67f77ef1e132579d17e83a1c84",
			"7c18452f7044113fadb08bc8dd2d27434e1081e4",
			"2c1414f9b76e1b3b1303144135f1627def0e9023",
			"3b4c8431c761aea7f51520c74c3c0f61a9b8966b",
			"7b902f580aa584c00ff19ed39d5ed8b8ef9f63ce",
			"4ed7b3a9d321e14c51f51729658598c6811307c4",
			"e24f6cf2e388f2ecd8a3e9a63ef8b0813135d702",
			"e49dd80805f6b991ce376c0c22bfb70746b5a2f8",
			"5c65fd713cd1eb9b734da6062acd8b0e7bef9b25",
			"ef41974c1a60c309d0d441001ef9964604b8c2e7",
			"b567286999482c9e1ce51faa3e99051a97e2dc2c",
			"ec82433c04dfb7618f7a805283e36f44b8cbabca",
			"70c3516c4c18dc70b4fa8c89cfe1398a1ac659c8",
			"dfe39acbb72a399d3afd848a39c49d25094478d7",
			"8cd4005a70af668f11f8ae0e441536d50507eff3",
			"5609a1d561dc90c4c714d3004f849727c5817f20",
			"ac904105ba901c1cbd62e39622154fd9ad765a34",
			"e585a201c30109eb04eeb0717e18982cda922749",
			"442e9f437e51a47767c098b0d91147dd5a2e3f2f",
			"802b09abce3560b7976b27b6c3eb3a71f4310921",
			"47d70078f00514020cf5e6ce3cfebda8b780bc74",
			"322853b7efc353c07587ba081cabef11afd334e2",
			"7ef0007e3fb62cc015e3ce247005f60f521de994",
			"d28f9f044d35514a709de01916cf2d4c2fe6cf0d",
			"b5fe7d80a3075943019825d845bfc9f29144417a",
			"6c2fcd87c0095ced0a39342bdccce3eebe7eb57f",
			"21a1cc6376aecfa912d978c5327b5856b6600b00",
			"2b4de3f014cec4089c266b0d610ac3b80a5aa1d0",
			"2f948b71f47653bee24c7bf417c470607a1e845b",
			"a3e3fa48fae1f80022e9131fbb32ce01189ed6c4",
			"346aa12a802627b9f9e8ddafb179343347db9e59",
			"51425edcfb291624cc66a1fa86ca9d7286b1c95c",
			"113f87a513d941dec62a661033afac7609ae0bc7",
			"41b0a68d41fa9a503a25bf9a6de618e4d98b3067",
			"c5eb05f76aced26d1b690b13b652f881169a7c8f",
			"3d6063597bf6b28075018efef9d74dcbd97c2e6d",
			"dd8a04c85c8db6f1dec5829aa9f96e11d48d45af",
			"811d3151313a8ab2e2f65dc5dc2847e094de22b3",
			"ce885ea3a52beed12f75d0986fe1a8f541d07151",
			"adb6b691867d5bcd5f697a3aeafb59c11b43618b",
			"a10a3b4740b9739329cbec8feb0d76fb62e0967d",
			"aef5eefad2d159c0f312d106098ca25135b887cd",
			"81b2efd5b436f5b9883dc5b4565976b7e7096990",
			"6f9faa8bbc36015906a4681de614e7e545180832",
			"390def5ee935cd2215069aeb407018f6258ede3e",
			"9a9cd1073b4a9b8f0a59135a7e63c556a287b418",
			"f18ed2b7b0c0cdb8c0209f5e89f11967a4a0b534",
			"391bedad0f2d00e9ae777cc776ac0c54374ae7b9",
			"f16f8b3aef0ad14aea84a4bd0fc530dd9b3991a6",
			"85ce3a972b31bc4e69b5669d592355f3e871110f",
			"96ac9da6d36a97530b3766a4b8264a269cb81c1a",
			"a9387b78270ee36c16538efc659f248ac0c2e756",
			"111c3f5005b8d1894c773502cd87614b8bd1cf24",
			"74aa53b2f254920eada22ef0f38cc4e71b4fa922",
			"d533e1c0c0a641859cfeb176020162d05120a0ec",
			"c964d9067fe12552ea4c7fab2068e53b4acec78e",
			"15dfc5516d6323ddf7f5221430367099c9350fc3",
			"b7b5dc6f0d91e30ef3a6f7be638a7567f1e3d829",
			"fce0a2407bf4764bb583034a94715d70ed030bac",
			"4fc015f25fe5f69fb8c01b91e1af2b82c7b162da",
			"1955ae0ddf7f682442579d609be29310ba3fe7ea",
			"e036f68238f77acea63a8095a0cf396eea33f7d0",
			"4e4f1911e8144b55f06b34532a6699648d0098cd",
			"afc77afffaeb448e332c0a9ac72738d6a295441c",
			"7e6a02c9d808c5afab65c21982a093e6c672ec04",
			"160e14d68385ff7809b23d948f4fca40e4648899",
			"7d5263a425f9464e0951845ef4686ed4e199490d",
			"303eb770b964a85ce44a89027078009aa8ed7b41",
			"d1cd47ccd02f443162948bbe40efa8023f2bb9cc",
			"ae045f240af72a7419706b57ef8bb2f455660728",
			"961274bc7f05b45e2a563f6e0a6b804762b93a91",
			"45e45031673b7634acd2276d179737bf1acfbf2f",
			"f988bd24ae797a3f4bcca662c66aef730a294566",
			"b3b6bdff4dd4a9e6303c7803ceb2332081d7aa89",
			"51090567ca1bab6a864e25a11718630751f5684e",
			"799b9dc795c3c92d23fad70eef84870ca2c44f25",
			"b25664b3cf87fedc3eaaf826b1a7a0ca785ba398",
			"cc1ec2d72e723f6d214ec70636d99ec7a4683cc2",
			"4541be7ec10ca2c8e58323673a8893df7f291c73",
			"c723c5ca28082055cb43d4df8c492af0f9c06a7c",
			"96e0a9cf1a5b75b004fc7976e37502d4da8b49d2",
			"feec5905d4d9fea4cba69af6927785e18cce32f0",
			"c1a2eecaf8fb3cbbbb20b4e60760c1d5c1f7ac86",
			"e53b54ac30386a02d1db5b07ace14887b2a61bb7",
			"4c9af60f131e7c27b105a25886e9c759d4a3aff0",
			"ac356c24bea3f07ec9a1578ba7e5170b6ea812db",
			"3c292bee81fb85fdabc1e33997a7e93d51078d0f",
			"5dded0c59c2aec09cf60775aad03588bd8a3f900",
			"bc81ccba0b5faf129f2e08a2f6264ca6fcbc43b8",
			"dc5a4f24ce415cb19f04e811d5caf373c32cce42",
			"99e8a511ad35dfaa7d6c7293a1c4d6ece5ff707b",
			"e53f6ba53ebbd64020eec5a2f179fa28018a77d4",
			"b822534fe493cdc1446fd8969e465e9671ef029a",
			"b8782d0cfd8dd829ee63125ffc9480bf6271da78",
			"f0df46ea94981f75da254124a4d35837d891d848",
			"a3e135a1ac3bcd629aeaab2f2c0315a93286036a",
			"210dc7bbce448e2ac2bf91be7276ccb21aab6899",
			"4754f295b4625d44450799a0f79a04a78b86cb51",
			"37b2d80206249106754a7dc92515e0b1fdd2515b",
			"60e74aedf6db24f4f7755ecda32221546829735f",
			"7c74e0da51f0b64e32aa4f55cc3a014be465e3fc",
			"b2aadeddc77eeef7670f3fd7615f6bc6685f3529",
			"38b10cc1a4a4a5fab2efcca500209062402cd6aa",
			"bf5089755610a8885ae88474d856b495f8e26edb",
			"c5fb269a00e575464360c735cb0ce461c288ce28",
			"7e0fe90933534c260bf3f8e87f629d783157f313",
			"e213e539f32417201cc790bb1629c3f2cabc3c7c",
			"75a93b0b267d4aac3b22dcc51ff52072026bf0b2",
			"f9095703d21cf0b70d78d2981d74f79b5185e517",
			"6f10f9184cb0725380518c9e4f82b0847bdbbc58",
			"c10bbae6b3fbed4ea58d9ff39c725d62ad42362e",
			"c24905b992821b615c635121bf68f9ffc2e13686",
			"8e6e78e4270df6acf4dc07bf2586f7e5ffe66d06",
			"2f6c0d464f33ca436d45f6c23e6dc00db2d133e5",
			"6ee0f24c10da65c68f3a76cde78b920c4dcc0482",
			"78d6892b051445c8d537bd005bbf62b5e6cdac60",
			"e3c9a9f0b476a5b37256f08048172ac28bd372be",
			"f8a509c7b0e6decb67d8b9640f30e90af6e4c62d",
			"bf3ed1a9466e0a00a99d828d41652573f398f9eb",
			"d97ba480e6056a3374f8d3acc4f6014a45adbe85",
			"c6fa13d6024e1cf7a5aeecef1e5fd569ddb40472",
			"8183a1fc1bb9bee41d61985dc7fdf4a679f53361",
			"14770e5c3410104f0e81eeb5f9c1e4272ac97498",
			"a153c2b4534acdc98f785369cd22429e2a50e49c",
			"eef1f72478bb5d0a1caff8d7b871385c01c76fc8",
			"5f1f3da54d73ee0f32f89ad1246e0cfa2a7861dd",
			"4dd0a8d36062cacbe84ca9801286e456b2357672",
			"3011ba33d9b20318191dd4ee3aa1311eb128f328",
			"c0f9332f712ab6a66bc27851e44944a4f34f1a6b",
			"c963d681035498ffbc09e00092533749d67bf481",
			"b1021166950feaf21277ca7e5ae48ad4be4f6f6c",
			"f96946070abca1d0e1bad15b1182d8ef020a38c4",
			"a0a2da31b86c6cb360aa2e78e8bd2d24d0ce0e06",
			"13d94196f6bca70aa1bc1746653cfd7a8824c5bf",
			"aed2b099796abd0ba14716bd24759d6117a4ae65",
			"8011a1bea1b84dbfa43687742b6eca885a27754d",
			"048c24a1252f9c1436aa2867edad3607fb6111e0",
			"bc225a7360ba12cba58673bbe7e822108fdbe3ba",
			"456e91a1541d94946c44798834036c89f0254edc",
			"2042cbc042147f7cbcb4d39c9f3d355da4d49e9a",
			"e661e6d4b5368950ab1b70df87e8eaa2cb192d0c",
			"453fe15525b2552262dadc2dd5d95c095fc5be6f",
			"128452b93c066b2dc0bbcce38eedc323b7162dce",
			"77d4fe57c42413a46e31b772f31fa8e71da13dda",
			"3fbbe46ff2502b55b9d236c1dcc466186a0a2f75",
			"640a709b987129b0930e52ccfe1773c7a5d45e16",
			"928fbbe3817be9fb17574c2dbe56a984749ffb35",
			"baaf7c4bfb55870a95a3d505fedde3d6288c6501",
			"15dbddb17d5b68d00ca32f39d6de0d9175d33f26",
			"046a250b4073a73577f5bd900a4dfe8c5358f65c",
			"00e58a06920bf1b9385c4e189e76411e2cdbcb4e",
			"921780760d828f10d25fcbf649698d321f1dd996",
			"bdbec091d95ac396339cea25dab59d896b4b103c",
			"82a300bf7251ef6275344a484e251f526a0044d9",
			"9d11e103b9001106c26520757c3ce40717816b82",
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
