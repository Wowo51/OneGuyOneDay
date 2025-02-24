# ConnectedComponentLabeling Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Connected Component Labeling is an algorithm used for identifying and labeling connected regions in a binary image. The algorithm scans the input image, and whenever it finds a foreground pixel that has not been labeled, it uses a depth-first search (DFS) approach to mark all adjacent foreground pixels with a unique label. As a result, each distinct connected region receives its own integer identifier while background pixels remain zero.

How to Use the SourceCode:
- Reference the ConnectedComponentLabeling library in your project.
- Use the ConnectedComponentLabeler.LabelComponents() method by passing a two-dimensional boolean array representing your image.
- The method returns a two-dimensional integer array where every connected component in the image is assigned a unique label.
- Consult the provided unit tests in the test project for usage examples and to verify the algorithm’s behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.