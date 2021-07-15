# AAXClean
Decrypts Audible's aax and aaxc files on the fly, to an m4b (lossless mp4) or mp3 (lossy). Also supports conversion of existing decrypted m4b to mp3. During decryption neither input nor output stream is required to be seekable, but output stream is required to be seekable if converting an existing m4b to mp3. All file-level and stream-level metadata is preserved with lossless decryption, but not with mp3 decryption/conversion. There is an option for reading/writing file-level mp4 metadata tags and adding different chapter information.

Aax Usage:
```C#
var activation_bytes = "0a1b2c3d";
var input = File.OpenRead(@"C:\Source File.aax");
var output = File.OpenWrite(@"C:\Decrypted book.m4b");
var aaxFile = new AaxFile(input);

var conversionResult = aaxcFile.DecryptAax(output, activation_bytes, OutputFormat.Mp4a);
```

Aaxc Usage:
```C#
var audible_key = "0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d";
var audible_iv = "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b";
var input = File.OpenRead(@"C:\Source File.aax");
var output = File.OpenWrite(@"C:\Decrypted book.mp3");
var aaxcFile = new AaxFile(input);

var conversionResult = aaxcFile.DecryptAaxc(output, audible_key, audible_iv, OutputFormat.Mp3);
```

Conversion Usage:
```C#
var input = File.OpenRead(@"C:\Decrypted book.m4b");
var output = File.OpenWrite(@"C:\Decrypted book.mp3");
var mp4File = new Mp4File(input);

var conversionResult = mp4File.ConvertToMp3(output);
```
