
# AdaptiveHuffmanCoding Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

AdaptiveHuffmanCoding is an adaptive compression algorithm that dynamically builds a Huffman tree based on live symbol frequencies. It begins with a special "Not Yet Transmitted" (NYT) node to handle symbols that have not been encountered before. As data is processed, the tree is updated in real-time to reflect current frequency distributions, making the encoding efficient even for streaming data.

How to Use:
- Compile the source code using Visual Studio or the .NET CLI.
- Run the unit tests in the AdaptiveHuffmanCodingTest project to verify functionality.
- Utilize the AdaptiveHuffmanEncoder to encode strings and the AdaptiveHuffmanDecoder to decode encoded bit streams.
- Refer to the project file structure and accompanying logs for guidance during development and troubleshooting.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  