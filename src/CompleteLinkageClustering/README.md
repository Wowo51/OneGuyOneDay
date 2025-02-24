# Complete Linkage Clustering

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Complete Linkage Clustering is an agglomerative hierarchical clustering algorithm. It starts by treating each data point as its own cluster and then iteratively merges the two clusters with the smallest maximum distance between any pair of their elements. This “complete linkage” criterion ensures that the merged clusters remain compact with a controlled level of dissimilarity among their members.

To use the SourceCode, compile the solution using .NET 9.0. The project is organized into a main library containing the clustering algorithm and a separate unit test project using MSTest to verify the functionality. The Program.cs file demonstrates a simple example that clusters a list of integers using a given distance function and threshold. You can modify the input data, threshold, or distance function as needed for your application.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.