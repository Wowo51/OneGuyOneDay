# Shortest Common Supersequence Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Shortest Common Supersequence algorithm computes the shortest string that contains all given sequences as subsequences. It works by identifying the longest common subsequence (LCS) between the input sequences and then merging the remaining parts around this common core. This method minimizes redundancy by only including the necessary characters from each sequence to preserve their order.

To use the SourceCode, simply call the SupersequenceSolver.ShortestCommonSupersequence method in your C# project. The function accepts two or more sequences and computes their supersequence iteratively. You can refer to the accompanying unit tests for examples of valid input and expected output. This library is designed to integrate seamlessly into your application with minimal setup.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>