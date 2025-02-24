#FletchersChecksum Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

FletchersChecksum is a checksum algorithm that calculates two sums over the input data using modulo arithmetic. The algorithm computes the first sum by adding each byte (modulo 255) and then computes a running total by adding the first sum (also modulo 255). The final checksum is obtained by shifting the second sum left by 8 bits and combining it with the first sum. This method delivers a fast and efficient way to detect errors in data.

To use the SourceCode, add the FletchersChecksum project to your solution, reference it in your test project, and run the provided unit tests. The implementation handles null and empty inputs by returning zero, ensuring robust error checking and validation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>