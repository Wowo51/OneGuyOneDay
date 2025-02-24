
# ItpMethod Algorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

ItpMethod is a robust root-finding algorithm that blends bisection, secant, and interpolation techniques to locate the roots of continuous functions. The algorithm maintains a bracketing interval that is iteratively refined, ensuring that the desired root is always enclosed. Often achieving superlinear convergence, it is both efficient and reliable.

How to use the SourceCode:
- Include the ItpMethodSolver.cs class in your project.
- Call the static Solve method with a function delegate, the interval endpoints, a tolerance value, and a maximum number of iterations.
- Run the accompanying unit tests in the ItpMethodTest project to verify functionality and integration.
- Use the provided solution and project files as examples for incorporating the algorithm in your own development workflow.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
