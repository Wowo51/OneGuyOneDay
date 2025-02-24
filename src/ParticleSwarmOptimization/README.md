# Particle Swarm Optimization Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Particle Swarm Optimization (PSO) is an efficient optimization algorithm inspired by the social behaviors of bird flocking and fish schooling. It works by initializing a swarm of particles, each representing a potential solution, which then iteratively adjusts its position in the search space based on individual experience and the collective knowledge of the swarm.

To use the SourceCode, instantiate the ParticleSwarmOptimizer with the desired parameters such as the number of particles, problem dimensions, search bounds, number of iterations, inertia weight, cognitive coefficient, and social coefficient. Then call the Optimize method by passing your objective function—a function that accepts an array of doubles representing a solution and returns a fitness value. The algorithm returns an OptimizationResult containing the best position achieved and its associated fitness score.

Unit tests are provided to demonstrate usage and validate the optimizer on benchmark functions like the sphere function and quadratic optimization. These tests serve as examples to help integrate and adapt the library into your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
