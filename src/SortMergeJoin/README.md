# SortMergeJoin Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The SortMergeJoin algorithm is designed to efficiently join two sorted collections by iterating through them concurrently. It compares keys from both collections and joins matching pairs. Because the collections are pre-sorted, the algorithm minimizes unnecessary comparisons, resulting in improved performance especially with large datasets.

To use the source code:
- Include the SortMergeJoin library in your C# project.
- Ensure your input data is sorted.
- Call the Join method from the SortMergeJoiner class with the appropriate key selectors.
- The algorithm will return a list of joined pairs for each matching key.
- Use the provided MSTest project to run unit tests and validate functionality.

Enjoy integrating and extending the SortMergeJoin algorithm to meet your project needs.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.