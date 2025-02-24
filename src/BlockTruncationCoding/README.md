# BlockTruncationCoding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Block Truncation Coding (BTC) is a lossy image compression algorithm designed for grayscale images. It works by dividing the image into blocks, computing the average intensity and variance of each block, and then generating a bit-plane that distinguishes pixels above and below the mean. In cases where the block is uniform, the mean is used for both low and high values; otherwise, distinct low and high intensity levels are calculated to best approximate the original pixel distribution.

To use this library, reference the BlockTruncationCoding project in your C# solution and utilize the BTCCompressor class. Call the Compress method with a 2D byte array representing your image along with a specified block size, and the method will return a list of BTCBlock objects containing the compressed data. When you need to reconstruct the image, use the Decompress method with the same block size to obtain an approximate version of the original image.

The repository contains comprehensive MSTest unit tests that demonstrate how to work with various image types, such as uniform images, gradient images, and even non-uniform block sizes. These tests can serve as practical examples to guide integration into your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
