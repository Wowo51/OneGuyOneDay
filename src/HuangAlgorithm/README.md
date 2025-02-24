
# HuangAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

HuangAlgorithm is an implementation of a distributed termination detection algorithm. It simulates distributed processes communicating via messages. The algorithm detects termination by checking that all processes have become inactive and that the total difference between sent and received messages is zero.

How to use the SourceCode:
1. Build the solution using the provided Visual Studio solution or the dotnet CLI targeting .NET 9.0.
2. Run the Program to see the simulation in action; it sets up distributed processes, runs the simulation, and displays each process's status.
3. Execute the MSTest unit tests from the HuangAlgorithmTest project to verify that the algorithm works as expected.
4. Review the source files to understand the implementation of distributed message passing and termination detection.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  