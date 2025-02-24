
# Fibonacci Coding Library Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The FibonacciCoding algorithm leverages Zeckendorf's theorem to represent positive integers as unique sums of non-consecutive Fibonacci numbers. It works by generating the Fibonacci sequence up to the given number, then constructing a binary code where each digit indicates whether a corresponding Fibonacci number is used in the sum. The encoded output always terminates with a '1' that marks the end of the sequence.

To use the source code, include the FibonacciCoding project in your solution and call the static methods provided by the FibonacciCoder class. Use Encode(int number) to obtain the Fibonacci code for a positive integer, and Decode(string code) to retrieve the original integer. The library is thoroughly unit tested using Microsoft's testing framework, ensuring reliability and correctness.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
