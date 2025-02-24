# Pohlig Hellman Discrete Logarithm Solver

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Pohlig-Hellman algorithm provides an efficient method for computing discrete logarithms when the group order factors into small primes. It breaks the overall problem into smaller subproblems for each prime factor and then recombines the results using the Chinese Remainder Theorem (CRT).

Usage:
- Supply the generator (g), target value (h), and prime modulus (p) as inputs.
- Call the ComputeDiscreteLog method to compute the discrete logarithm.
- The algorithm returns -1 if no valid solution exists.
- Refer to the SourceCode (which includes project files and unit tests) to understand the implementation and verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.