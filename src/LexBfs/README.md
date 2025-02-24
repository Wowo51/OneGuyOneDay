# LexBfs Implementation Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LexBfs algorithm provides a linear time ordering of graph vertices using lexicographic Breadth-First Search. It works by partitioning the vertex set and iteratively selecting the next vertex based on its connectivity in a lexicographical manner. This approach is useful for applications such as chordal graph recognition and sparse matrix computations.

How to Use the SourceCode:
- The project is organized as a pure C# library with an accompanying test project.
- The LexBfs algorithm is implemented in LexBfsAlgorithm.cs, and the graph data structure is defined in Graph.cs.
- A demo application in Program.cs illustrates how to build a graph, run the algorithm, and output the vertex ordering.
- Unit tests in LexBfsUnitTests.cs validate the implementation using Microsoft's MSTest framework.
- You can build and run the solution using Visual Studio or the dotnet CLI (using commands such as "dotnet build" and "dotnet test").

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>