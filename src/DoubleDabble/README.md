# DoubleDabble Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Double Dabble is a bit-level algorithm for converting binary integers into Binary-Coded Decimal (BCD) format. It works by iteratively shifting in each bit of the binary number and adjusting the partial BCD digits by adding three to any digit that is 5 or more. This process accurately transforms a binary value into its decimal representation without using multiplication or division.

To use the SourceCode, open the solution file (DoubleDabble.sln) in Visual Studio or use the .NET CLI. The project contains both the library implementation and comprehensive unit tests to verify its functionality. Build the solution and run the tests to ensure everything works as expected before integrating the library into your own C# applications.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>