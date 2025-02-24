# JumpSearch Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The JumpSearch algorithm is designed for efficient searching in sorted arrays. It divides the array into blocks of size √n and examines the last element of each block to determine if the target could be in that block. Once the appropriate block is identified, a linear search is performed within it to locate the target efficiently.

Usage:
- Include the JumpSearch library in your project.
- Call the Search method with your sorted array and the target value.
- The provided SourceCode is organized as a Visual Studio solution with a main project and a MSTest unit test project.
- Open the solution in Visual Studio, restore the required packages, build the projects, and run the tests to verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.