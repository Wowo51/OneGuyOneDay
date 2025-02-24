# LongestCommonSubstring Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LongestCommonSubstring algorithm is designed to find the longest substring that appears in all provided strings. It works by taking the first string as a reference and iteratively examining every possible substring of decreasing length to verify its presence in all other strings. This method ensures that once a common substring is found, it is the longest possible match shared among the inputs.

How to use the SourceCode:
- Include the LongestCommonSubstring project in your solution or reference it as a library.
- Call the static method FindLongestCommonSubstrings from the LongestCommonSubstringFinder class, passing an array of strings as input.
- Review the example in Program.cs to see a practical implementation.
- Consult the MSTest unit tests in LongestCommonSubstringTests.cs to understand various usage scenarios and ensure correct behavior.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.