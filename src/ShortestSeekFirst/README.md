
# ShortestSeekFirst Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ShortestSeekFirst algorithm is a disk scheduling technique that minimizes overall seek time by always selecting the pending request closest to the current disk head position. This approach reduces the delay caused by the head movement between requests and is particularly effective in environments with high I/O demands.

To use the source code:
1. Include the provided C# source files in your project.
2. Call the SSTFScheduler.Schedule method with a list of disk request positions and an initial head position.
3. Review the included unit tests to see examples handling various scenarios such as null input, duplicate requests, and large datasets.
4. Customize the implementation as needed for your specific application requirements.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
