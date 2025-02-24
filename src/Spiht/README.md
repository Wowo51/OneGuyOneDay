# SpihtAlgorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## About the SPIHT Algorithm
The SPIHT (Set Partitioning in Hierarchical Trees) algorithm is an efficient image compression technique that encodes wavelet-transformed data using bit-plane coding and hierarchical tree structures. In this implementation, the algorithm processes a coefficient matrix by dividing it into successive bit-planes based on the significance of each coefficient. This method results in a compact representation that is useful for lossy compression, even though it omits full list management features found in more complete versions of SPIHT.

## Using the SourceCode
The provided source code includes two primary components:
- SpihtEncoder: Encodes a two-dimensional array of floating-point coefficients into a compressed byte array using bit-plane analysis.
- SpihtDecoder: Reconstructs the coefficient matrix from the encoded data, yielding a non-negative approximation of the original values.
  
To use the SourceCode, integrate the library into your C# project. The code is accompanied by a comprehensive set of unit tests using Microsoft's MSTest framework. These tests validate various scenarios including handling of null and empty inputs, correct encoding of values, and round-trip consistency between encoding and decoding. Simply build the solution and run the tests to ensure functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>