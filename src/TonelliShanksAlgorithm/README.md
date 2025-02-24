# Tonelli Shanks Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Tonelli Shanks algorithm is an efficient method for finding square roots modulo a prime number. It handles the case when the prime p is congruent to 3 modulo 4—using a simple exponentiation for quick computation—as well as the more general scenario when p is congruent to 1 modulo 4. The algorithm first checks trivial conditions such as whether the input is zero or not a quadratic residue modulo p. For nontrivial cases, it decomposes p – 1 into a power of two and an odd factor, then iteratively finds a quadratic non-residue to compute the square root modulo p.

To use the SourceCode, add the TonelliShanks class to your C# project and reference it as needed. Call the public method SquareRootModuloPrime(a, p) with your chosen values, where ‘a’ is the integer whose square root modulo the prime ‘p’ is to be computed. Unit tests provided in the solution (using MSTest) illustrate various usage scenarios. Simply compile the project and run the tests to verify that the functionality works as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>