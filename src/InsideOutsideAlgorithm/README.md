# InsideOutsideAlgorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT license.

Inside-Outside Algorithm Help:
The Inside-Outside Algorithm is a dynamic programming method used to train probabilistic context-free grammars by re-estimating the probabilities of production rules. It computes "inside" probabilities for generating substrings from non-terminals and "outside" probabilities for the context in which those non-terminals appear. By combining these probabilities, the algorithm calculates the expected counts of each production rule, which are then used to update the rule probabilities.

Usage:
1. Define a probabilistic grammar by creating instances of ProbabilisticGrammar and adding GrammarRule objects.
2. Set the start symbol and initialize the grammar with its production rules.
3. Tokenize the input sentence that you wish to parse.
4. Call the InsideOutsideCalculator.Reestimate() method with the grammar and tokenized sentence. This method will adjust the production probabilities based on the computed inside and outside values.
5. Refer to the provided unit tests, which use Microsoft's MSTest framework, for examples and validation of various parsing scenarios.

The source code is modularized into separate files for grammar definitions, algorithm implementation, and testing. Explore the project structure to better understand how the classes interact and to customize the grammar for your specific needs.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.