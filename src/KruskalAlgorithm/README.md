
# KruskalAlgorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Kruskal's Algorithm is designed to compute a minimum spanning tree (MST) for a connected, undirected graph. The algorithm begins by sorting all the edges by weight and then iteratively selects the smallest edge that does not form a cycle, which is efficiently checked using a disjoint-set (union-find) data structure.

To use the SourceCode:
1. Review the Graph, Edge, and DisjointSet classes to understand the core data structures.
2. Examine the KruskalMST class which contains the FindMinimumSpanningTree method implementing the algorithm.
3. Use the Program class as an example to build a graph and display its MST.
4. Run the provided MSTest unit tests to verify proper functionality and correctness across various scenarios.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
