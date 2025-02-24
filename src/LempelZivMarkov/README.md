
# LempelZivMarkov Compression Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LempelZivMarkov algorithm is a hybrid compression approach that combines Lempel-Ziv encoding techniques with a Markov chain model. It detects repeating patterns in the input data and replaces them with back-references, while simultaneously building a statistical model of byte transitions to optimize compression decisions.

How to Use:
- Build the solution using Visual Studio or the .NET CLI.
- Run the provided MSTest unit tests to validate the functionality.
- Reference the compiled library in your project.
- Utilize the LzmaCompressor class to compress and decompress byte arrays.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.
  