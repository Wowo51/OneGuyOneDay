# KargersAlgorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

KargersAlgorithm is a randomized (Monte Carlo) algorithm designed to approximate the minimum cut of a connected graph. The algorithm works by contracting edges randomly until only two vertices remain, at which point the remaining edges represent a potential minimum cut. Due to its probabilistic nature, running the algorithm multiple times increases the chances of finding the optimal cut.

To use the source code:
1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Build the project to compile the library and run the unit tests.
4. Integrate the KargersAlgorithm library into your C# project by referencing the compiled DLL or including the source files.
5. Call the FindMinCut method with your Graph object to compute the minimum cut of your graph.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>