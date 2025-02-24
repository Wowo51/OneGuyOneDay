
# Grabcut Graph Cuts Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The GrabcutGraphCuts algorithm implements a graph-cut based segmentation method to separate an image into foreground and background regions. It builds a graph where each pixel is represented as a node, and edges are assigned based on data fidelity and smoothness constraints. The algorithm computes the maximum flow and, correspondingly, the minimum cut on this graph to determine the optimal segmentation.

To use the SourceCode, compile the solution using Visual Studio or the .NET CLI. The solution is structured in a modular way, with classes such as GraphCutSolver, GraphCutMinimizer, and MaxFlowCalculator implementing different parts of the algorithm. Unit tests are provided to validate segmentation on images of various sizes. This design makes it easy to understand, extend, or integrate the code into your own image processing projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
