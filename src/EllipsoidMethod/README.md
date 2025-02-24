# EllipsoidMethod Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The EllipsoidMethod algorithm is a powerful iterative procedure designed for solving convex feasibility and optimization problems. It works by iteratively adjusting an ellipsoid that encloses a convex set. In each iteration, the algorithm queries a separation oracle with the current center of the ellipsoid. If the oracle returns null, it signifies that the center is feasible with respect to all constraints, and the algorithm terminates. Otherwise, the oracle provides a subgradient that acts as a cutting plane to refine the ellipsoid's shape. This process continues until a satisfactory solution is found or a maximum number of iterations is reached.

How to use the SourceCode:
- The SourceCode includes an implementation of the EllipsoidSolver class which encapsulates the algorithm.
- The MatrixHelper class supports essential matrix operations (e.g., multiplication, inner products, matrix scaling, and outer products) needed for the updates.
- The Program class demonstrates how to set up a convex feasibility problem with specific constraints and how to invoke the solver using a separation oracle.
- A comprehensive suite of unit tests is provided using MSTest to validate the functionality of both the algorithm and the helper classes.
- Simply compile the projects and run the tests or execute the Program to see the algorithm in action.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.