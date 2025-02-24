
# SimpleLrParser Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## Help and Overview

The SimpleLrParser algorithm implements a simple SLR(1) parsing technique for context-free grammars. It constructs a canonical collection of LR(0) items, computes FIRST and FOLLOW sets, and builds action and goto tables to drive a shift-reduce parser. The algorithm augments the grammar by adding an initial production and uses closure and goto functions to build the collection of parser states. During parsing, the parser determines whether to shift a token, reduce using a production, or accept the input based on the parsing tables.

## How to Use the Source Code

To use the source code, add the SimpleLrParser library to your .NET project. First, create a Grammar instance with your desired start symbol and add productions using the provided methods. Then, instantiate an SLRParser with the configured Grammar and call the Parse method with a list of tokens. The parser will return true if the token sequence is valid according to the grammar, or false if an error is encountered.

The source code is thoroughly unit tested using Microsoft's MSTest framework and is maintained as a lightweight, dependency-free library. It is ideal for educational purposes or simple parser implementations.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
