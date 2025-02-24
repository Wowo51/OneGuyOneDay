
# MillerRabinTest Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Miller-Rabin Primality Test is a probabilistic algorithm designed to determine whether a given number is prime. It works by expressing n - 1 as d * 2^s, where d is odd. In each iteration, a random base a is chosen from the range [2, n - 2] and the algorithm computes a^d mod n. If the result is 1 or n - 1, the test passes; otherwise, the result is squared repeatedly (up to s - 1 times) to check for n - 1. If n - 1 is never reached, the number is declared composite. Repeating the test multiple times reduces the chance of a false positive, making it effective even for large numbers.

Usage:
- Include the MillerRabinTest project in your solution.
- Invoke MillerRabin.IsProbablyPrime by providing a BigInteger number and the desired number of iterations.
- Validate the functionality using the provided MSTest unit tests.
- Integrate this self-contained library into any .NET project with ease.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
