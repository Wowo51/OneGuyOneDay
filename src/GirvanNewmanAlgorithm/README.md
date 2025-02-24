# GirvanNewman Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Girvan-Newman algorithm is used for detecting communities in complex networks. It works by iteratively removing edges with the highest betweenness centrality to split the network into distinct communities. The process involves calculating the betweenness for every edge, identifying and removing the edge(s) that most strongly connect different parts of the graph, and repeating these steps until the graph divides into isolated clusters.

The provided SourceCode contains a complete C# implementation of this algorithm including classes for graph manipulation and unit tests using MSTest. To use the SourceCode, clone the repository, open the solution in Visual Studio or use the .NET CLI to build it, and run the unit tests to verify that everything is functioning as expected. This will allow you to explore, modify, or integrate the algorithm into your own projects while ensuring its reliability through the available tests.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>