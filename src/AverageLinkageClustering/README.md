# AverageLinkageClustering Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Average Linkage Clustering is a hierarchical clustering algorithm that iteratively merges clusters based on the average distance between all pairs of points in different clusters. The provided implementation is generic, allowing you to specify a custom distance function to measure similarity between data items. The algorithm starts by treating each data item as an individual cluster and then repeatedly merges the pair of clusters with the smallest average linkage distance until the desired number of clusters is reached.

To use the SourceCode, open the included solution in Visual Studio or your preferred .NET development environment. The project structure includes unit tests that demonstrate how the AverageLinkageClusterer class is used to perform clustering. These tests serve as practical examples of how to integrate and run the algorithm in your own projects. Review the project files, explore the implementation in the AverageLinkageClusterer and Cluster classes, and refer to the unit tests for guidance on customizing the clustering behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.