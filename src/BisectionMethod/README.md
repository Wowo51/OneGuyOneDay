# Bisection Method Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Bisection Method algorithm is an iterative technique for finding a root of a continuous function. It begins by ensuring the function values at the endpoints of an interval have opposite signs. The method then repeatedly calculates the midpoint of the interval and evaluates the function at that point. Based on the sign of the function at the midpoint, the algorithm selects the subinterval that contains the root, halving the interval each time until the approximation is within a specified tolerance.

How to use the SourceCode:
- Incorporate the library into your project.
- Call the BisectionSolver.FindRoot method with your function delegate, the lower and upper bounds of the interval, and the desired tolerance (with an optional parameter for maximum iterations).
- Refer to the included unit tests in the BisectionMethodTest project for usage examples and to verify the algorithm’s correctness.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)

Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.