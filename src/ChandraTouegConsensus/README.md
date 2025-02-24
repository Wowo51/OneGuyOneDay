# ChandraToueg Consensus Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Chandra-Toueg consensus algorithm is a distributed protocol designed to help a set of processes agree on a common value despite failures. It works by electing a coordinator from the pool of live processes (based on the lowest process ID) and then executing a two-phase process. In the first phase, the coordinator collects the current estimates from all active processes. In the second phase, it broadcasts the chosen value (either the first received proposal or its own estimate if no proposals were received) back to all live processes. This process repeats for a fixed number of rounds until consensus is reached or the rounds are exhausted.

Usage:
- Compile the solution using .NET 9.0.
- Run the MSTest unit tests provided to validate the algorithm.
- Integrate the library into your project where distributed consensus is required.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>