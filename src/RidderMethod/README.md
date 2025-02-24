# RidderMethod Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Ridder's method is a robust bracketing algorithm for finding the root of a continuous function. It begins with an interval where the function values at the endpoints have opposite signs. By calculating a midpoint and applying an exponential scaling adjustment, the algorithm refines the interval to converge quickly towards the true root. Iterations continue until the difference between endpoints falls below a specified tolerance or a maximum number of iterations is reached.

To use the SourceCode, incorporate the RidderMethodSolver class into your C# project. Call its Solve method by passing:
- A delegate (Func<double, double>) representing your function.
- Two endpoints that bracket the root (ensuring the function values are of opposite signs).
- A tolerance value to set the desired precision.
- A maximum iteration count to avoid infinite loops.

The provided unit tests demonstrate common scenarios such as solving quadratic, linear, and trigonometric functions. They serve as practical examples of how to integrate and verify the algorithm in your application.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.