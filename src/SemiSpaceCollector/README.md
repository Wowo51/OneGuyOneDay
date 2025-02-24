# SemiSpaceCollector Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The SemiSpaceCollector algorithm implements an early copying garbage collector. It divides memory into two equal regions: the from-space and the to-space. New objects are allocated in the active space (typically the from-space), and when that space becomes full, the collector copies all live objects to the inactive space. This copying frees up the old space for future allocations and swaps the roles of the spaces for subsequent allocations.

To use the source code, include the project in your solution and build it with the .NET SDK. The repository contains both the implementation (in SemiSpaceCollector.cs) and comprehensive unit tests (in SemiSpaceCollectorTests.cs) demonstrating allocation behavior, garbage collection, and error handling. Running the provided tests with Microsoft's unit testing framework will validate the functionality of the algorithm.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.