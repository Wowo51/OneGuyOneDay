
# SelfOrganizingMap Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Self-Organizing Map (SOM) is an unsupervised neural network algorithm that projects high-dimensional input data onto a two-dimensional grid while preserving the topological relationships of the data. The algorithm works by initializing a grid of neurons with random weights and then iteratively adjusting these weights based on the input samples using competitive learning. Over time, neurons become specialized to represent clusters in the data, making SOM effective for visualization, clustering, and feature extraction tasks.

To use the source code, compile the library with .NET (using Visual Studio or the dotnet CLI) and run the main application (in Program.cs) to observe the training process. Unit tests provided in the solution (using MSTest) validate the algorithm’s behavior. You can adjust parameters such as the grid dimensions, learning rate, and the number of iterations to suit your specific application requirements.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
