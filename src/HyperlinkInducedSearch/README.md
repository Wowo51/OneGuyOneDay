
# HyperlinkInducedSearch Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Hyperlink Induced Topic Search (HITS) algorithm is a link analysis algorithm that assigns hub and authority scores to nodes in a graph. Authorities indicate pages with valuable content, and hubs are pages that point to multiple authorities. The algorithm works iteratively: authority scores are updated based on the hub scores of incoming links, and hub scores are updated based on the authority scores of outgoing links. After each iteration, the scores are normalized.

To use the source code, perform the following steps:
1. Create an instance of the Graph class.
2. Add edges using AddEdge to define connections between nodes.
3. Invoke the Compute method of the HITSAlgorithm class with your graph and a specific iteration count.
4. Retrieve and use the resulting hub and authority scores as needed.
5. Refer to the accompanying unit tests and project files for examples of integration and deployment.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
