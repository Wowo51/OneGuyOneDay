# Simulated Annealing Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Simulated Annealing is a probabilistic optimization algorithm inspired by the annealing process in metallurgy. It begins with a random solution and iteratively searches for an improved solution by exploring its neighbors. The algorithm sometimes accepts worse solutions based on a temperature-controlled probability, which decreases over time. This approach helps it escape local minima and increases the chance of finding a global optimum.

To use the source code, include the SimulatedAnnealing library in your C# project. Instantiate the SimulatedAnnealingAlgorithm class and configure parameters such as InitialTemperature, CoolingRate, and Iterations. Then call the Execute method to run the optimization process. The provided MSTest unit tests serve as examples and help ensure the code behaves as expected. Customize the algorithm settings as needed for your specific optimization challenges.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.