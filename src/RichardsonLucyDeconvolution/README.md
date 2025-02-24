#Richardson Lucy Deconvolution Help

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

RichardsonLucyDeconvolution is an iterative image deblurring algorithm that restores blurred images by iteratively updating an estimate using convolution operations with a point spread function (PSF). Each iteration computes a correction factor by dividing the blurred image by the convolution of the current estimate with the PSF, and then applies this correction by multiplying with a reflected version of the PSF. The process gradually sharpens the image.

To use the SourceCode, integrate the provided C# library into your project. The main entry point is the static Deconvolve method in the Deconvolver class. Provide it with your blurred image, the PSF, and the desired iteration count. Unit tests are included to verify functionality and serve as usage examples. The library follows best practices, ensuring code clarity and maintainability.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>