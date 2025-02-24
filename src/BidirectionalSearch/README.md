
# Bidirectional Search Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Bidirectional Search algorithm implemented in this library performs a simultaneous search from both the start and the goal nodes within a graph. By expanding from both directions, the algorithm can often find the shortest path more efficiently than traditional unidirectional search methods. It uses queues to manage the frontier and dictionaries to track parent nodes for reconstructing the path. The search terminates once a meeting node is found by both the forward and backward explorations, at which point the algorithm constructs the complete shortest path.

To use the SourceCode, compile the project in any .NET 9.0 compatible environment, such as Visual Studio or the .NET CLI. The repository includes a demonstration application and unit tests to validate the algorithm. You can run the application to see an example of finding the shortest path in a sample graph, or modify the Graph and algorithm implementations to meet your own project's requirements.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  