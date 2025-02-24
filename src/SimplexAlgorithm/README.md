
# Simplex Algorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

SimplexAlgorithm implements the simplex method to solve linear programming problems. It constructs an initial simplex tableau from a constraints matrix (A), a right-hand side vector (b), and objective function coefficients (c). Through iterative pivot operations, the algorithm moves along the vertices of the feasible region until an optimal solution is reached or the problem is determined to be unbounded.

To use the source code, include the SimplexSolver class in your project and call the Solve method by passing your A, b, and c values. The method returns the optimal solution vector if one exists. Comprehensive unit tests are provided to validate the functionality under various conditions.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
