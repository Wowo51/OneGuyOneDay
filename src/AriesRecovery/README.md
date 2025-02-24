
# AriesRecovery: Transaction Recovery Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

AriesRecovery is a robust algorithm that provides comprehensive transaction recovery. It achieves durability and atomicity by logging all changes with sequence numbers (LSN). The algorithm performs an analysis phase to identify active transactions and dirty pages, a redo phase to reapply committed operations since the last checkpoint, and an undo phase to rollback uncommitted transactions by writing compensating log records (CLR).

To use the source code, add the AriesRecovery project to your solution, reference it from your application, and utilize the LogManager to record logs and the RecoveryManager to execute the recovery process. Review the unit tests in the AriesRecoveryTest project to understand usage examples and verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
