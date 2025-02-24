# BronKerboschAlgorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The BronKerbosch Algorithm is a recursive approach designed to find all maximal cliques in an undirected graph. It works by progressively building candidate cliques and recursively exploring neighboring vertices until no further expansion is possible, thereby identifying all cliques that cannot be extended further.

To use the SourceCode, include the provided C# library in your project. Create an instance of the Graph class, add vertices and edges to represent your graph, and then call BronKerbosch.FindMaximalCliques passing your graph to obtain a list of maximal cliques. The accompanying unit tests demonstrate various scenarios including handling empty graphs, single vertex graphs, complete graphs, and disconnected subgraphs.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.