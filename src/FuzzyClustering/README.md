# Fuzzy Clustering Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

FuzzyClustering is an implementation of fuzzy clustering, an algorithm that assigns each data point a degree of membership to multiple clusters rather than a hard assignment to a single cluster. The algorithm iteratively computes cluster centers and membership values based on a fuzziness parameter until convergence is achieved. Each membership row sums to 1, indicating the relative degree of association of a data point with each cluster.

How to Use the Source Code:
1. Add a reference to the library in your .NET project.
2. Instantiate the FuzzyClusteringAlgorithm class.
3. Prepare your data as a double[][] and specify the desired number of clusters, fuzziness, maximum iterations, and convergence threshold (epsilon).
4. Call the Cluster method to obtain a FuzzyClusterResult, which contains the computed cluster centers and membership matrix.
5. Review the provided MSTest unit tests for examples and further guidance on integrating the algorithm into your application.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>