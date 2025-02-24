# LossyNormalMap Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

LossyNormalMap is a lossy compression algorithm for normal maps. It converts input normal vectors, whose components are floats in the range [-1,1], into a compressed 8-bit representation. The compression method maps the floating point values to bytes using a quantization technique, and stores a header with the map's width and height. The decompression function reverses this process by dequantizing the byte values back to approximate float values. This algorithm is designed for scenarios where exact precision is not critical, and a lossy but efficient compression is acceptable for normal map data in graphics applications.

The SourceCode contains a complete, unit-tested implementation in pure C#. To use it, simply compile the provided projects with .NET 9 or later. The library exposes methods to compress and decompress normal maps and includes unit tests to verify its behavior. You can integrate the library into your graphics application as needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>