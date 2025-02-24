# BfgsMethod Library Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## BfgsMethod Algorithm Help

The BFGS Method is a powerful quasi-Newton optimization algorithm used for finding the minimum of nonlinear functions. Instead of computing the full Hessian matrix, it builds up an approximation using gradient information from successive iterations. This approach allows for efficient optimization even when the computation of second derivatives is too costly or impractical.

### How to Use the SourceCode

The source code is organized into clearly separated components:
- BfgsOptimizer.cs: Implements the BFGS optimization routine.
- IObjectiveFunction.cs: Defines an interface for objective functions, requiring methods to compute the function’s value and its gradient.
- MathUtils.cs: Provides utility functions for performing essential mathematical operations such as dot products, vector addition, and matrix manipulations.
- Unit Tests: The BfgsMethodTest project contains tests that verify the optimizer’s functionality using various quadratic functions.

To use the library, include the BfgsMethod project in your solution, implement the IObjectiveFunction interface to model your specific optimization problem, and then create an instance of BfgsOptimizer. Call the Optimize method with an initial guess, along with tolerance and maximum iteration parameters. You can also run the provided unit tests to validate the implementation before integrating it into your project.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.