# ShannonFano Coding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Shannon-Fano coding algorithm is a technique for data compression that assigns variable-length binary codes to input symbols based on their frequencies. Frequent symbols are given shorter codes while less frequent symbols receive longer codes. The method works by recursively partitioning the set of symbols into two groups with nearly equal total frequencies and then assigning a "0" to one group and a "1" to the other. This process continues until each symbol has a unique binary code.

To use the SourceCode, add the ShannonFanoEncoder class to your project. Use the BuildCodes() method to generate a map of characters to their binary codes, Encode() to convert plain text into its encoded binary form, and Decode() to reconstruct the original text from the binary string. The included unit tests demonstrate practical usage and verify that the encoding and decoding operate correctly.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>