# Binary Splitting Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The BinarySplitting algorithm uses a divide and conquer strategy to efficiently compute the sum of a numerical series. It recursively splits the summation range into two halves, computes partial sums for each half using a user-supplied term function, and then combines the results to form the final sum. This approach not only enhances performance for large-scale summations but also helps in maintaining numerical stability.

How to use the SourceCode:
1. Reference the BinarySplitting library in your C# project.
2. Invoke the BinarySplitter.SumSeries method by providing the start index, exclusive end index, and a function that computes the value for each term.
3. Review the unit tests in the BinarySplittingTest project to understand sample usage and to validate correct behavior.
4. Modify or extend the library as needed for your specific use-case while ensuring that all changes pass the accompanying tests.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.