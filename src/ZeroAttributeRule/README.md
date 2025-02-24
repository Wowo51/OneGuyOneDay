# Zero Attribute Rule Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ZeroAttributeRule algorithm checks for the presence of attribute-like syntax in a given C# source code string. It works by scanning for both the "[" and "]" characters in the text. If both are found, the algorithm infers that an attribute might be present, helping enforce a coding guideline that prohibits attribute annotations.

To use the SourceCode, incorporate it into your project along with its comprehensive unit tests. These tests cover scenarios such as null or empty input, mismatched brackets, and large inputs with and without attribute-like syntax. Running the tests ensures the algorithm behaves as expected and helps maintain compliance with the zero attribute rule.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.