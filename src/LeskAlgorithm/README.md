#LeskAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LeskAlgorithm is a method for word sense disambiguation which determines the correct meaning of a word by comparing the overlap between the context in which it appears and the definitions of its possible senses. The algorithm tokenizes the provided context and each candidate definition, then scores the overlap by counting common words. The candidate with the highest overlap score is selected as the best sense.

To use the SourceCode, include the LeskAlgorithm library in your project and pass a context string along with a dictionary mapping candidate senses to their definitions. The provided unit tests, which use Microsoft's testing framework, demonstrate usage and verify that the implementation handles various scenarios including empty inputs and tie situations.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.