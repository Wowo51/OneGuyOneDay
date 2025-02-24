
# WardsMethod Clustering Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

WardsMethod is an agglomerative clustering algorithm that begins by treating each data point as an individual cluster and then iteratively merges the pair of clusters whose combination produces the smallest increase in the total sum of squared deviations. This approach minimizes internal cluster variance and progressively reveals the natural grouping within the data.

To use the SourceCode, simply reference the compiled library in your project. The provided classes demonstrate the core functionality: the Cluster class computes centroids based on data points, while the WardClustering class implements the merging logic using Ward’s criterion. Unit tests accompany the code to illustrate usage and validate correctness. Reviewing the test cases and project structure will guide you in integrating the algorithm into your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
