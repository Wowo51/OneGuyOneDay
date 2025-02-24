# FisherYatesShuffle Algorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Fisher-Yates Shuffle algorithm is an efficient method used to generate a random permutation of a finite sequence—in plain language, to shuffle a list. The algorithm works by iterating from the last element of the list backward and swapping each element with one chosen at random among the elements not yet moved. This ensures that every permutation is equally likely.

To use the SourceCode provided, start by compiling the C# library project. The SourceCode sample contains the implementation in the 'ShuffleExtensions.cs' file. It extends the IList<T> interface with a Shuffle() method that you can call on any list to randomly rearrange its elements. Unit tests are included in the project 'FisherYatesShuffleTest' to validate the shuffle implementation. Simply run the tests using your preferred unit testing tool such as Microsoft's MSTest, and enjoy using the library in your own C# projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>