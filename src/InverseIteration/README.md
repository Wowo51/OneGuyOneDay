# Inverse Iteration Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The InverseIteration algorithm approximates an eigenvector corresponding to a specified eigenvalue using iterative refinement. In each iteration, the matrix A is modified by subtracting the target eigenvalue from its diagonal, and the resulting linear system is solved to produce a new approximation. With successive iterations, the method converges toward the eigenvector associated with the given eigenvalue.

To use the SourceCode, compile the solution using .NET 9.0 with the provided project files. Run the MSTest unit tests in the InverseIterationTest project to verify the functionality. The code is organized into modular components where InverseIterationSolver implements the iterative algorithm and MatrixHelper manages supporting matrix operations.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>