# Biconjugate Gradient Method Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Biconjugate Gradient Method is an iterative algorithm for solving systems of linear equations, particularly effective for large, non-symmetric problems. It works by generating two sets of conjugate search directions—one associated with the coefficient matrix and another with its transpose—to stabilize convergence even when the matrix is not symmetric.

To use the SourceCode, add the provided project file to your solution, build the library, and invoke the BiconjugateGradientSolver.Solve method by supplying your coefficient matrix (A), right-hand side vector (b), an optional initial guess, a tolerance, and a maximum iteration count. The included unit tests demonstrate typical usage scenarios and verify the solver's accuracy.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.