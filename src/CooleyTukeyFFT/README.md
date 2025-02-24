
# CooleyTukeyFFT Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Cooley-Tukey FFT algorithm is a highly efficient method for computing the discrete Fourier transform (DFT) and its inverse. By recursively breaking down a DFT of a composite size into smaller DFTs, the algorithm reduces computational complexity from O(n²) to O(n log n). This implementation in C# handles both inputs whose lengths are already a power of two and those requiring padding with zeros for non-power-of-two lengths.

To use the SourceCode, add the CooleyTukeyFFT library to your project and reference it. The primary method, FFT.Compute, accepts an array of System.Numerics.Complex numbers and returns their Fourier transform. Extensive unit tests using Microsoft's testing framework are provided to guide you in integrating and verifying the functionality in your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.<br>
Authored by Warren Harding. AI assisted.<br>
  