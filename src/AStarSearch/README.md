# AStar Search Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The A* Search algorithm is a best-first search algorithm that finds the shortest path by combining the actual cost to reach a node with an estimated cost from that node to the goal. It efficiently navigates graphs and grids by considering both the distance already traveled and the estimated distance remaining.

To use the SourceCode:
- Include the AStarSearch library in your project.
- Call the AStarAlgorithm.FindPath method, supplying the start node, goal node, a function that returns neighboring nodes, a function to compute the cost between nodes, and a heuristic function to estimate the remaining distance.
- Refer to the unit tests provided in the repository as practical examples of how to integrate and use the algorithm in your application.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.