# Chandy Lamport Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Chandy-Lamport Algorithm is a distributed snapshot algorithm used to record a consistent global state of an asynchronous system. It achieves this by having each process record its local state, and the communication channels record the messages in transit. When a process receives a marker on a channel for the first time, it records its local state and then sends marker messages on its outgoing channels. This mechanism ensures that even if processes are executing at different speeds, the snapshot reflects a consistent view of the system state.

To use the SourceCode:
1. Review the implementation in the ChandyLamportAlgorithm namespace, which contains classes such as Process, Channel, GlobalSnapshot, and SnapshotManager.
2. Build the solution using Visual Studio or the .NET CLI.
3. Run the provided unit tests located in the ChandyLamportAlgorithmTest project to verify functionality.
4. Customize or extend the algorithm implementation as needed for your distributed system or for educational purposes.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.