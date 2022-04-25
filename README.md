
# AAXClean
Decrypts Audible's aax and aaxc files, without FFMpeg, to an m4b (lossless mp4) or mp3 (lossy). Also supports conversion of existing decrypted m4b to mp3. The input stream is not required to be seekable when decrypting. All file-level and stream-level metadata is preserved with lossless decryption, but not with mp3 decryption/conversion. There is an option for reading/writing file-level mp4 metadata tags and adding different chapter information.

## Nuget
Include the [AAXClean.Codecs](https://www.nuget.org/packages/AAXClean.Codecs/) NuGet package to your project.

## Usage:

```C#
using AAXClean.Codecs

var audible_key = "0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d";
var audible_iv = "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b";
aaxcFile.SetDecryptionKey(audible_key, audible_iv);
```
### Convert to Mp3:
```C#
var conversionResult = aaxcFile.ConvertToMp3(File.Open(@"C:\Decrypted book.mp3", FileMode.OpenOrCreate, FileAccess.ReadWrite);
```
Note that the output stream must be Readable, Writable and Seekable for the mp3 Xing header to be written. See [NAudio.Lame #24](https://github.com/Corey-M/NAudio.Lame/issues/24)

### Detect Silence
```C#
var silences = aax.DetectSilence(-30, TimeSpan.FromSeconds(0.25));
```
### Conversion Usage:
```C#
var mp4File = new Mp4File(File.OpenRead(@"C:\Decrypted book.m4b"));
var conversionResult = mp4File.ConvertToMp3(File.Open(@"C:\Decrypted book.mp3", FileMode.Create, FileAccess.ReadWrite));
```
### Multipart Conversion Example:
Note that the input stream needs to be seekable to call GetChapterInfo()


```C#
var chapters = aaxcFile.GetChapterInfo();
aaxcFile.ConvertToMultiMp4a(chapters, NewSplit);
            
private static void NewSplit(NewSplitCallback newSplitCallback)
{
	string dir = @"C:\book split\";

	string fileName = newSplitCallback.Chapter.Title.Replace(":", "") + ".m4b";

	newSplitCallback.OutputFile = File.OpenWrite(Path.Combine(dir, fileName));
}
```
