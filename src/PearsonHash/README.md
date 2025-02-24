
# PearsonHash Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The PearsonHash algorithm utilizes a fixed permutation table (derived from the AES S-box) to compute an 8‑bit hash value for a given input. It works by initializing an accumulator to zero, then iteratively updating that accumulator by XORing it with each byte of the input and using the result as an index into the permutation table. This produces a simple, fast hash function that is especially well-suited for small pieces of data such as short strings or byte arrays.

To use the provided source code, incorporate the PearsonHasher class into your project. Two overloads are available: one that accepts a byte array and another that accepts a string (which is converted to UTF-8 bytes). The accompanying MSTest unit tests ensure consistency and reliability. Simply build the library with .NET, run the tests, and integrate the PearsonHasher into your code as needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  