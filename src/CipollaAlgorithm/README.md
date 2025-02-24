# Cipolla Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT license.

Cipolla's Algorithm is a number theoretic method used to compute square roots modulo a prime number. It works by first checking whether a square root exists using the Legendre symbol and then constructing a quadratic extension field to perform the necessary computations. The algorithm finds a quadratic non-residue to define the extension and employs modular exponentiation to obtain the square root.

To use the SourceCode, reference the CipollaAlgorithm library in your C# project. The primary method, ComputeSquareRoot(n, p) in the CipollaCalculator class, takes an integer n and a prime p, and returns a valid square root modulo p or null if none exists. The accompanying unit tests demonstrate how to integrate and verify the functionality of the algorithm in diverse scenarios.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.