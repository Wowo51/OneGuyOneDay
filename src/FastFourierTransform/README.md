# Fast Fourier Transform Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Fast Fourier Transform (FFT) algorithm is an efficient method for computing the Discrete Fourier Transform (DFT) of a sequence. This implementation uses a recursive divide-and-conquer approach to separate the input into even and odd indexed elements, compute the FFT on these smaller arrays, and then combine the results using complex exponential factors. This minimizes the computational complexity compared to the naive DFT method.

To use the SourceCode, include the FFT implementation file in your C# project and ensure that the length of your input array is a power of two. The provided solution comes with a full suite of unit tests using Microsoft's MSTest framework. Run these tests via Visual Studio or the dotnet test command to verify that the implementation works correctly.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>