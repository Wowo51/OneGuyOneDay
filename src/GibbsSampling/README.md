# GibbsSampling Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Gibbs Sampling is a Markov chain Monte Carlo algorithm designed to generate samples from a complex joint probability distribution by iteratively sampling each variable from its conditional distribution. It is particularly useful when direct sampling from the joint distribution is impractical. By updating one variable at a time while keeping the others fixed, the algorithm eventually produces a sequence of samples that approximates the target distribution.

To use the SourceCode:
1. Compile the project in a .NET compatible environment.
2. Refer to the Program.cs file for an example of initializing the GibbsSampler with an initial state and a collection of conditional sampling functions.
3. Review the unit tests in the GibbsSamplingTest project to understand various usage scenarios and edge cases.
4. Customize the conditional samplers to suit your specific probability model.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.