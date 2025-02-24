# Apriori Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The Apriori algorithm is a classic method used for mining frequent itemsets from transactional datasets. It works by first identifying individual items that satisfy a specified minimum support threshold, then iteratively combining these items to form larger itemsets that occur frequently together. This makes it ideal for applications like market basket analysis and association rule mining.

How to Use:
- Add the AprioriAlgorithm source code to your solution.
- Call the Apriori.RunApriori method with your transactions (each represented as a HashSet) and a minimum support value (a double between 0 and 1).
- Refer to the unit tests in the AprioriAlgorithmTest project for practical examples and to understand edge-case handling.
- Build and run tests using your preferred .NET test runner to ensure everything is working as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.