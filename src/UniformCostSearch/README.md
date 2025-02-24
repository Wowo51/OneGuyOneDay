
# Uniform Cost Search Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Uniform Cost Search is a graph search algorithm that finds the least-cost path from a given start node to a target goal node by expanding nodes in order of their cumulative cost. It uses a frontier to always select the next most promising node, ensuring that when a goal is reached, it has been reached via an optimal route.

To use the SourceCode:
1. Include the UniformCostSearch library in your project.
2. Invoke the FindPath method from the UniformCostSearchAlgorithm class with:
   • A starting state,
   • A goal test function to verify when the goal is reached,
   • A neighbor function that returns adjacent states along with their traversal costs.
3. Consult the unit tests in the UniformCostSearchTest project for practical examples of constructing graphs and validating results.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  