# Canny Edge Detector

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The Canny edge detection algorithm is a multi-stage process that begins by converting an image to grayscale, then reducing noise with a Gaussian blur. Next, the algorithm computes the intensity gradients of the image, which are used to determine the edge strength and direction. Non-maximum suppression is applied to thin the edges, and finally, double thresholding with hysteresis is used to decide which edges are truly significant.

Usage:
Compile the source code using Microsoft Visual Studio or the .NET CLI. Run the resulting executable from the command line by providing an input image path and an output image path. The project is organized with a main library that implements the algorithm and a separate test project that verifies functionality using unit tests.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>