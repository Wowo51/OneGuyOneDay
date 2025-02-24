
# Buddy Memory Allocation Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Buddy Memory Allocation is an efficient memory management algorithm that minimizes fragmentation by dividing memory into blocks sized in powers of two. When a request is made, the allocator splits larger blocks into two equal "buddy" blocks until the smallest sufficient block is obtained. When blocks are freed, it checks if the adjacent buddy is also free and merges them, optimizing memory utilization.

Usage: To use the source code, include the BuddyAllocator class in your C# project. Build the solution using the provided Visual Studio solution file and run the accompanying MSTest unit tests to verify functionality. The code is designed for easy integration and can be extended as needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
