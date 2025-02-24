# ConjugateGradient Solver Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Conjugate Gradient algorithm is an iterative method for solving systems of linear equations where the coefficient matrix is symmetric and positive-definite. It is especially useful for large, sparse systems in scientific and engineering applications. Instead of directly computing an inverse, the algorithm progressively refines an initial guess by minimizing the error (residual) at each iteration. At every step, it computes a new search direction that is conjugate with respect to the matrix, ensuring that previous computation is not undone. The process continues until the residual is below a specified tolerance or a maximum number of iterations is reached.

How to Use the SourceCode

To use the SourceCode, simply include the provided C# library in your project. Supply a method for matrix-vector multiplication that matches your problem’s requirements, along with the right-hand side vector, a tolerance level, and a maximum iteration count. The solver will return an approximate solution vector. Unit tests using Microsoft’s testing framework demonstrate the correct usage and validate the functionality of the algorithm.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.