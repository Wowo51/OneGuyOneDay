# LR Parser Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LRParser algorithm is a robust bottom-up parsing strategy designed to efficiently analyze and interpret token streams. It supports several variants including Canonical LR, LALR, SLR, OperatorPrecedence, and SimplePrecedence. The algorithm builds a parse tree by employing shift-reduce operations according to a defined grammar, ensuring that the structure of the input is accurately captured.

How to Use the SourceCode:
1. Include the library in your C# project.
2. Use the ParserFactory to create an instance of the desired parser type (e.g., CanonicalLR, LALR, SLR, OperatorPrecedence, SimplePrecedence).
3. Call the Parse method with a list of Token objects.
4. Examine the returned ParseTree to verify the syntactic structure.
5. Refer to the unit tests in the LRParserTest project as examples for usage and validation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.