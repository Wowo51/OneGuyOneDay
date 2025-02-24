# Karns Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Karn's Algorithm calculates reliable round-trip time estimates for TCP by ignoring ambiguous retransmission measurements. It continuously updates an estimated RTT and its deviation for non-retransmitted samples, providing an adaptable retransmission timeout value. To use the SourceCode, integrate the library into your project, instantiate the RttEstimator, and refer to the provided unit tests for guidance on proper integration and verification.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>