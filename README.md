# AAXClean
Decrypts Audible's aax and aaxc files, without FFMpeg, to an m4b (lossless mp4) or mp3 (lossy). Also supports conversion of existing decrypted m4b to mp3. The input stream is not required to be seekable when decrypting. All file-level and stream-level metadata is preserved with lossless decryption, but not with mp3 decryption/conversion. There is an option for reading/writing file-level mp4 metadata tags and adding different chapter information.

Usage:

```C#
var aaxcFile = new AaxFile(File.OpenRead(@"C:\Source File.aax"));
```
Aaxc:
```C#
var audible_key = "0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d";
var audible_iv = "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b";
aaxcFile.SetDecryptionKey(audible_key, audible_iv);
```
Aax:
```C#
var activation_bytes = "0a1b2c3d";
aaxcFile.SetDecryptionKey(activation_bytes);
```
Output:
```C#
var conversionResult = aaxcFile.ConvertToMp4a(File.OpenWrite(@"C:\Decrypted book.mb4"));
```
OR
```C#
var conversionResult = aaxcFile.ConvertToMp3(File.OpenWrite(@"C:\Decrypted book.mp3"));
```

Conversion Usage:
```C#
var mp4File = new Mp4File(File.OpenRead(@"C:\Decrypted book.m4b"));
var conversionResult = mp4File.ConvertToMp3(File.OpenWrite(@"C:\Decrypted book.mp3"));
mp4File.InputStream.Close();
```
Multipart Conversion Example:
Note that the input stream needs to be seekable to call GetChapterInfo()


```C#
var chapters = aaxcFile.GetChapterInfo();
aaxcFile.ConvertToMultiMp4a(chapters, NetMp3Split);
            
private static void NetMp3Split(NewSplitCallback newSplitCallback)
{
	string dir = @"C:\book split\";

	string fileName = newSplitCallback.Chapter.Title.Replace(":", "") + ".m4b";

	newSplitCallback.OutputFile = File.OpenWrite(Path.Combine(dir, fileName));
}
```
