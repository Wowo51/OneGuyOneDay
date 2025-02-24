# ShannonFanoElias Algorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Shannon-Fano-Elias coding is an information-theoretic method that assigns binary codewords to symbols based on their probabilities. The algorithm computes the cumulative probability for each symbol and uses the midpoint to generate a binary fraction representation. The length of each codeword is derived from the negative logarithm (base 2) of the symbol's probability plus an extra bit to ensure prefix-free coding.

To use the SourceCode, simply include the ShannonFanoElias library in your project. Instantiate the Encoder class and call the Encode method with a dictionary mapping symbols to their probability values. The method returns a dictionary of binary codewords corresponding to each symbol. Unit tests using MSTest are provided to validate functionality. Compile the project with your preferred .NET build tool to integrate or run tests.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.