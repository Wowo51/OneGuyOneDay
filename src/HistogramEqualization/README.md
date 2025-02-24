# Histogram Equalization Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Histogram Equalization is an image processing technique used to enhance the contrast of grayscale images by redistributing the pixel intensity values. The algorithm calculates the histogram of an image’s pixel intensities and produces a cumulative distribution function (CDF). This CDF is then used to remap the original intensity values, spreading them across the full range from 0 to 255, which enhances details in both dark and bright regions.

To use the SourceCode, compile the project according to the provided project files (targeting .NET 9.0) and run the accompanying unit tests to ensure correctness. Once verified, integrate the HistogramEqualizer class by passing a Bitmap image to its Equalize method to obtain a contrast-enhanced version of the original image.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.