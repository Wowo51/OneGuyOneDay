
# LPBoost Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

LPBoost is a boosting algorithm that leverages linear programming techniques to compute an optimal margin. By using the median of the training data, it minimizes the absolute deviation loss function, providing a simple yet effective approach to boosting.

How to Use:
- Add a reference to the LPBoost library in your C# project and include the LPBoost namespace.
- Create an instance of the LPBoostAlgorithm class.
- Use the Train method to supply your training data (an array of doubles), which computes the optimal margin.
- Call the Predict method with new input data to generate predictions based on the computed margin.

The provided source code includes:
• LPBoostAlgorithm.cs – Implements the core training and prediction logic.
• LinearProgramSolver.cs – Contains a method for computing the median as the optimal margin.
• Program.cs – Provides an example entry point to demonstrate training.
• LPBoostTest project – A comprehensive suite of unit tests using Microsoft's MSTest framework.
• Project files and additional resources to compile and run the solution using Visual Studio.

Compile, test, and integrate the LPBoost algorithm into your projects with ease. The included unit tests ensure robust, reliable performance under different scenarios.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)
<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
</br>
Authored by Warren Harding. AI assisted.
  