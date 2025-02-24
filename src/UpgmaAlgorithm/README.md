# Upgma Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

UPGMA (Unweighted Pair Group Method with Arithmetic Mean) Algorithm:
--------------------------------------------------------------
The UPGMA algorithm is a distance-based method for constructing phylogenetic trees. It operates by identifying the pair of taxa (or clusters) with the smallest distance, merging them to form a new cluster, and then updating the distance matrix by computing the weighted average of distances based on the size of each cluster. This process continues iteratively until a single tree encompassing all taxa is formed.

How to Use the Source Code:
---------------------------
The source code is organized as a .NET solution containing the UPGMA algorithm implementation along with extensive unit tests using MSTest. To use the code, open the solution file in Visual Studio or a compatible .NET development environment, restore the NuGet packages, build the solution, and run the tests to ensure everything functions as expected. Review the inline comments and naming conventions for guidance on customization and integration into your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>