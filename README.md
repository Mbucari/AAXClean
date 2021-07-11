# AAXClean
Decrypts Audible's aaxc files on the fly, with minimal changes to the source file. Input stream is not required to be seekable, but output stream is. All file-level and stream-level metadata is preserved. There is an option for reading/writing file-level metadata tags and adding different chapter information.

Usage:
```C#
string audible_key = "0a0b0c0d0e0f1a1b1c1d1e1f2a2b2c2d";
string audible_iv = "2e2f3a3b3c3d3e3f4a4b4c4d4e4f5a5b";
FileStream input = File.OpenRead(@"C:\Source File.aax");
FileStream output = File.OpenWrite(@"C:\Decrypted book.m4b");
Mp4File aaxFile = new Mp4File(input);

Mp4File decryptedFile = input.DecryptAaxc(output, audible_key, audible_iv);
```
