
# Christofides Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Christofides Algorithm provides a 1.5-approximation to the Traveling Salesman Problem for metric graphs. It works by first constructing a Minimum Spanning Tree (MST) of the graph. Then, it identifies all vertices with an odd degree in the MST and computes a minimum weight perfect matching among these vertices. By combining the MST and the matching, it forms an Eulerian multigraph. An Eulerian tour is then computed and shortcut to produce a Hamiltonian circuit, ensuring the final tour is within 1.5 times the optimal solution.

To use the SourceCode, include the provided library in your project. Invoke the SolveTSP method of the ChristofidesSolver class, passing a 2D array of distances as its input. The accompanying MSTest unit tests demonstrate how to set up input matrices and validate the resulting circuit. This structure allows for easy testing, integration, and further development.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
