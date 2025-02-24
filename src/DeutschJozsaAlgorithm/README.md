# DeutschJozsa Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Deutsch-Jozsa algorithm is designed to determine whether a Boolean function is constant (returns the same value for every input) or balanced (returns an equal number of true and false outputs for all possible inputs). Although originally devised as a quantum algorithm, this implementation simulates its decision process on classical hardware by exhaustively evaluating the function over its input domain.

How to use the SourceCode:
1. Integrate the provided C# library into your project.
2. Define your Boolean function using the "Func<bool[], bool>" signature.
3. Call the method DeutschJozsa.GetFunctionType(yourFunction, n), where n is the number of input bits.
4. The method returns "constant" if the function's output is the same for every input, or "balanced" if the outputs differ.
5. Consult the unit tests included in the source to see practical examples of using the algorithm.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>