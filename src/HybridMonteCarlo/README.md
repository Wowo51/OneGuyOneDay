# HybridMonteCarlo Sampling Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Hybrid Monte Carlo is a robust algorithm that leverages Hamiltonian dynamics combined with Monte Carlo sampling to efficiently explore complex, high-dimensional probability distributions. It simulates the movement of particles in a potential energy field by balancing potential and kinetic energy. The method proposes new states by performing a deterministic simulation via the leapfrog integrator and then stochastically accepts or rejects the proposals to maintain detailed balance.

To use the source code, open the provided Visual Studio solution file and build the project. The solution includes the core HybridMonteCarlo library and its accompanying unit tests. Run the tests using Microsoft's unit testing framework to verify functionality. You can integrate the library into your own projects by referencing the compiled assembly or by including the source files directly.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>