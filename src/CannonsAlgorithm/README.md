# CannonsAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

CannonsAlgorithm implements a distributed matrix multiplication algorithm optimized for processors arranged in an N×N mesh. The algorithm pre-aligns matrix blocks across a processor grid and iteratively shifts sub-matrices to perform local multiplications in parallel. This minimizes data movement and maximizes computational efficiency.

To use the algorithm, call the Multiply method with your input matrices and a grid size that evenly divides the matrix dimensions. Ensure that the matrices are square and compatible with the grid size. The SourceCode includes comprehensive unit tests and sample usage in the test project, which serves as a practical guide for integration and validation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>