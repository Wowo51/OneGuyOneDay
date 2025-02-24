
# Optics Clustering Algorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Optics Clustering Algorithm (OPTICS) is a density‑based clustering method that orders data points based on their reachability distances. This ordered list reveals the underlying density structure of a dataset and helps to identify clusters, even when clusters have different densities.

How to use the SourceCode:
1. Add the project to your solution and reference it in your application.
2. Instantiate the OpticsAlgorithm class and call its Run method with a list of Point objects, an epsilon value (defining the neighborhood radius), and a minimum number of points required to form a cluster.
3. Use the ClusterVisualizer class to generate an SVG representation of the reachability plot. This visualization helps in understanding the cluster structure and identifying significant density changes.
4. Refer to the provided unit tests for examples of how to invoke the algorithm and validate its behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
