
# PetricksMethod Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Petrick's Method algorithm is a systematic procedure for Boolean function minimization. It works by converting a covering table into a product-of-sums expression and then simplifying the expression to determine all minimal combinations of implicants that cover a Boolean function. The algorithm iteratively combines rows of the chart, retaining only the smallest, non-redundant sets that represent valid cover solutions.

To use the SourceCode, include the library in your C# project and call the PetrickSimplifier.Simplify method with your chart data. The library comes with a complete set of unit tests that demonstrate its behavior with various inputs, including null and empty charts, as well as charts with duplicate or common implicants. Refer to the test project as a guide for integrating and validating the algorithm in your own applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  