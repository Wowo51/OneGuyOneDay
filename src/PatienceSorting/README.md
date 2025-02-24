# Patience Sorting Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

PatienceSorting is an efficient sorting algorithm inspired by the card game "patience" (solitaire). The algorithm works by distributing the input elements into several piles. Each new element is placed on the leftmost pile whose top element is greater than or equal to it; if no such pile exists, a new pile is started. The final sorted sequence is obtained by merging these piles, resulting in an ordered list. This method not only illustrates an intuitive approach to sorting but also demonstrates effective techniques in data grouping and merging.

To use the SourceCode, include the PatienceSorting library in your solution and reference it from your project. Run the provided unit tests using Microsoft's testing framework to verify functionality. The implementation targets .NET 9.0 and supports generic types that implement IComparable, making it adaptable to various data types.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>