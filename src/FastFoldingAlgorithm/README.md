# FastFoldingAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The FastFoldingAlgorithm algorithm efficiently detects periodic patterns in time-series data by folding the series across candidate periods. It computes a folded profile for each period and identifies those where the aggregate signal exceeds a preset threshold. This makes it ideal for analyzing large datasets to uncover recurring patterns quickly and accurately.

To use the SourceCode:
1. Add the FastFoldingAlgorithm library to your C# project.
2. Reference the FastFoldingAlgorithm namespace.
3. Create an instance of the FoldingAlgorithm class.
4. Call the DetectApproximatePeriod method with your time series data, specifying the minimum period, maximum period, and threshold values.
5. Consult the provided unit tests for usage examples and validation with Microsoft's unit testing framework.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>