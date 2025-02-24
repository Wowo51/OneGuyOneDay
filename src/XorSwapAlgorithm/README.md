# Xor Swap Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The XorSwapAlgorithm is an efficient technique that swaps two integer values without using an additional temporary variable by leveraging the bitwise XOR operation. It performs three sequential XOR operations:
1. a = a XOR b
2. b = b XOR a
3. a = a XOR b

This method works reliably even when the numbers are identical, preventing redundant operations. The library comes with comprehensive unit tests that cover various scenarios—including positive numbers, negative numbers, zero, and the extreme bounds of integer values—ensuring its robustness.

To use the source code, add the XorSwapAlgorithm project to your solution and reference it in your application. Then, simply call the static Swap method to exchange two integer variables. The accompanying unit tests, built with Microsoft’s MSTest framework, provide practical examples of its usage.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.