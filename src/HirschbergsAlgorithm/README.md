# HirschbergsAlgorithm Library Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

This library implements Hirschberg's algorithm, a divide-and-conquer approach to sequence alignment that efficiently computes the optimal alignment between two sequences with reduced memory usage. The algorithm splits one sequence and recursively computes alignment costs to merge results, ensuring an optimal alignment with minimal resource overhead.

To use the source code, include the HirschbergsAlgorithm project in your solution and add a reference to it from your application or test projects. Invoke the Align method with two sequences as input to receive the aligned sequences along with the computed cost. Unit tests provided in the HirschbergsAlgorithmTest project help verify the functionality and correctness of the implementation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>