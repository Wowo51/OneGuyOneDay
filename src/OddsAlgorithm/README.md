# Odds Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

OddsAlgorithm is an implementation of the Bruss odds algorithm that determines the optimal stopping strategy for selecting the last occurrence of an event in a sequence of random outcomes. The algorithm works by calculating a threshold index based on the cumulative odds derived from a uniform probability list. Once the threshold is established, it predicts the last occurrence by choosing the first true event that appears on or after that index.

To use the SourceCode, simply include the library in your C# project and reference the OddsAlgorithm namespace. Use the BrussAlgorithm class methods such as ComputeThresholdIndex, PredictLastOccurrence, and CreateUniformProbabilityList to integrate the functionality into your application. Unit tests using MSTest ensure reliability, and the code is structured to be easily maintained and extended.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)

Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.