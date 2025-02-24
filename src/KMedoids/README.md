
# KMedoids Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The K-Medoids algorithm is a robust clustering algorithm that selects actual data points (medoids) as centers. Unlike K-Means, which computes centroids, K-Medoids ensures that the cluster centers are real instances from the data, making it more resilient to outliers and noise.

How it works:
- Initialization: Select k initial medoids, either randomly or using a heuristic.
- Assignment: Assign each data point to the nearest medoid to form clusters.
- Optimization: Iteratively swap medoids with non-medoid points if the swap reduces the overall clustering cost.
- Termination: Continue the process until no further cost improvements can be found.

How to use the SourceCode:
- Build the solution using .NET 9.0 as specified in the project files.
- Run the provided MSTest unit tests to validate functionality.
- Reference the compiled KMedoids library in your own projects.
- Review the inline comments in the source code for additional guidance on customization and extension.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
