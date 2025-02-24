# PigeonholeSort Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## Overview

PigeonholeSort is an efficient sorting algorithm when the range of input numbers is not significantly larger than the number of elements. It works by determining the minimum and maximum values of the array to define a range, creating a bucket (or "pigeonhole") for each possible value, and then distributing the values into these buckets. Finally, the sorted array is reconstructed by collecting values from each pigeonhole in order.

## How It Works

1. Determine the minimum and maximum values in the input array.
2. Compute the range of values.
3. Allocate an array of pigeonholes corresponding to each distinct value within that range.
4. Traverse the input array, placing each element in its respective pigeonhole.
5. Reconstruct the sorted array by iterating over the pigeonholes sequentially.

## Using the Source Code

The source code provided is a pure C# implementation of the PigeonholeSort algorithm. To use it in your project:
- Include the PigeonholeSorter.cs file in your C# project.
- Add a reference to the library if needed.
- Call PigeonholeSorter.Sort(array) to sort your integer array.
- Run the accompanying unit tests to verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.