
# Canopy Clustering Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Canopy Clustering Algorithm is an unsupervised pre‑clustering method that groups data points into overlapping canopies. It works by iteratively selecting a data point as a canopy center and then clustering nearby points based on two thresholds—a loose threshold to include points as members of the canopy and a tight threshold to remove points from further consideration. This process efficiently partitions large datasets, making it a valuable preliminary step before more detailed analyses.

To use this SourceCode, add the CanopyClusteringAlgorithm library to your C# project. Then, prepare your dataset as a list of data points and call the static Cluster method from the CanopyClusterer class, providing appropriate loose and tight threshold values. Comprehensive unit tests are included to demonstrate usage and ensure the correctness of the implementation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
  