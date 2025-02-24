# WinnowAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The WinnowAlgorithm implements a binary classifier using a multiplicative weight‑update scheme. It maintains a set of weights for each feature and makes predictions by summing the weights where the corresponding features are active, then comparing the sum to a threshold. If the prediction is incorrect, the algorithm updates the weights: increasing them when the actual label is true and the prediction was false, and decreasing them when the actual label is false but the prediction was true.

Usage:
1. Initialize the WinnowClassifier with the number of features, a threshold, and an alpha (multiplicative factor).
2. Call the Predict method with an array of booleans representing feature activations to determine a classification.
3. Use the Update method to adjust weights when the predicted label differs from the actual label.
4. Retrieve the current weight values with the GetWeights method for inspection or debugging.

The source code is organized into multiple projects:
• The main library (WinnowAlgorithm.csproj) contains the classifier implementation.
• The test project (WinnowAlgorithmTest) contains several MSTest unit tests to ensure proper behavior.
• Additional files such as LICENSE.md and solution/project files support building and testing.

To use the source code, compile the solution using .NET 9.0 and run the provided unit tests to verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>