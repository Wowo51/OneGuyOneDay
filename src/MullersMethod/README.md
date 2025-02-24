# MullersMethod: A Quadratic Root-Finding Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

MullersMethod is a numerical algorithm for finding roots of equations, including those with complex solutions. It uses quadratic interpolation derived from three initial approximations to estimate a root. At each iteration, a quadratic polynomial is constructed from the three current points, and its intersection with the x-axis provides a new approximation. The process repeats until the desired tolerance is achieved.

To use the SourceCode:
1. Include the MullersMethod project in your solution.
2. Reference the MullersMethod namespace in your C# project.
3. Call MullersMethodSolver.FindRoot(), providing a function delegate and three initial guesses.
4. Refer to the unit tests in the MullersMethodTest project for practical examples and handling of edge cases.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.