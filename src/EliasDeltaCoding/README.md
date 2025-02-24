# Elias Delta Coding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Elias Delta Coding is a universal method for encoding positive integers efficiently. The algorithm works by determining the binary representation of an integer and then encoding the length of that representation using Elias Gamma Coding. The final delta code is constructed by concatenating the gamma-coded length with the binary representation of the integer minus its leading bit. This approach provides a compact, self-delimiting code that is useful for data compression and transmission.

To use the source code:
1. Add the provided C# library to your project.
2. Reference the library and call the static methods in the EliasCoding class: GammaEncode/GammaDecode, DeltaEncode/DeltaDecode, and OmegaEncode/OmegaDecode.
3. Review the accompanying unit tests to understand proper usage and error handling.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>