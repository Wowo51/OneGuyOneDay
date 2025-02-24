
# BlockLoopJoin Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

BlockLoopJoin is an algorithm designed to efficiently perform block nested loop joins on two datasets. It divides the left dataset into blocks and, for each record in a block, iterates through the right dataset to find matching pairs based on provided key selector functions. This approach minimizes unnecessary comparisons, making it well-suited for processing large amounts of data.

To use the SourceCode, add the library to your project and reference the BlockNestedLoopJoin class. Simply call the static Join method with your data collections and key selectors. The included unit tests in BlockLoopJoinUnitTests.cs demonstrate typical usage and help verify the implementation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.
  