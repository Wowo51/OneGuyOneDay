
# PrattParserDocumentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Pratt Parser algorithm is a top-down operator precedence parser that efficiently processes arithmetic expressions and other structured input. It divides the parsing process into two key methods: Nud (null denotation), which is used for tokens that start an expression, and Led (left denotation), which processes infix operators based on their precedence. This design allows the parser to handle various operators and operand combinations in a concise and flexible manner.

To use the provided SourceCode, start by tokenizing your input using the Lexer class; it will break the input into a sequence of tokens. Then, use the Parser to convert these tokens into an abstract syntax tree (AST) composed of Expression objects such as NumberExpression, BinaryExpression, and UnaryExpression. The AST can then be evaluated or further transformed as needed. The included unit tests, built with Microsoft's MSTest framework, demonstrate how to work with simple numbers, binary operations, unary operations, and complex expressions.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  