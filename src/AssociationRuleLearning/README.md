# Association Rule Learning Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The AssociationRuleLearning algorithm implements the Apriori algorithm for mining frequent itemsets and generating association rules. It identifies frequent individual items from transactional data and builds larger itemsets iteratively. For each frequent itemset, it generates potential association rules by evaluating different combinations of antecedents and consequents based on minimum support and confidence thresholds. This process helps reveal interesting patterns and associations within datasets.

Using the SourceCode:
- The code is organized into two projects: the core library implementing the algorithm and a test project with unit tests.
- To use the library, reference the AssociationRuleLearning project in your C# solution.
- Supply your transactional data as a list of string lists and call the GenerateAssociationRules method with your chosen minimum support and confidence values.
- The provided unit tests illustrate usage for null, empty, and realistic datasets.
- All components are fully tested and designed for easy integration in your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.