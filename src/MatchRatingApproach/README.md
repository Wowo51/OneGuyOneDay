# Match Rating Approach Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

MatchRatingApproach is a phonetic similarity algorithm that evaluates two input strings by encoding them into a simplified form and comparing the inner characters for similarity. The algorithm first normalizes the input strings by trimming them and converting them to uppercase. It then generates an encoded representation by preserving the first and last letters while filtering out vowels from the middle of the string. The algorithm determines similarity based on the number of matching inner characters relative to the length of the encoded strings, returning true if they meet the required threshold and false otherwise.

To use the source code, add the C# library to your project and reference the MatchRatingApproach namespace. Invoke the MatchRatingApproachAlgorithm.IsSimilar method with two strings to check their similarity. Comprehensive unit tests are provided using Microsoft's MSTest framework to verify the behavior and ensure robustness.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>