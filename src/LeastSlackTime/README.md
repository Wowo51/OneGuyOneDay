# LeastSlackTime Scheduling Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LeastSlackTime algorithm schedules tasks by computing each task's slack time as the difference between its deadline and its processing time. It then orders the tasks in increasing order of slack, effectively prioritizing those with tighter deadlines. This approach helps minimize the risk of missing deadlines by ensuring tasks with minimal slack are executed first.

To use the SourceCode:
1. Clone or download the repository.
2. Open the solution file in Visual Studio or use the dotnet CLI.
3. Build the solution to compile the library.
4. Run the MSTest unit tests to verify the scheduler's correctness.
5. Modify or integrate the scheduler as needed for your scheduling requirements.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.