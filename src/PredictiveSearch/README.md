
# PredictiveSearch Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

PredictiveSearch is a search algorithm that adapts binary search principles by estimating the target position based on the values at the boundaries of a sorted collection. The interpolation performed allows it to effectively search arrays with uniform distribution, commonly known as interpolated search. By calculating a mid index using the formula, the algorithm narrows down the potential location of the target, enhancing performance over binary search under the right conditions.

To use the source code, add the PredictiveSearch project to your solution and reference the associated assembly in your application. Call the static InterpolatedSearch method by passing a sorted array and the desired target value as arguments. The provided unit tests in the source showcase different cases, including scenarios involving null arrays, single-element arrays, and large data sets.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
