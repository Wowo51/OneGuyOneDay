
# Baby Giant Step Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Baby Giant Step algorithm is an efficient method for solving the discrete logarithm problem, where the goal is to find an exponent x such that g^x ≡ h mod p. This method splits the process into a "baby-step" phase, which precomputes small powers of g, and a "giant-step" phase, which leaps forward in larger increments to find a match. This balanced approach significantly reduces the search space compared to a brute-force search.

To use the source code, open the solution file in Visual Studio or run it using the dotnet CLI. The repository is organized into separate projects for the main algorithm and its unit tests. Simply call the DiscreteLogSolver.FindDiscreteLog method with appropriate parameters, and refer to the unit tests for usage examples. The code is modular, well-documented, and ready for integration into your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
