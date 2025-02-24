# Gaussian Elimination Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Gaussian Elimination is a method for solving systems of linear equations by systematically eliminating variables. The algorithm transforms the system's augmented matrix into an upper triangular form, which allows for back substitution to find the solution. It employs techniques such as partial pivoting to enhance numerical stability, ensuring reliable results even in cases where the coefficients might cause numerical issues.

Usage:
- Add the GaussianEliminationSolver class to your project.
- Call the Solve method by passing in your coefficient matrix and constant vector.
- Check the returned solution vector; an empty array indicates either a singular matrix or a dimension mismatch.
- Refer to the provided unit tests as examples of how to handle unique solution cases, singular matrices, and input errors.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.