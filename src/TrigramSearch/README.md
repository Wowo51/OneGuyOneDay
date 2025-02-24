# Trigram Search Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The TrigramSearch algorithm is designed to break down input text into trigrams—overlapping sequences of three characters—allowing for efficient fuzzy matching and text similarity comparisons. The algorithm works by creating a set of trigrams for each document, which can then be compared with the set of trigrams extracted from a search query. By computing the intersection and union of these sets, the algorithm calculates a similarity score that determines whether a document matches the search criteria based on a specified threshold.

To use the SourceCode, follow these steps:
1. Include the library in your C# project.
2. Use the TrigramHelper to generate trigrams from your text.
3. Add documents to the TrigramIndex using their unique IDs and content.
4. Perform searches by supplying a query string and a similarity threshold to find the best matching documents.
5. Review the accompanying unit tests in the repository for examples and further integration details.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
