
# FELICS Image Compression Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

FELICS (Fast Efficient & Lossless Image Compression System) is a lossless image compression algorithm implemented in C#. The algorithm compresses image data by detecting runs of repeated bytes and encoding them as count-value pairs. It efficiently reduces file sizes without sacrificing any image information, making it ideal for applications where lossless data integrity is crucial. The algorithm handles long runs by splitting counts that exceed the maximum allowed value.

To use the SourceCode, add the FELICSImageCompression project to your solution and reference it in your application. Instantiate the FELICSCompressor class and call its Compress method to compress image data, or its Decompress method to restore the original data. The provided unit tests demonstrate typical usage and verify the functionality for various scenarios, including edge cases like empty inputs and long repeated sequences.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
