# HopfieldNet Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The HopfieldNet algorithm is a form of recurrent neural network designed for associative memory. It employs a symmetric weight matrix updated via Hebbian learning to store patterns. When provided with an input— which may be noisy or incomplete—the network iteratively updates its state until it converges to a stable fixed point that ideally corresponds to one of the trained patterns.

To use the SourceCode, compile the solution using Visual Studio or the .NET CLI. The project comes with comprehensive MSTest unit tests that validate both the training and recall processes under various conditions. Running these tests will help ensure the algorithm behaves as expected before integrating the library into your own applications for pattern recognition or error-correcting memory tasks.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.