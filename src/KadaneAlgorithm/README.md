
# Kadane Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Kadane's algorithm efficiently computes the maximum subarray sum in an array by iteratively calculating the maximum sum ending at each index. By comparing the current element with the sum of the current element and the previous subarray sum, it determines whether to continue the existing subarray or start a new one. This approach works effectively with arrays containing both positive and negative numbers.

To use the source code:
1. Open the solution file in Microsoft Visual Studio or use the dotnet CLI to build the project.
2. Run the provided unit tests to verify the algorithm's correctness.
3. Reference the compiled library in your project and call the FindMaxSubArray method by passing an integer array. The method returns a KadaneResult object with the maximum sum, start and end indices, and the corresponding subarray.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
  