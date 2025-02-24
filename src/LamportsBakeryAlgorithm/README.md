# Lamports Bakery Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Lamport's Bakery Algorithm is a mutual exclusion algorithm designed to allow multiple processes or threads to safely access shared resources. It works by assigning each process a number (like taking a ticket in a bakery), ensuring that the process with the smallest number enters its critical section first. This mechanism guarantees fairness and prevents race conditions.

To use the SourceCode:
1. Add the BakeryLock class from the library to your C# project.
2. Initialize a BakeryLock instance with the total number of processes or threads.
3. Before entering any critical section, call the Lock method with the appropriate process identifier; after completing the critical operations, call the Unlock method.
4. Run the included unit tests to verify that the algorithm works as expected.
5. Customize and integrate the code into your concurrent applications as needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>