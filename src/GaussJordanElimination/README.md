
# GaussJordanElimination Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Gauss Jordan Elimination is a fundamental algorithm for solving systems of linear equations and for computing the reduced row echelon form (RREF) of a matrix. It works by performing elementary row operations such as swapping rows, scaling rows to create leading ones, and eliminating other entries in the pivot columns. This process transforms the matrix into a form where each leading entry is 1 and is the only non-zero value in its column, thereby simplifying the system for analysis or solution.

Using the provided SourceCode:
- Include the GaussJordanElimination project in your C# solution.
- Call the static method GaussJordanSolver.ReduceToRREF to convert your input 2D array (matrix) into its RREF.
- The implementation comes with comprehensive unit tests (found in the GaussJordanEliminationTest project) to verify correctness under various scenarios.
- Refer to the project files and the unit tests for examples on handling square, non-square, and singular matrices.
- The library is self-contained and designed for easy integration into your projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
  