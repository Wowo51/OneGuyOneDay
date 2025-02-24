
# Chain Matrix Multiplication Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Chain Matrix Multiplication algorithm is a dynamic programming strategy used to determine the optimal way to multiply a chain of matrices by minimizing the number of scalar multiplications. It works by constructing two tables: one that records the minimum multiplication costs and another that traces the optimal splitting points. This approach helps in efficiently computing the best order of multiplication, which is crucial for performance-critical applications involving matrix computations.

To use the SourceCode, compile the provided solution using Microsoft Visual Studio or the .NET CLI. The solution contains the main implementation, a test project with unit tests, and additional supporting files. Simply reference the ChainMatrixMultiplication library in your own C# project, and call the MatrixChainMultiplicationSolver.ComputeOptimalOrder() method with your matrix dimension array to obtain both the minimum cost and the optimal parenthesization.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  