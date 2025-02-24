# OperatorPrecedenceParser Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

OperatorPrecedenceParser is a recursive descent parser that implements operator precedence parsing for arithmetic expressions. It uses a Lexer to tokenize the input string into numbers, operators, and parentheses. The Parser then constructs an abstract syntax tree (AST) following the operator precedence rules. The AST is composed of NumberNodes representing numeric values and BinaryOperationNodes representing arithmetic operations, ensuring that multiplication and division are evaluated with higher precedence than addition and subtraction.

Usage:
1. Include the source code in your C# project or reference the compiled library.
2. Create an instance of the Parser class by passing in an arithmetic expression as a string.
3. Invoke the Parse() method to obtain an ExpressionNode that represents the parsed expression.
4. Call the Evaluate() method on the ExpressionNode to compute the result.
5. Refer to the provided MSTest unit tests for examples covering operator precedence, parentheses, and error handling scenarios.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>