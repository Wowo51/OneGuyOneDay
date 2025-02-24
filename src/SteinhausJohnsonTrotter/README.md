# SteinhausJohnsonTrotter Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Steinhaus–Johnson–Trotter algorithm generates all permutations of a sequence by iteratively swapping adjacent elements. It works by finding the largest mobile element—that is, an element that can move in the direction it is currently pointing—and then swapping it with the adjacent element in that direction. After each swap, the directions of all elements larger than the swapped element are reversed. This process continues until no mobile elements remain, thereby producing a complete set of permutations in a systematic and efficient manner.

To use the SourceCode:
- Review the SteinhausJohnsonTrotterAlgorithm.cs file to understand the implementation of the algorithm.
- Run the Program.cs file to see a demonstration of the algorithm in action.
- Explore the unit tests in the SteinhausJohnsonTrotterTest project for examples and validation of the functionality.
- Use the provided solution and project files to build and test the library within a .NET 9.0 environment.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>