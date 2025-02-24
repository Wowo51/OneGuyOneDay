# Truncated Binary Encoding Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The TruncatedBinaryEncoding algorithm is designed to efficiently encode and decode integer values using a truncated binary approach. It computes the bit-length L as the floor of log₂(n) and determines a threshold value that decides whether a number can be represented in L bits or needs L+1 bits. For values less than the threshold, the number is encoded in L bits; for values equal to or greater than the threshold, the encoder adjusts the value and uses L+1 bits. This method minimizes the overall bit usage while ensuring unique decodings.

To use the SourceCode, simply integrate the provided C# library into your project. The library exposes two primary methods: Encode, which converts an integer into its truncated binary string representation, and Decode, which recovers the original integer from the binary string while also returning the number of bits used. The accompanying unit tests demonstrate how to use these methods in practical scenarios and validate the functionality of the algorithm.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>