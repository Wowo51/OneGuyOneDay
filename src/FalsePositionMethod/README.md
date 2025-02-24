# False Position Method Implementation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

False Position Method Algorithm:
The False Position Method, also known as the Regula Falsi method, is a bracketing technique used to find a root of a continuous function. Given two endpoints a and b where the function values f(a) and f(b) have opposite signs, the method computes an approximation of the root by linear interpolation using the formula:
  
   c = (a * f(b) - b * f(a)) / (f(b) - f(a))
  
The function is then evaluated at c. Depending on the sign of f(c), the algorithm replaces either a or b with c so that the new interval still brackets the root. This process repeats until the value of f(c) is within an acceptable tolerance or a maximum number of iterations is reached.

Using the SourceCode:
The provided source code includes a complete C# implementation of the False Position Method along with comprehensive unit tests. To use the library, include the FalsePositionMethod project in your solution and call the 'FindRoot' method from the FalsePositionMethodSolver class. You need to supply:
- A function delegate representing the mathematical function.
- Two endpoints (a and b) that bracket a root.
- A tolerance value to determine convergence.
- A maximum iteration count to prevent infinite loops.

The unit tests offer practical examples and demonstrate edge-case handling. Build the solution using Visual Studio or the .NET CLI (targeting .NET 9.0) to run and verify the tests.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>