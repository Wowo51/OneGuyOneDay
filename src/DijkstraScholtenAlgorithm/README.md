# DijkstraScholtenAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Dijkstra-Scholten Algorithm Help
=================================

Overview:
The Dijkstra-Scholten algorithm is designed for detecting termination in distributed systems by managing work propagation and acknowledgements between nodes. In this C# implementation, each node is represented by a ProcessNode which tracks work credits and maintains a parent-child relationship to ensure proper propagation of work and termination acknowledgement.

How It Works:
When a node sends work to another node, it increases its credit and the target node is activated and linked as a child. As work is completed, nodes decrement their credits and propagate acknowledgements back to their parent. Termination is detected at the root when its credit returns to zero and all nodes have completed their work.

Using the Source Code:
The provided SourceCode includes both the algorithm library and a suite of unit tests using MSTest. To use it:
1. Open the solution file (DijkstraScholtenAlgorithm.sln) in Visual Studio or use the dotnet CLI.
2. Build the solution to compile the library.
3. Run the unit tests in the DijkstraScholtenAlgorithmTest project to validate the algorithm’s behavior.
4. Refer to the test cases and inline comments for practical examples of work propagation and termination detection.
5. Modify and extend the implementation as needed for your distributed systems experiments.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.