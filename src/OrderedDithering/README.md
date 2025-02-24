# OrderedDithering Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Ordered Dithering Help:
Ordered dithering is an algorithm that converts a grayscale image into a black and white image using a fixed threshold matrix known as the Bayer matrix. The algorithm divides the image into smaller segments and compares each pixel's brightness to a threshold derived from the matrix. If the pixel value is below the threshold, it is set to black; otherwise, it is set to white.

How to use the SourceCode:
- The library is implemented in pure C# with no external dependencies.
- Instantiate the DitheringProcessor from the OrderedDithering namespace.
- Call the ApplyOrderedDithering method with a Bitmap object as input; a valid Bitmap is either loaded from a file or created using the provided FakeDrawing classes.
- The source code includes MSTest unit tests which demonstrate how to use the functionality.
- Integrate this library into your project to apply ordered dithering effects to images or to study the algorithm.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.