# Nested Loop Join Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Nested Loop Join algorithm is a straightforward join method which compares every element of one collection (called the left collection) with every element of another collection (called the right collection). For each pair that satisfies a specified condition, the algorithm produces a result using a custom result selector function. This approach, while simple, is very effective for small datasets or scenarios where advanced optimizations are not required.

To use the source code, simply include the NestedLoopJoin project in your solution. Invoke the Join method from the NestedLoopJoinAlgorithm class by passing in your collections, a predicate function to define the join condition, and a result selector to shape the output. The provided unit tests demonstrate various usage scenarios and serve as practical examples.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>