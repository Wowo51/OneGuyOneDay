
# NestedSamplingAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

NestedSamplingAlgorithm implements the nested sampling technique, a computational method for Bayesian model comparison. It iteratively refines a collection of "live" samples drawn from the prior distribution, replacing the sample with the lowest likelihood with one sampled from the prior that meets a specified likelihood threshold. This method gradually accumulates evidence while ensuring that the contributions from lower likelihood regions are minimized. The algorithm relies on two key interfaces: ILikelihoodEvaluator to compute the likelihood of a given sample, and IPrior to generate samples from the prior distribution.

To use the SourceCode, integrate the NestedSamplingAlgorithm project into your solution. Implement the ILikelihoodEvaluator and IPrior interfaces to tailor the algorithm to your specific model. Build the solution with .NET 9.0 and run the provided MSTest unit tests (using either the dotnet test command or Visual Studio's Test Explorer) to ensure proper functionality. The project is distributed under the MIT license, making it free for open source use.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
