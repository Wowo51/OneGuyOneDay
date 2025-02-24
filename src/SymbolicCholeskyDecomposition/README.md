
# Symbolic Cholesky Decomposition

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Symbolic Cholesky Decomposition algorithm performs a symbolic analysis of a sparse matrix to determine the structure of its Cholesky factorization without computing numerical values. It works by building an elimination tree that identifies fill-in patterns and dependencies between the matrix rows. This analysis helps optimize both memory usage and computational efficiency before the actual numerical decomposition is carried out.

To use the SourceCode, compile the library with .NET 9.0 as specified in the project file and include it in your project. The provided unit tests demonstrate how to create a sparse matrix, apply the decomposition, and verify the resulting lower-triangular structure. Detailed inline comments in the code further explain the logic of the algorithm, making it easier to adapt or extend for your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
