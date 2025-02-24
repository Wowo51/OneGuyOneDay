
# TopNodesAlgorithm Guide

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## HelpContent

The TopNodesAlgorithm algorithm identifies top-level calendar events by filtering out events that are completely contained within other events. It examines each ResourceCalendarEvent in a list and only returns those events that are not enveloped by any other event’s start and end times. This approach is ideal for applications that require a clear, non-redundant display of scheduled events.

To use the SourceCode, add the TopNodesAlgorithm class to your C# project and reference it where needed. The included unit tests demonstrate how to create ResourceCalendarEvent instances and call the GetTopNodes method to obtain the top-level events. The project repository also provides examples, a license file, and additional resources to help integrate the code seamlessly into your solution.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  