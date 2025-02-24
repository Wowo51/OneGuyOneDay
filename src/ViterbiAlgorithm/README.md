# Viterbi Algorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ViterbiAlgorithm algorithm implements the classic Viterbi algorithm for hidden Markov models. It efficiently computes the most likely sequence of hidden states given an observation sequence using dynamic programming. The algorithm first initializes probabilities based on a given hidden Markov model, then recursively computes the maximum probability path, and finally backtracks to reconstruct the optimal state sequence.

To use the SourceCode, include the ViterbiAlgorithm library in your C# project and reference the Viterbi class. Call the static method FindMostLikelySequence with a properly configured HiddenMarkovModel instance and a list of observations. The provided unit tests demonstrate various usage scenarios and ensure that the implementation behaves as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.