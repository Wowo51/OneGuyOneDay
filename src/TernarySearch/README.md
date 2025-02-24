
# Ternary Search Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Ternary Search algorithm is a divide and conquer method for finding the optimum (maximum or minimum) of a unimodal function. It works by splitting the interval into three equal parts and comparing function values at two midpoints, gradually converging on the optimum value.

Usage:
- To find a maximum, feed a unimodal function and a range to the FindMaximum method.
- To find a minimum, use the FindMinimum method similarly.
- Validation is included; if a null function is provided, the method returns the left interval endpoint.

The provided source code includes comprehensive unit tests (using MSTest) and project files to help integrate and test the library in your development environment.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  