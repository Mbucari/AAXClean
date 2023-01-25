
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
### Edit Metadata Tags:
```C#
aaxFile.AppleTags.Generes = "Adventure"
await aaxFile.SaveAsync();
```
### Relocate the moov atom to the beginning of an mp4 file:
```C#
await Mp4File.RelocateMoovAsync(@"C:\audiobook.m4b");
```
### Output:
```C#
await aaxFile.ConvertToMp4aAsync(File.OpenWrite(@"C:\Decrypted book.mb4"));
```
### Multipart Conversion Example:
Note that the input stream needs to be seekable to call GetChapterInfo()
```C#
var chapters = await aaxFile.GetChapterInfoAsync();
	//or aaxFile.GetChaptersFromMetadata();
var conversionResult = await aaxFile.ConvertToMultiMp4aAsync(chapters, NewSplit);
            
private static void NewSplit(NewSplitCallback newSplitCallback)
{
	string dir = @"C:\book split\";
	string fileName = newSplitCallback.Chapter.Title.Replace(":", "") + ".m4b";
	newSplitCallback.OutputFile = File.OpenWrite(Path.Combine(dir, fileName));
}
```
## Mp4Operation
All `ConvertTo___Async()` methods return an `Mp4Operation`. `Mp4Operation` contains the conversion task that's created in a suspended state. The `Mp4Operation` can be started by calling `Start()` or by awaiting it, and it can be cancelled by calling `CancelAsync()`. If you run the task by calling `Start()`, you may access the running task at `Mp4Operation.OperationTask` The `Mp4Operation` also contains the operation's progress and has an event you may subscribe to for progress updates.

## Successive Convert operations
`Mp4File` is opened with a `Stream`, and it reads from this input stream when converting or running `GetChapterInfoAsync()`. If the `Stream` is seekable, multiple conversion operations may be performed on the same `Mp4File` instance. If the `Stream` is not seekable, each new operation must be run on a new instance with the `Stream.Position = 0`.

## Concurrent Operations
Because `Mp4File` only has one input stream, it does not support concurrency. However, you may run as many concurrent operation on one file as you have `Stream` instances of that file. If the file source is a local filesystem file, this may be accomplished like so:
```C#
var aaxFile1 = new AaxFile(File.Open(@"C:\Source File.aax", FileMode.Open, FileAccess.Read, FileShare.Read));
var aaxFile2 = new AaxFile(File.Open(@"C:\Source File.aax", FileMode.Open, FileAccess.Read, FileShare.Read));

aaxFile1.SetDecryptionKey("0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d", "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b");
aaxFile2.SetDecryptionKey("0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d", "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b");

var mp3Operation = aaxFile1.ConvertToMp3Async(File.Open(@"HP_Zero.mp3", FileMode.OpenOrCreate, FileAccess.ReadWrite));
var mp4Operation = aaxFile2.ConvertToMp4aAsync(File.OpenWrite(@"HP_Zero_reencode.m4b"));

mp3Operation.ConversionProgressUpdate += ProgressReport;
mp4Operation.ConversionProgressUpdate += ProgressReport;

mp3Operation.Start();
mp4Operation.Start();

await Task.WhenAll(mp3Operation.OperationTask, mp4Operation.OperationTask);

```

Note that ConvertToMp3Async is only available with [AAXClean.Codecs](https://github.com/Mbucari/AAXClean.Codecs).
