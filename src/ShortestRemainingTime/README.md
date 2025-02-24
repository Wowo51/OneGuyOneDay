# ShortestRemainingTime Scheduler Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ShortestRemainingTime algorithm implements a preemptive scheduling approach where processes are executed based on the shortest remaining execution time. At any moment, the process with the smallest remaining burst time is selected. If a new process arrives with a shorter remaining time than the one currently executing, the scheduler preempts the current process in favor of the new one.

To use the SourceCode:
1. Include the library in your C# project.
2. Define your processes with their Process ID, Arrival Time, and Burst Time.
3. Instantiate the ShortestRemainingTimeScheduler and call the GetSchedule method with your list of processes.
4. Use the returned schedule to determine the execution intervals for each process.

The provided unit tests demonstrate how to set up processes and verify the scheduler's behavior using Microsoft's unit testing framework.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>