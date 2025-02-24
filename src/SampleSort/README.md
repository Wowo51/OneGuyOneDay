
# SampleSort Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

SampleSort is a divide-and-conquer sorting algorithm that uses sampling to determine effective bucket boundaries. It works by selecting a small sample from the input collection, sorting that sample, and then using the sorted sample as pivots to partition the full list into multiple buckets. Each bucket is then recursively sorted, with a simple sort applied for small sublists to optimize performance.

To use the provided SourceCode, include the SampleSort project in your solution and reference it in your application. The codebase includes:
- The implementation of the SampleSort algorithm in C#.
- A full set of unit tests using MSTest to validate various scenarios such as handling null or empty collections, sorting lists with a single element, pre-sorted lists, reversed lists, duplicates, and large random lists.
- Project configuration files and a MIT license that allow you to freely use and extend the library.

Integration steps:
1. Add a reference to the SampleSort library in your project.
2. Call the SampleSorter.Sort<T>(IEnumerable<T> collection, IComparer<T> comparer) method with the appropriate comparer.
3. Run the unit tests to ensure that the integration works correctly.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.
