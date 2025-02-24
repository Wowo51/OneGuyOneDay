
# Linde Buzo Gray Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Linde-Buzo-Gray algorithm is a vector quantization technique that constructs an optimal codebook from training data. It begins by computing a global centroid of the input data and then iteratively splits centroids by applying a small perturbation (controlled by an epsilon parameter) until the desired number of codewords is reached. After each split, the algorithm refines the codebook by reassigning training data to the nearest centroid and recalculating centroids until convergence is achieved according to a specified threshold.

To use the SourceCode, open the solution file in Microsoft Visual Studio and build the project. The library includes comprehensive unit tests using MSTest to verify functionality. You can adjust parameters such as epsilon and the convergence threshold to tailor the algorithm to your dataset.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
