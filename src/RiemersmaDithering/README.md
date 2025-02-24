# Riemersma Dithering Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## About the Riemersma Dithering Algorithm

The Riemersma Dithering algorithm is an image processing technique that applies a unique error diffusion approach. It begins by converting an image into grayscale using the formula (0.299×R + 0.587×G + 0.114×B). Each pixel is then thresholded to either black or white based on a mid-point threshold (128). The resulting quantization error is evenly distributed among the nearby unprocessed pixels. Uniquely, the algorithm processes pixels in a spiral order starting from the image center, yielding a more uniform and visually pleasing result compared to traditional methods.

### How to Use the SourceCode

To use this library, add a reference to the provided C# project in your solution. Call the static method RiemersmaDitherer.ApplyRiemersmaDithering and pass a Bitmap object (from the custom drawing namespace) to obtain a dithered image. The included MSTest unit tests demonstrate usage and verify that the output pixels are strictly black or white. This lightweight, MIT-licensed library is designed for .NET applications and requires no additional binaries.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.