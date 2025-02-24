# List Scheduling Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT license.

## Overview

The List Scheduling algorithm is a heuristic approach that assigns each job to the machine which becomes available the earliest. By always selecting the machine with the least current workload, the algorithm minimizes idle time and efficiently balances the processing load across available machines. This simple yet effective strategy is particularly useful in environments where tasks need to be scheduled rapidly with minimal computational overhead.

## How to Use the SourceCode

1. Include the library in your C# project.
2. Create a list of jobs using the provided Job class, specifying each job's processing time.
3. Call the Scheduler.ListScheduling method with the job list and the desired number of machines.
4. The method returns a list of ScheduledTask objects, each detailing the job ID, machine ID, start time, and finish time.
5. Review and run the unit tests in the repository to verify correct behavior and understand example usage.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.