# Rayleigh Quotient Iteration Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Rayleigh Quotient Iteration algorithm is an iterative method for computing an eigenpair (an eigenvalue and its corresponding eigenvector) of a square matrix. It starts with an initial guess for the eigenvector and normalizes it. In each iteration, the algorithm computes the Rayleigh quotient to estimate the eigenvalue, then forms a shifted matrix by subtracting the eigenvalue times the identity from the original matrix. This shifted system is solved to obtain a new vector, which is normalized and compared with the previous iteration. The process continues until the difference between successive approximations is smaller than a specified tolerance, indicating convergence.

How to use the SourceCode:

The SourceCode is organized into separate classes that each handle a specific concern. The RayleighQuotientIterationAlgorithm class implements the iterative procedure, while helper classes like VectorHelper and LinearSystemSolver provide essential functionality for vector operations and solving linear systems. Unit tests using Microsoft’s MSTest framework are provided to verify correctness of the implementation. You can compile the solution using Visual Studio or the .NET CLI and run the tests to ensure everything works as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.