#ElevatorAlgorithmDocumentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

ElevatorAlgorithm is a disk scheduling algorithm that simulates the operation of an elevator by organizing a list of service requests based on the current position and the chosen direction of travel. When moving upward, the algorithm processes all requests at or above the initial position in ascending order before handling the remaining requests in descending order; when moving downward, the order is reversed. This method minimizes overall travel distance and wait times, offering an efficient scheduling strategy for both elevator and disk operations.

To use the SourceCode, open the solution file (ElevatorAlgorithm.sln) in Visual Studio or use the .NET CLI to build the project. Run the accompanying unit tests in the ElevatorAlgorithmTest project to verify functionality. The key logic is encapsulated within the ElevatorScheduler class’s Schedule method, which partitions and orders requests based on the provided parameters. Customize the initial position, direction, and maximum range as needed for your specific scenario.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
