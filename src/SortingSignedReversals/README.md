# SortingSignedReversals Library
A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

SortingSignedReversals is an algorithm designed to sort permutations of signed integers using a series of reversal and flipping operations. It scans the list and, for each element that is not in its proper position, locates the element (by matching the absolute value) that should be there. It then reverses the sublist between these positions while flipping the signs. If an element is the negative of its correct value, it gets flipped to be positive. The process repeats until the permutation is fully sorted in ascending order with all positive numbers.

To use the SourceCode:
1. Add the SortingSignedReversals class library to your C# project.
2. Call the static method GreedySort from the SignedReversalSorter class, passing a list of integers representing a permutation.
3. Review the returned list of strings, which shows each intermediate step leading to the final sorted permutation.
4. Run the provided MSTest unit tests to validate functionality and to see example usage scenarios.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>