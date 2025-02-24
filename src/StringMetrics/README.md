# StringMetrics Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The StringMetrics algorithm computes the similarity between two strings by calculating the Levenshtein distance—the minimum number of single-character edits (insertions, deletions, or substitutions) required to change one string into another. This distance is then normalized to yield a score between 0.0 and 1.0, where 1.0 means the strings are identical and 0.0 indicates they are completely different.

To use the SourceCode, add the StringMetrics project to your solution and reference it in your application. Invoke the static methods in the StringMetricCalculator class to compute distances and normalized similarity scores. Comprehensive unit tests accompany the source code to ensure robust handling of edge cases such as null and empty strings.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>