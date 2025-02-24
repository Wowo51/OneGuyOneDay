# LLParser Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

LLParser is a recursive descent parser that utilizes the LL parsing strategy to evaluate simple arithmetic expressions involving numbers and addition operators. The algorithm reads the input sequentially, skipping whitespace, and identifies tokens to build a parse tree that reflects the structure of the expression. It also provides error handling for unexpected input or missing elements.

To use the SourceCode, open the provided Visual Studio solution which contains both the LLParser project and a corresponding MSTest project for testing. Compile the solution targeting .NET 9.0, and run the unit tests to ensure the parser functions as expected. The code is designed to be easily extendable for more complex grammars or additional operations if needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>