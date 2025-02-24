# Yarrow Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## Help Content

The YarrowAlgorithm is a cryptographically secure pseudorandom generator implemented in C#. It leverages HMACSHA256 and an internal counter to produce high-quality random numbers. By combining system time and GUIDs as part of its seed generation, it ensures both unpredictability and reproducibility when needed. The design also supports reseeding through entropy addition, making it suitable for long-running applications where maintaining randomness quality is critical.

### How to Use the Source Code

- Include the source files from the library directly into your C# project.
- Choose between the two main classes: use YarrowRandom for basic pseudorandom number generation or YarrowGenerator if you need enhanced entropy management and automatic reseeding.
- To generate reproducible results, initialize the generator with a specific seed; otherwise, let the algorithm create a seed from system values.
- Review the provided unit tests in the YarrowAlgorithmTest project for examples on how to initialize the generators, add entropy, and retrieve random numbers.
- Integrate the library into your project, and leverage its modular design for secure, efficient random number generation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>