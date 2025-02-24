# BruunsFftAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Bruun's FFT Algorithm is an efficient method for computing the Discrete Fourier Transform (DFT) by leveraging polynomial division techniques. The algorithm evaluates a polynomial at specific complex roots of unity to obtain frequency components. It handles non-power-of-two input lengths by padding the coefficient array and uses both direct evaluation (for frequencies 0 and m/2) and polynomial division remainders (for intermediate frequencies).

To use the SourceCode:
1. Open the solution file located at D:\Code2025\GithubRepos\OneGuyOneDay\src\BruunsFftAlgorithm\BruunsFftAlgorithm.sln using Visual Studio or the dotnet CLI.
2. Build the project targeting .NET 9.0.
3. Run the application through the Program.cs or execute the unit tests in the BruunsFftAlgorithmTest project to verify functionality.
4. Review the source files such as BruunsFft.cs to understand the algorithm’s implementation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.