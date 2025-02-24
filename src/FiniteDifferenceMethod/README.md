# Finite Difference Method Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Finite Difference Method algorithm implemented in this library uses central difference formulas to approximate the first and second derivatives of a given function. It evaluates the function at points offset by a small step value (h) and computes the derivative as the ratio of the difference in function values to the difference in the input values. This approach is widely used in numerical analysis, especially when analytical derivatives are difficult or impossible to obtain.

To use the SourceCode, include the FiniteDifferenceMethod library in your C# project. Simply call the methods ApproximateFirstDerivative and ApproximateSecondDerivative by providing the function you wish to differentiate, the point of evaluation, and a small step value h. The accompanying unit tests demonstrate various use cases, including handling null inputs, zero step values, and verifying the accuracy of derivative approximations for linear, trigonometric, and quadratic functions.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
