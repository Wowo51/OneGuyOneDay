
# SingleLinkageClustering Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The SingleLinkageClustering algorithm is an agglomerative hierarchical clustering method that groups data points based on the smallest distance between clusters. Initially, each point is treated as its own cluster. In each iteration, the two clusters with the minimum inter-cluster distance—computed as the smallest distance between any two points in different clusters—are merged. This process continues until the desired number of clusters is achieved, making it effective for discovering non-elliptical, irregular cluster shapes.

To use the source code, instantiate the SingleLinkageClusterer class with your data type, provide a collection of data points along with a distance function that computes the distance between any two points, and specify the desired number of clusters. The included unit tests demonstrate typical usage scenarios and validate the algorithm under various conditions.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
