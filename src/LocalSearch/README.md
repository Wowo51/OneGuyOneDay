# Local Search Algorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Local Search algorithm is a metaheuristic used for optimization. It starts with an initial solution and iteratively explores its neighboring solutions to find an improved objective value. At each step, it selects the best neighbor according to a provided objective function and an optional comparison function (which supports both minimization and maximization). The process continues until no neighbor can further improve the solution or until a maximum iteration limit is reached.

How to use the SourceCode:
- Integrate the library into your project by referencing the provided project files.
- The SourceCode is organized into separate files: the main algorithm implementation, unit tests, and project configuration files.
- Use the unit tests as a guide to understand different use cases, including handling null inputs, verifying correct behavior when no better neighbor exists, and checking both minimization and maximization scenarios.
- Customize and extend the algorithm for your own optimization problems as needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.