
# Verlet Integration Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Verlet Integration algorithm is a numerical integration technique that computes a particle's new position based on its current and previous positions along with its acceleration. This method is particularly suited for physics simulations where energy conservation is essential. The algorithm uses the formula:

  NewPosition = 2 * CurrentPosition - PreviousPosition + Acceleration * (dt * dt)

This implementation demonstrates how to simulate particle motion under forces, such as gravity, by updating the position of a particle over discrete time steps. The SourceCode is organized into concise components: 
- Particle: Represents an object with spatial attributes.
- Vector2D: Provides basic 2D vector arithmetic.
- VerletIntegrator: Implements the integration routine that uses prior states to compute the new state.

To use the SourceCode, compile the C# project and run the provided unit tests to verify the correctness of the integration algorithm. The code is structured to allow easy modifications for various simulation scenarios or force applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
