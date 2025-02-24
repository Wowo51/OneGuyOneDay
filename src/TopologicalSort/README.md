# Topological Sort Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The TopologicalSort algorithm organizes nodes representing tasks such that for every directed edge from one node to another, the first appears before the second in the final ordering. This functionality is especially useful for scheduling tasks, determining build orders, and resolving dependencies in any acyclic directed graph.

Usage:
1. Create a dependency graph as a dictionary where each key is a node and its value is a list of dependencies.
2. Call TopologicalSorter.TrySort to attempt sorting the graph. This method returns a boolean indicating success and an output parameter containing the sorted list. If a cycle is detected, the method returns false.
3. Alternatively, use TopologicalSorter.Sort to retrieve a sorted list directly; if a cycle exists, it will return an empty list.
4. Refer to the accompanying unit tests for examples covering cases such as empty graphs, graphs with cycles, large dependency chains, and special cases like self-dependency.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.