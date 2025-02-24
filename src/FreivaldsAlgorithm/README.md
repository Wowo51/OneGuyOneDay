# Freivalds Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

FreivaldsAlgorithm is a randomized algorithm used to verify the correctness of matrix multiplication. It works by selecting a random vector and using it to test whether the computed product of two matrices matches the expected result. By repeating this process with different random vectors, the algorithm minimizes the chance of error in verification while significantly reducing computational effort compared to a full deterministic check.

To use the SourceCode, compile the solution targeting .NET 9.0. The repository is organized into two projects: one containing the implementation of the algorithm and another with unit tests using MSTest. Running the tests will demonstrate how the algorithm verifies correct matrix multiplication and detects errors when the multiplication is incorrect.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>