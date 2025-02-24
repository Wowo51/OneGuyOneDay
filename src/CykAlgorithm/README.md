# CykAlgorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The CykAlgorithm implements the CYK (Cocke–Younger–Kasami) parsing algorithm for context-free grammars in Chomsky Normal Form. It uses a dynamic programming approach to determine if a given input string can be derived from a specified grammar. The algorithm constructs a triangular table where each cell contains nonterminals that can generate the corresponding substring. This process, which combines smaller substrings into larger ones, operates in O(n³) time relative to the length of the input string.

To use the SourceCode, start by defining your grammar using the Grammar and Production classes. Ensure that your grammar conforms to Chomsky Normal Form. Next, instantiate the CykParser and invoke its Parse method with your grammar and the input string you wish to validate. The accompanying unit tests in the CykAlgorithmTest project demonstrate proper usage with examples ranging from valid inputs to error cases such as empty or null inputs. These can be executed via Microsoft Visual Studio’s Test Explorer or the command-line using the 'dotnet test' command.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>