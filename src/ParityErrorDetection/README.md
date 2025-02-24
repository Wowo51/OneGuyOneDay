
# Parity Error Detection Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ParityErrorDetection algorithm computes the even parity bit for a given byte by counting the number of set bits and returning 1 if the count is odd, ensuring that the total number of 1s (including the parity bit) is even. This simple error detection method is effective at catching single-bit errors in data transmission. In addition to computing the parity, the library provides functions to validate a provided parity bit against the computed value and to append a parity byte to an array of data using XOR operations.

To use the SourceCode, include the ParityErrorDetection project in your solution and reference the ParityUtility class in your code. Use the static methods provided to compute, append, and validate parity bits for your data. The included MSTest unit tests offer examples of usage and help ensure that functionality remains intact with future modifications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
