#DiceCoefficientDocumentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

DiceCoefficient calculates the similarity between two strings by comparing their bigrams. The algorithm works by splitting each input string into two-character substrings (bigrams), counting the occurrences of each bigram, and then computing the Dice coefficient as (2 × number of common bigrams) divided by the total number of bigrams from both strings. The resulting value is between 0 and 1, where 1 means the strings are identical and 0 means they share no commonality.

To use the SourceCode, add a reference to the DiceCoefficient library in your C# project and call the DiceCoefficientCalculator.Calculate method with two string parameters. Unit tests are provided to verify the implementation, and you can modify the library as needed for your application.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>