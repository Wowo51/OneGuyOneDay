# RuzzoTompaAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

RuzzoTompaAlgorithm is an efficient algorithm for finding all non‑overlapping, contiguous, maximal scoring subsequences within an array of real numbers. The algorithm scans the input sequence by skipping initial negative values and then accumulating a segment of positive scores until further additions would decrease the cumulative total. It identifies the best scoring contiguous subsequence from each positive segment and returns a list of segments with their start and end indices as well as their total score.

To use this SourceCode, add a reference to the provided C# library in your project. Then call the static method FindMaximalSubsequences on the RuzzoTompaAlgorithm class, passing an array of double values. The method returns a list of ScoreSegment objects that indicate the boundaries of each maximal scoring subsequence and its corresponding score. Comprehensive MSTest unit tests ensure that the algorithm handles various cases, including null inputs, empty arrays, all negative values, and mixed data scenarios.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>