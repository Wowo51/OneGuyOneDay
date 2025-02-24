# Truncation Selection Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Truncation Selection Algorithm:
The TruncationSelection algorithm is designed to select a subset of individuals from a list based on their fitness levels. It sorts individuals in descending order of fitness and then picks the top fraction. The count is determined by taking the ceiling of the total number multiplied by the fraction, ensuring that the best-performing individuals are always included.

How to Use the SourceCode:
- The source code is organized into multiple projects, including the main library and associated unit tests.
- The core functionality is implemented in TruncationSelector.cs, which contains the logic for sorting and selecting individuals.
- Unit tests in the TruncationSelectionTest project cover various scenarios, including edge cases and invalid inputs, to ensure robust functionality.
- To run the tests, use the MSTest framework or any compatible test runner. The solution file groups the projects neatly for ease of navigation and development.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.<br>
Authored by Warren Harding. AI assisted.