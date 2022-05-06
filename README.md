
# AAXClean
Decrypts Audible's aax and aaxc files, without FFMpeg, to an m4b (lossless mp4). The input stream is not required to be seekable when decrypting. All file-level and stream-level metadata is preserved with lossless decryption. There is an option for reading/writing file-level mp4 metadata tags and adding different chapter information.

## Nuget
Include the [AAXClean](https://www.nuget.org/packages/AAXClean/) NuGet package to your project.

## Convert to Mp3

To convert audiobooks to Mp3, use [AAXClean.Codecs](https://github.com/Mbucari/AAXClean.Codecs).

## Usage:

```C# 
var aaxFile = new AaxFile(File.OpenRead(@"C:\Source File.aax"));
```
### Aaxc:
```C#
var audible_key = "0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d";
var audible_iv = "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b";
aaxFile.SetDecryptionKey(audible_key, audible_iv);
```
### Aax:
```C#
var activation_bytes = "0a1b2c3d";
aaxFile.SetDecryptionKey(activation_bytes);
```
### Output:
```C#
var conversionResult = await aaxFile.ConvertToMp4aAsync(File.OpenWrite(@"C:\Decrypted book.mb4"));
```
### Multipart Conversion Example:
Note that the input stream needs to be seekable to call GetChapterInfo()
```C#
var chapters = aaxFile.GetChapterInfoAsync();
var conversionResult = await aaxFile.ConvertToMultiMp4aAsync(chapters, NewSplit);
            
private static void NewSplit(NewSplitCallback newSplitCallback)
{
	string dir = @"C:\book split\";
	string fileName = newSplitCallback.Chapter.Title.Replace(":", "") + ".m4b";
	newSplitCallback.OutputFile = File.OpenWrite(Path.Combine(dir, fileName));
}
```
### Edit Metadata Tags:
```C#
aaxFile.AppleTags.Generes = "Adventure"
```
