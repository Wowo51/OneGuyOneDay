# Levenshtein Coding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LevenshteinCoding algorithm calculates the edit distance between two strings using a dynamic programming approach. It constructs a matrix where each cell represents the minimal number of single-character edits required to change one substring into another. The algorithm handles null inputs by treating them as empty strings and returns the correct number of operations needed to transform the source string into the target string.

To use the source code, include the LevenshteinCoding library in your C# project, add a reference to its namespace, and call the ComputeDistance method from the LevenshteinCalculator class with your input strings. Unit tests are provided to demonstrate typical use cases and ensure its proper behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>