
# BitonicSorter Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The BitonicSorter algorithm is a highly efficient parallel sorting algorithm that constructs bitonic sequences from unsorted arrays and then merges them to produce a fully sorted array. It works by recursively dividing the array into subarrays, sorting one half in ascending order and the other in descending order, and then merging these sub-sequences to form a completely sorted sequence. This approach makes it especially effective for parallel processing and large datasets.

To use the SourceCode:
1. Open the provided Visual Studio solution file.
2. Build the projects to compile the BitonicSorter library, the demonstration console application, and the MSTest unit tests.
3. Run the unit tests to verify the algorithm’s correctness.
4. Integrate the BitonicSorter library into your own project by referencing it and calling the static Sort method on your arrays.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
