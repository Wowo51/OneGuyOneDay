# Waca Clustering Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

WacaClusteringAlgorithm is a robust local clustering algorithm designed for dynamic networks. It groups nodes based on their connectivity by performing a breadth-first search from unvisited nodes. The algorithm determines clusters by grouping nearby nodes within a specified maximum number of hops. Additionally, it supports computing hierarchical clusters by accepting different hop thresholds, allowing users to observe clustering at multiple granularity levels.

The provided SourceCode includes all necessary components: definitions for Node and Cluster classes, the implementation of the clustering logic in the WacaClusteringAlgorithm class, and comprehensive unit tests to validate functionality. To use this solution, simply integrate the library into your project, instantiate Node objects with defined neighbors, and call the ComputeClusters or ComputeHierarchicalClusters methods with your desired parameters. The unit tests offer guidance on expected behaviors and serve as usage examples.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
