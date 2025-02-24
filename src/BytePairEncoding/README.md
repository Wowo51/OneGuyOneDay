# BytePairEncoding Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Byte Pair Encoding (BPE) is a subword tokenization algorithm that works by iteratively merging the most frequent pair of consecutive symbols in an input string. With each merge operation, the algorithm replaces the most common adjacent pair with a new token, resulting in a compressed and more efficient representation of the text. This process continues until the specified number of merge operations is reached or no further valid merges can be performed.

To use the SourceCode, add the BytePairEncoding library to your C# project and call either BytePairEncoder.Encode to get a list of tokens or BytePairEncoder.EncodeToString to receive a space-separated string of tokens. The provided solution includes a Visual Studio solution file and respective project files for both the library and its MSTest-based test suite. The unit tests demonstrate various use cases, including handling of null inputs, empty strings, multiple merge operations, and performance testing with large inputs.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>