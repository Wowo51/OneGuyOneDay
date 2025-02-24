#SuccessiveOverRelaxation Library Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Successive Over Relaxation (SOR) algorithm is an iterative technique for solving systems of linear equations. It improves the convergence of the Gauss–Seidel method by applying a relaxation factor to each iteration’s update. This factor can accelerate convergence when chosen appropriately or help stabilize the iteration process.

To use the source code:
1. Instantiate the SORSolver class from the library.
2. Prepare your coefficient matrix (A) and right-hand side vector (b).
3. Specify the relaxation factor, the tolerance for convergence, and the maximum number of iterations.
4. Call the Solve method to compute the solution vector.
5. Review and run the included unit tests (using Microsoft’s testing framework) as examples of usage and to validate functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>