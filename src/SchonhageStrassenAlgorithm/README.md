
# SchonhageStrassen Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Schonhage–Strassen Algorithm is an efficient method for multiplying very large integers by leveraging the Fast Fourier Transform (FFT) to perform convolution. By converting large integers into digit arrays and applying FFT, the algorithm significantly speeds up multiplication for numbers with many digits.

How to Use the Source Code:
1. Integrate the library into your C# project.
2. Call the SchonhageStrassen.Multiply method with your large integer inputs.
3. Review FFTUtil.cs for the FFT implementation that underpins the algorithm.
4. Refer to the unit tests in the associated test project to see practical examples and verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
