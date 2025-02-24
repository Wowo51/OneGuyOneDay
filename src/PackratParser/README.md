
# Packrat Parser Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Packrat Parser algorithm is a parsing technique for parsing expression grammars (PEGs) that achieves linear-time performance through memoization. By storing intermediate results in a memo table, the parser avoids redundant computations even when backtracking is required. This ensures that each part of the input is only processed once, making the parser both efficient and reliable.

How to use the SourceCode:
1. Compile the library as a C# project using the .NET SDK.
2. Reference the compiled library in your own projects.
3. Construct parsable expressions (such as literal, sequence, choice, and repetition) and use the Parser.Parse method to process input strings.
4. Study the provided unit tests to understand the behavior of different expressions and to verify your own implementations.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.
  