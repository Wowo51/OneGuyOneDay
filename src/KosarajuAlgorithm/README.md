# Kosaraju Algorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Kosaraju Algorithm efficiently computes the strongly connected components (SCCs) of a directed graph. It performs a depth-first search (DFS) to record finishing times, then reverses the graph and processes vertices in descending order of finish time to identify SCCs. This algorithm runs in O(V+E) time, making it highly effective for large, sparse graphs.

To use the source code:
1. Compile the solution using Visual Studio or the .NET CLI.
2. Run the MSTest unit tests to verify the algorithm’s functionality.
3. Explore the provided examples in the solution to understand various graph configurations.
4. Modify or extend the code as needed for your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>

Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>