# Earley Parser Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## About the Earley Parser Algorithm

The Earley Parser is an efficient parsing algorithm capable of processing all context-free grammars. It operates using three main operations: prediction, scanning, and completion. The algorithm begins by augmenting the grammar with a new start rule and then processes the input tokens sequentially. When a non-terminal is expected, the parser predicts possible productions; when a terminal is expected, it scans the input; and upon completion of a production, it goes back to update previous states.

Despite having a worst-case time complexity of O(n³) for highly ambiguous grammars, in practical applications—especially with unambiguous grammars—the performance is excellent.

## Using the Source Code

To use the provided source code:

1. Import the EarleyParserAlgorithm project into your C# solution.
2. Define your grammar by creating an instance of the Grammar class and adding Rule objects.
3. Create an instance of the EarleyParser with your configured grammar.
4. Pass a list of input tokens to the Parse method to determine if the input is accepted.
5. Consult the MSTest unit tests in the accompanying test project for examples and verification.

The code is modular, well-structured, and fully unit-tested, making it easy to integrate into your applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.