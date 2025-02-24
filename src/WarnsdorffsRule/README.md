# WarnsdorffsRule Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The WarnsdorffsRule algorithm is a heuristic for solving the Knight's Tour problem by selecting moves that minimize the number of subsequent moves. It evaluates each potential knight move based on the number of onward moves available, thereby reducing the risk of hitting a dead end. This approach increases the likelihood of completing a tour that visits every square exactly once on a chess board.

To use the source code, include the library in your C# project, reference the KnightTourSolver class, and invoke the Solve method with your desired board size and starting coordinates. The project comes with a sample Program and a suite of MSTest unit tests, which help illustrate how the algorithm works and verify its implementation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.