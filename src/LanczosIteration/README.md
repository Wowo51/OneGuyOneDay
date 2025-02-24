# LanczosIteration

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

LanczosIteration is an efficient algorithm for reducing a symmetric matrix to a tridiagonal form suitable for eigenvalue and eigenvector analysis. The algorithm iteratively builds an orthogonal basis and computes the necessary diagonal (alpha) and off-diagonal (beta) values that form the tridiagonal matrix. The provided source code includes comprehensive unit tests that demonstrate its usage and verify its functionality.

To use the source code, add a reference to the LanczosIteration project in your solution. Then, call the LanczosAlgorithm.ReduceToTridiagonal method with a valid, non-null square symmetric matrix as the argument. The method returns an object containing the alpha and beta values required to construct the tridiagonal matrix. Refer to the accompanying unit tests for examples of input structure and expected outputs.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.