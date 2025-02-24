# PushRelabel Maximum Flow Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Push Relabel Maximum Flow algorithm is an efficient method for computing the maximum flow in a network. It starts by initializing a preflow from the source, saturating its outgoing edges, and then repeatedly performs push operations to move excess flow along admissible paths. When no more pushes are possible, the algorithm relabels vertices to open up new pathways. This iterative process continues until no further flow can be pushed from the source to the sink, resulting in the maximum flow.

To use the source code, add the PushRelabelAlgorithm project to your solution. Instantiate the Graph class with the desired number of vertices, and use the AddEdge method to set up the network with specific capacities. Finally, call the GetMaxFlow method from the PushRelabel class with the graph, source vertex, and sink vertex as arguments. The accompanying MSTest unit tests provide practical examples that demonstrate how to construct graphs and verify the algorithm’s correctness.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>