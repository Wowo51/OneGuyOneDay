# ElserDifferenceMap Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ElserDifferenceMap algorithm is an iterative method for solving constraint satisfaction problems. It uses two projection operations to iteratively refine an initial guess until the changes fall below a specified tolerance. The algorithm works by first applying projection B to the current estimate, then reflecting that result about the current point and applying projection A. The difference between the two projections is used to update the solution. Convergence is reached when the norm of the difference is less than the tolerance.

To use the SourceCode, include the provided library in your C# project and instantiate the DifferenceMapSolver with your desired projection implementations. The built-in IdentityProjection is available for testing or simple scenarios, while custom projections can be provided to tackle more complex or domain-specific problems. Unit tests are included to demonstrate proper usage and verify the correctness of different projection strategies, including handling of null inputs and edge cases.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>