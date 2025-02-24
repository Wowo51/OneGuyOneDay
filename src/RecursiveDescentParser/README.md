# Recursive Descent Parser Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The Recursive Descent Parser algorithm is a top-down parsing approach that decomposes the grammar into a set of mutually recursive procedures. Each procedure is responsible for parsing a specific non-terminal in the grammar. The process begins with a Lexer that tokenizes the input string into numbers, operators, and parentheses. The Parser then consumes these tokens recursively, handling operator precedence and grouping with parentheses to correctly evaluate arithmetic expressions.

To use the SourceCode, integrate the provided project files into your solution. Key components include:
- Lexer.cs: Converts the raw input string into a series of tokens.
- Parser.cs: Implements the recursive descent algorithm to parse and evaluate expressions.
- Token.cs: Defines the structure and types of tokens.
- Unit tests: Validate the parser’s functionality with various expressions and error scenarios.

Simply reference the library in your project and call the Parse method with an arithmetic expression string to obtain the computed result. The included unit tests serve as practical examples of proper usage and help ensure that the parser works as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.