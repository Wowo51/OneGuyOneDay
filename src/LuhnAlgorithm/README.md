# Luhn Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Luhn Algorithm is a checksum formula used to validate identification numbers such as credit card numbers. It works by doubling every second digit from the right, subtracting 9 when the result exceeds 9, and confirming that the sum of all digits is divisible by 10.

The SourceCode includes:
- An implementation of the Luhn algorithm in the LuhnValidator class.
- A console application demonstrating how to prompt for and validate user input.
- A suite of unit tests covering various valid and invalid scenarios.

To use the SourceCode:
1. Open the solution in Visual Studio or your preferred IDE.
2. Build the project to compile the library and tests.
3. Run the console application to validate numbers interactively.
4. Execute the unit tests using MSTest or the "dotnet test" command to ensure functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>