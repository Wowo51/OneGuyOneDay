# Introduction to Karplus Strong Synthesis

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Karplus-Strong synthesis algorithm is a physical modeling method used to simulate the sound of a plucked string. The algorithm begins by initializing a buffer with random noise to mimic the initial excitation of a string. It then repeatedly applies an averaging and decay process over the buffer values, which produces a progressively damped waveform that closely resembles the natural vibration of a plucked string.

To use the source code, compile the provided C# projects targeting .NET 9.0. The solution includes a core library, a demo application (Program.cs) that generates and outputs audio samples, and a comprehensive set of unit tests to ensure correct functionality. Simply build the solution and run the demo application to experience the plucked string simulation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.