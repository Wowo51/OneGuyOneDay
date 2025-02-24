# LloydsAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## Help Content

LloydsAlgorithm implements Lloyd's algorithm (a variant of k-means clustering) which iteratively partitions data points into clusters by updating centroids until convergence. It starts by initializing centroids, then assigns each point to the nearest centroid, and recalculates centroids based on current cluster assignments until changes cease or a maximum iteration count is reached.

To use this SourceCode:
- Add the library to your C# project.
- Call the static method Cluster in the KMeans class by providing a List<double[]> for data points, the desired number of clusters, and the maximum number of iterations.
- Process the returned KMeansResult which includes final centroids and labels for each point.
- Use the provided unit tests as examples for integration into your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>