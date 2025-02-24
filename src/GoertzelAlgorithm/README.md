
#Goertzel Algorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Goertzel Algorithm is an efficient method to detect the presence and magnitude of a specific frequency in a block of samples. Instead of computing a complete Fourier transform, it zeroes in on one frequency component, making it ideal for applications such as tone detection (e.g., DTMF decoding) and other signal processing tasks.

To use the SourceCode, include the Goertzel.cs file in your project, instantiate the Goertzel class, and call the Process method by passing an array of samples, the target frequency, and the sample rate. The accompanying unit tests demonstrate its correct usage and validate its functionality, while additional project files provide build configurations and licensing details.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
