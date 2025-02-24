# Golomb Coding Optimal Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

GolombCodingOptimal is an efficient algorithm for Golomb coding, designed to compress integers optimally when the underlying data follows a geometric distribution. The encoding splits the input number into a quotient and a remainder; the quotient is encoded using unary coding (a series of 1's terminated by a 0) and the remainder is encoded in binary with an adaptive bit-length based on the parameter m. This efficient approach provides optimal compression for data with geometric distributions.

To use the SourceCode, include the GolombCodingOptimal library in your C# solution and call the GolombCoder.EncodeInteger and GolombCoder.DecodeInteger methods to encode and decode integers. The project is structured with a main library and an accompanying test project that contains comprehensive unit tests. You can build and run the tests using Visual Studio or the .NET CLI to validate its functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>