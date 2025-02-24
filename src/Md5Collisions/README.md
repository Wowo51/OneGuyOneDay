
# MD5 Collisions Library Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Md5Collisions algorithm is designed to demonstrate and generate MD5 hash collisions. It works by generating two distinct 128-byte arrays that share a fixed 8-byte collision marker. This marker forces the MD5 hash function to return a fixed hash value when processing specially crafted input data, thereby illustrating that two different inputs can produce the same MD5 hash.

To use the SourceCode:
- Compile the solution using the .NET SDK.
- Run the included unit tests to verify the behavior and collision generation.
- Execute the console application to see the generated MD5 hashes and confirm that a collision has been achieved.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
