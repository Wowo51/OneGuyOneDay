# MiserAlgorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The MiserAlgorithm implements a recursive Monte Carlo integration approach using adaptive stratified sampling to improve accuracy in evaluating multi-dimensional integrals. The algorithm estimates the integration error in different subregions of the domain and recursively subdivides those regions with higher error estimates. This adaptive sampling optimizes the allocation of computational resources, yielding accurate results with fewer samples.

To use the SourceCode, include the library in your C# project, instantiate the MiserIntegrator class, and call its Integrate method with a delegate representing the function to integrate along with appropriate lower and upper bounds. The project is structured as a pure C# solution with comprehensive unit tests using MSTest. The included documentation and test cases make it easy to integrate and validate the functionality in your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>