
# Quadratic Sieve Factorization Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The QuadraticSieve algorithm is a highly efficient method for integer factorization. It works by searching for congruences of squares modulo the target number to reveal non-trivial factors. The algorithm follows these key steps:
- Select a factor base consisting of small primes for which the target number is a quadratic residue.
- Compute values of the form x² - n for successive integers x.
- Factor these values over the factor base, recording the exponents modulo 2.
- Solve a system of linear equations over GF(2) to find dependencies among the exponent vectors.
- Use the discovered dependencies to form a congruence of squares and compute a non-trivial factor via the greatest common divisor.

To use the SourceCode:
1. Compile the project using .NET 9.0.
2. Run the provided unit tests in the QuadraticSieveTest project to verify functionality.
3. Integrate the library into your solution by referencing the project or its compiled assembly.
4. Invoke the QuadraticSieveAlgorithm.Factor(n) method with your target integer to factor it.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
