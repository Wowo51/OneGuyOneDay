# Lenstra Elliptic Factorization Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Lenstra Elliptic Factorization is an efficient algorithm for extracting nontrivial factors from a composite number by leveraging properties of elliptic curves. The algorithm works by randomly selecting an elliptic curve and a starting point on that curve, then computing multiples of that point. During these computations, if the process encounters a situation where a modular inverse cannot be computed (indicating a failure in the arithmetic), a nontrivial factor of the number is revealed. This approach is particularly effective when the composite number has relatively small factors.

To use the SourceCode, open the provided Visual Studio solution which includes projects for the library implementation and comprehensive MSTest unit tests. Run the interactive application (Program.cs) to factor an integer supplied at runtime, or execute the unit tests to verify functionality under various conditions, including handling of prime numbers, composite numbers, and edge cases in elliptic curve operations.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.