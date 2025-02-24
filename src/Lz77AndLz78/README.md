# Lz77AndLz78 Compression Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

LZ77 and LZ78 are classic lossless data compression algorithms. LZ77 uses a sliding window to locate previously seen sequences. It encodes repeated occurrences by referencing the distance and length of a matching substring followed by the next character. LZ78 builds a dictionary dynamically, outputting the index of a previously encountered sequence along with the next character to form new dictionary entries. Both algorithms reduce redundancy and efficiently compress data.

How to use the source code:
1. Build the project using the .NET CLI or Visual Studio.
2. The library is written in pure C#, and it has no external binary dependencies.
3. To compress data, call LZ77Compressor.Compress or LZ78Compressor.Compress with your input string.
4. To decompress data, use the corresponding Decompress method.
5. Unit tests are provided using Microsoft’s testing framework; run them via Visual Studio or using the dotnet test command.
6. You can review and modify the source code as needed for your specific compression requirements.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.