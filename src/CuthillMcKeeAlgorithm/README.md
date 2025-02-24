
# CuthillMcKee Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Cuthill-McKee Algorithm is a reordering technique designed to reduce the bandwidth of a symmetric sparse matrix. It works by selecting a vertex (typically one with the smallest degree) and then traversing the graph in a breadth-first manner, ordering neighboring vertices by increasing degree. This procedure clusters non-zero matrix elements closer to the diagonal, which can lead to performance improvements in numerical computations.

Usage:
The provided SourceCode implements the algorithm in a static class named CuthillMcKee. Two main methods are available:
- ComputeOrdering(IList<IList<int>> graph): Computes the ordering from an adjacency list representation of a graph.
- ComputeOrderingFromMatrix(int[, ] matrix): Converts a square symmetric sparse matrix into a graph and then computes its ordering.

To use the SourceCode, include the CuthillMcKeeAlgorithm project in your solution and reference its methods from your code. The accompanying unit tests demonstrate the expected behavior and usage scenarios for both methods.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
