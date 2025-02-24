
# Espresso Heuristic Minimizer Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The EspressoHeuristicMinimizer algorithm provides a robust method for minimizing Boolean expressions using a heuristic approach. It operates by iteratively merging Boolean cubes to identify prime implicants and then selecting an optimal subset that fully covers the original Boolean terms. This approach efficiently handles both small and large sets of Boolean cubes, reducing complexity while preserving logical accuracy.

Usage:
1. Add a reference to the EspressoHeuristicMinimizer library in your C# project.
2. Create BooleanCube instances to represent your Boolean terms.
3. Use the GetPrimeImplicants method to extract prime implicants.
4. Call the Minimize method to obtain a minimal cover that satisfies your Boolean function.
5. Run the provided MSTest unit tests to validate the implementation.

The source code is organized into distinct modules for representing Boolean cubes, executing merge and cover operations, and implementing the overall minimization algorithm. Its clean design and comprehensive testing make it easy to integrate, maintain, and extend.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  