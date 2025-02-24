# GerchbergSaxtonAlgorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## About the Gerchberg-Saxton Algorithm

The Gerchberg-Saxton algorithm is a phase retrieval method that iteratively refines the phase estimate of an optical wavefront using intensity measurements in both the object and Fourier domains. By alternating between these domains and enforcing the respective amplitude constraints, the algorithm converges on an accurate representation of the wavefront’s phase.

### How to Use This Source Code

- Build the solution using the provided .csproj files targeting the .NET 9.0 framework.
- Review the implementation in FourierTransform.cs and GerchbergSaxton.cs to understand the discrete Fourier transform operations and the iterative phase retrieval process.
- Run the MSTest unit tests in the GerchbergSaxtonAlgorithmTest project to validate functionality and convergence.
- Integrate the library into your projects without any external dependencies beyond the standard .NET libraries.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.