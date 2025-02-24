
# Cutting Plane Method Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

CuttingPlaneMethod is an iterative algorithm for solving optimization problems by progressively refining the feasible region through the addition of linear constraints (cuts). The algorithm begins with an initial feasible region defined by lower and upper bounds. In each iteration, a candidate solution is computed as the midpoint of the current bounds. A new cut is then generated based on this candidate to update the lower bound and reduce the search space. This process is repeated until convergence is achieved within a specified tolerance.

Usage:
1. Initialize the feasible region with appropriate lower and upper bounds.
2. Call the CuttingPlaneSolver's Solve method to start the iterative process.
3. Monitor the console output for details on candidate solutions and applied cuts.
4. Refer to the provided unit tests to understand how to validate the solver’s behavior and ensure convergence.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
