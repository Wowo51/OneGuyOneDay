
# Tridiagonal Matrix Algorithm Help

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Tridiagonal Matrix Algorithm (also known as the Thomas algorithm) is a specialized method for solving systems of linear equations where the coefficient matrix is tridiagonal. The algorithm works in two phases: a forward elimination phase that transforms the system into an upper triangular form, and a backward substitution phase that computes the solution vector efficiently in O(n) time. It includes safety checks to address division by zero and verifies that the input arrays have appropriate dimensions.

To use the SourceCode, add a reference to the library in your project and include its namespace. Call the Solve method from the ThomasAlgorithmSolver class by passing in the sub-diagonal, main diagonal, super-diagonal, and right-hand side vectors. The accompanying unit tests demonstrate various scenarios—from valid inputs to error cases—to help guide you in integrating and testing the algorithm in your applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
