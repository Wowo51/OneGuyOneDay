# EmbeddedZerotreeWavelet Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Embedded Zerotree Wavelet (EZW) algorithm is a progressive image compression technique that efficiently encodes wavelet coefficients by classifying them as significant (positive or negative) or insignificant relative to a dynamic threshold. This process allows for gradual refinement of image data, making it suitable for applications where scalability and progressive image transmission are required.

To use the source code:
1. Include the EmbeddedZerotreeWavelet library in your project.
2. Use the EZWProcessor.Encode() method to compress a two-dimensional coefficient matrix.
3. Use the EZWProcessor.Decode() method to reconstruct the coefficient matrix or image from the encoded string.
4. Refer to the unit tests provided for examples of usage and to verify correct behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>