# Simons Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

SimonsAlgorithm is a classical simulation of Simon's quantum algorithm that leverages quantum superposition and interference to uncover a hidden secret in a two-to-one function. The algorithm works by evaluating the function on random inputs, identifying collision pairs, and formulating a system of linear equations over GF(2) that ultimately reveals the secret.

To use the SourceCode, integrate the library into your C# project and reference the SimonsAlgorithm module. Call the FindSecret method with the appropriate bit-length and a custom oracle function that satisfies the two-to-one mapping property. Detailed unit tests are provided to demonstrate various use cases and ensure correct behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.