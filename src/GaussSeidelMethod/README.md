# GaussSeidel Method Algorithm and Usage

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Gauss-Seidel Method is an iterative algorithm for solving systems of linear equations. It begins with an initial guess and refines the solution iteratively until a specified tolerance is achieved. In each iteration, the update for a variable is computed using the most recent values of the other variables, ensuring rapid convergence for systems that are well-conditioned or diagonally dominant.

To use the source code:
1. Integrate the GaussSeidelSolver class into your project.
2. Supply a coefficient matrix, a corresponding right-hand side vector, an initial guess, a tolerance value, and a maximum iteration count.
3. Compile and run the application, or execute the provided unit tests to verify the implementation.
4. The sample Program demonstrates usage, while unit tests confirm correctness.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.