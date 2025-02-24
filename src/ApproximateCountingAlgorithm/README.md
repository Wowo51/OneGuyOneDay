
# ApproximateCountingAlgorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ApproximateCountingAlgorithm employs a probabilistic approach—commonly known as the Morris Counter algorithm—to efficiently approximate large event counts using a very small register. Instead of incrementing a counter deterministically, the algorithm increases the internal counter with a probability that decreases exponentially as the counter value increases. This allows the system to track vast numbers of events without requiring large amounts of memory.

To use the SourceCode, incorporate the MorrisCounter class into your project. Create an instance of the class and call the Increment() method each time an event occurs. The GetCount() method returns an approximate count based on the current internal counter value, and Reset() clears the counter for a fresh start. The provided unit tests demonstrate typical usage scenarios, including verification of initial values, monotonic increment behavior, reset functionality, and statistical accuracy.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
