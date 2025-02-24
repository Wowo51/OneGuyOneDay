# RichSalzWildmat Library Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The RichSalzWildmat algorithm is a recursive wildcard matching algorithm implemented in C#. It supports standard wildcard characters such as '*' for matching any sequence of characters, '?' for matching a single character, and bracket expressions for specifying character sets or negations. The recursive design examines the pattern one segment at a time and handles exceptional cases—like consecutive wildcards and unclosed bracket expressions—with robust efficiency.

To use the SourceCode, simply integrate the library into your C# project and call the static method Wildmat.Match(pattern, text) to evaluate if a given text matches the specified pattern. Comprehensive unit tests using Microsoft's testing framework validate the behavior across various scenarios, ensuring the algorithm's reliability in real-world applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
