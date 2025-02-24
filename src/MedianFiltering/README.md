#Median Filtering Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Median Filtering algorithm is a non-linear filter used primarily to remove salt-and-pepper noise from images while preserving edge details. It works by sliding a window over the image and replacing each pixel with the median value of its neighbors. This approach effectively reduces impulse noise without blurring the image.

To use the SourceCode, include the MedianFiltering library in your C# project and call the static method ApplyMedianFilter with your image (represented as a 2D integer array) and a specified window size. Note that if an even window size is provided, the algorithm will automatically adjust it to an odd number to ensure proper median calculation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.