# BurstSortTrie Library Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The BurstSortTrie algorithm uses a burst-trie structure to efficiently sort strings. It works by initially collecting words in a leaf node until a preset threshold is reached. When the threshold is exceeded, the node bursts into an internal node that distributes its words among child nodes based on the next character. This hybrid approach leverages the simplicity of list management for small collections along with the scalability of a trie for larger datasets, resulting in both memory-efficient storage and fast lexicographic sorting.

To use the SourceCode, include the library in your C# project by referencing its project file or the compiled assembly. Build the solution using Visual Studio or the dotnet CLI, and run the provided MSTest unit tests to verify its functionality. Once confirmed, integrate the BurstSorter.Sort method into your application to perform efficient sorting of string lists.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>