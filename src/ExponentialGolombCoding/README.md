#Exponential Golomb Coding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

ExponentialGolombCoding is a coding method used to efficiently encode non-negative integers using a variable-length code. It works by representing an integer N as a unary prefix that indicates the number of additional bits required to represent N+1, followed by the binary representation of N+1. This method produces a compact and directly decodable bitstream.

To use the SourceCode, simply include the ExponentialGolombCoding class in your C# project. Call the Encode method to convert a non-negative integer into its exponential-Golomb coded string, and use the Decode method to convert a codeword back into the original number. Unit tests provided in the project demonstrate how to integrate and verify the algorithm in your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.