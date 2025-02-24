# AC3 Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

AC3Algorithm is an implementation of the AC-3 (Arc Consistency) algorithm for solving constraint satisfaction problems (CSPs). It systematically enforces consistency across variables by eliminating domain values that do not satisfy the binary constraints. The algorithm works by iteratively revising arcs between variables until no more inconsistencies remain, ensuring that every remaining value can be paired successfully with some value in each neighboring variable’s domain.

To use the SourceCode, integrate the AC3Algorithm library into your C# project. Begin by creating a CSP instance and add variables with their respective domains. Define constraints using delegates or lambda expressions, and then invoke the AC3.EnforceArcConsistency method to prune the domains based on these constraints. The provided unit tests and project files in the repository serve as practical examples for proper integration and usage.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>