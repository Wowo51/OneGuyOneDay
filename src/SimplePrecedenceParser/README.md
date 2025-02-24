# SimplePrecedenceParser Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The SimplePrecedenceParser algorithm is a simple recursive descent parser designed to evaluate arithmetic expressions. It leverages methods like ParseExpression, ParseTerm, ParseFactor, and ParsePrimary to correctly process numbers, operators, and parentheses, ensuring that multiplication and division are evaluated with higher priority than addition and subtraction. The parser builds an abstract syntax tree (AST) with NumberExpr nodes representing numeric values and BinaryExpr nodes representing operations, which makes it easy to evaluate or transform the expression.

To use the SourceCode, add the SimplePrecedenceParser library to your project and reference it in your solution. This library is unit tested using Microsoft's MSTest framework, so you can run the provided tests to validate correctness. The source code is structured cleanly, facilitating modifications or extensions to the parser if necessary.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>